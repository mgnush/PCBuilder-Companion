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
            this.DMStatus = new System.Windows.Forms.Label();
            this.IgnoreDM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DMStatus
            // 
            this.DMStatus.AutoSize = true;
            this.DMStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DMStatus.Location = new System.Drawing.Point(12, 9);
            this.DMStatus.Name = "DMStatus";
            this.DMStatus.Size = new System.Drawing.Size(244, 26);
            this.DMStatus.TabIndex = 0;
            this.DMStatus.Text = "Check Device Manager!";
            // 
            // IgnoreDM
            // 
            this.IgnoreDM.Location = new System.Drawing.Point(262, 9);
            this.IgnoreDM.Name = "IgnoreDM";
            this.IgnoreDM.Size = new System.Drawing.Size(75, 23);
            this.IgnoreDM.TabIndex = 1;
            this.IgnoreDM.Text = "Ignore";
            this.IgnoreDM.UseVisualStyleBackColor = true;
            this.IgnoreDM.Visible = false;
            this.IgnoreDM.Click += new System.EventHandler(this.IgnoreDM_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IgnoreDM);
            this.Controls.Add(this.DMStatus);
            this.Name = "Form1";
            this.Text = "PCCG Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DMStatus;
        private System.Windows.Forms.Button IgnoreDM;
    }
}

