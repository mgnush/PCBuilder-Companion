/*
 * Program.cs
 * 
 * @Author Magnus Hjorth
 * 
 * File Description: Entry point of the application
 */

using System;
using System.Windows.Forms;

namespace Builder_Companion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());            
        }
    }
}
