﻿namespace PokeViewer.NET
{
    partial class MainViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainViewer));
            this.Connect = new System.Windows.Forms.Button();
            this.SwitchIP = new System.Windows.Forms.TextBox();
            this.View = new System.Windows.Forms.Button();
            this.ViewBox = new System.Windows.Forms.TextBox();
            this.PokeSprite = new System.Windows.Forms.PictureBox();
            this.Specialty = new System.Windows.Forms.PictureBox();
            this.OriginIcon = new System.Windows.Forms.PictureBox();
            this.Typing1 = new System.Windows.Forms.PictureBox();
            this.HidePIDEC = new System.Windows.Forms.CheckBox();
            this.Typing2 = new System.Windows.Forms.PictureBox();
            this.LiveStats = new System.Windows.Forms.TextBox();
            this.RefreshStats = new System.Windows.Forms.CheckBox();
            this.RefreshBox = new System.Windows.Forms.TextBox();
            this.Refresh = new System.Windows.Forms.Label();
            this.TodaysDate = new System.Windows.Forms.Label();
            this.WindowCapture = new System.Windows.Forms.Button();
            this.HpLabel = new System.Windows.Forms.Label();
            this.UniqueBox = new System.Windows.Forms.CheckBox();
            this.UniqueBox2 = new System.Windows.Forms.CheckBox();
            this.InGameScreenshot = new System.Windows.Forms.Button();
            this.ToggleSwitchProtocol = new System.Windows.Forms.CheckBox();
            this.ViewerControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ConnectionGroupBox = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ConnectionSpriteBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.PokeSprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Specialty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Typing1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Typing2)).BeginInit();
            this.ViewerControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ConnectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionSpriteBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(37, 57);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 25);
            this.Connect.TabIndex = 0;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // SwitchIP
            // 
            this.SwitchIP.Location = new System.Drawing.Point(37, 26);
            this.SwitchIP.Name = "SwitchIP";
            this.SwitchIP.Size = new System.Drawing.Size(128, 23);
            this.SwitchIP.TabIndex = 2;
            this.SwitchIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SwitchIP.TextChanged += new System.EventHandler(this.CheckForIP);
            // 
            // View
            // 
            this.View.Enabled = false;
            this.View.Location = new System.Drawing.Point(219, 264);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(47, 23);
            this.View.TabIndex = 3;
            this.View.Text = "View";
            this.View.UseVisualStyleBackColor = true;
            this.View.Visible = false;
            this.View.Click += new System.EventHandler(this.View_Click);
            // 
            // ViewBox
            // 
            this.ViewBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ViewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ViewBox.Location = new System.Drawing.Point(37, 88);
            this.ViewBox.Multiline = true;
            this.ViewBox.Name = "ViewBox";
            this.ViewBox.ReadOnly = true;
            this.ViewBox.Size = new System.Drawing.Size(182, 169);
            this.ViewBox.TabIndex = 4;
            this.ViewBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ViewBox.Visible = false;
            // 
            // PokeSprite
            // 
            this.PokeSprite.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.PokeSprite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PokeSprite.Location = new System.Drawing.Point(263, 88);
            this.PokeSprite.Name = "PokeSprite";
            this.PokeSprite.Size = new System.Drawing.Size(182, 169);
            this.PokeSprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PokeSprite.TabIndex = 7;
            this.PokeSprite.TabStop = false;
            // 
            // Specialty
            // 
            this.Specialty.Location = new System.Drawing.Point(219, 303);
            this.Specialty.Name = "Specialty";
            this.Specialty.Size = new System.Drawing.Size(47, 41);
            this.Specialty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Specialty.TabIndex = 8;
            this.Specialty.TabStop = false;
            // 
            // OriginIcon
            // 
            this.OriginIcon.ImageLocation = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/OriginMarks/icon_gene" +
    "ration_00%5Esb.png";
            this.OriginIcon.Location = new System.Drawing.Point(210, 18);
            this.OriginIcon.Name = "OriginIcon";
            this.OriginIcon.Size = new System.Drawing.Size(64, 64);
            this.OriginIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.OriginIcon.TabIndex = 10;
            this.OriginIcon.TabStop = false;
            // 
            // Typing1
            // 
            this.Typing1.Location = new System.Drawing.Point(376, 303);
            this.Typing1.Name = "Typing1";
            this.Typing1.Size = new System.Drawing.Size(69, 56);
            this.Typing1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Typing1.TabIndex = 12;
            this.Typing1.TabStop = false;
            // 
            // HidePIDEC
            // 
            this.HidePIDEC.AutoSize = true;
            this.HidePIDEC.Location = new System.Drawing.Point(37, 57);
            this.HidePIDEC.Name = "HidePIDEC";
            this.HidePIDEC.Size = new System.Drawing.Size(91, 19);
            this.HidePIDEC.TabIndex = 13;
            this.HidePIDEC.Text = "Hide PID/EC";
            this.HidePIDEC.UseVisualStyleBackColor = true;
            this.HidePIDEC.Visible = false;
            this.HidePIDEC.CheckedChanged += new System.EventHandler(this.CheckForHide);
            // 
            // Typing2
            // 
            this.Typing2.Location = new System.Drawing.Point(281, 303);
            this.Typing2.Name = "Typing2";
            this.Typing2.Size = new System.Drawing.Size(69, 56);
            this.Typing2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Typing2.TabIndex = 14;
            this.Typing2.TabStop = false;
            // 
            // LiveStats
            // 
            this.LiveStats.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.LiveStats.Location = new System.Drawing.Point(37, 294);
            this.LiveStats.Multiline = true;
            this.LiveStats.Name = "LiveStats";
            this.LiveStats.ReadOnly = true;
            this.LiveStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LiveStats.Size = new System.Drawing.Size(171, 98);
            this.LiveStats.TabIndex = 15;
            this.LiveStats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RefreshStats
            // 
            this.RefreshStats.AutoSize = true;
            this.RefreshStats.Location = new System.Drawing.Point(37, 267);
            this.RefreshStats.Name = "RefreshStats";
            this.RefreshStats.Size = new System.Drawing.Size(72, 19);
            this.RefreshStats.TabIndex = 16;
            this.RefreshStats.Text = "LiveStats";
            this.RefreshStats.UseVisualStyleBackColor = true;
            this.RefreshStats.CheckedChanged += new System.EventHandler(this.RefreshStats_CheckedChanged);
            // 
            // RefreshBox
            // 
            this.RefreshBox.Location = new System.Drawing.Point(120, 403);
            this.RefreshBox.Name = "RefreshBox";
            this.RefreshBox.Size = new System.Drawing.Size(71, 23);
            this.RefreshBox.TabIndex = 17;
            this.RefreshBox.Text = "2000";
            this.RefreshBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RefreshBox.Visible = false;
            // 
            // Refresh
            // 
            this.Refresh.AutoSize = true;
            this.Refresh.Location = new System.Drawing.Point(42, 407);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 15);
            this.Refresh.TabIndex = 18;
            this.Refresh.Text = "Refresh Rate:";
            this.Refresh.Visible = false;
            // 
            // TodaysDate
            // 
            this.TodaysDate.AutoSize = true;
            this.TodaysDate.Location = new System.Drawing.Point(281, 407);
            this.TodaysDate.Name = "TodaysDate";
            this.TodaysDate.Size = new System.Drawing.Size(57, 15);
            this.TodaysDate.TabIndex = 19;
            this.TodaysDate.Text = "DateTime";
            // 
            // WindowCapture
            // 
            this.WindowCapture.Image = ((System.Drawing.Image)(resources.GetObject("WindowCapture.Image")));
            this.WindowCapture.Location = new System.Drawing.Point(167, 57);
            this.WindowCapture.Name = "WindowCapture";
            this.WindowCapture.Size = new System.Drawing.Size(37, 25);
            this.WindowCapture.TabIndex = 20;
            this.WindowCapture.UseVisualStyleBackColor = true;
            this.WindowCapture.Click += new System.EventHandler(this.CaptureWindow_Click);
            // 
            // HpLabel
            // 
            this.HpLabel.AutoSize = true;
            this.HpLabel.Location = new System.Drawing.Point(124, 268);
            this.HpLabel.Name = "HpLabel";
            this.HpLabel.Size = new System.Drawing.Size(63, 15);
            this.HpLabel.TabIndex = 21;
            this.HpLabel.Text = "          HP%";
            // 
            // UniqueBox
            // 
            this.UniqueBox.AutoSize = true;
            this.UniqueBox.Location = new System.Drawing.Point(281, 276);
            this.UniqueBox.Name = "UniqueBox";
            this.UniqueBox.Size = new System.Drawing.Size(84, 19);
            this.UniqueBox.TabIndex = 22;
            this.UniqueBox.Text = "UniqueBox";
            this.UniqueBox.UseVisualStyleBackColor = true;
            // 
            // UniqueBox2
            // 
            this.UniqueBox2.AutoSize = true;
            this.UniqueBox2.Location = new System.Drawing.Point(355, 276);
            this.UniqueBox2.Name = "UniqueBox2";
            this.UniqueBox2.Size = new System.Drawing.Size(90, 19);
            this.UniqueBox2.TabIndex = 23;
            this.UniqueBox2.Text = "UniqueBox2";
            this.UniqueBox2.UseVisualStyleBackColor = true;
            // 
            // InGameScreenshot
            // 
            this.InGameScreenshot.Image = ((System.Drawing.Image)(resources.GetObject("InGameScreenshot.Image")));
            this.InGameScreenshot.Location = new System.Drawing.Point(280, 57);
            this.InGameScreenshot.Name = "InGameScreenshot";
            this.InGameScreenshot.Size = new System.Drawing.Size(37, 25);
            this.InGameScreenshot.TabIndex = 30;
            this.InGameScreenshot.UseVisualStyleBackColor = true;
            this.InGameScreenshot.Click += new System.EventHandler(this.InGameScreenshot_Click);
            // 
            // ToggleSwitchProtocol
            // 
            this.ToggleSwitchProtocol.AutoSize = true;
            this.ToggleSwitchProtocol.Enabled = false;
            this.ToggleSwitchProtocol.Location = new System.Drawing.Point(118, 61);
            this.ToggleSwitchProtocol.Name = "ToggleSwitchProtocol";
            this.ToggleSwitchProtocol.Size = new System.Drawing.Size(52, 19);
            this.ToggleSwitchProtocol.TabIndex = 32;
            this.ToggleSwitchProtocol.Text = "USB?";
            this.ToggleSwitchProtocol.UseVisualStyleBackColor = true;
            this.ToggleSwitchProtocol.CheckedChanged += new System.EventHandler(this.CheckForUSBChecked);
            // 
            // ViewerControl
            // 
            this.ViewerControl.Controls.Add(this.tabPage1);
            this.ViewerControl.Controls.Add(this.tabPage2);
            this.ViewerControl.Controls.Add(this.tabPage3);
            this.ViewerControl.Controls.Add(this.tabPage4);
            this.ViewerControl.Controls.Add(this.tabPage5);
            this.ViewerControl.Controls.Add(this.tabPage6);
            this.ViewerControl.Controls.Add(this.tabPage7);
            this.ViewerControl.Location = new System.Drawing.Point(0, 2);
            this.ViewerControl.Name = "ViewerControl";
            this.ViewerControl.SelectedIndex = 0;
            this.ViewerControl.Size = new System.Drawing.Size(511, 510);
            this.ViewerControl.TabIndex = 33;
            this.ViewerControl.SelectedIndexChanged += new System.EventHandler(this.ViewerControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.ConnectionGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(503, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connection 🔌";
            // 
            // ConnectionGroupBox
            // 
            this.ConnectionGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ConnectionGroupBox.Controls.Add(this.linkLabel1);
            this.ConnectionGroupBox.Controls.Add(this.VersionLabel);
            this.ConnectionGroupBox.Controls.Add(this.ConnectionSpriteBox);
            this.ConnectionGroupBox.Controls.Add(this.Connect);
            this.ConnectionGroupBox.Controls.Add(this.ToggleSwitchProtocol);
            this.ConnectionGroupBox.Controls.Add(this.SwitchIP);
            this.ConnectionGroupBox.Location = new System.Drawing.Point(40, 21);
            this.ConnectionGroupBox.Name = "ConnectionGroupBox";
            this.ConnectionGroupBox.Size = new System.Drawing.Size(200, 185);
            this.ConnectionGroupBox.TabIndex = 34;
            this.ConnectionGroupBox.TabStop = false;
            this.ConnectionGroupBox.Text = "Switch IP";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(8, 160);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 15);
            this.linkLabel1.TabIndex = 37;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(159, 10);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(37, 15);
            this.VersionLabel.TabIndex = 36;
            this.VersionLabel.Text = "v1.0.0";
            // 
            // ConnectionSpriteBox
            // 
            this.ConnectionSpriteBox.Location = new System.Drawing.Point(70, 90);
            this.ConnectionSpriteBox.Name = "ConnectionSpriteBox";
            this.ConnectionSpriteBox.Size = new System.Drawing.Size(64, 64);
            this.ConnectionSpriteBox.TabIndex = 35;
            this.ConnectionSpriteBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.OriginIcon);
            this.tabPage2.Controls.Add(this.InGameScreenshot);
            this.tabPage2.Controls.Add(this.Typing1);
            this.tabPage2.Controls.Add(this.Typing2);
            this.tabPage2.Controls.Add(this.View);
            this.tabPage2.Controls.Add(this.ViewBox);
            this.tabPage2.Controls.Add(this.PokeSprite);
            this.tabPage2.Controls.Add(this.Specialty);
            this.tabPage2.Controls.Add(this.UniqueBox2);
            this.tabPage2.Controls.Add(this.HidePIDEC);
            this.tabPage2.Controls.Add(this.UniqueBox);
            this.tabPage2.Controls.Add(this.LiveStats);
            this.tabPage2.Controls.Add(this.HpLabel);
            this.tabPage2.Controls.Add(this.RefreshStats);
            this.tabPage2.Controls.Add(this.WindowCapture);
            this.tabPage2.Controls.Add(this.RefreshBox);
            this.tabPage2.Controls.Add(this.TodaysDate);
            this.tabPage2.Controls.Add(this.Refresh);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(503, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View 🔎";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(503, 482);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Box 📦";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(503, 482);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Egg 🥚";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(503, 482);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Wide 🔭";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(503, 482);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "NPC 🤖";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(503, 482);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Raid 🎉";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // MainViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(290, 246);
            this.Controls.Add(this.ViewerControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainViewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PokeViewer.NET";
            this.Load += new System.EventHandler(this.PokeViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PokeSprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Specialty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Typing1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Typing2)).EndInit();
            this.ViewerControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ConnectionGroupBox.ResumeLayout(false);
            this.ConnectionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionSpriteBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button Connect;
        private TextBox SwitchIP;
        private Button View;
        private TextBox ViewBox;
        private PictureBox PokeSprite;
        private PictureBox Specialty;
        private PictureBox OriginIcon;
        private PictureBox Typing1;
        private CheckBox HidePIDEC;
        private PictureBox Typing2;
        private TextBox LiveStats;
        private CheckBox RefreshStats;
        private TextBox RefreshBox;
        private new Label Refresh;
        private Label TodaysDate;
        private Button WindowCapture;
        private Label HpLabel;
        private CheckBox UniqueBox;
        private CheckBox UniqueBox2;
        private Button InGameScreenshot;
        private CheckBox ToggleSwitchProtocol;
        private TabControl ViewerControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private GroupBox ConnectionGroupBox;
        private PictureBox ConnectionSpriteBox;
        private Label VersionLabel;
        private LinkLabel linkLabel1;
    }
}