using System.ComponentModel;
using System.Windows.Forms;

namespace disksrv_client
{
    public class chs_t
    {
        #region Property values, for PropertyGrid
        [Browsable(true)]
        [DisplayName("Tracks")]
        [ReadOnly(false)]
        public int _cyls
        {
            get { return cylinder; }
            set { cylinder = value; }
        }

        [Browsable(true)]
        [DisplayName("Sides")]
        [ReadOnly(false)]
        public byte _heads
        {
            get { return head; }
            set { head = value; }
        }

        [Browsable(true)]
        [DisplayName("Sectors")]
        [ReadOnly(false)]
        public byte _secs
        {
            get { return sector; }
            set { sector = value; }
        }

        // Note, this property is used by frmPreferences ONLY, and does not reflect a value used in application logic
        public ListViewItem lvi;
        [Browsable(true)]
        [DisplayName("Geometry name")]
        [ReadOnly(false)]
        public string _name
        {
            get {
                if (lvi != null)
                    return lvi.Text;
                else
                    return "";
            }

            set {
                if (lvi != null)
                    lvi.Text = value;
            }
        }

        
        #endregion

        #region Variables and constructor, as used in application logic
        public int cylinder;
        public byte head;
        public byte sector;

        public chs_t(int c, byte h, byte s)
        {
            // Set the CHS values
            cylinder = c;
            head = h;
            sector = s;

            // Set lvi, because a constructor must instantiate all fields
            lvi = null;
        }

        public chs_t()
        {
            // Do nothing
        }
        #endregion
    };
}
