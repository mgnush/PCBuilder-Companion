namespace PCCG_Tester
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
            this.IgnoreDM = new System.Windows.Forms.Button();
            this.CPUMonitor = new System.Windows.Forms.Label();
            this.CPUSpeed = new System.Windows.Forms.Label();
            this.CPUTemp = new System.Windows.Forms.Label();
            this.CPUPwr = new System.Windows.Forms.Label();
            this.IgnoreTemp = new System.Windows.Forms.Button();
            this.TestInfo = new System.Windows.Forms.RichTextBox();
            this.WUP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IgnoreDM
            // 
            this.IgnoreDM.Location = new System.Drawing.Point(490, 9);
            this.IgnoreDM.Name = "IgnoreDM";
            this.IgnoreDM.Size = new System.Drawing.Size(75, 23);
            this.IgnoreDM.TabIndex = 1;
            this.IgnoreDM.Text = "Ignore";
            this.IgnoreDM.UseVisualStyleBackColor = true;
            this.IgnoreDM.Visible = false;
            this.IgnoreDM.Click += new System.EventHandler(this.IgnoreDM_Click);
            // 
            // CPUMonitor
            // 
            this.CPUMonitor.AutoSize = true;
            this.CPUMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUMonitor.Location = new System.Drawing.Point(715, 9);
            this.CPUMonitor.Name = "CPUMonitor";
            this.CPUMonitor.Size = new System.Drawing.Size(195, 20);
            this.CPUMonitor.TabIndex = 2;
            this.CPUMonitor.Text = "CPU Monitor (Max Values)";
            // 
            // CPUSpeed
            // 
            this.CPUSpeed.AutoSize = true;
            this.CPUSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUSpeed.Location = new System.Drawing.Point(715, 38);
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
            this.IgnoreTemp.Location = new System.Drawing.Point(490, 60);
            this.IgnoreTemp.Name = "IgnoreTemp";
            this.IgnoreTemp.Size = new System.Drawing.Size(75, 23);
            this.IgnoreTemp.TabIndex = 6;
            this.IgnoreTemp.Text = "Ignore";
            this.IgnoreTemp.UseVisualStyleBackColor = true;
            this.IgnoreTemp.Visible = false;
            this.IgnoreTemp.Click += new System.EventHandler(this.IgnoreTemp_Click);
            // 
            // TestInfo
            // 
            this.TestInfo.BackColor = System.Drawing.SystemColors.Control;
            this.TestInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.TestInfo.Enabled = false;
            this.TestInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestInfo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TestInfo.Location = new System.Drawing.Point(17, 9);
            this.TestInfo.Name = "TestInfo";
            this.TestInfo.ReadOnly = true;
            this.TestInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TestInfo.Size = new System.Drawing.Size(467, 275);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 443);
            this.Controls.Add(this.WUP);
            this.Controls.Add(this.TestInfo);
            this.Controls.Add(this.IgnoreTemp);
            this.Controls.Add(this.CPUPwr);
            this.Controls.Add(this.CPUTemp);
            this.Controls.Add(this.CPUSpeed);
            this.Controls.Add(this.CPUMonitor);
            this.Controls.Add(this.IgnoreDM);
            this.Name = "Form1";
            this.Text = "PCCG Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button IgnoreDM;
        private System.Windows.Forms.Label CPUMonitor;
        private System.Windows.Forms.Label CPUSpeed;
        private System.Windows.Forms.Label CPUTemp;
        private System.Windows.Forms.Label CPUPwr;
        private System.Windows.Forms.Button IgnoreTemp;
        private System.Windows.Forms.RichTextBox TestInfo;
        private System.Windows.Forms.Label WUP;
    }
}

