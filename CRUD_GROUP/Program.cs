using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_GROUP
{
    internal static class Program
    {
        public static Login fLog;
        public static SignUpForm fSignUpForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fLog = new Login();
            fSignUpForm = new SignUpForm();
            Application.Run(fLog);
        }
    }
}
