using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using NetIO;

using System.Diagnostics;

namespace disksrv_client
{
    public partial class frmMain : Form
    {

        #region Global variables/constants/structures

        private const int PORT = 6000;
        private static byte[] ID = { 0x64, 0x73 };

        #endregion

        #region Convenience methods

        private void UpdateUI()
        {
            //MessageBox.Show("UpdateUI fired");

            // Enable/disable connect + disconnect buttons + server textbox
            cmdConnect.Enabled = !(state.connected);
            txtHost.Enabled = !(state.connected);
            cmdDisconnect.Enabled = state.connected;

            // Enable/disable disconnect/read/write/cancel action buttons
            if (state.connected)
            {
                cmdDisconnect.Enabled = !(state.async);
            }
            cmdReadDiskToFile.Enabled = !(state.async);
            cmdWriteFileToDisk.Enabled = !(state.async);
            cmdCancel.Enabled = state.async;

            // Also enable/disable path textbox + browse button - disable only if running an async operation
            txtLocalPath.Enabled = !(state.async);
            cmdBrowseLocal.Enabled = !(state.async);

            // Show/hide progress
            statProg.Visible = state.async;

            // Set status label
            if (!state.connected)
            {
                statLabel.Text = "Ready to connect";
            }

            if (state.connected & !state.async)
            {
                statLabel.Text = "Connected";
            }

            // We purposefully do not set the status label where we are connected + async operation in progress
            // The background worker will update the status as appropriate
        }

