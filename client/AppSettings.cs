using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using System.Xml;
using System.IO;


namespace disksrv_client
{
    public static class AppSettings
    {
        // Constants
        public static string SETTINGS_FILE = "settings.xml";

        // Setting constants
        public const string FLOPPY_MAXSECBUF = "floppy_maxsecbuf";
        public const string HARDDISK_MAXSECBUF = "harddisk_maxsecbuf";

        // Controllable settings
        public static Dictionary<string, object> global;

        public static Dictionary<string, chs_t> floppy_geometries;

        // Static class constructor
        public static void Initialize()
        {
            // Check if the settings file exists
            if (File.Exists(SETTINGS_FILE))
            {
                // Try to load from the file
                LoadSettings(SETTINGS_FILE);
            } else
            {
                // Initialize the default settings
                DefaultSettings();

                // Otherwise, save the settings as initialized already
                SaveSettings(SETTINGS_FILE);
            }
        }

        public static void DefaultSettings()
        {
            // Instantiate setting variables
            global = new Dictionary<string, object>();
            floppy_geometries = new Dictionary<string, chs_t>();

            // Add the default global settings
            global.Add(FLOPPY_MAXSECBUF, 4);
            global.Add(HARDDISK_MAXSECBUF, 0);

            // Add some default floppy geometries
            chs_t F144_35 = new chs_t(80, 2, 18);       // 3.5" 1.44MB
            chs_t F720_35 = new chs_t(80, 2, 9);        // 3.5" 720KB
            floppy_geometries.Add("3.5\" 1.44MB", F144_35);
            floppy_geometries.Add("3.5\" 720KB", F720_35);
        }

        public static void LoadSettings(string path)
        {
            // Instantiate the global variables
            global = new Dictionary<string, object>();
            floppy_geometries = new Dictionary<string, chs_t>();

            // Load the XML file
            XElement XML_root = XElement.Load(path);

            // If the global element does not exist, load defaults and save them
            if (!XML_root.Elements("global").Any())
            {
                DefaultSettings();
                SaveSettings(path);
            }

            // If the FloppyGeometries element does not exist, load defaults and save them
            if (!XML_root.Elements("FloppyGeometries").Any())
            {
                DefaultSettings();
                SaveSettings(path);
            }

            // Parse the global element
            XElement XML_globals = XML_root.Element("global");
            foreach (XElement XML_setting in XML_globals.Elements("setting"))
            {
                // Determine the value type
                switch (XML_setting.Attribute("type").Value)
                {
                    // Parse an int
                    case "System.Int32":
                        global.Add(XML_setting.Attribute("name").Value, Int32.Parse(XML_setting.Attribute("value").Value));
                        System.Diagnostics.Debug.WriteLine("AppSettings: Added {0}, {1}, {2}", XML_setting.Attribute("name"), XML_setting.Attribute("type"), XML_setting.Attribute("value"));
                        break;

                    // Parse a string
                    case "System.String":
                        global.Add(XML_setting.Attribute("name").Value, XML_setting.Attribute("value").Value);
                        System.Diagnostics.Debug.WriteLine("AppSettings: Added {0}, {1}, {2}", XML_setting.Attribute("name"), XML_setting.Attribute("type"), XML_setting.Attribute("value"));
                        break;

                    // Unhandled type - output this to the debug console, but silently fail.
                    default:
                        System.Diagnostics.Debug.WriteLine("Unhandled attribute type, {0}", XML_setting.Attribute("type").Value);
                        break;
                }
            }

            // Parse the FloppyGeometries element
            XElement XML_geometries = XML_root.Element("FloppyGeometries");
            foreach(XElement XML_geometry in XML_geometries.Elements("geometry"))
            {
                // Obtain the geometry's friendly name
                string name = XML_geometry.Attribute("name").Value;

                // Parse the CHS values for this geometry
                chs_t chs = new chs_t();
                chs.cylinder = Int16.Parse(XML_geometry.Attribute("tracks").Value);
                chs.head = Byte.Parse(XML_geometry.Attribute("sides").Value);
                chs.sector = Byte.Parse(XML_geometry.Attribute("sectors").Value);

                // Add it to the floppy_geometries dictionary
                floppy_geometries.Add(name, chs);
            }

            // No other settings to load at this time
        }

        public static void SaveSettings(string path)
        {
            // Create the children of the root XML structure

            XElement XML_globals = new XElement("global");

            XElement XML_geometries = new XElement("FloppyGeometries");

            // Populate global settings element
            foreach (KeyValuePair<string, object> kvp in global)
            {
                // Instantiate the XElement containing the setting
                XElement XML_setting = new XElement("setting");
                XML_setting.SetAttributeValue("name", kvp.Key);
                XML_setting.SetAttributeValue("type", kvp.Value.GetType().ToString());
                XML_setting.SetAttributeValue("value", kvp.Value);

                // Add the setting to XML_globals
                XML_globals.Add(XML_setting);
            }

            // Populate geometries settings element
            foreach (KeyValuePair<string, chs_t> kvp in floppy_geometries)
            {
                // Instantiate the XElement containing the geometry
                XElement XML_geometry = new XElement("geometry");
                XML_geometry.SetAttributeValue("name", kvp.Key);
                XML_geometry.SetAttributeValue("tracks", kvp.Value.cylinder);
                XML_geometry.SetAttributeValue("sides", kvp.Value.head);
                XML_geometry.SetAttributeValue("sectors", kvp.Value.sector);

                // Add the geometry to XML_geometries
                XML_geometries.Add(XML_geometry);
            }

            // Instantiate the XML root
            XElement XML_root =
                new XElement("disksrvclient",
                    XML_globals,
                    XML_geometries
                );

            // Write this to file, with UTF-8 encoding
            XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8);

            // Set the formatting to indented, to make it easy to edit for the user
            writer.Formatting = Formatting.Indented;

            // Write the XML contents
            XML_root.WriteTo(writer);

            // Flush and close the writer
            writer.Flush();
            writer.Close();
        }
    }
}
