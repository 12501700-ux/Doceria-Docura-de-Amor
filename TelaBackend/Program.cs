using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaBackend
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Usuario admin;
            using (var login = new FormLogin())
            {
                if (login.ShowDialog() != DialogResult.OK) return;
                admin = login.UsuarioLogado;
            }
            Application.Run(new FormProdutos(admin));
        }
    }
}
