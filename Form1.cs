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

        private void StartButton_Click(object sender, EventArgs e)
        {
            DoUpdates();
            //rgb software 
            //RGBInstaller.InstallSelectedSoftware(RGBList.SelectedIndices);
            RGBList.Enabled = false;

            int minDuration = 7;
            int maxDuration = 29;
            // Calculate the test duratin in minutes
            int durationMin = minDuration + (int)((maxDuration - minDuration) * (TestDuration.Value / 100));
            
            StartButton.Visible = false;
            TestDuration.Visible = false;
            TestDurationLabel.Text += "\n" + durationMin + "min";
            //easter egg opp.

            TestHandler(durationMin);
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
        }

        private void RGBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
