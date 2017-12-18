/* ETML
 * 
 * Auteur : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Main
 * 
 * 
 * */
using System;
using System.Windows.Forms;

namespace BruteForce
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BruteForceView());
        }
    }
}
