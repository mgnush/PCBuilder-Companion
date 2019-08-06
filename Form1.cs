/*
 * Form1.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: The custom part of the mainform (GUI) class, hosts all major control flow
 */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Builder_Companion
{
    public partial class Form1 : Form
    {
        // Font import
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        public Form1()
        {            
            InitializeComponent();
            InitGUI();
            DMStatusCheck();   // Check Device manager every time program is launched    

            LoadFontMemory();   
        }

        #region<------- Event Handlers ------->
        // Control what content should be visible
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFonts();
            SystemInfo.RetrieveSystemInfo();
            if (SystemInfo.CpuBrand == CPUBrand.AMD)
            {
                this.BackgroundImage = Properties.Resources.BC_RG;
                this.RGBList.BackColor = Color.DarkRed;
                this.PrimeDurationBar.BackColor = Color.DarkRed;
            }            
             
            switch (Properties.Settings.Default.CurrentPhase)
            {
                case Phase.Testing:
                    // Load all software items from xml file
                    RGBInstaller.ReadRGBSoftware();
                    foreach (string rgbSoftware in RGBInstaller.software)
                    {
                        this.RGBList.Items.Add(rgbSoftware);
                    }
                    break;
                case Phase.Benchmarking:
                    ManualHeavenButton.Visible = true;
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.RGBList.Visible = false;
                    this.SoftwareLabel.Visible = false;

                    LoadAllData();
                    break;
                case Phase.Updating:
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.RGBList.Visible = false;
                    this.SoftwareLabel.Visible = false;

                    LoadAllData();
                    break;
                case Phase.QCReady:
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;
                    this.RGBList.Visible = false;
                    this.SoftwareLabel.Visible = false;                                       

                    LoadAllData();
                    if (QCReadyBar.Value == 100)
                    {
                        this.RestartQCButton.Visible = true;
                    }
                    else
                    {
                        LoadQCReadyActions();
                    }
                    break;
                default:                    
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;
                    this.RGBList.Visible = false;
                    this.SoftwareLabel.Visible = false;

                    this.QCMegaLabel.Visible = true;

                    LoadAllData();
                    break;
            }

            PopulateSysInfo();   // Call last to overwrite LoadAllData()
        }

        // Control action to take after app has started
        private void Form1_Loaded(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.CurrentPhase)
            {
                case Phase.Benchmarking:
                    DoUpdates();
                    TestHeaven();
                    break;
                case Phase.Updating:
                    DoUpdates();
                    UpdatesTimeout();
                    break;
                case Phase.QC:
                    TaskServicer.DeleteTaskService();   // Clean up task created                      
                    QCHandler.LaunchManualChecks();
                    QCHandler.ClearHeaven();
                    QCHandler.ClearDesktop();                    
                    QCHandler.FormatDrives();   // No effect if already formatted
                    QCHandler.ClearToasts();
                    Properties.Settings.Default.CurrentPhase = Phase.Exiting;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            if (Properties.Settings.Default.CurrentPhase == Phase.Exiting)
            {
                QCHandler.ClearSettings();

                // Delete this executable shortly after closing the application
                ProcessStartInfo info = new ProcessStartInfo();
                info.Arguments = "/C choice /C Y /N /D Y /T 2 & Del " + "\"" + Application.ExecutablePath + "\"";
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.CreateNoWindow = true;
                info.FileName = "cmd.exe";
                Process.Start(info);
            }           
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            PrimeDurationBar.Visible = false;
            BarDurationLabel.Visible = false;
            TestDurationLabel.Visible = false;
            SoftwareStatusLabel.Visible = true;

            TaskServicer.CreateTaskService();   // Program will now run automatically until QC button has been pressed

            // Updates & software            
            DoUpdates();            
            RGBInstaller.InstallSelectedSoftware(RGBList.CheckedIndices);
            RGBList.Enabled = false;
            SoftwareStatusLabel.Visible = false;

            // Calculate the test duration in minutes
            int durationMin = PrimeDurationBar.Value;              
            
            TestHandler(durationMin);            
        }

        private void RestartQCButton_Click(object sender, EventArgs e)
        {
            RestartQCButton.Visible = false;
            Properties.Settings.Default.CurrentPhase = Phase.QC;
            SaveAllData();
            Restart();
        }

        private void QCButton_Click(object sender, EventArgs e)
        {
            QCHandler.FormatDrives();   // No effect if already formatted
            QCHandler.ClearDesktop();
            QCHandler.LaunchManualChecks();            
        }

        private void DMResync_Click(object sender, EventArgs e)
        {
            DMResyncButton.Visible = false;
            DMStatusCheck();            
        }

        private void IgnoreTemp_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CurrentPhase = Phase.Benchmarking;
            Properties.Settings.Default.Save();

            IgnoreTempButton.Visible = false;
            Thread.Sleep(1000); // Give user a chance to let go of mouse
            TestHeaven();            
        }

        private void PrimeDurationBar_onChanged(object sender, EventArgs e)
        {
            BarDurationLabel.Text = PrimeDurationBar.Value.ToString() + "min";
        }

        private void ManualHeavenButton_onClick(object sender, EventArgs e)
        {
            BenchmarkBar.Value = 100;
            Properties.Settings.Default.BenchmarkProgress = BenchmarkBar.Value;
            ManualHeavenButton.Visible = false;
            TableAddTestInfo("Heaven OK", true);
            TestingComplete();
        }

        private void RGBButton_onClick(object sender, EventArgs e)
        {
            RGBButton.Visible = false;
            // Reposition relevant icon
            Label RGB = TestInfoContains("RGB");
            RGB.Text = "RGB Configured";
            int iconLocY = RGBCheck.Location.Y + (TestInfoLabelPosition("RGB") * TESTLABEL_HEIGHT);
            RGBCheck.Location = new Point(RGBCheck.Location.X, iconLocY);
            RGBCheck.Visible = true;

            QCReadyBar.PerformStep();
            Properties.Settings.Default.QCReadyProgress = QCReadyBar.Value;

            if (QCReadyBar.Value == 100)
            {
                // Launch Windows activation
                string slui = Environment.GetFolderPath(Environment.SpecialFolder.System);
                ProcessStartInfo sInfo = new ProcessStartInfo(Path.Combine(slui, "slui.exe"));
                Process p = new Process();
                p.StartInfo = sInfo;
                p.Start();

                RestartQCButton.Visible = true;
                SaveAllData();
            }            
        }

        private void AudioButton_click(object sender, EventArgs e)
        {
            AudioButton.Visible = false;
            // Reposition relevant icon
            Label audio = TestInfoContains("Audio");            
            int iconLocY = AudioCheck.Location.Y + (TestInfoLabelPosition("Audio") * TESTLABEL_HEIGHT);
            audio.Text = "Audio OK";
            AudioCheck.Location = new Point(AudioCheck.Location.X, iconLocY);
            AudioCheck.Visible = true;

            QCReadyBar.PerformStep();
            Properties.Settings.Default.QCReadyProgress = QCReadyBar.Value;

            if (QCReadyBar.Value == 100)
            {
                // Launch Windows activation
                string slui = Environment.GetFolderPath(Environment.SpecialFolder.System);
                ProcessStartInfo sInfo = new ProcessStartInfo(Path.Combine(slui, "slui.exe"));
                Process p = new Process();
                p.StartInfo = sInfo;
                p.Start();

                RestartQCButton.Visible = true;
                SaveAllData();
            }            
        }

        // This is called when the updater agent search comes up empty (windows up to date)
        public void WUPDone()
        {
            WUP.Text = "Windows up to date";
            _updateSessionComplete = true;
            _upToDate = true;
            UpdatingBar.Value = 100;
            Properties.Settings.Default.UpdatingProgress = UpdatingBar.Value;
            Properties.Settings.Default.Save();

            // If testing is complete, begin next step
            if (Properties.Settings.Default.CurrentPhase == Phase.Updating)
            {
                Properties.Settings.Default.CurrentPhase = Phase.QCReady;
                LoadQCReadyActions();                                             
            }
        }
        #endregion<------- Event Handlers ------->

        private void PopulateSysInfo()
        {
            // Populate system info labels   
            CPUMonitor.Text = SystemInfo.Cpu.Name;
            GPULabel.Text = SystemInfo.Gpu.Name + " (" + SystemInfo.Gpu.Driver + ")";
            RAMSize.Text = SystemInfo.Ram.Size.ToString() + "GB";
            RAMSpeed.Text = "@" + SystemInfo.Ram.Speed.ToString() + "MHz";
        }

        private void DMStatusCheck()
        {
            DMLabel.Visible = true;
            if (DMChecker.GetStatus())
            {
                DMLabel.Text = ("Device Manager OK");
                DMError.Visible = false;
                DMCheck.Visible = true;
            }
            else
            {
                DMResyncButton.Visible = true;
                DMError.Visible = true;
                DMCheck.Visible = false;
                DMLabel.Text = ("Check Device Manager!");
                Process.Start("devmgmt.msc");   // Launch device manager                
            }
        }

        private void UpdateCPU()
        {
            CPUSpeed.Text = (int)TempHandler.MaxSpeed + "MHz";
            CPUPwr.Text = TempHandler.MaxPwr + "W";
            CPUTemp.Text = TempHandler.MaxTemp + "°";
            GPUTempValue.Text = TempHandler.MaxGPUTemp + "°";
        }

        private void SetPowerPerformanceMode()
        {
            PowerControl.SetToPerformance();
            PowerModeLabel.Visible = true;
            Properties.Settings.Default.PowerInfoVis = true;
            Properties.Settings.Default.Save();
        }

        // Restart after 1 second
        private void Restart()
        {
            ProcessStartInfo restart = new ProcessStartInfo();
            restart.WindowStyle = ProcessWindowStyle.Hidden;
            restart.FileName = "cmd";
            restart.Arguments = "/C shutdown -f -r -t 1";
            Process.Start(restart);
        }

        #region<------- Testing Methods ------->
        /// <summary>
        /// Handles the control flow of the testing procedure
        /// </summary>
        /// <param name="durationMin">The duration to run prime & furmark for</param>
        /// <returns></returns>
        private async void TestHandler(int durationMin)
        {
            int iconLocY;
            bool overheating = false;
            bool overheatingGPU = false;
            bool highdraw = false;

            SetPowerPerformanceMode();
            Task<bool> taskHandler = TaskHandler.RunPrimeFurmark(durationMin);   // Run testing asynchronously           

            // Poll prime results and temps every (10) seconds
            // Overheating will not cut testing short
            while (!taskHandler.IsCompleted)
            {
                await Task.Delay(10000);
                UpdateCPU();
                double durationSec = durationMin * 60;
                double timeElapsed = TaskHandler.stopwatch.Elapsed.TotalSeconds;
                int progress = (int)(100.0 * (timeElapsed / durationSec));
                if (progress <= 100)
                {
                    StressBar.Value = progress;
                } else
                {
                    StressBar.Value = 100;
                }

                if ((TempHandler.MaxTemp > 95) && !overheating)
                {              
                    overheating = true;
                    // Reposition associated icon as needed
                    iconLocY = OverheatingFlame.Location.Y + (TableAddTestInfo("CPU Overheating!") * TESTLABEL_HEIGHT);
                    OverheatingFlame.Location = new Point(OverheatingFlame.Location.X, iconLocY);
                    OverheatingFlame.Visible = true;
                }
                if ((TempHandler.MaxPwr > 185) && !highdraw) {
                    TableAddTestInfo("High CPU wattage!");
                    highdraw = true;
                }
                if ((TempHandler.MaxGPUTemp > 90) && !overheatingGPU)
                {
                    overheatingGPU = true;
                    if (overheating)
                    {
                        // Reposition associated icon as needed
                        iconLocY = StressEggs.Location.Y + (TableAddTestInfo("Insert eggs now") * TESTLABEL_HEIGHT);
                        OverheatingFlame.Location = new Point(StressEggs.Location.X, iconLocY);
                        StressEggs.Visible = true;
                    } else
                    {
                        // Reposition associated icon as needed
                        iconLocY = OverheatingFlame.Location.Y + (TableAddTestInfo("GPU Overheating!") * TESTLABEL_HEIGHT);
                        OverheatingFlame.Location = new Point(OverheatingFlame.Location.X, iconLocY);
                        OverheatingFlame.Visible = true;
                    }                 
                }

                SaveAllData();
            }

            await Task.WhenAll(taskHandler);
            
            switch (taskHandler.Result)
            {                 
                case false:
                    // Halt program
                    // Reposition associated icon as needed
                    string msg = String.Format("Prime failed! {0:00}:{1:00}", TaskHandler.stopwatch.Elapsed.Minutes, TaskHandler.stopwatch.Elapsed.Seconds);
                    iconLocY = StressError.Location.Y + (TableAddTestInfo(msg) * TESTLABEL_HEIGHT);
                    StressError.Location = new Point(StressError.Location.X, iconLocY);
                    StressError.Visible = true;
                    break;
                case true when (overheating || overheatingGPU):
                    // Do not advance automatically if overheating
                    // Reposition associated icons as needed
                    iconLocY = StressCheck.Location.Y + (TableAddTestInfo("Prime OK") * TESTLABEL_HEIGHT);
                    StressCheck.Location = new Point(StressCheck.Location.X, iconLocY);
                    StressCheck.Visible = true;

                    StressBar.Value = 100;

                    IgnoreTempButton.Parent = InfoTable;
                    InfoTable.SetCellPosition(IgnoreTempButton, new TableLayoutPanelCellPosition(1, testLabelRecentRow));
                    if (SystemInfo.Cpu.Name.Contains("8700") || SystemInfo.Cpu.Name.Contains("9700"))
                    {
                        IgnoreTempButton.Text = "Dave Mode";
                    }
                    IgnoreTempButton.Visible = true;
                    break;
                case true when !(overheating || overheatingGPU):
                    Properties.Settings.Default.CurrentPhase = Phase.Benchmarking;
                    // Advance when not overheating
                    // Reposition associated icons as needed
                    iconLocY = StressCheck.Location.Y + (TableAddTestInfo("Prime OK") * TESTLABEL_HEIGHT);
                    StressCheck.Location = new Point(StressCheck.Location.X, iconLocY);
                    StressCheck.Visible = true;

                    StressBar.Value = 100;
                    Properties.Settings.Default.StressProgress = StressBar.Value;

                    TestHeaven();
                    break;
            }
            SaveAllData();
        }

        private HeavenHandler heavenHandler;
        
        private void TestHeaven()
        {
            heavenHandler = new HeavenHandler(this);            
        }

        /// <summary>
        /// Adds the heaven score to the testinfo screen. This should only be called by 
        /// heaven event handlers or after heaven has been run, or score will always be 0. 
        /// </summary>
        public void ReportHeavenScore()
        {
            int score = heavenHandler.Score;
            string heavenScore = "Heaven Score: " + score;

            int iconLocY = HeavenIcon.Location.Y + (TableAddTestInfo(heavenScore) * TESTLABEL_HEIGHT);
            HeavenIcon.Location = new Point(HeavenIcon.Location.X, iconLocY);
            HeavenIcon.Visible = true;

            if (score > 0)
            {
                BenchmarkBar.Value = 100;
                Properties.Settings.Default.BenchmarkProgress = BenchmarkBar.Value;
                TestingComplete();
            }
            else
            {
                // Reposition associated icons as needed
                iconLocY = HeavenError.Location.Y + (TableAddTestInfo("Run Heaven manually") * TESTLABEL_HEIGHT);
                HeavenError.Location = new Point(StressCheck.Location.X, iconLocY);
                HeavenError.Visible = true;

                ManualHeavenButton.Parent = InfoTable;
                InfoTable.SetCellPosition(ManualHeavenButton, new TableLayoutPanelCellPosition(1, testLabelRecentRow));
                ManualHeavenButton.Visible = true;
            }
        }       

        private void TestingComplete()
        {
            Properties.Settings.Default.CurrentPhase = Phase.Updating;           
            SaveAllData();
            
            // Restart if testing and updating is finished
            if (_updateSessionComplete && _upToDate)
            {               
                {
                    TableAddTestInfo("Configure RGB");
                    RGBButton.Parent = InfoTable;
                    InfoTable.SetCellPosition(RGBButton, new TableLayoutPanelCellPosition(1, testLabelRecentRow));
                    RGBButton.Visible = true;

                    TableAddTestInfo("Test Audio");
                    AudioButton.Parent = InfoTable;
                    InfoTable.SetCellPosition(AudioButton, new TableLayoutPanelCellPosition(1, testLabelRecentRow));
                    AudioButton.Visible = true;
                }
            } else
            {
                Restart();
            }
        }
        #endregion<------- Testing Methods ------->

        #region<------- Save & Load Form Data ------->
        /// <summary>
        /// Saves all obtained test and system info to default application settings.
        /// </summary>
        public void SaveAllData()
        {
            System.Collections.Specialized.StringCollection testLabelStrings = new System.Collections.Specialized.StringCollection();            
            foreach (Label label in testLabels)
            {
                testLabelStrings.Add(label.Text);
            }
            Properties.Settings.Default.TestInfo = testLabelStrings;

            // Icons
            Properties.Settings.Default.StressCheckY = StressCheck.Location.Y;
            Properties.Settings.Default.StressCheckVis = StressCheck.Visible;
            Properties.Settings.Default.HeavenIconY = HeavenIcon.Location.Y;
            Properties.Settings.Default.HeavenIconVis = HeavenIcon.Visible;
            Properties.Settings.Default.AudioCheckY = AudioCheck.Location.Y;
            Properties.Settings.Default.AudioCheckVis = AudioCheck.Visible;
            Properties.Settings.Default.RGBCheckY = RGBCheck.Location.Y;
            Properties.Settings.Default.RGBCheckVis = RGBCheck.Visible;

            Properties.Settings.Default.CPUInfo = this.CPUMonitor.Text;
            Properties.Settings.Default.CPUPwr = this.CPUPwr.Text;
            Properties.Settings.Default.CPUTemp = this.CPUTemp.Text;
            Properties.Settings.Default.CPUSpeed = this.CPUSpeed.Text;
            Properties.Settings.Default.GPUInfo = this.GPULabel.Text;
            Properties.Settings.Default.GPUTemp = this.GPUTempValue.Text;
            Properties.Settings.Default.RAMSize = this.RAMSize.Text;
            Properties.Settings.Default.RAMSpeed = this.RAMSpeed.Text;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Loads all saved test and system info settings into their respective place in the GUI.
        /// </summary>
        public void LoadAllData()
        {
            PowerModeLabel.Visible = Properties.Settings.Default.PowerInfoVis;
            for (int i = 0; i < Properties.Settings.Default.TestInfo.Count; i++)
            {
                testLabels.ToArray()[i].Text = Properties.Settings.Default.TestInfo[i];
            }

            // Icons
            StressCheck.Visible = Properties.Settings.Default.StressCheckVis;
            StressCheck.Location = new Point(StressCheck.Location.X, Properties.Settings.Default.StressCheckY);
            HeavenIcon.Visible = Properties.Settings.Default.HeavenIconVis;
            HeavenIcon.Location = new Point(HeavenIcon.Location.X, Properties.Settings.Default.HeavenIconY);
            AudioCheck.Visible = Properties.Settings.Default.AudioCheckVis;
            AudioCheck.Location = new Point(AudioCheck.Location.X, Properties.Settings.Default.AudioCheckY);
            RGBCheck.Visible = Properties.Settings.Default.AudioCheckVis;
            RGBCheck.Location = new Point(RGBCheck.Location.X, Properties.Settings.Default.RGBCheckY);

            // Progress bars
            StressBar.Value = Properties.Settings.Default.StressProgress;
            BenchmarkBar.Value = Properties.Settings.Default.BenchmarkProgress;
            UpdatingBar.Value = Properties.Settings.Default.UpdatingProgress;
            QCReadyBar.Value = Properties.Settings.Default.QCReadyProgress;

            this.CPUMonitor.Text = Properties.Settings.Default.CPUInfo;
            this.CPUPwr.Text = Properties.Settings.Default.CPUPwr;
            this.CPUTemp.Text = Properties.Settings.Default.CPUTemp;
            this.CPUSpeed.Text = Properties.Settings.Default.CPUSpeed;
            this.GPULabel.Text = Properties.Settings.Default.GPUInfo;
            this.GPULabel.Text = Properties.Settings.Default.GPUInfo;
            this.GPUTempValue.Text = Properties.Settings.Default.GPUTemp;
            this.RAMSize.Text = Properties.Settings.Default.RAMSize;
            this.RAMSpeed.Text = Properties.Settings.Default.RAMSpeed;
        }
        #endregion<------- Save & Load Form Data ------->

        #region<------- Button Mouseover Methods ------->
        private void Button_MouseEnterCheck(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button_p));
        }
        private void Button_MouseLeaveCheck(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button));
        }

        private void Button_MouseEnterGlow(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button_g));
        }
        private void Button_MouseLeaveGlow(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = ((System.Drawing.Image)(Properties.Resources.button));
        }

        private void DMResyncButton_MouseEnter(object sender, EventArgs e)
        {
            this.DMResyncButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.resync_p));
        }
        private void DMResyncButton_MouseLeave(object sender, EventArgs e)
        {
            this.DMResyncButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.resync));
        }
        #endregion<------- Button Mouseover Methods -------> 

    }
}