        private void Error(string err)
        {
            MessageBox.Show(err, "disksrv client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // This function compares 2 byte arrays
        private bool arrcmp(ref byte[] a, ref byte[] b, int length = 0)
        {
            // If the lengths are not the same, and length is not set, the arrays are not equal
            if ((a.Length != b.Length) && length == 0)
                return false;

            // If either array is less then length, the arrays are not equal or invalid
            if ((a.Length < length) || (b.Length < length))
                return false;

            // If length is not set, set it to the length of the array
            if (length == 0)
                length = a.Length;

            // Compare each byte in the arrays. If any byte is unequal, return false immediately
            for (int i = 0; i < length; i++)
                if (a[i] != b[i])
                    return false;
            
            // If the loop completed, all bytes must be identical. Return true
            return true;
        }

        static void LBAtoCHS(ref chs_t chs, chs_t geometry, int LBA)
        {
            chs.cylinder = LBA / (geometry.head * geometry.sector);
            chs.head = (byte)((LBA / geometry.sector) % geometry.head);
            chs.sector = (byte)((LBA % geometry.sector) + 1);
        }

        static void CHStoLBA(out int LBA, chs_t geometry, chs_t chs)
        {
            LBA = ((chs.cylinder * geometry.head + chs.head) * geometry.sector + (chs.sector - 1));
        }

        private void PopulateDiskList()
        {
            // Clear the list
            lstServerDisks.Items.Clear();

            // Validate we're connected
            if (!state.connected)
                return;

            // Get the number of floppy disks and hard disks
            // This declares FDDs and HDDs
            GetDiskCounts(out int FDDs, out int HDDs);

            // Populate the list with entires, "FDD 0..x", "HDD 0..x"
            for (int i = 0; i < FDDs; i++)
                lstServerDisks.Items.Add(String.Format("FDD {0}", i));
            for (int i = 0; i < HDDs; i++)
                lstServerDisks.Items.Add(String.Format("HDD {0}", i));
        }

        private void PopulateGeometries()
        {
            Debug.WriteLine("PopulateGeometries: Fired, {0} items to populate", AppSettings.floppy_geometries.Count);

            // Clear the combo box
            cmbFloppyType.Items.Clear();

            // Add the friendly name of each geometry
            foreach (KeyValuePair<string, chs_t> kvp in AppSettings.floppy_geometries)
            {
                cmbFloppyType.Items.Add(kvp.Key);
            }

            // Select the first item by default (if settings.xml is unmodified, this is '3.5" 1.44MB'), if there are any items
            if (cmbFloppyType.Items.Count > 0)
            {
                cmbFloppyType.SelectedIndex = 0;
            }
        }

        // This returns the BIOS number for the selected disk, or -1 if no disk is selected
        private int GetSelectedDisk()
        {
            // Validate the list has a selected item
            if (lstServerDisks.SelectedIndex < 0)
                return -1;

            // Interpret the selected disk
            string item = (string)lstServerDisks.SelectedItems[0];
            bool isFloppy;
            int diskno;

            // Tokenize the string
            string[] items = item.Split(new char[] { ' ' });
            
            // If we have anything other than 2 tokens, then PopulateDiskList has changed
            if (items.Length != 2)
            {
                Error(String.Format("Could not get selected disk, error:\n\nTokenized selected item, expected 2 tokens, got {0}", items.Length));
                return -1;
            }

            // Determine isFloppy
            if (items[0] == "FDD")
                isFloppy = true;
            else if (items[0] == "HDD")
                isFloppy = false;
            else
            {
                Error(String.Format("Could not get selected disk, error:\n\nList items expected to be in format 'FDD 0..x' or 'HDD 0..x', got {0}", item));
                return -1;
            }

            // Determine diskno
            if (!int.TryParse(items[1], out diskno))
            {
                Error("Could not get selected disk, error:\n\nCould not parse items[1] as an int, while determining diskno");
                return -1;
            }

            // If this is a hard drive, its BIOS disk number = [index from 0] + 0x80
            if (!isFloppy)
                diskno += 0x80;

            // Return diskno
            return diskno;
        }

        // This returns a chs_t representing the chosen floppy geometry. It is required that the calling function asserts a geometry has been selected, else a runtime exception will go uncaught.
        private chs_t GetSelectedGeometry()
        {
            return AppSettings.floppy_geometries[(string)cmbFloppyType.SelectedItem];
        }

        #endregion // Convenience methods

        #region Application logic

        private struct AppState {
            // Status variables
            public bool connected;
            public bool async;

            // Connection specific variables
            public TcpClient client;
            public NetReader nr;
            public NetWriter nw;

            // Async variables
            public string filepath;
            public byte diskno;
            public chs_t diskgeometry;
            public int maxbufoverride;
        };

        private AppState state;

        public frmMain()
        {
            InitializeComponent();

            // Initialise the appstate
            state.connected = false;
            state.async = false;

            state.maxbufoverride = 0;
        }

        [Serializable]
        public class ServerException : Exception
        {
            public ServerException() { }

            public ServerException(string message)
                : base(message) { }

            public ServerException(string message, Exception inner)
                : base(message) { }
        }


        private void Connect(string host)
        {
            // Instantiate the TcpClient and attempt to connect
            state.client = new TcpClient();

            try
            {
                state.client.Connect(host, PORT);
            } catch (SocketException e)
            {
                Error(String.Format("Could not connect to {0}, error:\n\n{1}", host, e.Message));
                return;
            }

            // Create our stream + NetIO instances
            Stream netstream = state.client.GetStream();
            state.nr = new NetReader(netstream);
            state.nw = new NetWriter(netstream);

            // Read the server identifying information
            try
            {
                byte[] packetbuf = new byte[4];
                state.nr.readByteArray(ref packetbuf, 0, 4);

                // Interpret the identifying info

                // Ensure the ID was correct
                if (!arrcmp(ref packetbuf, ref ID, 2))
                {
                    // Disconnect
                    state.client.Close();

                    // Display error
                    Error(String.Format("Could not connect to {0}, error:\n\nThe server returned an invalid magic sequence", host));
                    return;
                }

                // Interpret major & minor
                byte major = packetbuf[2];
                byte minor = packetbuf[3];

                // We only understand protocol major version 1, make sure we have this
                if (major != 1)
                {
                    Error(String.Format("Could not connect to {0}, error:\n\nThe server's major protocol version was not 1. Actual: {1}", host, major));
                    return;
                }
            } catch (SocketException e)
            {
                // Output error
                Error(String.Format("Could not connect to {0}, error:\n\n{1}", host, e.Message));

                // Set connection status
                state.connected = false;
                UpdateUI();

                return;
            }

            // We are now connected
            state.connected = true;

            // Update the UI
            UpdateUI();
        }

        private void Disconnect()
        {
            // Check we're connected
            if (state.client.Connected)
            {
                try
                {
                    // Tell the server we're disconnecting
                    state.nw.writeByte(0);  // REQ 0 - QUIT
                    state.nw.writeInt16(0); // Data length

                    // Close the connection
                    state.client.Close();
                } catch (IOException)
                {
                    // If we have an IOException while closing, this is probably why we were called
                }
            }

            // Ensure state variables are set correctly
            state.connected = false;
            state.async = false;
            state.client = null;
            state.nr = null;
            state.nw = null;

            // Update UI
            UpdateUI();
        }

        // GetDiskCounts will output -1 to floppydisks and harddisks, if the application is not connected
        // to a server, receives a SocketException (and disconnects), or receives an invalid response from
        // the server
        private void GetDiskCounts(out int floppydisks, out int harddisks)
        {
            // Validate we're connected
            if (!state.connected)
            {
                floppydisks = -1;
                harddisks = -1;
                return;
            }

            // Try to make the request to the server and obtain the disk counts
            // Disconnect on socket error
            try
            {
                // Make our request to the server
                state.nw.writeByte(1);      // REQ 1 - GET DISK COUNT
                state.nw.writeInt16(0);     // Data length

                // Receive the response from the server
                byte response = state.nr.readByte();
                int datalen = state.nr.readInt16();

                // Validate datalen = 2
                if (datalen != 2)
                {
                    Error(String.Format("Could not obtain disk counts from server. error:\n\ndatalen is invalid. Expected: 2, got: {0}", datalen));
                    floppydisks = -1;
                    harddisks = -1;
                    return;
                }

                // Read the values
                floppydisks = state.nr.readByte();
                harddisks = state.nr.readByte();
            } catch (SocketException e)
            {
                Error(String.Format("Could not obtain disk counts from server. error:\n\n{0}", e.Message));

                Disconnect();

                floppydisks = -1;
                harddisks = -1;
                return;
            }
        }

        // GetDiskGeometry throws an ArgumentException if the server fails to return geometry, or responds in an invalid fasion
        private chs_t GetDiskGeometry(byte diskno)
        {
            byte[] packetbuf = new byte[4];

            // Call GET DISK INFO
            state.nw.writeByte(2);          // REQ 2 - GET HARD DISK INFO
            state.nw.writeInt16(1);         // Data length
            state.nw.writeByte(diskno);    // HDD number, indexed from 0

            // Get response
            byte success = state.nr.readByte();
            int datalen = state.nr.readInt16();

            // Validate success + datalen
            if (success != 1)
                throw new ServerException(String.Format("GetDiskGeometry: Server failed to return geometry for drive {0}", diskno));

            if (datalen != 4)
                throw new ServerException(String.Format("GetDiskGeometry: Server responded with invalid data length. Expected 4, got {0}", datalen));


            // Get the response
            byte sector = state.nr.readByte();
            byte head = state.nr.readByte();
            int cylinder = state.nr.readInt16();

            // Return it
            return new chs_t(cylinder, head, sector);

        }

        private int GetDiskBufSize()
        {
            byte[] packetbuf = new byte[2];

            // Call GET MAX DISK BUF SIZE
            state.nw.writeByte(5);      // REQ 5 - GET MAX DISK BUFFER SIZE
            state.nw.writeInt16(0);     // Data length

            // Get results
            byte result = state.nr.readByte();
            int datalen = state.nr.readInt16();

            // Validate the server returned success
            if (result != 1)
                throw new ServerException("GetDiskBufSize: Server failed to return disk buffer size");

            // Validate we are expecting 2 bytes of data (16 bit int)
            if (datalen != 2)
                throw new ServerException(String.Format("GetDiskBufSize: Incorrect amount of data received. Expected: 2, got: {0}", datalen));

            // Read the maximum disk buffer size and return it
            return state.nr.readInt16();
        }

        #region Read Disk Worker methods
        private void workerReadDisk_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event
            BackgroundWorker worker = sender as BackgroundWorker;

            // Obtain the maximum disk buffer size
            int maxbuf = GetDiskBufSize();
            if ((state.maxbufoverride != 0) && (state.maxbufoverride <= maxbuf))
                maxbuf = state.maxbufoverride;

            byte[] packetbuf = new byte[4];
            byte[] diskbuf = new byte[maxbuf];

            // Try to dump the whole disk
            FileStream fs = new FileStream(state.filepath, FileMode.Create);
            NetWriter fsnw = new NetWriter(fs);

            // Loop using LBA
            int maxLBA = state.diskgeometry.cylinder * state.diskgeometry.head * state.diskgeometry.sector;
            int maxbufsec = maxbuf / 512;
            

            // Iterate over entire geometry, dumping until complete
            for (int i = 0; i < maxLBA; i += maxbufsec)
            {
                chs_t reqchs = new chs_t();

                int seccount = (i + maxbufsec < maxLBA) ? maxbufsec : (maxLBA - i);

                // Calculate the originating CHS value
                LBAtoCHS(ref reqchs, state.diskgeometry, i);

                // Call for the sectors to be read
                state.nw.writeByte(6);      // READ MULTIPLE DISK SECTORS
                state.nw.writeInt16(6);     // DATA SIZE 6

                state.nw.writeByte(state.diskno);           // DISK NUMBER
                state.nw.writeByte(reqchs.sector);          // SECTOR
                state.nw.writeByte(reqchs.head);            // HEAD
                state.nw.writeInt16((short)reqchs.cylinder);       // CYLINDER
                state.nw.writeByte((byte)seccount);                // SECTOR COUNT

                // Get response
                byte success = state.nr.readByte();
                int datalen = state.nr.readInt16();

                // Validate response
                if (success != 1)
                {
                    // If we failed, close the file and throw an exception
                    fs.Close();
                    throw new ServerException(String.Format("DumpFast: Server returned failure reading CHS {0} {1} {2} seccount {3}", reqchs.cylinder, reqchs.head, reqchs.sector, seccount));
                }

                if (datalen != (seccount * 512))
                {
                    // If we were given an invalid amount of data, close the file and throw an exception
                    fs.Close();
                    throw new ServerException(String.Format("DumpFast: Server returned incorrect amount of data. Expected {0} got {1}", seccount * 512, datalen));
                }

                // Read the data
                state.nr.readByteArray(ref diskbuf, 0, datalen);

                // Write the data to file
                fsnw.writeByteArray(ref diskbuf, 0, datalen);

                // Check if a cancellation is pending
                if (worker.CancellationPending)
                {
                    // Set the cancel flag
                    e.Cancel = true;

                    // Return from the function so as not to proceed
                    return;
                }

                // Calculate progress + report it
                float cur = i;
                float max = maxLBA;
                float progress = ((cur / max) * 100f);
                worker.ReportProgress((int)progress, i);
            }

            fs.Close();
        }

        private void workerReadDisk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the progress bar
            statProg.Value = e.ProgressPercentage;

            // Interpret the sector being read right now
            int cursecs = (int)e.UserState;

            // Get LBA values to understand data written + total volume of data
            CHStoLBA(out int maxsecs, state.diskgeometry, state.diskgeometry);

            // Calculate current + total MBs
            float curMB = (cursecs * 512f) / 1024f / 1024f;
            float totMB = (maxsecs * 512f) / 1024f / 1024f;

            // Display it nicely in the status
            statLabel.Text = String.Format("Reading... ({0}/{1}MiB)", curMB.ToString("N2"), totMB.ToString("N2"));
        }

