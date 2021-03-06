﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinDynamicDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Environment.CurrentDirectory = UwpDesktop.GetHelper().GetCurrentDirectory();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                OnUnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppContext());
        }

        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errorMessage = ((Exception)e.ExceptionObject).ToString() + "\n";
            string logFilename = Path.Combine(Directory.GetCurrentDirectory(),
                Environment.GetCommandLineArgs()[0] + ".log");

            try
            {
                File.AppendAllText(logFilename, errorMessage);

                MessageBox.Show("See the logfile '" + logFilename + "' for details",
                    "Errors occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("The logfile '" + logFilename + "' could not be opened:\n " +
                    errorMessage, "Errors occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
