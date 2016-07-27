/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acrolinx.Demo.Sidebar
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

            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InstalledUICulture; //Set the Ui language of the system
            
            Application.Run(new Sample());
        }
    }
}
