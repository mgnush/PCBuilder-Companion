using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Builder_Companion
{
    public partial class Form1 : Form
    {        
        public Form1()
        {            
            InitializeComponent();
            DMStatusCheck();   // Check Device manager every time program is launched             
        }

        #region<------- Event Handlers ------->
        // Control what content should be visible & run on load
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime expiryDate = new DateTime(2019, 7, 17);
            int comp = DateTime.Compare(TrialLock.GetNistTime(), expiryDate);
            int isHacks = DateTime.Compare(TrialLock.GetNistTime(), DateTime.MinValue);

            if ((comp > 0) || (isHacks == 0))
            {
                Application.Exit();
            }

            switch (Properties.Settings.Default.CurrentPhase)
            {
                case Phase.Testing:
                    this.RGBLabel.AppendText("R", Color.Red);
                    this.RGBLabel.AppendText("G", Color.Green);
                    this.RGBLabel.AppendText("B", Color.Blue);
                    this.RGBLabel.AppendText(" Stuff", Color.Black);

                    // Load all software items from xml file
                    RGBInstaller.ReadRGBSoftware();
                    foreach (string rgbSoftware in RGBInstaller.software)
                    {
                        this.RGBList.Items.Add(rgbSoftware);
                    }
                    break;
                case Phase.Updating:
                    this.TestDurationLabel.Visible = false;
                    this.TestDuration.Visible = false;
                    this.StartButton.Visible = false;
                    LoadAllData();
                    DoUpdates();
                    break;
                case Phase.QCReady:
                    this.TestDurationLabel.Visible = false;
                    this.TestDuration.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;

                    LoadAllData();

                    TaskServicer.DeleteTaskService();   // Clean up task created  
                    QCHandler.FormatDrives();   // No effect if already formatted
                    QCHandler.LaunchManualChecks();                    
                    QCHandler.ClearDesktop();
                    QCHandler.ClearToasts();
                    Properties.Settings.Default.CurrentPhase = Phase.QC;
                    Properties.Settings.Default.Save();
                    break;
                default:                    
                    this.TestDurationLabel.Visible = false;
                    this.TestDuration.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;

                    LoadAllData();
                    break;
            }
            
        }

        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            if (Properties.Settings.Default.CurrentPhase == Phase.QC)
            {
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

            // Updates & software
            DoUpdates();
            RGBInstaller.InstallSelectedSoftware(RGBList.CheckedIndices);
            RGBList.Enabled = false;

            // Testing
            double minDuration = 9;
            double maxDuration = 29;
            // Calculate the test duration in minutes
            int durationMin = (int)(minDuration + ((maxDuration - minDuration) * ((double)TestDuration.Value / 100)));            
           
            TestDuration.Visible = false;
            TestDurationLabel.Text += "\n" + durationMin + "min";
            //easter egg opp.
            
            TestHandler(durationMin);            
        }

        private void RestartQCButton_Click(object sender, EventArgs e)
        {
            RestartQCButton.Visible = false;
            Properties.Settings.Default.CurrentPhase = Phase.QCReady;
            Properties.Settings.Default.Save();
            Restart();
        }

        private void QCButton_Click(object sender, EventArgs e)
        {
            TaskServicer.DeleteTaskService();   // Clean up task created  
            QCHandler.FormatDrives();   // No effect if already formatted
            QCHandler.LaunchManualChecks();
        }

        private void DMResync_Click(object sender, EventArgs e)
        {
            DMStatusCheck();
            DMResync.Visible = false;
        }

        private void IgnoreTemp_Click(object sender, EventArgs e)
        {
            IgnoreTemp.Visible = false;
            TestHeaven();            
        }

        private void RGBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AudioButton_click(object sender, EventArgs e)
        {
            TestInfo.Undo();
            TestInfo.AppendText("Audio OK", Color.Green);
            AudioButton.Visible = false;
            RestartQCButton.Visible = true;

            string slui = Environment.GetFolderPath(Environment.SpecialFolder.System);
            ProcessStartInfo sInfo = new ProcessStartInfo(Path.Combine(slui, "slui.exe"));
            Process p = new Process();
            p.StartInfo = sInfo;
            p.Start();
        }

        public void WUPDone()
        {
            WUP.Text = "Windows up to date";
            WUP.ForeColor = Color.Green;
            _updateSessionComplete = true;
            _upToDate = true;

            if (Properties.Settings.Default.CurrentPhase == Phase.Updating)
            {        
                TestInfo.AppendText("Test Audio \n", Color.Black);
                AudioButton.Visible = true;                
            }
        }
        #endregion<------- Event Handlers ------->

        private void DMStatusCheck()
        {
            TestInfo.Text = "";
            if (DMChecker.GetStatus())
            {
                TestInfo.AppendText("Device Manager OK \n", Color.Green);
            }
            else
            {
                TestInfo.AppendText("Check Device Manager! \n", Color.Red);
                Process.Start("devmgmt.msc");   // Launch device manager
                DMResync.Visible = true;
            }
        }

        private void UpdateCPU()
        {
            CPUSpeed.Text = TempHandler.MaxSpeed + "MHz";
            CPUPwr.Text = TempHandler.MaxPwr + "W";
            CPUTemp.Text = TempHandler.MaxTemp + "°";
            GPUTempValue.Text = TempHandler.MaxGPUTemp + "°";
        }

        private void SetPowerPerformanceMode()
        {
            PowerControl.SetToPerformance();
            TestInfo.AppendText("Power mode was changed to Performance \n", Color.Green);
        }

        #region<------- Testing Methods ------->
        /// <summary>
        /// Handles the control flow of the testing procedure
        /// </summary>
        /// <param name="durationMin">The duration to run prime & furmark for</param>
        /// <returns></returns>
        private async void TestHandler(int durationMin)
        {
            // Populate system info labels
            SystemInfo.RetrieveSystemInfo();
            CPUMonitor.Text = SystemInfo.Cpu.Name + " (Max values)";
            GPULabel.Text = SystemInfo.Gpu.Name + " (Max temp)";
            GPUDriverLabel.Text = SystemInfo.Gpu.Driver;
            RAMLabel.Text = SystemInfo.Ram.Description;

            bool overheating = false;
            bool overheatingGPU = false;
            bool highdraw = false;

            SetPowerPerformanceMode();
            Task<bool> taskHandler = TaskHandler.RunPrimeFurmark(durationMin);   // Run testing asynchronously           

            // Poll prime results and temps every (10) seconds
            // Overheating will not stop testing immediately
            while (!taskHandler.IsCompleted)
            {
                await Task.Delay(10000);
                UpdateCPU();
                if ((TempHandler.MaxTemp > 95) && !overheating)
                {
                    TestInfo.AppendText("Overheating! Check cooling \n", Color.Red);
                    overheating = true;
                }
                if ((TempHandler.MaxPwr > 185) && !highdraw) {
                    TestInfo.AppendText("High wattage, check MCE \n", Color.YellowGreen);
                    highdraw = true;
                }
                if ((TempHandler.MaxGPUTemp > 90) && !overheatingGPU)
                {
                    TestInfo.AppendText("Overheating GPU! \n", Color.Red);
                    overheatingGPU = true;
                }
            }

            await Task.WhenAll(taskHandler);
            
            switch (taskHandler.Result)
            {
                case false:
                    // Halt program
                    TestInfo.AppendText("Prime failed! \n", Color.Red);
                    break;
                case true when (overheating || overheatingGPU):
                    // Do not advance automatically if overheating
                    TestInfo.AppendText("Prime OK \n", Color.Green);
                    IgnoreTemp.Visible = true;
                    break;
                case true when !(overheating || overheatingGPU):
                    // Advance when not overheating
                    TestInfo.AppendText("Prime OK \n", Color.Green);
                    TestHeaven();
                    break;
            }
        }

        private void TestHeaven()
        {
            if (HeavenHandler.RunHeaven())
            {
                int heavenScore = HeavenHandler.EvaluateHeaven();
                TestInfo.AppendText("Heaven Score: " + heavenScore + "\n", Color.Black);
                if (heavenScore > 0)
                {
                    TestingComplete();
                } else
                {
                    TestInfo.AppendText("Wrong heaven score, run manually \n", Color.Red);
                }                       
            }
            // If heaven failed, let builder deal with it (do nothing)
        }

        private void TestingComplete()
        {
            Properties.Settings.Default.CurrentPhase = Phase.Updating;
            SaveAllData();
           
            // Need to account for cases where windows is up to date already

            TaskServicer.CreateTaskService();   // Program will now run automatically until QC button has been pressed
            // Restart if testing and updating is finished
            if (_updateSessionComplete && !_upToDate)
            {                
                Restart();
            }
            if (_updateSessionComplete && _upToDate)
            {               
                {
                    TestInfo.AppendText("Test Audio \n", Color.Black);
                    AudioButton.Visible = true;
                }
            }
        }
        #endregion<------- Testing Methods ------->

        public void SaveAllData()
        {
            Properties.Settings.Default.TestInfo = this.TestInfo.Text;
            Properties.Settings.Default.CPUInfo = this.CPUMonitor.Text;
            Properties.Settings.Default.CPUPwr = this.CPUPwr.Text;
            Properties.Settings.Default.CPUTemp = this.CPUTemp.Text;
            Properties.Settings.Default.CPUSpeed = this.CPUSpeed.Text;
            Properties.Settings.Default.GPUInfo = this.GPULabel.Text;
            Properties.Settings.Default.GPUTemp = this.GPUTempValue.Text;
            Properties.Settings.Default.GPUDriver = this.GPUDriverLabel.Text;
            Properties.Settings.Default.RAMInfo = this.RAMLabel.Text;

            Properties.Settings.Default.Save();
        }

        public void LoadAllData()
        {
            this.TestInfo.Text = Properties.Settings.Default.TestInfo;
            this.CPUMonitor.Text = Properties.Settings.Default.CPUInfo;
            this.CPUPwr.Text = Properties.Settings.Default.CPUPwr;
            this.CPUTemp.Text = Properties.Settings.Default.CPUTemp;
            this.CPUSpeed.Text = Properties.Settings.Default.CPUSpeed;
            this.GPULabel.Text = Properties.Settings.Default.GPUInfo;
            this.GPULabel.Text = Properties.Settings.Default.GPUInfo;
            this.GPUTempValue.Text = Properties.Settings.Default.GPUTemp;
            this.GPUDriverLabel.Text = Properties.Settings.Default.GPUDriver;
            this.RAMLabel.Text = Properties.Settings.Default.RAMInfo;
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
    }
}
