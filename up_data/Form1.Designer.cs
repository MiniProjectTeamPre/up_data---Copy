namespace up_data {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ctms_up_data = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_kill_chrome = new System.Windows.Forms.ToolStripMenuItem();
            this.delayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_kill_chrome_delay = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_password_ = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_password = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_check_log_error = new System.Windows.Forms.ToolStripMenuItem();
            this.uriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_iot = new System.Windows.Forms.ToolStripMenuItem();
            this.httpiotteampcbacom1880PRISMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_ip = new System.Windows.Forms.ToolStripMenuItem();
            this.http19216811151880PRISMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_Skip_REST = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_testAfterAsm = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.v202201ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.application_run = new System.Windows.Forms.Timer(this.components);
            this.timer_get_log_err = new System.Windows.Forms.Timer(this.components);
            this.ctms_up_data.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctms_up_data
            // 
            this.ctms_up_data.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.ctms_kill_chrome,
            this.ctms_password_,
            this.ctms_check_log_error,
            this.uriToolStripMenuItem,
            this.ctms_Skip_REST,
            this.ctms_testAfterAsm,
            this.versionToolStripMenuItem});
            this.ctms_up_data.Name = "ctms_up_data";
            this.ctms_up_data.Size = new System.Drawing.Size(181, 202);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // ctms_kill_chrome
            // 
            this.ctms_kill_chrome.Checked = true;
            this.ctms_kill_chrome.CheckOnClick = true;
            this.ctms_kill_chrome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctms_kill_chrome.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delayToolStripMenuItem});
            this.ctms_kill_chrome.Name = "ctms_kill_chrome";
            this.ctms_kill_chrome.Size = new System.Drawing.Size(180, 22);
            this.ctms_kill_chrome.Text = "Kill chrome";
            this.ctms_kill_chrome.Click += new System.EventHandler(this.ctms_kill_chrome_Click);
            // 
            // delayToolStripMenuItem
            // 
            this.delayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctms_kill_chrome_delay});
            this.delayToolStripMenuItem.Name = "delayToolStripMenuItem";
            this.delayToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.delayToolStripMenuItem.Text = "delay";
            // 
            // ctms_kill_chrome_delay
            // 
            this.ctms_kill_chrome_delay.Name = "ctms_kill_chrome_delay";
            this.ctms_kill_chrome_delay.Size = new System.Drawing.Size(98, 22);
            this.ctms_kill_chrome_delay.Text = "5000";
            this.ctms_kill_chrome_delay.Click += new System.EventHandler(this.ctms_kill_chrome_delay_Click);
            // 
            // ctms_password_
            // 
            this.ctms_password_.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctms_password});
            this.ctms_password_.Name = "ctms_password_";
            this.ctms_password_.Size = new System.Drawing.Size(180, 22);
            this.ctms_password_.Text = "Password";
            // 
            // ctms_password
            // 
            this.ctms_password.Name = "ctms_password";
            this.ctms_password.Size = new System.Drawing.Size(86, 22);
            this.ctms_password.Text = "01";
            this.ctms_password.Click += new System.EventHandler(this.ctms_password_Click);
            // 
            // ctms_check_log_error
            // 
            this.ctms_check_log_error.Name = "ctms_check_log_error";
            this.ctms_check_log_error.Size = new System.Drawing.Size(180, 22);
            this.ctms_check_log_error.Text = "Check log error";
            this.ctms_check_log_error.Click += new System.EventHandler(this.ctms_check_log_error_Click);
            // 
            // uriToolStripMenuItem
            // 
            this.uriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctms_iot,
            this.ctms_ip});
            this.uriToolStripMenuItem.Name = "uriToolStripMenuItem";
            this.uriToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uriToolStripMenuItem.Text = "url";
            // 
            // ctms_iot
            // 
            this.ctms_iot.Checked = true;
            this.ctms_iot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctms_iot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.httpiotteampcbacom1880PRISMToolStripMenuItem});
            this.ctms_iot.Name = "ctms_iot";
            this.ctms_iot.Size = new System.Drawing.Size(88, 22);
            this.ctms_iot.Text = "iot";
            this.ctms_iot.Click += new System.EventHandler(this.iotToolStripMenuItem_Click);
            // 
            // httpiotteampcbacom1880PRISMToolStripMenuItem
            // 
            this.httpiotteampcbacom1880PRISMToolStripMenuItem.Name = "httpiotteampcbacom1880PRISMToolStripMenuItem";
            this.httpiotteampcbacom1880PRISMToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.httpiotteampcbacom1880PRISMToolStripMenuItem.Text = "http://iot.teampcba.com:1880/PRISM/";
            // 
            // ctms_ip
            // 
            this.ctms_ip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.http19216811151880PRISMToolStripMenuItem});
            this.ctms_ip.Name = "ctms_ip";
            this.ctms_ip.Size = new System.Drawing.Size(88, 22);
            this.ctms_ip.Text = "ip";
            this.ctms_ip.Click += new System.EventHandler(this.ipToolStripMenuItem_Click);
            // 
            // http19216811151880PRISMToolStripMenuItem
            // 
            this.http19216811151880PRISMToolStripMenuItem.Name = "http19216811151880PRISMToolStripMenuItem";
            this.http19216811151880PRISMToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.http19216811151880PRISMToolStripMenuItem.Text = "http://192.168.11.15:1880/PRISM/";
            // 
            // ctms_Skip_REST
            // 
            this.ctms_Skip_REST.CheckOnClick = true;
            this.ctms_Skip_REST.Name = "ctms_Skip_REST";
            this.ctms_Skip_REST.Size = new System.Drawing.Size(180, 22);
            this.ctms_Skip_REST.Text = "Skip REST";
            this.ctms_Skip_REST.Click += new System.EventHandler(this.ctms_Skip_REST_Click);
            // 
            // ctms_testAfterAsm
            // 
            this.ctms_testAfterAsm.CheckOnClick = true;
            this.ctms_testAfterAsm.Name = "ctms_testAfterAsm";
            this.ctms_testAfterAsm.Size = new System.Drawing.Size(180, 22);
            this.ctms_testAfterAsm.Text = "TestAfterAsm";
            this.ctms_testAfterAsm.Click += new System.EventHandler(this.ctms_testAfterAsm_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.v202201ToolStripMenuItem});
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.versionToolStripMenuItem.Text = "Version";
            // 
            // v202201ToolStripMenuItem
            // 
            this.v202201ToolStripMenuItem.Name = "v202201ToolStripMenuItem";
            this.v202201ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.v202201ToolStripMenuItem.Text = "V2023.03";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ContextMenuStrip = this.ctms_up_data;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "PRISM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // application_run
            // 
            this.application_run.Enabled = true;
            this.application_run.Interval = 1000;
            this.application_run.Tick += new System.EventHandler(this.application_run_Tick);
            // 
            // timer_get_log_err
            // 
            this.timer_get_log_err.Enabled = true;
            this.timer_get_log_err.Interval = 30000;
            this.timer_get_log_err.Tick += new System.EventHandler(this.timer_get_log_err_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(130, 50);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "up data";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ctms_up_data.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ctms_up_data;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ctms_kill_chrome;
        private System.Windows.Forms.ToolStripMenuItem delayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctms_kill_chrome_delay;
        private System.Windows.Forms.ToolStripMenuItem ctms_password_;
        private System.Windows.Forms.ToolStripMenuItem ctms_password;
        private System.Windows.Forms.ToolStripMenuItem ctms_check_log_error;
        private System.Windows.Forms.Timer application_run;
        private System.Windows.Forms.Timer timer_get_log_err;
        private System.Windows.Forms.ToolStripMenuItem uriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctms_iot;
        private System.Windows.Forms.ToolStripMenuItem ctms_ip;
        private System.Windows.Forms.ToolStripMenuItem httpiotteampcbacom1880PRISMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem http19216811151880PRISMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctms_Skip_REST;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem v202201ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ctms_testAfterAsm;
    }
}