        private void workerReadDisk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // We are no longer performing an async function
            state.async = false;

            // Reset maxbufoverride
            state.maxbufoverride = 0;

            // Update the UI
            UpdateUI();

            // If we completed because of an error, display the error
            if (e.Cancelled)
            {
                statLabel.Text = "Cancelled";
            } else if (e.Error != null)
            {
                Error(String.Format("Reading disk failed, error:\n\n{0}", e.Error.Message));
            }
        }
        #endregion // Read Disk Worker methods

        #region Write Disk Worker methods

        private void workerWriteDisk_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event
            BackgroundWorker worker = sender as BackgroundWorker;

            // Obtain the maximum disk buffer size
            int maxbuf = GetDiskBufSize();
            if ((state.maxbufoverride != 0) && (state.maxbufoverride <= maxbuf))
                maxbuf = state.maxbufoverride;

            byte[] packetbuf = new byte[4];
            byte[] diskbuf = new byte[maxbuf];

            // Try and dump a floppy image
            FileStream fs = new FileStream(state.filepath, FileMode.Open);
            NetReader fsnr = new NetReader(fs);

            // Loop using LBA
            int maxLBA = state.diskgeometry.cylinder * state.diskgeometry.head * state.diskgeometry.sector;
            int maxbufsec = maxbuf / 512;
            chs_t reqchs = new chs_t();

