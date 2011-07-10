using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chashavshavon
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private extern static bool InternetGetConnectedState(ref InternetConnectionState_e lpdwFlags, int dwReserved);

        [Flags]
        enum InternetConnectionState_e : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            if(string.IsNullOrEmpty(Properties.Settings.Default.ChashFilesPath))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Chashavshavon Files";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                Properties.Settings.Default.ChashFilesPath = path;
                Properties.Settings.Default.Save();
            }
            if (args.Length > 0)
            {
                Application.Run(new frmMain(args[0]));
            }
            else
            {
                Application.Run(new frmMain());
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            System.IO.File.AppendAllText(System.IO.Directory.GetCurrentDirectory() + "\\ErrorLog.csv",
                "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"" +
                (e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message) +
                "\"" + Environment.NewLine);
            MessageBox.Show("���� �����" + "\n\n" + (e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message),
                            "�����",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        public static bool IsConnectedToInternet()
        {
            InternetConnectionState_e flags = 0;
            return InternetGetConnectedState(ref flags, 0);
        }

        #region Extention Methods
        /// <summary>
        /// Tests to see if an object equals any one of the objects in a list.
        /// This function works like the SQL keyword "IN" - "SELECT * FROM Orders WHERE OrderId IN (5432, 9886, 8824)".
        /// </summary>
        /// <param name="test">Object to be searched for - is supplied by the compiler using the caller object</param>
        /// <param name="list">List of objects to search in.</param>
        /// <returns></returns>		
        public static bool In(this Object o, params object[] list)
        {
            return Array.IndexOf(list, o) > -1;
        }

        /// <summary>
        /// Determines if the two given DateTime object refer to the same day.
        /// NOTE: This function was created for the situation where the time factor 
        /// of the given DateTime objects is not a factor in determinig if they are the same;
        /// if they refer to the same date, the function returns true.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public static bool IsSameday(this DateTime firstDate, DateTime secondDate)
        {
            bool isSameDate = false;
            if (firstDate.Year == secondDate.Year
                &&
                firstDate.Month == secondDate.Month
                &&
                firstDate.Day == secondDate.Day)
            {
                isSameDate = true;
            }
            return isSameDate;
        }
        #endregion
    }
}