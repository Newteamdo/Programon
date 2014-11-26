namespace ProgramonLauncher
{
    partial class frmLauncher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLauncher));
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.cbFullscreen = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.lblFullscreen = new System.Windows.Forms.Label();
            this.tbMasterVolume = new System.Windows.Forms.TrackBar();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbMasterVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Items.AddRange(new object[] {
            "800x600",
            "1024x768",
            "1152x864",
            "1280x720",
            "1280x960",
            "1280x1024",
            "1366x768",
            "1400x1050",
            "1600x900",
            "1600x1024",
            "1920x1080"});
            this.cbResolution.Location = new System.Drawing.Point(226, 139);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(170, 21);
            this.cbResolution.TabIndex = 3;
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFullscreen.FormattingEnabled = true;
            this.cbFullscreen.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.cbFullscreen.Location = new System.Drawing.Point(226, 189);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(170, 21);
            this.cbFullscreen.TabIndex = 4;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(51, 139);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(57, 13);
            this.lblResolution.TabIndex = 5;
            this.lblResolution.Text = "Resolution";
            // 
            // lblFullscreen
            // 
            this.lblFullscreen.AutoSize = true;
            this.lblFullscreen.Location = new System.Drawing.Point(51, 189);
            this.lblFullscreen.Name = "lblFullscreen";
            this.lblFullscreen.Size = new System.Drawing.Size(55, 13);
            this.lblFullscreen.TabIndex = 6;
            this.lblFullscreen.Text = "Fullscreen";
            // 
            // tbMasterVolume
            // 
            this.tbMasterVolume.Location = new System.Drawing.Point(226, 242);
            this.tbMasterVolume.Maximum = 100;
            this.tbMasterVolume.Name = "tbMasterVolume";
            this.tbMasterVolume.Size = new System.Drawing.Size(170, 45);
            this.tbMasterVolume.TabIndex = 7;
            this.tbMasterVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMasterVolume.Scroll += new System.EventHandler(this.tbMasterVolume_Scroll);
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.Location = new System.Drawing.Point(51, 242);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(77, 13);
            this.lblMaster.TabIndex = 8;
            this.lblMaster.Text = "Master Volume";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(292, 258);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(92, 13);
            this.lblVolume.TabIndex = 9;
            this.lblVolume.Text = "Ricky is awesome";
            // 
            // btnLaunch
            // 
            this.btnLaunch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnLaunch.FlatAppearance.BorderSize = 0;
            this.btnLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaunch.Location = new System.Drawing.Point(302, 306);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(94, 39);
            this.btnLaunch.TabIndex = 10;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = false;
            this.btnLaunch.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(54, 318);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(146, 17);
            this.cbClose.TabIndex = 11;
            this.cbClose.Text = "Close launcher on launch";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbLogo.BackgroundImage")));
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(448, 101);
            this.pbLogo.TabIndex = 13;
            this.pbLogo.TabStop = false;
            // 
            // frmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(446, 371);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.cbClose);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.lblMaster);
            this.Controls.Add(this.tbMasterVolume);
            this.Controls.Add(this.lblFullscreen);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.cbFullscreen);
            this.Controls.Add(this.cbResolution);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmLauncher";
            this.Text = "Programon Launcher 1.0";
            ((System.ComponentModel.ISupportInitialize)(this.tbMasterVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.ComboBox cbFullscreen;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.Label lblFullscreen;
        private System.Windows.Forms.TrackBar tbMasterVolume;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.CheckBox cbClose;
        private System.Windows.Forms.PictureBox pbLogo;

    }
}

