using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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
            //string json = File.ReadAllText(".\\system.Zelda");
            //SystemProperties prop = 
            fLog = new Login();
            fSignUpForm = new SignUpForm();
            Application.Run(fLog);
        }
    }

    public sealed class SystemProperties
    {
        public string Host {  get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
