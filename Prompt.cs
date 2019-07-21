/*
 * Prompt.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class hosts static methods that will bring up information boxes in a separate form
 */

using System.Windows.Forms;

namespace Builder_Companion
{
    public static class Prompt
    {
        /// <summary>
        /// Displays a textbox with an OK button in a separate form.
        /// </summary>
        /// <returns>The user response.</returns>
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 210,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 180, ReadOnly = true, Text = text, BorderStyle = BorderStyle.None, Enabled = false, TextAlign = HorizontalAlignment.Center };
            Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
