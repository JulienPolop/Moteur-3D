using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Projet_IMA
{
    static class Program
    {
        static public Form1 MyForm;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyForm = new Form1();
            Application.Run(MyForm);
        }
    }
}
