using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EZ_Sales_lisence;
//using SplashScreen;

namespace Sales
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
            //Application.Run(new frmMain());


            //SplashScreen.SplashScreen.ShowSplashScreen();
            //Application.DoEvents();
            //SplashScreen.SplashScreen.SetStatus("Loading module 1");
            //System.Threading.Thread.Sleep(500);
            //SplashScreen.SplashScreen.SetStatus("Loading module 2");
            //System.Threading.Thread.Sleep(300);
            //SplashScreen.SplashScreen.SetStatus("Loading module 3");
            //System.Threading.Thread.Sleep(900);
            //SplashScreen.SplashScreen.SetStatus("Loading module 4");
            //System.Threading.Thread.Sleep(100);
            //SplashScreen.SplashScreen.SetStatus("Loading module 5");
            //System.Threading.Thread.Sleep(400);
            //SplashScreen.SplashScreen.SetStatus("Loading module 6");
            //System.Threading.Thread.Sleep(50);
            //SplashScreen.SplashScreen.SetStatus("Loading module 7");
            //System.Threading.Thread.Sleep(240);
            //SplashScreen.SplashScreen.SetStatus("Loading module 8");
            //System.Threading.Thread.Sleep(900);
            //SplashScreen.SplashScreen.SetStatus("Loading module 9");
            //System.Threading.Thread.Sleep(240);
            //SplashScreen.SplashScreen.SetStatus("Loading module 10");
            //System.Threading.Thread.Sleep(90);
            //SplashScreen.SplashScreen.CloseForm();

            TrialMaker t = new TrialMaker("EZ_Sales_App_2010", Application.StartupPath + "\\RegFile.reg",
                Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\TMSetp.dbf",
                "Phone:+0903332010 \nMobile:+0909577079",
                30, 300, "66234879121237699861");

            byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
            4, 54, 87, 56, 123, 10, 3, 62,
            7, 9, 20, 36, 37, 21, 101, 57};
            t.TripleDESKey = MyOwnKey;

            TrialMaker.RunTypes RT = t.ShowDialog();
            bool is_trial;
            if (RT != TrialMaker.RunTypes.Expired)
            {
                if (RT == TrialMaker.RunTypes.Full)
                    is_trial = false;
               else
                    is_trial = true;

                Application.Run(new frmMain(is_trial));
            }            
        }
    }
}