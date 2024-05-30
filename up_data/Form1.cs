using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;

namespace up_data {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.TransparencyKey = Color.Black;
            this.BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
        }

        private string path_file_data = "D:\\DATA_MIS";
        private string path_file_data_err = "D:\\DATA_MIS_ERROR";
        private string path_file_data_passed = "D:\\DATA_MIS_PASSED";
        AutoItX3Lib.AutoItX3 autoit = new AutoItX3Lib.AutoItX3();
        private void Form1_Load(object sender, EventArgs e) {
            try { ctms_kill_chrome_delay.Text = File.ReadAllText("../../config/up_data_kill_chrome_delay.txt"); } catch {
                File.WriteAllText("../../config/up_data_kill_chrome_delay.txt","5000");
            }
            try { ctms_kill_chrome.Checked = Convert.ToBoolean(File.ReadAllText("../../config/up_data_kill_chrome.txt")); } catch {
                File.WriteAllText("../../config/up_data_kill_chrome.txt","True");
            }
            try { ctms_password.Text = File.ReadAllText("../../config/up_data_password.txt"); } catch {
                File.WriteAllText("../../config/up_data_password.txt","01");
            }
            try {
                string ff = File.ReadAllText("../../config/up_data_uri.txt");
                if (ff == "iot") {
                    url_string = "http://iot.teampcba.com:1880/PRISM/";
                    ctms_iot.Checked = true;
                    ctms_ip.Checked = false;
                }
                if (ff == "ip") {
                    url_string = "http://192.168.11.15:1880/PRISM/";
                    ctms_ip.Checked = true;
                    ctms_iot.Checked = false;
                }
            } catch { }
            try { ctms_Skip_REST.Checked = Convert.ToBoolean(File.ReadAllText("../../config/up_data_skip_rest.txt")); } catch { }

            try {
                ctms_testAfterAsm.Checked = Convert.ToBoolean(File.ReadAllText("../../config/up_data_testAfterAsm.txt"));
            } catch { }

            Process[] pname = Process.GetProcessesByName("up_data");
            if (pname.Length == 2) {
                Application.Exit();
                return;
            }

            config c = new config();
            c.write_xml();
            c.write_xml2();
            c.Dispose();

            TeamPrecision.PRISM.frmUserLogin mm = new TeamPrecision.PRISM.frmUserLogin();
            mm.Show();
            mm.Close();
            if (!Directory.Exists(path_file_data)) Directory.CreateDirectory(path_file_data);
            if (!Directory.Exists(path_file_data_err)) Directory.CreateDirectory(path_file_data_err);
            if (!Directory.Exists(path_file_data_passed)) Directory.CreateDirectory(path_file_data_passed);
        }

        private void timer_get_log_err_Tick(object sender, EventArgs e) {
            //return;
            timer_get_log_err.Enabled = false;
            string[] zxc = Directory.GetFiles(path_file_data_err);
            for (int i = 0; i < zxc.Count(); i++) {
                string s = zxc[i].Replace(path_file_data_err + "\\", "");
                File.Copy(zxc[i], path_file_data + "\\" + s);
                File.Delete(zxc[i]);
            }
            timer_get_log_err.Enabled = true;
        }
        private void application_run_Tick(object sender, EventArgs e) {
            application_run.Enabled = false;
            try {
                File.ReadAllText("up_data_login.txt");
                File.Delete("up_data_login.txt");
                user_id f = new user_id();
                f.ShowDialog();
                if (File.ReadAllText("../../config/up_data_user_id.txt") == "") File.WriteAllText("up_data_login_fail.txt", "");
                else File.WriteAllText("up_data_login_ok.txt", "");
            } catch { }
            try {
                File.ReadAllText("up_data_config.txt");
                File.Delete("up_data_config.txt");
                config f = new config();
                f.ShowDialog();
                File.WriteAllText("up_data_config_ok.txt", "");
            } catch { }
            List<string> file_data = new List<string>();
            try {
                string[] zxc = Directory.GetFiles(path_file_data);
                file_data = zxc.ToList<string>();
            } catch { }
            string s = "";
            for (int i = 0; i < 10; i++) {
                try { s = file_data[i].Replace(path_file_data + "\\", ""); } catch { break; }
                s = s.Replace("!", "/");
                string[] s_split = s.Split('_');
                string msg_pass = "ผ่านขั้นตอน";
                try { msg_pass = File.ReadAllText("../../config/prism_retest_text_pass.txt"); } catch { }
                string msg_fail = "FCT1 สถานะ : FAIL";
                try { msg_fail = File.ReadAllText("../../config/prism_retest_text_fail.txt"); } catch { }

                if (s_split[0] == "PRISM") {
                    s_split[3] = s_split[3].Replace("-", "/");
                    string sn = rest_api(mis_cmd.GetFinalSN, s_split[2]);
                    if (sn != s_split[2]) {
                        if (sn.Contains("ASM")) File.Copy(file_data[i], path_file_data_passed + "\\" + s);
                        else File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    string sdfa = rest_api(mis_cmd.GetPreviouseStep, sn);// "VALID"  "!!...Proecess :FCT1 สถานะ : FAIL "    "!!...ผ่านขั้นตอน FCT1 แล้ว"
                    if (sdfa != "VALID" && !sdfa.Contains(msg_fail)) {
                        File.Copy(file_data[i], path_file_data_passed + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    bool bnjh = false;
                    for (int hj = 0; hj < 10; hj++) {
                        //มันยังเขียน txt ไม่สมบูรณ์ แต่อีกโปรแกรมมันรีบมาอ่านก่อน
                        try { TestResult_sup = File.ReadAllText(file_data[i]); } catch { Thread.Sleep(100); continue; }
                        bnjh = true;
                        break;
                    }
                    if (!bnjh) {
                        MessageBox.Show("ERR Line: " + new System.Diagnostics.StackFrame(0, true).GetFileLineNumber());
                        continue;
                    }
                    string sss = "";
                    string result_AddTestResult = rest_api(mis_cmd.AddTestResult, sn, s_split[3], s_split[4], s_split[5]);
                    if (result_AddTestResult != "SUCCESS") {
                        File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        label1.ForeColor = Color.Red;
                    } else label1.ForeColor = Color.Blue;
                    Stopwatch tt = new Stopwatch();
                    tt.Restart();
                    while (tt.ElapsedMilliseconds < 30000) {
                        sss = rest_api(mis_cmd.GetPreviouseStep, sn);
                        if (!sss.Contains(msg_pass) && !sss.Contains(msg_fail)) {
                            System.Threading.Thread.Sleep(5000);
                            continue;
                        }
                        tt.Stop();
                        break;
                    }
                    if (tt.IsRunning) {
                        File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    File.Move(file_data[i], file_data[i].Replace("D:\\DATA_MIS", "D:\\DATA_MIS_SUCCESS"));
                    File.WriteAllText("D:\\DATA_MIS_SUCCESS\\" + sn + "_" + s_split[6], result_AddTestResult);
                    //File.Delete(file_data[i]);
                }

                if (s_split[0] == "ASM") {
                    s_split[3] = s_split[3].Replace("-", "/");
                    string sn = rest_api(mis_cmd.GetFinalSN, s_split[2]);
                    if (!sn.Contains("ASM")) {
                        File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    string sdfa = rest_api(mis_cmd.GetPreviouseStep, sn);// "VALID"  "!!...Proecess :FCT1 สถานะ : FAIL "    "!!...ผ่านขั้นตอน FCT1 แล้ว"
                    if (sdfa != "VALID" && !sdfa.Contains(msg_fail)) {
                        File.Copy(file_data[i], path_file_data_passed + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    bool bnjh = false;
                    for (int hj = 0; hj < 10; hj++) {
                        //มันยังเขียน txt ไม่สมบูรณ์ แต่อีกโปรแกรมมันรีบมาอ่านก่อน
                        try { TestResult_sup = File.ReadAllText(file_data[i]); } catch { Thread.Sleep(100); continue; }
                        bnjh = true;
                        break;
                    }
                    if (!bnjh) {
                        MessageBox.Show("ERR Line: " + new System.Diagnostics.StackFrame(0, true).GetFileLineNumber());
                        continue;
                    }
                    string sss = "";
                    string result_AddTestResult = rest_api(mis_cmd.AddTestResult, sn, s_split[3], s_split[4], s_split[5]);
                    if (result_AddTestResult != "SUCCESS") {
                        File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        label1.ForeColor = Color.Red;
                    } else label1.ForeColor = Color.Blue;
                    Stopwatch tt = new Stopwatch();
                    tt.Restart();
                    while (tt.ElapsedMilliseconds < 45000) {
                        sss = rest_api(mis_cmd.GetPreviouseStep, sn);
                        if (!sss.Contains(msg_pass) && !sss.Contains(msg_fail)) {
                            System.Threading.Thread.Sleep(5000);
                            continue;
                        }
                        tt.Stop();
                        break;
                    }
                    if (tt.IsRunning) {
                        File.Copy(file_data[i], path_file_data_err + "\\" + s);
                        File.Delete(file_data[i]);
                        continue;
                    }
                    //File.Delete(file_data[i]);// "!!...ยังไม่ผ่านขั้นตอน  FCT1" // "!!....ไม่พบข้อมูลหมายเลข ASM210325104076490.64"  //"The remote server returned an error: (407) Proxy Authentication Required."
                    File.Move(file_data[i], file_data[i].Replace("D:\\DATA_MIS", "D:\\DATA_MIS_SUCCESS"));
                    File.WriteAllText("D:\\DATA_MIS_SUCCESS\\" + sn + "_" + s_split[6], result_AddTestResult);
                }

                if (s_split[0] == "DLL") {
                    if (ctms_testAfterAsm.Checked) {
                        dll_denali_nextgen(file_data, i, s_split, msg_pass, msg_fail, s);

                    } else {
                        dll_nomal(file_data, i, s_split, msg_pass, msg_fail, s);
                    }
                }
            }

            try {
                string readWo = File.ReadAllText("up_data_getOutPut.txt");
                File.Delete("up_data_getOutPut.txt");
                string outPut = "0";
                string[] strArrGetWO = TeamPrecision.PRISM.cSNs.getWO(readWo, TeamPrecision.PRISM.cSettingValues.ProcessName);
                outPut = strArrGetWO[4];
                File.WriteAllText("up_data_getOutPut_ok.txt", outPut);
            } catch { }

            application_run.Enabled = true;
        }

        private void dll_nomal(List<string> file_data, int i, string[] s_split, string msg_pass, string msg_fail, string s) {
            bool bnjh = false;
            for (int hj = 0; hj < 10; hj++) {
                //มันยังเขียน txt ไม่สมบูรณ์ แต่อีกโปรแกรมมันรีบมาอ่านก่อน
                try { TestResult_sup = File.ReadAllText(file_data[i]); } catch { Thread.Sleep(100); continue; }
                bnjh = true;
                break;
            }
            if (!bnjh) {
                MessageBox.Show("ERR Line: " + new System.Diagnostics.StackFrame(0, true).GetFileLineNumber());
                return;
            }
            string employeeID = File.ReadAllText("../../config/up_data_user_id.txt");
            if (employeeID == "") {
                while (true) {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("_ใส่ User ID", "User ID", "", 500, 300);
                    if (input == "") continue;
                    if (input.Length != 5) {
                        MessageBox.Show("not format");
                        continue;
                    }
                    employeeID = input;
                    break;
                }
            }
            TeamPrecision.PRISM.cSettingValues.EmployeeID = employeeID;
            string MsgPRISM = "";
            up_prism_timeout_user = s_split[1];
            up_prism_timeout_sn = s_split[2];
            up_prism_timeout_wo = s_split[3];
            up_prism_timeout_fg = s_split[4];
            up_prism_timeout_result = s_split[5];
            up_prism_timeout_dataSummary = TestResult_sup.Replace("'", "");
            bool up_ok = false;
            for (int hjkh = 0; hjkh < 2; hjkh++) {
                //MsgPRISM = function_timeout(up_prism_timeout, 7500);
                MsgPRISM = TeamPrecision.PRISM.cResults.SaveTestResult(s_split[2], s_split[5], up_prism_timeout_dataSummary);
                if (MsgPRISM != "SUCCESS")
                {
                    LogUpPrismError(up_prism_timeout_sn, "SaveTestResult", MsgPRISM);
                    continue;
                }
                up_ok = true;
                break;
            }
            if (!up_ok)
            {
                File.Copy(file_data[i], path_file_data_err + "\\" + s);
                File.Delete(file_data[i]);
                LogUpPrismError(up_prism_timeout_sn, "MoveFile", "");
                return;
            }
            DllUpYield();
            Stopwatch tt = new Stopwatch();
            tt.Restart();
            while (tt.ElapsedMilliseconds < 30000)
            {
                //MsgPRISM = function_timeout(check_prism_timeout, 7500);
                MsgPRISM = TeamPrecision.PRISM.cSNs.CheckStatusSN(up_prism_timeout_sn);
                if (!MsgPRISM.Contains(msg_pass) && !MsgPRISM.Contains(msg_fail))
                {
                    Thread.Sleep(500);
                    LogUpPrismError(up_prism_timeout_sn, "CheckStatusSN", MsgPRISM);
                    continue;
                }
                tt.Stop();
                break;
            }
            if (tt.IsRunning)
            {
                File.Copy(file_data[i], path_file_data_err + "\\" + s);
                File.Delete(file_data[i]);
                LogUpPrismError(up_prism_timeout_sn, "MoveFile", "");
                return;
            }

            File.Delete(file_data[i]);
        }
        private void dll_denali_nextgen(List<string> file_data, int i, string[] s_split, string msg_pass, string msg_fail, string s) {
            bool bnjh = false;
            for (int hj = 0; hj < 10; hj++) {
                //มันยังเขียน txt ไม่สมบูรณ์ แต่อีกโปรแกรมมันรีบมาอ่านก่อน
                try { TestResult_sup = File.ReadAllText(file_data[i]); } catch { Thread.Sleep(100); continue; }
                bnjh = true;
                break;
            }
            if (!bnjh) {
                MessageBox.Show("ERR Line: " + new StackFrame(0, true).GetFileLineNumber());
                return;
            }
            string employeeID = File.ReadAllText("../../config/up_data_user_id.txt");
            if (employeeID == "") {
                while (true) {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("_ใส่ User ID", "User ID", "", 500, 300);
                    if (input == "") continue;
                    if (input.Length != 5) {
                        MessageBox.Show("not format");
                        continue;
                    }
                    employeeID = input;
                    break;
                }
            }
            TeamPrecision.PRISM.cSettingValues.EmployeeID = employeeID;
            string MsgPRISM = "";
            up_prism_timeout_user = s_split[1];
            up_prism_timeout_sn = s_split[2];
            up_prism_timeout_wo = s_split[3];
            up_prism_timeout_fg = s_split[4];
            up_prism_timeout_result = s_split[5];
            up_prism_timeout_dataSummary = TestResult_sup.Replace("'", "");
            bool up_ok = false;
            for (int hjkh = 0; hjkh < 2; hjkh++) {
                //MsgPRISM = function_timeout(up_prism_timeout, 35000);
                MsgPRISM = TeamPrecision.PRISM.cResults.SaveTestResult(s_split[2], s_split[5], up_prism_timeout_dataSummary);
                if (MsgPRISM != "SUCCESS")
                {
                    LogUpPrismError(up_prism_timeout_sn, "SaveTestResult", MsgPRISM);
                    continue;
                }
                up_ok = true;
                break;
            }
            if (!up_ok)
            {
                File.Copy(file_data[i], path_file_data_err + "\\" + s);
                File.Delete(file_data[i]);
                LogUpPrismError(up_prism_timeout_sn, "MoveFile", "");
                return;
            }
            DllUpYield();
            string sn_asm = TeamPrecision.PRISM.cSNs.getSerialASM(up_prism_timeout_sn);
            up_prism_timeout_sn = sn_asm;
            Stopwatch tt = new Stopwatch();
            tt.Restart();
            while (tt.ElapsedMilliseconds < 30000)
            {
                //MsgPRISM = function_timeout(check_prism_timeout, 30000);
                MsgPRISM = TeamPrecision.PRISM.cSNs.CheckStatusSN(up_prism_timeout_sn);
                if (!MsgPRISM.Contains(msg_pass) && !MsgPRISM.Contains(msg_fail))
                {
                    Thread.Sleep(500);
                    LogUpPrismError(up_prism_timeout_sn, "CheckStatusSN", MsgPRISM);
                    continue;
                }
                tt.Stop();
                break;
            }
            if (tt.IsRunning)
            {
                File.Copy(file_data[i], path_file_data_err + "\\" + s);
                File.Delete(file_data[i]);
                LogUpPrismError(up_prism_timeout_sn, "MoveFile", "");
                return;
            }
            File.Delete(file_data[i]);
        }

        /// <summary>
        /// Up yield to prism with dll
        /// </summary>
        /// <returns>True is SUCCESS</returns>
        private bool DllUpYield() {
            string processName = File.ReadAllText("../../config/up_data_config_process_name.txt");
            for (int loop = 0; loop < 2; loop++)
            {
                string result = TeamPrecision.PRISM.cResults.saveYield(up_prism_timeout_fg, up_prism_timeout_wo.Replace("-", "/"),
                    processName, up_prism_timeout_sn, up_prism_timeout_result, GetStepFail(), up_prism_timeout_user);
                //if (result != Define2.success) continue;
                break;
            }
            return true;
        }

        private string up_prism_timeout_user = string.Empty;
        private string up_prism_timeout_sn = string.Empty;
        private string up_prism_timeout_result = string.Empty;
        private string up_prism_timeout_dataSummary = string.Empty;
        private string up_prism_timeout_wo = string.Empty;
        private string up_prism_timeout_fg = string.Empty;
        private string up_prism_timeout() {
            return TeamPrecision.PRISM.cResults.SaveTestResult(up_prism_timeout_sn, up_prism_timeout_result, up_prism_timeout_dataSummary);
        }
        private string check_prism_timeout() {
            return TeamPrecision.PRISM.cSNs.CheckStatusSN(up_prism_timeout_sn);
        }
        private string check_prism2_timeout()
        {
            string[] check = TeamPrecision.PRISM.cSNs.CheckStatusSNv2(up_prism_timeout_sn, up_prism_timeout_wo);
            return check[1];
        }
        public string function_timeout(Func<string> function, int timeout) {
            Task<string> task = Task.Run(function);
            if (task.Wait(timeout)) return task.Result;
            else return "over timeout";
        }

        private static string url_string = "http://iot.teampcba.com:1880/PRISM/";
        private string TestResult_sup = "";
        public string rest_api(string uri_list, string sn_ = "", string workorder_ = "", string fg_ = "", string teststatus_ = "", string employeeid_ = "") {
            bool flag_retest = false;
            bool flag_autoit = false;
            rrrrrrr:
            string url = url_string + uri_list;
            string employeeid = "";
            string sn = "";
            string process = "";
            string workorder = "";
            string stationname = "";
            string computername = "";
            string partno = "";
            string customersn = "";
            string testresult = "";
            string teststatus = "";
            switch (uri_list) {
                case "GetEmployeeID":
                    employeeid = employeeid_;
                    break;
                case "GetFinalSN":
                    sn = sn_;
                    break;
                case "AddTestResult":
                    employeeid = File.ReadAllText("../../config/up_data_user_id.txt");
                    sn = sn_;
                    process = File.ReadAllText("../../config/up_data_config_process_name.txt");
                    workorder = workorder_;
                    stationname = File.ReadAllText("../../config/up_data_config_station_name.txt");
                    computername = File.ReadAllText("../../config/up_data_config_computer_name.txt");
                    partno = fg_;
                    testresult = TestResult_sup;
                    teststatus = teststatus_;
                    break;
                case "GetPreviouseStep":
                    sn = sn_;
                    process = File.ReadAllText("../../config/up_data_config_process_name.txt");
                    break;
            }

            string fm_json = new JavaScriptSerializer().Serialize(new {
                EmployeeID = employeeid,
                SN = sn,
                Process = process,
                WorkOrder = workorder,
                StationName = stationname,
                ComputerName = computername,
                PartNo = partno,
                CustomerSN = customersn,
                TestResult = testresult,
                TestStatus = teststatus
            });
            string json = string.Empty;
            HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);
            http.Method = "POST";
            http.ContentType = "application/json";
            //http.Timeout = 7500;
            try {
                StreamWriter swJSONPayload = new StreamWriter(http.GetRequestStream());
                swJSONPayload.Write(fm_json);
                swJSONPayload.Close();
            } catch { }
            HttpWebResponse http_sup = null;
            try {
                http_sup = (HttpWebResponse)http.GetResponse();
                Stream stream = http_sup.GetResponseStream();
                if (stream != null) {
                    StreamReader reader = new StreamReader(stream);
                    json = reader.ReadToEnd();
                }
            } catch (Exception ex) {
                json = "{\"errorMessages\":\"" + ex.Message.ToString() + "\"}";
            }
            List<string> values = new List<string>();
            List<string> keys = new List<string>();  
            string pattern = @"\""(?<key>[^\""]+)\""\:\""?(?<value>[^\"",}]+)\""?\,?";
            foreach (Match m in Regex.Matches(json, pattern)) {
                if (m.Success) {
                    values.Add(m.Groups["value"].Value);
                    keys.Add(m.Groups["key"].Value);
                }
            }
            string mnm = string.Join(",", values);
            if(mnm.Contains("error: (407)")) {
                //Process.Start("chrome.exe", "www.google.co.th");
                Process process_chrome = new Process();
                process_chrome.StartInfo.FileName = "chrome.exe";
                process_chrome.StartInfo.Arguments = "www.google.co.th";
                process_chrome.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process_chrome.Start();
                Cursor.Hide();
                System.Threading.Thread.Sleep(Convert.ToInt32(File.ReadAllText("../../config/up_data_kill_chrome_delay.txt")));
                Cursor.Show();
                if (flag_retest && !flag_autoit) {
                    flag_autoit = true;
                    Thread.Sleep(3000);
                    autoit.Send("anocha@teampcba.com");
                    Thread.Sleep(300);
                    autoit.Send("{TAB}");
                    Thread.Sleep(300);
                    autoit.Send("24339@p" + File.ReadAllText("../../config/up_data_password.txt"));
                    Thread.Sleep(300);
                    autoit.Send("{ENTER}");
                    Thread.Sleep(5000);
                    if (Convert.ToBoolean(File.ReadAllText("../../config/up_data_kill_chrome.txt"))) {
                        Process[] chrome = Process.GetProcessesByName("chrome");
                        for (int kkl = 0; kkl < chrome.Count(); kkl++) {
                            Process p = chrome[kkl];
                            p.Kill();
                        }
                    }
                    goto rrrrrrr;
                }
                if (Convert.ToBoolean(File.ReadAllText("../../config/up_data_kill_chrome.txt"))) {
                    Process[] chrome = Process.GetProcessesByName("chrome");
                    for (int kkl = 0; kkl < chrome.Count(); kkl++) {
                        Process p = chrome[kkl];
                        p.Kill();
                    }
                }
                if (!flag_retest) { flag_retest = true; System.Threading.Thread.Sleep(3000); goto rrrrrrr; }
            }
            return  mnm;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void label1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                label1.Capture = false;
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message msg = Message.Create(this.Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }
        private void ctms_kill_chrome_delay_Click(object sender, EventArgs e) {
            int asd = 0;
            while (true) {
                string input = Microsoft.VisualBasic.Interaction.InputBox("_ใส่ delay time (ms)", "delay time", ctms_kill_chrome_delay.Text, 500, 300);
                if (input == "") return;
                try {
                    asd = Convert.ToInt32(input);
                } catch {
                    MessageBox.Show("not format");
                    continue;
                }
                if (asd >= 0 && asd <= 100000) break;
                MessageBox.Show("_ใส่ค่าได้ตั้งแต่ 0 - 100000 เท่านั้น");
            }
            ctms_kill_chrome_delay.Text = asd.ToString();
            File.WriteAllText("../../config/up_data_kill_chrome_delay.txt", ctms_kill_chrome_delay.Text);
        }
        private void ctms_kill_chrome_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/up_data_kill_chrome.txt", ctms_kill_chrome.Checked.ToString());
        }
        private void ctms_password_Click(object sender, EventArgs e) {
            while (true) {
                string input = Microsoft.VisualBasic.Interaction.InputBox("_ใส่ Password (01 - 05)", "Password", ctms_password.Text, 500, 300);
                if (input == "") return;
                if (input != "01" && input != "02" && input != "03" && input != "04" && input != "05") {
                    MessageBox.Show("not format");
                    continue;
                }
                ctms_password.Text = input;
                break;
            }
            File.WriteAllText("../../config/up_data_password.txt", ctms_password.Text);
        }
        private void ctms_check_log_error_Click(object sender, EventArgs e) {
            List<string> file_data = new List<string>();
            try {
                string[] zxc = Directory.GetFiles(path_file_data_err);
                file_data = zxc.ToList<string>();
            } catch { }
            foreach (string hh in file_data) {
                string s = "";
                try { s = hh.Replace(path_file_data_err + "\\", ""); } catch { }
                File.Copy(hh, path_file_data + "\\" + s);
                File.Delete(hh);
            }
        }

        private void iotToolStripMenuItem_Click(object sender, EventArgs e) {
            ctms_iot.Checked = true;
            ctms_ip.Checked = false;
            url_string = "http://iot.teampcba.com:1880/PRISM/";
            File.WriteAllText("../../config/up_data_uri.txt", "iot");
        }
        private void ipToolStripMenuItem_Click(object sender, EventArgs e) {
            ctms_ip.Checked = true;
            ctms_iot.Checked = false;
            url_string = "http://192.168.11.15:1880/PRISM/";
            File.WriteAllText("../../config/up_data_uri.txt", "ip");
        }

        private void ctms_Skip_REST_Click(object sender, EventArgs e) {
            string vbgh = "หากเปิดใช้ ตรงส่วนของการ login จะต้องเป็น ID ที่มีอยู่ใน list เท่านั้น\r\n" +
                          "และต้อง set ในโปรแกรมเทส ให้ gen เป็นคำว่า DLL ออกมา";
            MessageBox.Show(vbgh);
            File.WriteAllText("../../config/up_data_skip_rest.txt", ctms_Skip_REST.Checked.ToString());
        }
        private void ctms_testAfterAsm_Click(object sender, EventArgs e) {
            File.WriteAllText("../../config/up_data_testAfterAsm.txt", ctms_testAfterAsm.Checked.ToString());
        }

        /// <summary>
        /// For log data error up prism
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="message"></param>
        public void LogUpPrismError(string sn, string cmd, string message) {
            string path = "D:\\LogError\\UpPrism";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DateTime now = DateTime.Now;
            StreamWriter swOut = new StreamWriter(path + "\\" + now.Year + "_" + now.Month + ".csv", true);
            string time = now.Day.ToString("00") + ":" + now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Second.ToString("00");
            swOut.WriteLine(time + ",SN=" + sn + ",CMD=" + cmd + ",Message=" + message);
            swOut.Close();
        }

        /// <summary>
        /// Set step fail in data format json
        /// </summary>
        /// <returns>Data Failure</returns>
        private string GetStepFail() {
            JsonConvertData jsonRead;
            try
            {
                jsonRead = JsonConvert.DeserializeObject<JsonConvertData>(up_prism_timeout_dataSummary);
            } catch
            {
                return "Json";
            }
            string result = jsonRead.Failure.ToString();
            result += "," + GetDeScripTion(jsonRead);
            //result += "," + GetValueFail(jsonRead);
            return result;
        }

        /// <summary>
        /// Get description of step fail
        /// </summary>
        /// <param name="jsonRead">is object read data format json</param>
        /// <returns>description of step fail in data ormat json</returns>
        private string GetDeScripTion(JsonConvertData jsonRead) {
            int rowData = jsonRead.ResultString.Count;
            string failure = jsonRead.Failure.ToString();
            for (int loop = 0; loop < rowData; loop++)
            {
                if (jsonRead.ResultString[loop].Step == failure)
                {
                    return jsonRead.ResultString[loop].Description;
                }
            }
            return string.Empty;
        }
    }

    public class mis_cmd {
        public static string GetEmployeeID = "GetEmployeeID";
        public static string GetTeamSN = "GetTeamSN";
        public static string GetFinalSN = "GetFinalSN";
        public static string GetNextStep = "GetNextStep";
        public static string GetPreviouseStep = "GetPreviouseStep";
        public static string GetFGMemeber = "GetFGMemeber";
        public static string GetWorkOrder = "GetWorkOrder";
        public static string AddAssambly = "AddAssambly";
        public static string AddMatchingCustomerSN = "AddMatchingCustomerSN";
        public static string AddTestResult = "AddTestResult";
    }

    /// <summary>
    /// Class for read data format json
    /// </summary>
    public class JsonConvertData {
        public string Date { get; set; }
        public string Time { get; set; }
        public string LoginID { get; set; }
        public string SWVersion { get; set; }
        public string FWVersion { get; set; }
        public string SpecVersion { get; set; }
        public string TestTime { get; set; }
        public string LoadInOut { get; set; }
        public string Mode { get; set; }
        public string FinalResult { get; set; }
        public string SN { get; set; }
        public object Failure { get; set; }
        public List<ResultString_> ResultString { get; set; }

        public JsonConvertData() {
            Date = string.Empty;
            Time = string.Empty;
            LoginID = string.Empty;
            SWVersion = string.Empty;
            FWVersion = string.Empty;
            SpecVersion = string.Empty;
            TestTime = string.Empty;
            LoadInOut = string.Empty;
            Mode = string.Empty;
            FinalResult = string.Empty;
            SN = string.Empty;
            Failure = string.Empty;
            ResultString = new List<ResultString_>();
        }
        public class ResultString_ {
            public string Step { get; set; }
            public string Description { get; set; }
            public string Tolerance { get; set; }
            public string Measured { get; set; }
            public string Result { get; set; }

            public ResultString_() {
                Step = string.Empty;
                Description = string.Empty;
                Tolerance = string.Empty;
                Measured = string.Empty;
                Result = string.Empty;
            }
        }
    }
}
