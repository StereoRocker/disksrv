using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace disksrv_client
{
    public partial class frmPreferences : Form
    {
        private bool edited = false;

        private List<chs_t> geometries = new List<chs_t>();

        public void ValueChanged(object sender, EventArgs e)
        {
            edited = true;
        }

        public frmPreferences()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            // Write new values to AppSettings for Global
            AppSettings.global[AppSettings.FLOPPY_MAXSECBUF] = (int)valFloppySecMax.Value;
            AppSettings.global[AppSettings.HARDDISK_MAXSECBUF] = (int)valHDDSecMax.Value;

            // Replace the array AppSettings.floppy_geometries
            AppSettings.floppy_geometries = new Dictionary<string, chs_t>();
            foreach (chs_t chs in geometries)
            {
                AppSettings.floppy_geometries.Add(chs._name, new chs_t(chs.cylinder, chs.head, chs.sector));
            }

            // Save AppSettings
            AppSettings.SaveSettings(AppSettings.SETTINGS_FILE);

            // We have saved changes, so set edited to false
            edited = false;
            
            // Close by setting DialogResult to OK
            this.DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            // The user has chosen to cancel the dialog, do not prompt to save changes
            edited = false;

            this.DialogResult = DialogResult.Cancel;
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            // Insert the values from AppSettings for Global
            valFloppySecMax.Value = (int)(AppSettings.global[AppSettings.FLOPPY_MAXSECBUF]);
            valHDDSecMax.Value = (int)(AppSettings.global[AppSettings.HARDDISK_MAXSECBUF]);

            // Populate the geometry list (should only be run once)
            PopulateList();

            // Set edited to false
            edited = false;
        }

        private void frmPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If a setting has been modified
            if (edited)
            {
                DialogResult result = MessageBox.Show("Settings have been modified, save them first?", "disksrv", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:      // User has chosen to save before closing
                        // Call cmdSave_Click
                        cmdSave_Click(sender, new EventArgs());
                        break;
                    case DialogResult.No:       // User has chosen not to save, before closing
                        // Respect the decision, and do nothing
                        break;
                    case DialogResult.Cancel:   // User has chosen to not close the dialog
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void cmdAddGeometry_Click(object sender, EventArgs e)
        {
            // Create a new chs_t
            chs_t chs = new chs_t(0, 0, 0);

            // Add it to the internal list
            geometries.Add(chs);

            // Add it to the ListView
            ListViewItem lvi = lstGeometries.Items.Add("New geometry");

            // Set link in chs_t back to the list view item
            chs.lvi = lvi;

            // Set link in the ListViewItem back to the chs value
            lvi.Tag = chs;
        }

        private void PopulateList()
        {
            // Clear the list
            lstGeometries.Items.Clear();

            // Clone AppSettings.floppy_geometries into a new List<chs_t>
            foreach (KeyValuePair<string, chs_t> kvp in AppSettings.floppy_geometries)
            {
                // Clone the chs_t value
                chs_t chs = new chs_t()
                {
                    // Copy the attribute values
                    cylinder = kvp.Value.cylinder,
                    head = kvp.Value.head,
                    sector = kvp.Value.sector
                };

                // Create the ListViewItem
                ListViewItem lvi = lstGeometries.Items.Add(kvp.Key);
                chs.lvi = lvi;
                lvi.Tag = chs;

                // Add it to the list
                geometries.Add(chs);
            }
        }

        private void lstGeometries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is no longer a selected item, set PropertyGrid to point at null
            if (lstGeometries.SelectedIndices.Count < 1)
            {
                propGeometry.SelectedObject = null;
                return;
            }

            // Set the PropertyGrid to point at the correct chs_t value
            propGeometry.SelectedObject = lstGeometries.SelectedItems[0].Tag;
        }

        private void cmdRemoveGeometry_Click(object sender, EventArgs e)
        {
            // If there is no selected item, do nothing
            if (lstGeometries.SelectedIndices.Count < 1)
                return;

            // Remove the item from the internal list
            geometries.RemoveAt(lstGeometries.SelectedIndices[0]);

            // Remove the item from lstGeometries
            lstGeometries.Items.RemoveAt(lstGeometries.SelectedIndices[0]);
        }
    }
}
