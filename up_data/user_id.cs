using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace up_data {
    public partial class user_id : Form {
        public user_id() {
            InitializeComponent();
        }

        private void user_id_Load(object sender, EventArgs e) {

        }

        private void user_id_FormClosed(object sender, FormClosedEventArgs e) {
            if(flag_id_pass) File.WriteAllText("../../config/up_data_user_id.txt", textBox1.Text);
            else File.WriteAllText("../../config/up_data_user_id.txt", "");
            TeamPrecision.PRISM.cSettingValues.EmployeeID = textBox1.Text;
        }
        private bool flag_id_pass = false;
        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter) return;
            if (textBox1.Text.Count() != 5) { MessageBox.Show("not format"); return; }
            //ชั่วคราว==============
            //string[] id = { "81087", "81155", "71027", "71020", "36027", "71099", "71064", "72006", "72009", "36254",
            //                "37218", "22134", "71045", "15151", "34899", "33735", "72035", "62019", "60080", "54086",
            //                "53431", "71054", "72057", "72098", "30282", "62074"};
            //foreach (string id_ in id) {
            //    if (textBox1.Text == id_) goto passs;
            //}
            //List<string> id_id = new List<string>();
            //try {
            //    string[] hhgh = File.ReadAllLines("up_data_login_id_add_new.txt");
            //    id_id = hhgh.ToList<string>();
            //} catch { }
            //foreach (string id_ in id_id) {
            //    if (textBox1.Text == id_) goto passs;
            //}
            //====================
            //Form1 f = new Form1();
            //string s = f.rest_api(mis_cmd.GetEmployeeID, "", "", "", "", textBox1.Text);
            //if (s != textBox1.Text) { MessageBox.Show(s); return; }

            if (!TeamPrecision.PRISM.cUsers.UserLogin(textBox1.Text)) {
                MessageBox.Show("_ไม่พบข้อมูลในระบบ");
                return;
            } 

            //passs:
            flag_id_pass = true;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;
            this.Activate();
            if (Form.ActiveForm != this) {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
