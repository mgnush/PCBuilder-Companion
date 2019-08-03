using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Builder_Companion
{
    public partial class Form1: Form
    {
        private List<Label> testLabels;
        private const int TESTLABEL_HEIGHT = 60;
        private int testLabelRecentRow = 2;

        private void InitGUI()
        {
            // Add all labels inside the main table, which are intended for test reports
            testLabels = new List<Label>();
            testLabels.Add(TestLabel1);
            testLabels.Add(TestLabel2);
            testLabels.Add(TestLabel3);
            testLabels.Add(TestLabel4);
            testLabels.Add(TestLabel5);
        }

        public int TableAddTestInfo(string text)
        {
            return TableAddTestInfo(text, false);
        }

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
                    return testLabelRecent;                    
                }
            }
            // Shouldn't reach here.
            Prompt.ShowDialog("Insufficient GUI space", "Warning");
            return 0;
        }
    }
}
