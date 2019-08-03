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
            this.WUP = new System.Windows.Forms.Label();
            this.GPULabel = new System.Windows.Forms.Label();
            this.GPUTempValue = new System.Windows.Forms.Label();
            this.GPUDriverLabel = new System.Windows.Forms.Label();
            this.RGBList = new System.Windows.Forms.CheckedListBox();
            this.RGBLabel = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.TestDurationLabel = new System.Windows.Forms.Label();
            this.QCButton = new System.Windows.Forms.Button();
            this.RestartQCButton = new System.Windows.Forms.Button();
            this.RAMLabel = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.PrimeDurationBar = new System.Windows.Forms.TrackBar();
            this.BarDurationLabel = new System.Windows.Forms.Label();
            this.DMLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DMResyncButton = new System.Windows.Forms.Button();
            this.InfoTable = new System.Windows.Forms.TableLayoutPanel();
            this.PowerModeLabel = new System.Windows.Forms.Label();
            this.TestLabel1 = new System.Windows.Forms.Label();
            this.PwrCheck = new System.Windows.Forms.PictureBox();
            this.DMCheck = new System.Windows.Forms.PictureBox();
            this.DMError = new System.Windows.Forms.PictureBox();
            this.StressCheck = new System.Windows.Forms.PictureBox();
            this.StressError = new System.Windows.Forms.PictureBox();
            this.OverheatingFlame = new System.Windows.Forms.PictureBox();
            this.StressEggs = new System.Windows.Forms.PictureBox();
            this.TestLabel2 = new System.Windows.Forms.Label();
            this.TestLabel3 = new System.Windows.Forms.Label();
            this.TestLabel4 = new System.Windows.Forms.Label();
            this.TestLabel5 = new System.Windows.Forms.Label();
            this.IgnoreTempButton = new System.Windows.Forms.Button();
            this.HeavenError = new System.Windows.Forms.PictureBox();
            this.HeavenIcon = new System.Windows.Forms.PictureBox();
            this.ManualHeavenButton = new System.Windows.Forms.Button();
            this.AudioButton = new System.Windows.Forms.Button();
            this.AudioCheck = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PrimeDurationBar)).BeginInit();
            this.InfoTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PwrCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DMCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DMError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverheatingFlame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressEggs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeavenError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeavenIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AudioCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // CPUMonitor
            // 
            this.CPUMonitor.AutoSize = true;
            this.CPUMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUMonitor.Location = new System.Drawing.Point(1029, 9);
            this.CPUMonitor.Name = "CPUMonitor";
            this.CPUMonitor.Size = new System.Drawing.Size(195, 20);
            this.CPUMonitor.TabIndex = 2;
            this.CPUMonitor.Text = "CPU Monitor (Max Values)";
            // 
            // CPUSpeed
            // 
            this.CPUSpeed.AutoSize = true;
            this.CPUSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUSpeed.Location = new System.Drawing.Point(1020, 38);
            this.CPUSpeed.Name = "CPUSpeed";
            this.CPUSpeed.Size = new System.Drawing.Size(49, 17);
            this.CPUSpeed.TabIndex = 3;
            this.CPUSpeed.Text = "Speed";
            // 
            // CPUTemp
            // 
            this.CPUTemp.AutoSize = true;
            this.CPUTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUTemp.Location = new System.Drawing.Point(1090, 38);
            this.CPUTemp.Name = "CPUTemp";
            this.CPUTemp.Size = new System.Drawing.Size(44, 17);
            this.CPUTemp.TabIndex = 4;
            this.CPUTemp.Text = "Temp";
            // 
            // CPUPwr
            // 
            this.CPUPwr.AutoSize = true;
            this.CPUPwr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUPwr.Location = new System.Drawing.Point(1140, 38);
            this.CPUPwr.Name = "CPUPwr";
            this.CPUPwr.Size = new System.Drawing.Size(47, 17);
            this.CPUPwr.TabIndex = 5;
            this.CPUPwr.Text = "Power";
            // 
            // WUP
            // 
            this.WUP.AutoSize = true;
            this.WUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WUP.Location = new System.Drawing.Point(554, 616);
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
            this.GPULabel.Location = new System.Drawing.Point(1075, 80);
            this.GPULabel.Name = "GPULabel";
            this.GPULabel.Size = new System.Drawing.Size(121, 17);
            this.GPULabel.TabIndex = 9;
            this.GPULabel.Text = "GPU Temp (Max):";
            // 
            // GPUTempValue
            // 
            this.GPUTempValue.AutoSize = true;
            this.GPUTempValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.GPUTempValue.Location = new System.Drawing.Point(1202, 74);
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
            this.RGBList.Location = new System.Drawing.Point(992, 212);
            this.RGBList.Name = "RGBList";
            this.RGBList.Size = new System.Drawing.Size(204, 165);
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
            this.RGBLabel.Location = new System.Drawing.Point(992, 177);
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
            this.StartButton.Location = new System.Drawing.Point(921, 443);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(113, 48);
            this.StartButton.TabIndex = 16;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // TestDurationLabel
            // 
            this.TestDurationLabel.AutoSize = true;
            this.TestDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestDurationLabel.Location = new System.Drawing.Point(1000, 549);
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
            this.QCButton.Location = new System.Drawing.Point(841, 531);
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
            this.RestartQCButton.Location = new System.Drawing.Point(1078, 443);
            this.RestartQCButton.Name = "RestartQCButton";
            this.RestartQCButton.Size = new System.Drawing.Size(113, 48);
            this.RestartQCButton.TabIndex = 20;
            this.RestartQCButton.Text = "Restart (QC)";
            this.RestartQCButton.UseVisualStyleBackColor = false;
            this.RestartQCButton.Visible = false;
            this.RestartQCButton.Click += new System.EventHandler(this.RestartQCButton_Click);
            // 
            // RAMLabel
            // 
            this.RAMLabel.AutoSize = true;
            this.RAMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.RAMLabel.Location = new System.Drawing.Point(1140, 123);
            this.RAMLabel.Name = "RAMLabel";
            this.RAMLabel.Size = new System.Drawing.Size(38, 17);
            this.RAMLabel.TabIndex = 22;
            this.RAMLabel.Text = "RAM";
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.AutoSize = true;
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhaseLabel.Location = new System.Drawing.Point(67, 652);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(56, 20);
            this.PhaseLabel.TabIndex = 23;
            this.PhaseLabel.Text = "Phase";
            this.PhaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrimeDurationBar
            // 
            this.PrimeDurationBar.BackColor = System.Drawing.Color.White;
            this.PrimeDurationBar.Location = new System.Drawing.Point(992, 568);
            this.PrimeDurationBar.Maximum = 30;
            this.PrimeDurationBar.Minimum = 1;
            this.PrimeDurationBar.Name = "PrimeDurationBar";
            this.PrimeDurationBar.Size = new System.Drawing.Size(104, 45);
            this.PrimeDurationBar.TabIndex = 24;
            this.PrimeDurationBar.Value = 30;
            this.PrimeDurationBar.ValueChanged += new System.EventHandler(this.PrimeDurationBar_onChanged);
            // 
            // BarDurationLabel
            // 
            this.BarDurationLabel.AutoSize = true;
            this.BarDurationLabel.BackColor = System.Drawing.Color.White;
            this.BarDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarDurationLabel.Location = new System.Drawing.Point(1020, 597);
            this.BarDurationLabel.Name = "BarDurationLabel";
            this.BarDurationLabel.Size = new System.Drawing.Size(43, 16);
            this.BarDurationLabel.TabIndex = 25;
            this.BarDurationLabel.Text = "30min";
            // 
            // DMLabel
            // 
            this.DMLabel.AutoSize = true;
            this.DMLabel.BackColor = System.Drawing.Color.Transparent;
            this.DMLabel.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DMLabel.ForeColor = System.Drawing.Color.White;
            this.DMLabel.Location = new System.Drawing.Point(3, 0);
            this.DMLabel.Name = "DMLabel";
            this.DMLabel.Size = new System.Drawing.Size(411, 57);
            this.DMLabel.TabIndex = 27;
            this.DMLabel.Text = "Device Manager OK";
            this.DMLabel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(70, 597);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(53, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 29;
            // 
            // DMResyncButton
            // 
            this.DMResyncButton.BackColor = System.Drawing.Color.Transparent;
            this.DMResyncButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DMResyncButton.BackgroundImage")));
            this.DMResyncButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DMResyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DMResyncButton.Font = new System.Drawing.Font("Architects Daughter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DMResyncButton.ForeColor = System.Drawing.Color.White;
            this.DMResyncButton.Location = new System.Drawing.Point(623, 3);
            this.DMResyncButton.Name = "DMResyncButton";
            this.DMResyncButton.Size = new System.Drawing.Size(68, 54);
            this.DMResyncButton.TabIndex = 12;
            this.DMResyncButton.UseVisualStyleBackColor = false;
            this.DMResyncButton.Visible = false;
            this.DMResyncButton.Click += new System.EventHandler(this.DMResync_Click);
            // 
            // InfoTable
            // 
            this.InfoTable.BackColor = System.Drawing.Color.Transparent;
            this.InfoTable.ColumnCount = 2;
            this.InfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.InfoTable.Controls.Add(this.DMResyncButton, 1, 0);
            this.InfoTable.Controls.Add(this.PowerModeLabel, 0, 1);
            this.InfoTable.Controls.Add(this.DMLabel, 0, 0);
            this.InfoTable.Controls.Add(this.TestLabel1, 0, 2);
            this.InfoTable.Controls.Add(this.TestLabel3, 0, 4);
            this.InfoTable.Controls.Add(this.TestLabel4, 0, 5);
            this.InfoTable.Controls.Add(this.TestLabel5, 0, 6);
            this.InfoTable.Controls.Add(this.TestLabel2, 0, 3);
            this.InfoTable.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.InfoTable.Location = new System.Drawing.Point(71, 12);
            this.InfoTable.Name = "InfoTable";
            this.InfoTable.RowCount = 7;
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.InfoTable.Size = new System.Drawing.Size(694, 439);
            this.InfoTable.TabIndex = 30;
            // 
            // PowerModeLabel
            // 
            this.PowerModeLabel.AutoSize = true;
            this.PowerModeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PowerModeLabel.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerModeLabel.ForeColor = System.Drawing.Color.White;
            this.PowerModeLabel.Location = new System.Drawing.Point(3, 60);
            this.PowerModeLabel.Name = "PowerModeLabel";
            this.PowerModeLabel.Size = new System.Drawing.Size(613, 57);
            this.PowerModeLabel.TabIndex = 29;
            this.PowerModeLabel.Text = "Power Mode -> Performance";
            this.PowerModeLabel.Visible = false;
            // 
            // TestLabel1
            // 
            this.TestLabel1.AutoSize = true;
            this.TestLabel1.BackColor = System.Drawing.Color.Transparent;
            this.TestLabel1.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel1.ForeColor = System.Drawing.Color.White;
            this.TestLabel1.Location = new System.Drawing.Point(3, 120);
            this.TestLabel1.Name = "TestLabel1";
            this.TestLabel1.Size = new System.Drawing.Size(0, 57);
            this.TestLabel1.TabIndex = 30;
            // 
            // PwrCheck
            // 
            this.PwrCheck.BackColor = System.Drawing.Color.Transparent;
            this.PwrCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PwrCheck.BackgroundImage")));
            this.PwrCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PwrCheck.Location = new System.Drawing.Point(3, 68);
            this.PwrCheck.Name = "PwrCheck";
            this.PwrCheck.Size = new System.Drawing.Size(65, 53);
            this.PwrCheck.TabIndex = 30;
            this.PwrCheck.TabStop = false;
            this.PwrCheck.Visible = false;
            // 
            // DMCheck
            // 
            this.DMCheck.BackColor = System.Drawing.Color.Transparent;
            this.DMCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DMCheck.BackgroundImage")));
            this.DMCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DMCheck.ErrorImage = null;
            this.DMCheck.Location = new System.Drawing.Point(3, 9);
            this.DMCheck.Name = "DMCheck";
            this.DMCheck.Size = new System.Drawing.Size(65, 53);
            this.DMCheck.TabIndex = 32;
            this.DMCheck.TabStop = false;
            this.DMCheck.Visible = false;
            // 
            // DMError
            // 
            this.DMError.BackColor = System.Drawing.Color.Transparent;
            this.DMError.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DMError.BackgroundImage")));
            this.DMError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DMError.ErrorImage = null;
            this.DMError.Location = new System.Drawing.Point(3, 9);
            this.DMError.Name = "DMError";
            this.DMError.Size = new System.Drawing.Size(65, 53);
            this.DMError.TabIndex = 33;
            this.DMError.TabStop = false;
            this.DMError.Visible = false;
            // 
            // StressCheck
            // 
            this.StressCheck.BackColor = System.Drawing.Color.Transparent;
            this.StressCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StressCheck.BackgroundImage")));
            this.StressCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StressCheck.Location = new System.Drawing.Point(3, 132);
            this.StressCheck.Name = "StressCheck";
            this.StressCheck.Size = new System.Drawing.Size(65, 53);
            this.StressCheck.TabIndex = 34;
            this.StressCheck.TabStop = false;
            this.StressCheck.Visible = false;
            // 
            // StressError
            // 
            this.StressError.BackColor = System.Drawing.Color.Transparent;
            this.StressError.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StressError.BackgroundImage")));
            this.StressError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StressError.ErrorImage = null;
            this.StressError.Location = new System.Drawing.Point(3, 132);
            this.StressError.Name = "StressError";
            this.StressError.Size = new System.Drawing.Size(65, 53);
            this.StressError.TabIndex = 35;
            this.StressError.TabStop = false;
            this.StressError.Visible = false;
            // 
            // OverheatingFlame
            // 
            this.OverheatingFlame.BackColor = System.Drawing.Color.Transparent;
            this.OverheatingFlame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OverheatingFlame.BackgroundImage")));
            this.OverheatingFlame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OverheatingFlame.ErrorImage = null;
            this.OverheatingFlame.Location = new System.Drawing.Point(3, 134);
            this.OverheatingFlame.Name = "OverheatingFlame";
            this.OverheatingFlame.Size = new System.Drawing.Size(65, 53);
            this.OverheatingFlame.TabIndex = 36;
            this.OverheatingFlame.TabStop = false;
            this.OverheatingFlame.Visible = false;
            // 
            // StressEggs
            // 
            this.StressEggs.BackColor = System.Drawing.Color.Transparent;
            this.StressEggs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StressEggs.BackgroundImage")));
            this.StressEggs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StressEggs.ErrorImage = null;
            this.StressEggs.Location = new System.Drawing.Point(0, 132);
            this.StressEggs.Name = "StressEggs";
            this.StressEggs.Size = new System.Drawing.Size(65, 53);
            this.StressEggs.TabIndex = 37;
            this.StressEggs.TabStop = false;
            this.StressEggs.Visible = false;
            // 
            // TestLabel2
            // 
            this.TestLabel2.AutoSize = true;
            this.TestLabel2.BackColor = System.Drawing.Color.Transparent;
            this.TestLabel2.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel2.ForeColor = System.Drawing.Color.White;
            this.TestLabel2.Location = new System.Drawing.Point(3, 180);
            this.TestLabel2.Name = "TestLabel2";
            this.TestLabel2.Size = new System.Drawing.Size(0, 57);
            this.TestLabel2.TabIndex = 31;
            // 
            // TestLabel3
            // 
            this.TestLabel3.AutoSize = true;
            this.TestLabel3.BackColor = System.Drawing.Color.Transparent;
            this.TestLabel3.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel3.ForeColor = System.Drawing.Color.White;
            this.TestLabel3.Location = new System.Drawing.Point(3, 240);
            this.TestLabel3.Name = "TestLabel3";
            this.TestLabel3.Size = new System.Drawing.Size(0, 57);
            this.TestLabel3.TabIndex = 32;
            // 
            // TestLabel4
            // 
            this.TestLabel4.AutoSize = true;
            this.TestLabel4.BackColor = System.Drawing.Color.Transparent;
            this.TestLabel4.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel4.ForeColor = System.Drawing.Color.White;
            this.TestLabel4.Location = new System.Drawing.Point(3, 300);
            this.TestLabel4.Name = "TestLabel4";
            this.TestLabel4.Size = new System.Drawing.Size(0, 57);
            this.TestLabel4.TabIndex = 33;
            // 
            // TestLabel5
            // 
            this.TestLabel5.AutoSize = true;
            this.TestLabel5.BackColor = System.Drawing.Color.Transparent;
            this.TestLabel5.Font = new System.Drawing.Font("Architects Daughter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestLabel5.ForeColor = System.Drawing.Color.White;
            this.TestLabel5.Location = new System.Drawing.Point(3, 360);
            this.TestLabel5.Name = "TestLabel5";
            this.TestLabel5.Size = new System.Drawing.Size(0, 57);
            this.TestLabel5.TabIndex = 34;
            // 
            // IgnoreTempButton
            // 
            this.IgnoreTempButton.BackColor = System.Drawing.Color.Transparent;
            this.IgnoreTempButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("IgnoreTempButton.BackgroundImage")));
            this.IgnoreTempButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.IgnoreTempButton.FlatAppearance.BorderSize = 0;
            this.IgnoreTempButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IgnoreTempButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IgnoreTempButton.Font = new System.Drawing.Font("Architects Daughter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IgnoreTempButton.ForeColor = System.Drawing.Color.White;
            this.IgnoreTempButton.Location = new System.Drawing.Point(799, 195);
            this.IgnoreTempButton.Name = "IgnoreTempButton";
            this.IgnoreTempButton.Size = new System.Drawing.Size(68, 54);
            this.IgnoreTempButton.TabIndex = 35;
            this.IgnoreTempButton.UseVisualStyleBackColor = false;
            this.IgnoreTempButton.Visible = false;
            this.IgnoreTempButton.Click += new System.EventHandler(this.IgnoreTemp_Click);
            // 
            // HeavenError
            // 
            this.HeavenError.BackColor = System.Drawing.Color.Transparent;
            this.HeavenError.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HeavenError.BackgroundImage")));
            this.HeavenError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HeavenError.ErrorImage = null;
            this.HeavenError.Location = new System.Drawing.Point(0, 132);
            this.HeavenError.Name = "HeavenError";
            this.HeavenError.Size = new System.Drawing.Size(65, 53);
            this.HeavenError.TabIndex = 38;
            this.HeavenError.TabStop = false;
            this.HeavenError.Visible = false;
            // 
            // HeavenIcon
            // 
            this.HeavenIcon.BackColor = System.Drawing.Color.Transparent;
            this.HeavenIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HeavenIcon.BackgroundImage")));
            this.HeavenIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HeavenIcon.ErrorImage = null;
            this.HeavenIcon.Location = new System.Drawing.Point(0, 132);
            this.HeavenIcon.Name = "HeavenIcon";
            this.HeavenIcon.Size = new System.Drawing.Size(65, 53);
            this.HeavenIcon.TabIndex = 39;
            this.HeavenIcon.TabStop = false;
            this.HeavenIcon.Visible = false;
            // 
            // ManualHeavenButton
            // 
            this.ManualHeavenButton.BackColor = System.Drawing.Color.Transparent;
            this.ManualHeavenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ManualHeavenButton.BackgroundImage")));
            this.ManualHeavenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ManualHeavenButton.FlatAppearance.BorderSize = 0;
            this.ManualHeavenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ManualHeavenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManualHeavenButton.Font = new System.Drawing.Font("Architects Daughter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualHeavenButton.ForeColor = System.Drawing.Color.White;
            this.ManualHeavenButton.Location = new System.Drawing.Point(799, 269);
            this.ManualHeavenButton.Name = "ManualHeavenButton";
            this.ManualHeavenButton.Size = new System.Drawing.Size(68, 54);
            this.ManualHeavenButton.TabIndex = 36;
            this.ManualHeavenButton.UseVisualStyleBackColor = false;
            this.ManualHeavenButton.Visible = false;
            this.ManualHeavenButton.Click += new System.EventHandler(this.ManualHeavenButton_onClick);
            // 
            // AudioButton
            // 
            this.AudioButton.BackColor = System.Drawing.Color.Transparent;
            this.AudioButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AudioButton.BackgroundImage")));
            this.AudioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AudioButton.FlatAppearance.BorderSize = 0;
            this.AudioButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AudioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AudioButton.Font = new System.Drawing.Font("Architects Daughter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioButton.ForeColor = System.Drawing.Color.White;
            this.AudioButton.Location = new System.Drawing.Point(812, 352);
            this.AudioButton.Name = "AudioButton";
            this.AudioButton.Size = new System.Drawing.Size(68, 54);
            this.AudioButton.TabIndex = 40;
            this.AudioButton.UseVisualStyleBackColor = false;
            this.AudioButton.Visible = false;
            this.AudioButton.Click += new System.EventHandler(this.AudioButton_click);
            // 
            // AudioCheck
            // 
            this.AudioCheck.BackColor = System.Drawing.Color.Transparent;
            this.AudioCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AudioCheck.BackgroundImage")));
            this.AudioCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AudioCheck.Location = new System.Drawing.Point(0, 68);
            this.AudioCheck.Name = "AudioCheck";
            this.AudioCheck.Size = new System.Drawing.Size(65, 53);
            this.AudioCheck.TabIndex = 41;
            this.AudioCheck.TabStop = false;
            this.AudioCheck.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.AudioCheck);
            this.Controls.Add(this.AudioButton);
            this.Controls.Add(this.HeavenIcon);
            this.Controls.Add(this.HeavenError);
            this.Controls.Add(this.StressEggs);
            this.Controls.Add(this.OverheatingFlame);
            this.Controls.Add(this.StressError);
            this.Controls.Add(this.StressCheck);
            this.Controls.Add(this.DMError);
            this.Controls.Add(this.DMCheck);
            this.Controls.Add(this.ManualHeavenButton);
            this.Controls.Add(this.IgnoreTempButton);
            this.Controls.Add(this.InfoTable);
            this.Controls.Add(this.PwrCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BarDurationLabel);
            this.Controls.Add(this.PrimeDurationBar);
            this.Controls.Add(this.PhaseLabel);
            this.Controls.Add(this.RAMLabel);
            this.Controls.Add(this.RestartQCButton);
            this.Controls.Add(this.QCButton);
            this.Controls.Add(this.TestDurationLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.RGBLabel);
            this.Controls.Add(this.RGBList);
            this.Controls.Add(this.GPUDriverLabel);
            this.Controls.Add(this.GPUTempValue);
            this.Controls.Add(this.GPULabel);
            this.Controls.Add(this.WUP);
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
            this.InfoTable.ResumeLayout(false);
            this.InfoTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PwrCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DMCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DMError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverheatingFlame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StressEggs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeavenError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeavenIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AudioCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CPUMonitor;
        private System.Windows.Forms.Label CPUSpeed;
        private System.Windows.Forms.Label CPUTemp;
        private System.Windows.Forms.Label CPUPwr;
        private System.Windows.Forms.Label WUP;
        private System.Windows.Forms.Label GPULabel;
        private System.Windows.Forms.Label GPUTempValue;
        private System.Windows.Forms.Label GPUDriverLabel;
        private System.Windows.Forms.CheckedListBox RGBList;
        private System.Windows.Forms.RichTextBox RGBLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label TestDurationLabel;
        private System.Windows.Forms.Button QCButton;
        private System.Windows.Forms.Button RestartQCButton;
        private System.Windows.Forms.Label RAMLabel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.TrackBar PrimeDurationBar;
        private System.Windows.Forms.Label BarDurationLabel;
        private System.Windows.Forms.Label DMLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button DMResyncButton;
        private System.Windows.Forms.TableLayoutPanel InfoTable;
        private System.Windows.Forms.Label PowerModeLabel;
        private System.Windows.Forms.PictureBox PwrCheck;
        private System.Windows.Forms.PictureBox DMCheck;
        private System.Windows.Forms.PictureBox DMError;
        private System.Windows.Forms.Label TestLabel1;
        private System.Windows.Forms.PictureBox StressCheck;
        private System.Windows.Forms.PictureBox StressError;
        private System.Windows.Forms.PictureBox OverheatingFlame;
        private System.Windows.Forms.PictureBox StressEggs;
        private System.Windows.Forms.Label TestLabel2;
        private System.Windows.Forms.Label TestLabel3;
        private System.Windows.Forms.Label TestLabel4;
        private System.Windows.Forms.Label TestLabel5;
        private System.Windows.Forms.Button IgnoreTempButton;
        private System.Windows.Forms.PictureBox HeavenError;
        private System.Windows.Forms.PictureBox HeavenIcon;
        private System.Windows.Forms.Button ManualHeavenButton;
        private System.Windows.Forms.Button AudioButton;
        private System.Windows.Forms.PictureBox AudioCheck;
    }
}

