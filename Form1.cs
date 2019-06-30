using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;

namespace PCCG_Tester
{
    public partial class Form1 : Form
    {
        private bool _overheating;

        public Form1()
        {            
            InitializeComponent();
            _overheating = false;

            if (DMStatusCheck())
            {                
                TestHandler();                
            } else
            {
                IgnoreDM.Visible = true;
            }  
            
        }

        private bool DMStatusCheck()
        {
            if (DMChecker.GetStatus())
            {
                this.DMStatus.Text = "Device Manager OK \n";
                this.DMStatus.ForeColor = Color.Green;
                return true;
            } else
            {
                this.DMStatus.Text = "Check Device Manager! \n";
                this.DMStatus.ForeColor = Color.Red;
                return false;
            }
        }

        private void IgnoreDM_Click(object sender, EventArgs e)
        {
            TestHandler();
            IgnoreDM.Visible = false;
        }

        private void IgnoreTemp_Click(object sender, EventArgs e)
        {
            DMStatus.ForeColor = Color.Green;
            IgnoreTemp.Visible = false;
            TestHeaven();            
        }

        private void UpdateCPU()
        {
            CPUSpeed.Text = TempHandler.MaxSpeed + "MHz";
            CPUPwr.Text = TempHandler.MaxPwr + "W";
            CPUTemp.Text = TempHandler.MaxTemp + "°";
        }

        private void SetPowerControl()
        {
            PowerControl.SetToPerformance();
            this.DMStatus.Text += "Power mode was changed to Performace \n";
        }

        private async void TestHandler()
        {
            SetPowerControl();

            Task<bool> taskHandler = TaskHandler.RunPrimeFurmark();            

            while (!taskHandler.IsCompleted)
            {
                await Task.Delay(10000);
                UpdateCPU();
                if ((TempHandler.MaxTemp > 95) && !_overheating)
                {
                    DMStatus.Text += "Overheating! Check Cooling \n";
                    DMStatus.ForeColor = Color.Red;
                    _overheating = true;
                }
            }

            await Task.WhenAll(taskHandler);

            switch (taskHandler.Result)
            {
                case false:
                    DMStatus.Text += "Prime failed! \n";
                    DMStatus.ForeColor = Color.Red;
                    break;
                case true when _overheating:
                    DMStatus.Text += "Prime OK \n";
                    DMStatus.ForeColor = Color.Red;
                    IgnoreTemp.Visible = true;
                    break;
                case true when !_overheating:
                    DMStatus.Text += "Prime OK \n";
                    DMStatus.ForeColor = Color.Green;
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
                if (heavenScore == 0) { DMStatus.ForeColor = Color.Red; }
                DMStatus.Text += "Heaven Score: " + heavenScore + "\n";
            }
        }
    }
}
