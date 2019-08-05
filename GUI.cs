/*
 * GUI.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This partial form class includes methods related directly to displaying items in the GUI.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Builder_Companion
{
    public partial class Form1: Form
    {
        private List<Label> testLabels;
        private const int TESTLABEL_HEIGHT = 60;
        private int testLabelRecentRow = 1;

        private void InitGUI()
        {
            // Add all labels inside the main table which are intended for test reports
            testLabels = new List<Label>();
            testLabels.Add(TestLabel1);
            testLabels.Add(TestLabel2);
            testLabels.Add(TestLabel3);
            testLabels.Add(TestLabel4);
            testLabels.Add(TestLabel5);
            testLabels.Add(TestLabel6);
        }

        /// <summary>
        /// Adds text in the next available (empty) test label.
        /// </summary>
        /// <returns>The test label # that was written to.</returns>
        public int TableAddTestInfo(string text)
        {
            return TableAddTestInfo(text, false);
        }

        /// <summary>
        /// Adds text in the next available (empty) test label.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="overwrite">Whether the last non-empty label should be overwritten.</param>
        /// <returns>The test label # that was written to.</returns>
        public int TableAddTestInfo(string text, bool overwrite)
        {
            int testLabelRecent = 0;
            for (int i = 0; i < testLabels.Count; i++)
            {
                if (String.IsNullOrWhiteSpace(testLabels.ElementAt(i).Text))
                {
                    if (overwrite && (i > 0))  { testLabelRecent = i - 1; }
                    else                       { testLabelRecent = i; }
                    
                    testLabels.ElementAt(testLabelRecent).Text = text;
                    testLabelRecentRow = testLabelRecent + 2;   // Row # in table
                    return testLabelRecent + 1;                    
                }
            }
            // Shouldn't reach here.
            Prompt.ShowDialog("Insufficient GUI space", "Warning");
            return 0;
        }

        /// <summary>
        /// Finds the label which contains the specified text, if any.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The first label which contains the specified text, null if no matches.</returns>
        public Label TestInfoContains(string text)
        {
            foreach (Label label in testLabels)
            {
                if (label.Text.Contains(text))
                {
                    return label;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the position of the label which contains the specified text, if any.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The row position of the first label which contains the specified text.</returns>
        public int TestInfoLabelPosition(string text)
        {
            Label label = TestInfoContains(text);
            if (label != null)
            {
                return testLabels.IndexOf(label) + 1;
            } else
            {
                return 0;
            }
        }

        public void LoadQCReadyActions()
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

        #region<------- Font Loading ------->
        // Create the font in memory with specified size
        private void LoadFontMemory()
        {
            byte[] fontData = Properties.Resources.ArchitectsDaughter;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.ArchitectsDaughter.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.ArchitectsDaughter.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
        }

        private System.Drawing.Font CreateFontMemory(float emSize)
        {       

            return new Font(fonts.Families[0], emSize);
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void LoadFonts()
        {
            // Labels
            var labels = GetAll(this, typeof(Label));
            foreach (Label label in labels)
            {
                float emSize = label.Font.Size;
                label.Font = CreateFontMemory(emSize);
            }

            // Buttons
            var buttons = GetAll(this, typeof(Button));
            foreach (Button button in buttons)
            {
                float emSize = button.Font.Size;
                button.Font = CreateFontMemory(emSize);
            }
        }
        #endregion<------- Font Loading ------->
    }
}