            // Iterate over entire geometry, dumping until complete
            for (int i = 0; i < maxLBA; i += maxbufsec)
            {
                int seccount = (i + maxbufsec < maxLBA) ? maxbufsec : (maxLBA - i);

                // Calculate the originating CHS value
                LBAtoCHS(ref reqchs, state.diskgeometry, i);

                // Calculate data write requirements
                byte datalow, datahigh;
                int disklen = 6 + (seccount * 512);
                datahigh = (byte)(disklen >> 8);
                datalow = (byte)disklen;

                // Read the data from the file
                fsnr.readByteArray(ref diskbuf, 0, seccount * 512);

                // Call for the sectors to be written
                state.nw.writeByte(7);                  // WRITE MULTIPLE DISK SECTORS
                state.nw.writeInt16((short)disklen);    // DATA LENGTH

                state.nw.writeByte(state.diskno);               // DISK NUMBER
                state.nw.writeByte(reqchs.sector);              // SECTOR
                state.nw.writeByte(reqchs.head);                // HEAD
                state.nw.writeInt16((short)reqchs.cylinder);    // CYLINDER
                state.nw.writeByte((byte)seccount);             // SECTOR COUNT

                // Write the data
                state.nw.writeByteArray(ref diskbuf, 0, seccount * 512);

                // Get response
                byte success = state.nr.readByte();
                int datalen = state.nr.readInt16();

                // Validate response
                if (success != 1)
                {
                    // If the server returned a failure, close the file and throw an exception
                    fs.Close();
                    throw new Exception(String.Format("WriteFast: Server returned failure writing CHS {0} {1} {2} seccount {3}", reqchs.cylinder, reqchs.head, reqchs.sector, seccount));
                }

                if (datalen != 0)
                {
                    // If the server returned any data, close the file and throw an exception
                    fs.Close();
                    throw new Exception(String.Format("WriteFast: Server returned incorrect amount of data. Expected {0} got {1}", 0, datalen));
                }

                // Calculate progress + report it
                float cur = i;
                float max = maxLBA;
                float progress = ((cur / max) * 100f);
                worker.ReportProgress((int)progress, i);
            }

