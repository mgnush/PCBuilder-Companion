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

        public Form1()
        {            
            InitializeComponent();

            if (DMStatusCheck())
            {
                SetPowerControl();
                TestHandler();
                
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

        private void SetPowerControl()
        {
            PowerControl.SetToPerformance();
            this.DMStatus.Text += "Power mode was changed to Performace \n";
        }

        private async void TestHandler()
        {
            Task<bool> taskHandler = TaskHandler.RunTasks();
           

            await Task.WhenAll(taskHandler);
            bool taskRes = taskHandler.Result;

            if (taskRes)
            {
                this.DMStatus.Text += "Prime OK \n";
            } else
            {
                this.DMStatus.Text += "Prime failed! \n";
                this.DMStatus.ForeColor = Color.Red;
            }
        }
    }
}
