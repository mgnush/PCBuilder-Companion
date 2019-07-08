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


namespace PCCG_Tester
{
    public partial class Form1 : Form
    {        
        public Form1()
        {            
            InitializeComponent();
            InitChecks();          
        }

        private void InitChecks()
        {
            if (!DMStatusCheck())
            {
                Process.Start("devmgmt.msc");   // Launch device manager
                DMResync.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.TestComplete)
            {
                this.TestDurationLabel.Visible = false;
                this.TestDuration.Visible = false;
                this.StartButton.Visible = false;
                LoadAllData();
                DoUpdates();
            } else
            {
                this.RGBLabel.AppendText("R", Color.Red);
                this.RGBLabel.AppendText("G", Color.Green);
                this.RGBLabel.AppendText("B", Color.Blue);
                this.RGBLabel.AppendText(" Stuff", Color.Black);

                RGBInstaller.ReadRGBSoftware();
                foreach (string rgbSoftware in RGBInstaller.software)
                {
                    this.RGBList.Items.Add(rgbSoftware);
                }
            }  
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            DoUpdates();
            //rgb software 
            RGBInstaller.InstallSelectedSoftware(RGBList.CheckedIndices);
            RGBList.Enabled = false;

            double minDuration = 7;
            double maxDuration = 29;
            // Calculate the test duratin in minutes
            int durationMin = (int)(minDuration + ((maxDuration - minDuration) * ((double)TestDuration.Value / 100)));
            
            StartButton.Visible = false;
            TestDuration.Visible = false;
            TestDurationLabel.Text += "\n" + durationMin + "min";
            //easter egg opp.
            
            TestHandler(durationMin);           
        }

        private void QCButton_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private bool DMStatusCheck()
        {
            TestInfo.Text = "";
            if (DMChecker.GetStatus())
            {
                TestInfo.AppendText("Device Manager OK \n", Color.Green);
                return true;
            } else
            {
                TestInfo.AppendText("Check Device Manager! \n", Color.Red);
                return false;
            }
        }

        private void DMResync_Click(object sender, EventArgs e)
        {
            InitChecks();
            DMResync.Visible = false;
        }

        private void IgnoreTemp_Click(object sender, EventArgs e)
        {
            TestInfo.ForeColor = Color.Green;
            IgnoreTemp.Visible = false;
            TestHeaven();            
        }

        private void UpdateCPU()
        {
            CPUSpeed.Text = TempHandler.MaxSpeed + "MHz";
            CPUPwr.Text = TempHandler.MaxPwr + "W";
            CPUTemp.Text = TempHandler.MaxTemp + "°";
            GPUTempValue.Text = TempHandler.MaxGPUTemp + "°";
        }

        private void SetPowerControl()
        {
            PowerControl.SetToPerformance();
            TestInfo.AppendText("Power mode was changed to Performance \n", Color.Green);
        }

        private async void TestHandler(int durationMin)
        {
            SystemInfo.RetrieveSystemInfo();
            CPUMonitor.Text = SystemInfo.Cpu.Name + " (Max values)";
            GPULabel.Text = SystemInfo.Gpu.Name + " (Max temp)";
            GPUDriverLabel.Text = SystemInfo.Gpu.Driver;

            bool overheating = false;
            bool overheatingGPU = false;
            bool highdraw = false;

            SetPowerControl();
            Task<bool> taskHandler = TaskHandler.RunPrimeFurmark(durationMin);            

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
                    TestInfo.AppendText("Prime failed! \n", Color.Red);
                    break;
                case true when (overheating || overheatingGPU):
                    TestInfo.AppendText("Prime OK \n", Color.Green);
                    IgnoreTemp.Visible = true;
                    break;
                case true when !(overheating || overheatingGPU):
                    TestInfo.AppendText("Prime OK \n", Color.Green);
                    TestHeaven();
                    break;
            }
        }

        private async void TestHeaven()
        {
            Task<bool> taskHandler = TaskHandler.RunHeaven();
            await Task.WhenAll(taskHandler);

            if (taskHandler.Result)
            {
                int heavenScore = HeavenHandler.EvaluateHeaven();
                TestInfo.AppendText("Heaven Score: " + heavenScore, Color.Black);
            }

            SaveAllData();
            Restart();
        }

        private void RGBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SaveAllData()
        {
            Properties.Settings.Default.TestComplete = true;
            Properties.Settings.Default.TestInfo = this.TestInfo.Text;
            Properties.Settings.Default.CPUInfo = this.CPUMonitor.Text;
            Properties.Settings.Default.CPUPwr = this.CPUPwr.Text;
            Properties.Settings.Default.CPUTemp = this.CPUTemp.Text;
            Properties.Settings.Default.CPUSpeed = this.CPUSpeed.Text;
            Properties.Settings.Default.GPUInfo = this.GPULabel.Text;
            Properties.Settings.Default.GPUTemp = this.GPUTempValue.Text;
            Properties.Settings.Default.GPUDriver = this.GPUDriverLabel.Text;

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
        }

        private void Restart()
        {
            ProcessStartInfo restart = new ProcessStartInfo();
            restart.WindowStyle = ProcessWindowStyle.Hidden;
            restart.FileName = "cmd";
            restart.Arguments = "/C shutdown -f -r";
            Process.Start(restart);
        }
    }
}
