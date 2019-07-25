namespace Builder_Companion
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CPUMonitor = new System.Windows.Forms.Label();
            this.CPUSpeed = new System.Windows.Forms.Label();
            this.CPUTemp = new System.Windows.Forms.Label();
            this.CPUPwr = new System.Windows.Forms.Label();
            this.IgnoreTemp = new System.Windows.Forms.Button();
            this.TestInfo = new System.Windows.Forms.RichTextBox();
            this.WUP = new System.Windows.Forms.Label();
            this.GPULabel = new System.Windows.Forms.Label();
            this.GPUTempValue = new System.Windows.Forms.Label();
            this.GPUDriverLabel = new System.Windows.Forms.Label();
            this.RGBList = new System.Windows.Forms.CheckedListBox();
            this.RGBLabel = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.DMResync = new System.Windows.Forms.Button();
            this.TestDurationLabel = new System.Windows.Forms.Label();
            this.QCButton = new System.Windows.Forms.Button();
            this.RestartQCButton = new System.Windows.Forms.Button();
            this.AudioButton = new System.Windows.Forms.Button();
            this.RAMLabel = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.PrimeDurationBar = new System.Windows.Forms.TrackBar();
            this.BarDurationLabel = new System.Windows.Forms.Label();
            this.ManualHeavenButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PrimeDurationBar)).BeginInit();
            this.SuspendLayout();
            // 
            // CPUMonitor
            // 
            this.CPUMonitor.AutoSize = true;
            this.CPUMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUMonitor.Location = new System.Drawing.Point(698, 9);
            this.CPUMonitor.Name = "CPUMonitor";
            this.CPUMonitor.Size = new System.Drawing.Size(195, 20);
            this.CPUMonitor.TabIndex = 2;
            this.CPUMonitor.Text = "CPU Monitor (Max Values)";
            // 
            // CPUSpeed
            // 
            this.CPUSpeed.AutoSize = true;
            this.CPUSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUSpeed.Location = new System.Drawing.Point(699, 38);
            this.CPUSpeed.Name = "CPUSpeed";
            this.CPUSpeed.Size = new System.Drawing.Size(49, 17);
            this.CPUSpeed.TabIndex = 3;
            this.CPUSpeed.Text = "Speed";
            // 
            // CPUTemp
            // 
            this.CPUTemp.AutoSize = true;
            this.CPUTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUTemp.Location = new System.Drawing.Point(809, 38);
            this.CPUTemp.Name = "CPUTemp";
            this.CPUTemp.Size = new System.Drawing.Size(44, 17);
            this.CPUTemp.TabIndex = 4;
            this.CPUTemp.Text = "Temp";
            // 
            // CPUPwr
            // 
            this.CPUPwr.AutoSize = true;
            this.CPUPwr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUPwr.Location = new System.Drawing.Point(859, 38);
            this.CPUPwr.Name = "CPUPwr";
            this.CPUPwr.Size = new System.Drawing.Size(47, 17);
            this.CPUPwr.TabIndex = 5;
            this.CPUPwr.Text = "Power";
            // 
            // IgnoreTemp
            // 
            this.IgnoreTemp.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.IgnoreTemp.Location = new System.Drawing.Point(490, 73);
            this.IgnoreTemp.Name = "IgnoreTemp";
            this.IgnoreTemp.Size = new System.Drawing.Size(75, 23);
            this.IgnoreTemp.TabIndex = 6;
            this.IgnoreTemp.Text = "Ignore";
            this.IgnoreTemp.UseVisualStyleBackColor = false;
            this.IgnoreTemp.Visible = false;
            this.IgnoreTemp.Click += new System.EventHandler(this.IgnoreTemp_Click);
            // 
            // TestInfo
            // 
            this.TestInfo.BackColor = System.Drawing.SystemColors.Control;
            this.TestInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.TestInfo.Enabled = false;
            this.TestInfo.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestInfo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TestInfo.Location = new System.Drawing.Point(17, 9);
            this.TestInfo.Name = "TestInfo";
            this.TestInfo.ReadOnly = true;
            this.TestInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TestInfo.Size = new System.Drawing.Size(548, 275);
            this.TestInfo.TabIndex = 7;
            this.TestInfo.TabStop = false;
            this.TestInfo.Text = "";
            // 
            // WUP
            // 
            this.WUP.AutoSize = true;
            this.WUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WUP.Location = new System.Drawing.Point(383, 414);
            this.WUP.Name = "WUP";
            this.WUP.Size = new System.Drawing.Size(135, 20);
            this.WUP.TabIndex = 8;
            this.WUP.Text = "Windows Update";
            this.WUP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GPULabel
            // 
            this.GPULabel.AutoSize = true;
            this.GPULabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.GPULabel.Location = new System.Drawing.Point(699, 80);
            this.GPULabel.Name = "GPULabel";
            this.GPULabel.Size = new System.Drawing.Size(121, 17);
            this.GPULabel.TabIndex = 9;
            this.GPULabel.Text = "GPU Temp (Max):";
            // 
            // GPUTempValue
            // 
            this.GPUTempValue.AutoSize = true;
            this.GPUTempValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.GPUTempValue.Location = new System.Drawing.Point(884, 80);
            this.GPUTempValue.Name = "GPUTempValue";
            this.GPUTempValue.Size = new System.Drawing.Size(22, 17);
            this.GPUTempValue.TabIndex = 10;
            this.GPUTempValue.Text = "0°";
            // 
            // GPUDriverLabel
            // 
            this.GPUDriverLabel.AutoSize = true;
            this.GPUDriverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPUDriverLabel.Location = new System.Drawing.Point(716, 96);
            this.GPUDriverLabel.Name = "GPUDriverLabel";
            this.GPUDriverLabel.Size = new System.Drawing.Size(0, 16);
            this.GPUDriverLabel.TabIndex = 11;
            // 
            // RGBList
            // 
            this.RGBList.BackColor = System.Drawing.SystemColors.Control;
            this.RGBList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RGBList.CheckOnClick = true;
            this.RGBList.FormattingEnabled = true;
            this.RGBList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RGBList.Location = new System.Drawing.Point(702, 194);
            this.RGBList.Name = "RGBList";
            this.RGBList.Size = new System.Drawing.Size(204, 180);
            this.RGBList.TabIndex = 13;
            this.RGBList.SelectedIndexChanged += new System.EventHandler(this.RGBList_SelectedIndexChanged);
            // 
            // RGBLabel
            // 
            this.RGBLabel.BackColor = System.Drawing.SystemColors.Control;
            this.RGBLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RGBLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.RGBLabel.Enabled = false;
            this.RGBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGBLabel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.RGBLabel.Location = new System.Drawing.Point(702, 159);
            this.RGBLabel.Name = "RGBLabel";
            this.RGBLabel.ReadOnly = true;
            this.RGBLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RGBLabel.Size = new System.Drawing.Size(153, 29);
            this.RGBLabel.TabIndex = 15;
            this.RGBLabel.TabStop = false;
            this.RGBLabel.Text = "";
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(797, 386);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(113, 48);
            this.StartButton.TabIndex = 16;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // DMResync
            // 
            this.DMResync.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.DMResync.Location = new System.Drawing.Point(490, 9);
            this.DMResync.Name = "DMResync";
            this.DMResync.Size = new System.Drawing.Size(75, 23);
            this.DMResync.TabIndex = 12;
            this.DMResync.Text = "Resync";
            this.DMResync.UseVisualStyleBackColor = false;
            this.DMResync.Visible = false;
            this.DMResync.Click += new System.EventHandler(this.DMResync_Click);
            // 
            // TestDurationLabel
            // 
            this.TestDurationLabel.AutoSize = true;
            this.TestDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestDurationLabel.Location = new System.Drawing.Point(692, 371);
            this.TestDurationLabel.Name = "TestDurationLabel";
            this.TestDurationLabel.Size = new System.Drawing.Size(96, 16);
            this.TestDurationLabel.TabIndex = 18;
            this.TestDurationLabel.Text = "Prime Duration";
            // 
            // QCButton
            // 
            this.QCButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.QCButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QCButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QCButton.Location = new System.Drawing.Point(797, 386);
            this.QCButton.Name = "QCButton";
            this.QCButton.Size = new System.Drawing.Size(113, 48);
            this.QCButton.TabIndex = 19;
            this.QCButton.Text = "QC";
            this.QCButton.UseVisualStyleBackColor = false;
            this.QCButton.Visible = false;
            this.QCButton.Click += new System.EventHandler(this.QCButton_Click);
            // 
            // RestartQCButton
            // 
            this.RestartQCButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.RestartQCButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RestartQCButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestartQCButton.Location = new System.Drawing.Point(797, 387);
            this.RestartQCButton.Name = "RestartQCButton";
            this.RestartQCButton.Size = new System.Drawing.Size(113, 48);
            this.RestartQCButton.TabIndex = 20;
            this.RestartQCButton.Text = "Restart (QC)";
            this.RestartQCButton.UseVisualStyleBackColor = false;
            this.RestartQCButton.Visible = false;
            this.RestartQCButton.Click += new System.EventHandler(this.RestartQCButton_Click);
            // 
            // AudioButton
            // 
            this.AudioButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.AudioButton.Location = new System.Drawing.Point(490, 123);
            this.AudioButton.Name = "AudioButton";
            this.AudioButton.Size = new System.Drawing.Size(75, 23);
            this.AudioButton.TabIndex = 21;
            this.AudioButton.Text = "Audio OK";
            this.AudioButton.UseVisualStyleBackColor = false;
            this.AudioButton.Visible = false;
            this.AudioButton.Click += new System.EventHandler(this.AudioButton_click);
            // 
            // RAMLabel
            // 
            this.RAMLabel.AutoSize = true;
            this.RAMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.RAMLabel.Location = new System.Drawing.Point(699, 123);
            this.RAMLabel.Name = "RAMLabel";
            this.RAMLabel.Size = new System.Drawing.Size(38, 17);
            this.RAMLabel.TabIndex = 22;
            this.RAMLabel.Text = "RAM";
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.AutoSize = true;
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseLabel.Location = new System.Drawing.Point(12, 415);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(56, 20);
            this.PhaseLabel.TabIndex = 23;
            this.PhaseLabel.Text = "Phase";
            this.PhaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrimeDurationBar
            // 
            this.PrimeDurationBar.Location = new System.Drawing.Point(687, 390);
            this.PrimeDurationBar.Maximum = 30;
            this.PrimeDurationBar.Minimum = 8;
            this.PrimeDurationBar.Name = "PrimeDurationBar";
            this.PrimeDurationBar.Size = new System.Drawing.Size(104, 45);
            this.PrimeDurationBar.TabIndex = 24;
            this.PrimeDurationBar.Value = 30;
            this.PrimeDurationBar.ValueChanged += new System.EventHandler(this.PrimeDurationBar_onChanged);
            // 
            // BarDurationLabel
            // 
            this.BarDurationLabel.AutoSize = true;
            this.BarDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarDurationLabel.Location = new System.Drawing.Point(716, 419);
            this.BarDurationLabel.Name = "BarDurationLabel";
            this.BarDurationLabel.Size = new System.Drawing.Size(43, 16);
            this.BarDurationLabel.TabIndex = 25;
            this.BarDurationLabel.Text = "30min";
            // 
            // ManualHeavenButton
            // 
            this.ManualHeavenButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ManualHeavenButton.Location = new System.Drawing.Point(490, 123);
            this.ManualHeavenButton.Name = "ManualHeavenButton";
            this.ManualHeavenButton.Size = new System.Drawing.Size(75, 23);
            this.ManualHeavenButton.TabIndex = 26;
            this.ManualHeavenButton.Text = "Heaven OK";
            this.ManualHeavenButton.UseVisualStyleBackColor = false;
            this.ManualHeavenButton.Visible = false;
            this.ManualHeavenButton.Click += new System.EventHandler(this.ManualHeavenButton_onClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(922, 443);
            this.Controls.Add(this.ManualHeavenButton);
            this.Controls.Add(this.BarDurationLabel);
            this.Controls.Add(this.PrimeDurationBar);
            this.Controls.Add(this.PhaseLabel);
            this.Controls.Add(this.IgnoreTemp);
            this.Controls.Add(this.RAMLabel);
            this.Controls.Add(this.AudioButton);
            this.Controls.Add(this.RestartQCButton);
            this.Controls.Add(this.QCButton);
            this.Controls.Add(this.TestDurationLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.RGBLabel);
            this.Controls.Add(this.RGBList);
            this.Controls.Add(this.DMResync);
            this.Controls.Add(this.GPUDriverLabel);
            this.Controls.Add(this.GPUTempValue);
            this.Controls.Add(this.GPULabel);
            this.Controls.Add(this.WUP);
            this.Controls.Add(this.TestInfo);
            this.Controls.Add(this.CPUPwr);
            this.Controls.Add(this.CPUTemp);
            this.Controls.Add(this.CPUSpeed);
            this.Controls.Add(this.CPUMonitor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Builder Companion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Loaded);
            ((System.ComponentModel.ISupportInitialize)(this.PrimeDurationBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CPUMonitor;
        private System.Windows.Forms.Label CPUSpeed;
        private System.Windows.Forms.Label CPUTemp;
        private System.Windows.Forms.Label CPUPwr;
        private System.Windows.Forms.Button IgnoreTemp;
        private System.Windows.Forms.RichTextBox TestInfo;
        private System.Windows.Forms.Label WUP;
        private System.Windows.Forms.Label GPULabel;
        private System.Windows.Forms.Label GPUTempValue;
        private System.Windows.Forms.Label GPUDriverLabel;
        private System.Windows.Forms.CheckedListBox RGBList;
        private System.Windows.Forms.RichTextBox RGBLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button DMResync;
        private System.Windows.Forms.Label TestDurationLabel;
        private System.Windows.Forms.Button QCButton;
        private System.Windows.Forms.Button RestartQCButton;
        private System.Windows.Forms.Button AudioButton;
        private System.Windows.Forms.Label RAMLabel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.TrackBar PrimeDurationBar;
        private System.Windows.Forms.Label BarDurationLabel;
        private System.Windows.Forms.Button ManualHeavenButton;
    }
}

