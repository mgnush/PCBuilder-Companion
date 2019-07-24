/*
 * Form1.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: The custom part of the mainform (GUI) class, hosts all major control flow
 */

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
        // Control what content should be visible
        private void Form1_Load(object sender, EventArgs e)
        {
            SetPhaseLabel();
            switch (Properties.Settings.Default.CurrentPhase)
            {
                case Phase.Testing:
                    this.RGBLabel.AppendText("R", Color.Red);
                    this.RGBLabel.AppendText("G", Color.Green);
                    this.RGBLabel.AppendText("B", Color.Blue);
                    this.RGBLabel.AppendText(" & Stuff", Color.Black);

                    // Load all software items from xml file
                    RGBInstaller.ReadRGBSoftware();
                    foreach (string rgbSoftware in RGBInstaller.software)
                    {
                        this.RGBList.Items.Add(rgbSoftware);
                    }
                    break;
                case Phase.Updating:
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    LoadAllData();
                    break;
                case Phase.QCReady:
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;

                    LoadAllData();
                    SetPhaseLabel();
                    break;
                default:                    
                    this.TestDurationLabel.Visible = false;
                    this.PrimeDurationBar.Visible = false;
                    this.BarDurationLabel.Visible = false;
                    this.StartButton.Visible = false;
                    this.WUP.Visible = false;
                    this.QCButton.Visible = true;

                    LoadAllData();
                    SetPhaseLabel();
                    break;
            }
            
        }

        // Control action to take after app has started
        private void Form1_Loaded(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.CurrentPhase)
            {
                case Phase.Updating:
                    DoUpdates();
                    UpdatesTimeout();
                    break;
                case Phase.QCReady:
                    TaskServicer.DeleteTaskService();   // Clean up task created                      
                    QCHandler.LaunchManualChecks();
                    QCHandler.ClearDesktop();
                    QCHandler.ClearHeaven();
                    QCHandler.FormatDrives();   // No effect if already formatted
                    //QCHandler.ClearToasts();
                    Properties.Settings.Default.CurrentPhase = Phase.QC;
                    Properties.Settings.Default.Save();
                    SetPhaseLabel();
                    break;
                default:
                    break;
            }
        }

        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            if (Properties.Settings.Default.CurrentPhase == Phase.QC)
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

            // Updates & software
            DoUpdates();
            RGBInstaller.InstallSelectedSoftware(RGBList.CheckedIndices);
            RGBList.Enabled = false;

            // Calculate the test duration in minutes
            int durationMin = PrimeDurationBar.Value;           
           
            PrimeDurationBar.Visible = false;
            BarDurationLabel.Visible = false;
            TestDurationLabel.Text += "\n" + durationMin + "min";
            //easter egg opp.
            
            TestHandler(durationMin);            
        }

        private void RestartQCButton_Click(object sender, EventArgs e)
        {
            RestartQCButton.Visible = false;
            Properties.Settings.Default.CurrentPhase = Phase.QCReady;
            Properties.Settings.Default.Save();
            SetPhaseLabel();
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
            DMStatusCheck();
            DMResync.Visible = false;
        }

        private void IgnoreTemp_Click(object sender, EventArgs e)
        {
            IgnoreTemp.Visible = false;
            Thread.Sleep(1000); // Give user a chance to let go of mouse
            TestHeaven();            
        }

        private void RGBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PrimeDurationBar_onChanged(object sender, EventArgs e)
        {
            BarDurationLabel.Text = PrimeDurationBar.Value.ToString() + "min";
        }

        private void ManualHeavenButton_onClick(object sender, EventArgs e)
        {
            ManualHeavenButton.Visible = false;
            TestInfo.AppendText("Heaven OK", Color.Green);
            TestingComplete();
        }

        private void AudioButton_click(object sender, EventArgs e)
        {
            TestInfo.Undo();
            TestInfo.AppendText("Audio OK", Color.Green);
            AudioButton.Visible = false;            

            string slui = Environment.GetFolderPath(Environment.SpecialFolder.System);
            ProcessStartInfo sInfo = new ProcessStartInfo(Path.Combine(slui, "slui.exe"));
            Process p = new Process();
            p.StartInfo = sInfo;
            p.Start();
            RestartQCButton.Visible = true;
        }

        // This is called when the updater agent search comes up empty (windows up to date)
        public void WUPDone()
        {
            WUP.Text = "Windows up to date";
            WUP.ForeColor = Color.Green;
            _updateSessionComplete = true;
            _upToDate = true;

            // If testing is complete, begin next step
            if ((Properties.Settings.Default.CurrentPhase == Phase.Updating) || (Properties.Settings.Default.CurrentPhase == Phase.QCReady))
            {        
                TestInfo.AppendText("Test Audio \n", Color.Black);
                AudioButton.Visible = true;
                PhaseLabel.Text += ": Configure RGB software if any!";
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

        private void SetPhaseLabel()
        {
            this.PhaseLabel.Text = "Phase: " +  Properties.Settings.Default.CurrentPhase.ToString();
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
            TestInfo.AppendText("Heaven Score: " + score + "\n", Color.Black);
            if (score > 0)
            {
                TestingComplete();
            }
            else
            {
                TestInfo.AppendText("Wrong heaven score, run manually \n", Color.Red);
                ManualHeavenButton.Visible = true;
            }
        }       

        private void TestingComplete()
        {
            Properties.Settings.Default.CurrentPhase = Phase.Updating;           
            SaveAllData();
            SetPhaseLabel();

            TaskServicer.CreateTaskService();   // Program will now run automatically until QC button has been pressed
            // Restart if testing and updating is finished
            if (_updateSessionComplete && _upToDate)
            {               
                {
                    TestInfo.AppendText("Test Audio \n", Color.Black);
                    AudioButton.Visible = true;
                }
            } else
            {
                Restart();
            }
        }
        #endregion<------- Testing Methods ------->

        /// <summary>
        /// Saves all obtained test and system info to default application settings.
        /// </summary>
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

        /// <summary>
        /// Loads all saved test and system info settings into their respective place in the GUI.
        /// </summary>
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
