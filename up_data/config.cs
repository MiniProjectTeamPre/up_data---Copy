﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace up_data {
    public partial class config : Form {
        public config() {
            InitializeComponent();
        }

        private string mode_debug = "Debug";
        private string mode_operation = "Operation";
        private void config_Load(object sender, EventArgs e) {
            
        }
        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;
            string s = mode_debug;
            try { s = File.ReadAllText("../../config/up_data_config_mode.txt"); } catch { }
            if (s == mode_debug) {
                pgb_debug.Image = Properties.Resources.debug;
                pgb_operation.Image = Properties.Resources.operation_null2;
                flag_string_debug = true;
            } else {
                pgb_operation.Image = Properties.Resources.operation;
                pgb_debug.Image = Properties.Resources.debug_null2;
                flag_string_debug = false;
            }
            try { textBox1.Text = File.ReadAllText("../../config/up_data_config_database_server.txt"); } catch { }
            try { textBox2.Text = File.ReadAllText("../../config/up_data_config_database_name.txt"); } catch { }
            try { textBox3.Text = File.ReadAllText("../../config/up_data_config_computer_name.txt"); } catch { }
            try { textBox4.Text = File.ReadAllText("../../config/up_data_config_station_name.txt"); } catch { }
            try { textBox5.Text = File.ReadAllText("../../config/up_data_config_process_name.txt"); } catch { }

            this.Activate();
            if (Form.ActiveForm != this) {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;
            }
            button1.Focus();
        }

        private void Config_Activated(object sender, EventArgs e) {
            
        }

        private void pgb_debug_Click(object sender, EventArgs e) {
            pgb_debug.Image = Properties.Resources.debug;
            pgb_operation.Image = Properties.Resources.operation_null2;
            flag_string_debug = true;
        }

        private void pgb_operation_Click(object sender, EventArgs e) {
            pgb_operation.Image = Properties.Resources.operation;
            pgb_debug.Image = Properties.Resources.debug_null2;
            flag_string_debug = false;
        }

        private bool flag_string_debug = true;
        private void button1_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("_กรุณายืนยันการตั้งค่า", "config", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;
            if(flag_string_debug) File.WriteAllText("../../config/up_data_config_mode.txt", mode_debug);
            else File.WriteAllText("../../config/up_data_config_mode.txt", mode_operation);
            File.WriteAllText("../../config/up_data_config_database_server.txt", textBox1.Text);
            File.WriteAllText("../../config/up_data_config_database_name.txt", textBox2.Text);
            File.WriteAllText("../../config/up_data_config_computer_name.txt", textBox3.Text);
            File.WriteAllText("../../config/up_data_config_station_name.txt", textBox4.Text);
            File.WriteAllText("../../config/up_data_config_process_name.txt", textBox5.Text);

            write_xml();
            write_xml2();
        }

        private string xml_DatabaseServer = "Vd147+pBWCihy3FzdahxTg==";//tpp
        private string xml_DatabaseName = "U/AFYtHi4S8yjwyD3O/AmA==";//tpp
        public void write_xml() {
            string dfdf = "";
            try { dfdf = File.ReadAllText("../../config/up_data_config_database_name.txt"); } catch { }
            if (dfdf == "TPR_PRISM") {
                xml_DatabaseServer = "Vd147+pBWChvWVcRsdZvHQ==";
                xml_DatabaseName = "awiuCMQfI7kyjwyD3O/AmA==";
            } else {
                xml_DatabaseServer = "Vd147+pBWCihy3FzdahxTg==";
                xml_DatabaseName = "U/AFYtHi4S8yjwyD3O/AmA==";
            }
            XmlTextWriter writer = new XmlTextWriter("TeamPrecision.PRISM.Setting.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("cSettingSerial");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            writer.WriteStartElement("TestingMode");
            try { writer.WriteString(File.ReadAllText("../../config/up_data_config_mode.txt")); } catch { writer.WriteString("Operation"); }
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseServer");
            writer.WriteString(xml_DatabaseServer);
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseName");
            writer.WriteString(xml_DatabaseName);
            writer.WriteEndElement();
            writer.WriteStartElement("DatabaseUser");
            writer.WriteString("qNMPB0293rI=");
            writer.WriteEndElement();
            writer.WriteStartElement("DatabasePassword");
            writer.WriteString("m/2+3pRMmYg=");
            writer.WriteEndElement();
            writer.WriteStartElement("ComputerName");
            try { writer.WriteString(File.ReadAllText("../../config/up_data_config_computer_name.txt")); } catch { writer.WriteString("SST-PC-FCT1"); }
            writer.WriteEndElement();
            writer.WriteStartElement("StationName");
            try { writer.WriteString(File.ReadAllText("../../config/up_data_config_station_name.txt")); } catch { writer.WriteString("Operation"); }
            writer.WriteEndElement();
            writer.WriteStartElement("ProcessName");
            try { writer.WriteString(File.ReadAllText("../../config/up_data_config_process_name.txt")); } catch { writer.WriteString("Operation"); }
            writer.WriteEndElement();
            writer.WriteStartElement("UsePasswordWhenLogin");
            writer.WriteString("false");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        /// <summary>
        /// For .config Read file xml config and write new config
        /// </summary>
        public void write_xml2() {
            string dataBaseConfig = String.Empty;
            try{
                dataBaseConfig = File.ReadAllText("../../config/up_data_config_database_name.txt"); 
            } catch { }
            if (dataBaseConfig == "TPR_PRISM")
            {
                xml_DatabaseServer = "Annop Server";
                xml_DatabaseName = "PRISM_TPR";
            }
            else
            {
                xml_DatabaseServer = "Annop Server";
                xml_DatabaseName = "PRISM_TPP";
            }

            string nameFileConfig = "TeamPrecision.PRISM.dll.config";
            XmlSerializer reader = new XmlSerializer(typeof(Configuration));
            StreamReader file = new StreamReader(nameFileConfig);
            Configuration overview = (Configuration)reader.Deserialize(file);
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[0].Value = File.ReadAllText("../../config/up_data_config_mode.txt");
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[1].Value = xml_DatabaseServer;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[2].Value = xml_DatabaseName;
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[5].Value = File.ReadAllText("../../config/up_data_config_computer_name.txt");
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[6].Value = File.ReadAllText("../../config/up_data_config_station_name.txt");
            overview.UserSettings.TeamPrecisionPRISMPropertiesSettings.Setting[7].Value = File.ReadAllText("../../config/up_data_config_process_name.txt");
            file.Close();

            var writer = new XmlSerializer(typeof(Configuration));
            var wfile = new StreamWriter(nameFileConfig);
            writer.Serialize(wfile, overview);
            wfile.Close();
        }

        [XmlRoot(ElementName = "section")]
        public class Section {

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }

            [XmlAttribute(AttributeName = "allowExeDefinition")]
            public string AllowExeDefinition { get; set; }

            [XmlAttribute(AttributeName = "requirePermission")]
            public bool RequirePermission { get; set; }
        }
        [XmlRoot(ElementName = "sectionGroup")]
        public class SectionGroup {

            [XmlElement(ElementName = "section")]
            public Section Section { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }
        [XmlRoot(ElementName = "configSections")]
        public class ConfigSections {

            [XmlElement(ElementName = "sectionGroup")]
            public SectionGroup SectionGroup { get; set; }
        }
        [XmlRoot(ElementName = "setting")]
        public class Setting {

            [XmlElement(ElementName = "value")]
            public string Value { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "serializeAs")]
            public string SerializeAs { get; set; }

            [XmlText]
            public string Text { get; set; }
        }
        [XmlRoot(ElementName = "TeamPrecision.PRISM.Properties.Settings")]
        public class TeamPrecisionPRISMPropertiesSettings {

            [XmlElement(ElementName = "setting")]
            public List<Setting> Setting { get; set; }
        }
        [XmlRoot(ElementName = "userSettings")]
        public class UserSettings {

            [XmlElement(ElementName = "TeamPrecision.PRISM.Properties.Settings")]
            public TeamPrecisionPRISMPropertiesSettings TeamPrecisionPRISMPropertiesSettings { get; set; }
        }
        [XmlRoot(ElementName = "supportedRuntime")]
        public class SupportedRuntime {

            [XmlAttribute(AttributeName = "version")]
            public string Version { get; set; }

            [XmlAttribute(AttributeName = "sku")]
            public string Sku { get; set; }
        }
        [XmlRoot(ElementName = "startup")]
        public class Startup {

            [XmlElement(ElementName = "supportedRuntime")]
            public SupportedRuntime SupportedRuntime { get; set; }
        }
        [XmlRoot(ElementName = "configuration")]
        public class Configuration {

            [XmlElement(ElementName = "configSections")]
            public ConfigSections ConfigSections { get; set; }

            [XmlElement(ElementName = "userSettings")]
            public UserSettings UserSettings { get; set; }

            [XmlElement(ElementName = "startup")]
            public Startup Startup { get; set; }
        }

        private void tPRToolStripMenuItem_Click(object sender, EventArgs e) {
            textBox1.Text = "192.168.10.19";
            textBox2.Text = "TPR_PRISM";
            File.WriteAllText("../../config/up_data_config_database_server.txt", textBox1.Text);
            File.WriteAllText("../../config/up_data_config_database_name.txt", textBox2.Text);
            tPRToolStripMenuItem.Checked = true;
            tPPToolStripMenuItem.Checked = false;
        }
        private void tPPToolStripMenuItem_Click(object sender, EventArgs e) {
            textBox1.Text = "192.168.11.38";
            textBox2.Text = "TPP_PRISM";
            File.WriteAllText("../../config/up_data_config_database_server.txt", textBox1.Text);
            File.WriteAllText("../../config/up_data_config_database_name.txt", textBox2.Text);
            tPPToolStripMenuItem.Checked = true;
            tPRToolStripMenuItem.Checked = false;
        }
    }
}
