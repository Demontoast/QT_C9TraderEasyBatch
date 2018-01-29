using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LM_C9Master
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
            frmMainForm mainForm = new frmMainForm();
            generateNewSettingsFilePrompt filePrompt = new generateNewSettingsFilePrompt();
            Application.Run(filePrompt);
            Application.Run(mainForm);
        }
    }
}
