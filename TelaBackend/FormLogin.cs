using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaBackend
{
    public partial class FormLogin : Form
    {
        public Usuario UsuarioLogado { get; private set; }

        public FormLogin()
        {
            InitializeComponent();
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            btnEntrar.Enabled = false;
            try
            {
                var u = await Api.Login(txtEmail.Text.Trim(), txtSenha.Text);
                if (u == null)
                {
                    MessageBox.Show("E-mail ou senha inválidos, ou conta sem permissão de administrador.");
                    return;
                }
                UsuarioLogado = u;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar na API: " + ex.Message);
            }
            finally
            {
                btnEntrar.Enabled = true;
            }
        }
    }
}