            fs.Close();
        }

        private void workerWriteDisk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the progress bar
            statProg.Value = e.ProgressPercentage;

            // Interpret the sector being read right now
            int cursecs = (int)e.UserState;

            // Get LBA values to understand data written + total volume of data
            CHStoLBA(out int maxsecs, state.diskgeometry, state.diskgeometry);

            // Calculate current + total MBs
            float curMB = (cursecs * 512f) / 1024f / 1024f;
            float totMB = (maxsecs * 512f) / 1024f / 1024f;

            // Display it nicely in the status
            statLabel.Text = String.Format("Writing... ({0}/{1}MiB)", curMB.ToString("N2"), totMB.ToString("N2"));
        }

        private void workerWriteDisk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // We are no longer performing an async function
            state.async = false;

            // Reset maxbufoverride
            state.maxbufoverride = 0;

            // Update the UI
            UpdateUI();

            // If we completed because of an error, display the error
            if (e.Cancelled)
            {
                statLabel.Text = "Cancelled";
            }
            else if (e.Error != null)
            {
                Error(String.Format("Writing disk failed, error:\n\n{0}", e.Error.Message));
            }
        }
        #endregion // Write Disk Worker methods

        #endregion // Application logic

        #region UI Event handlers

        private void frmMain_Shown(object sender, EventArgs e)
        {
            // Update UI
            UpdateUI();

            // Populate floppy geometries
            PopulateGeometries();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state.async)            // If we're running an async operation, cancel closing
            {
                Error("You must cancel the operation before closing the application");
                e.Cancel = true;
            }
            else if (state.connected)   // Otherwise, if we're connected, disconnect
                Disconnect();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            // Ensure host is present
            if (txtHost.Text.Length == 0)
            {
                Error("Could not connect - a hostname/IP address must be provided");
                return;
            }

            // Make sure we aren't already connected
            if (state.connected)
            {
                Error("Cannot connect to server - already connected to a server");
                return;
            }

            // Connect
            Connect(txtHost.Text);

            // Obtain disk counts + populate lstDisks
            PopulateDiskList();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            // Disconnect
            Disconnect();
        }

        private void cmdBrowseLocal_Click(object sender, EventArgs e)
        {
            // Open a save file dialog - we will disable the overwrite warning to make it a SelectFileDialog - load/save, later determined by another UI interaction
            SaveFileDialog fd = new SaveFileDialog();
            fd.OverwritePrompt = false;
            fd.Title = "Select an image file";
            fd.Filter = "Raw image files (*.img)|*.img|All files (*.*)|*.*";
            fd.FilterIndex = 1;
            
            
            DialogResult result = fd.ShowDialog();

            // Only take an action if the result was OK (Save/Select)
            if (result == DialogResult.OK)
            {
                // User accepted a file, replace the contents of the path textbox
                txtLocalPath.Text = fd.FileName;
            }

            switch (result)
            {
                case DialogResult.OK:
                    
                    break;
                default:
                    break;
            }
                
        }

        private void cmdReadDiskToFile_Click(object sender, EventArgs e)
        {
            // Validate that we're connected
            if (!state.connected)
                return;

            // Validate there is text in the path box
            if (txtLocalPath.Text.Length == 0)
                return;

            // Ensure nothing async is already going on
            if (state.async)
                return;

            // Validate a disk is selected
            int diskno = GetSelectedDisk();
            if (diskno < 0)
                return;

            // Populate state variables
            state.diskno = (byte)diskno;
            state.filepath = txtLocalPath.Text;

            // If this is a floppy, set diskgeometry by the combo box
            // Otherwise, run a command to obtain disk geometry
            if (state.diskno < 0x80)
            {
                // Validate a floppy geometry is selected
                if (cmbFloppyType.SelectedIndex < 0)
                {
                    Error(String.Format("You must select a floppy disk geometry, before attempting to read from FDD{0}", state.diskno));
                    return;
                }

                // TODO: Obtain disk geometry from combo box, instead of assuming 3.5" 1.44MB
                state.diskgeometry = GetSelectedGeometry();

                // The VirtualBox BIOS threw a hissy fit trying to read more than 4 sectors at once - providing invalid information for 8 sectors at once, and straight up failing for 16.
                // Its unclear whether this is a fault of trying to read that much information at once, a fault of the VirtualBox BIOS, or a fault in Borland C's implementation of biosdisk();
                state.maxbufoverride = (int)AppSettings.global[AppSettings.FLOPPY_MAXSECBUF] * 512;
            } else
            {
                // Get the disk geometry
                try
                {
                    state.diskgeometry = GetDiskGeometry((byte)(state.diskno - 0x80));
                } catch (ArgumentException ex)
                {
                    // Failed to obtain disk geometry - raise an error
                    Error(String.Format("Failed to read disk to file, error\n\n{0}", ex.Message));
                    return;
                }

                // Set maxbufoverride, if present
                state.maxbufoverride = (int)AppSettings.global[AppSettings.HARDDISK_MAXSECBUF] * 512;
            }

            // We are now declaring we're running async as all validations have succeeded
            state.async = true;

            // Run the background worker async
            workerReadDisk.RunWorkerAsync();

            // Update UI
            UpdateUI();
        }

        private void cmdWriteFileToDisk_Click(object sender, EventArgs e)
        {
            // Validate that we're connected
            if (!state.connected)
                return;

            // Validate there is text in the path box
            if (txtLocalPath.Text.Length == 0)
                return;

            // Ensure nothing async is already going on
            if (state.async)
                return;

            // Validate a disk is selected
            int diskno = GetSelectedDisk();
            if (diskno < 0)
                return;

            // Populate state variables
            state.diskno = (byte)diskno;
            state.filepath = txtLocalPath.Text;

            // If this is a floppy, set diskgeometry by the combo box
            // Otherwise, run a command to obtain disk geometry
            if (state.diskno < 0x80)
            {
                // Validate a floppy geometry is selected
                if (cmbFloppyType.SelectedIndex < 0)
                {
                    Error(String.Format("You must select a floppy disk geometry, before attempting to write to FDD{0}", state.diskno));
                    return;
                }

                // TODO: Obtain disk geometry from combo box, instead of assuming 3.5" 1.44MB
                state.diskgeometry = GetSelectedGeometry();

                // The VirtualBox BIOS threw a hissy fit trying to read more than 4 sectors at once - providing invalid information for 8 sectors at once, and straight up failing for 16.
                // Its unclear whether this is a fault of trying to read that much information at once, a fault of the VirtualBox BIOS, or a fault in Borland C's implementation of biosdisk();
                state.maxbufoverride = (int)AppSettings.global[AppSettings.FLOPPY_MAXSECBUF] * 512;
            }
            else
            {
                // Get the disk geometry
                try
                {
                    state.diskgeometry = GetDiskGeometry((byte)(state.diskno - 0x80));
                }
                catch (ArgumentException ex)
                {
                    // Failed to obtain disk geometry - raise an error
                    Error(String.Format("Failed to write file to disk, error\n\n{0}", ex.Message));
                    return;
                }

                // Set maxbufoverride, if present
                state.maxbufoverride = (int)AppSettings.global[AppSettings.HARDDISK_MAXSECBUF] * 512;
            }

            // We are now declaring we're running async as all validations have succeeded
            state.async = true;

            // Run the background worker async
            workerWriteDisk.RunWorkerAsync();

            // Update UI
            UpdateUI();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            // Validate that an async operation is in progress
            if (!state.async)
                return;

            // Send a cancel event to both background workers
            workerReadDisk.CancelAsync();
            workerWriteDisk.CancelAsync();
        }

        // Automatically attempt to connect, if the user presses enter on the textbox (and isn't already connected)
        private void txtHost_KeyDown(object sender, KeyEventArgs e)
        {
            if (state.connected && e.KeyCode == Keys.Enter)
            {
                cmdConnect_Click(sender, new EventArgs());
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // We do not check if we are connected, or if an async operation is in progress
            // This is because we set the variables for the async operation, in a location, other than AppSettings

            // Get the result of opening frmPreferences()
            DialogResult result = (new frmPreferences()).ShowDialog();

            // If the user pressed "Save"
            if (result == DialogResult.OK)
            {
                // Populate floppy geometries again
                PopulateGeometries();
            }
        }

        private void aboutDisksrvClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new frmAbout()).ShowDialog();
        }

        private void onlineDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://stereorocker.github.io/disksrv/");
        }

        #endregion // UI Event Handlers

    }
}
