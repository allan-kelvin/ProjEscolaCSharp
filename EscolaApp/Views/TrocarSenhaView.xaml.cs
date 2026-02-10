using EscolaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EscolaApp.Views
{
    /// <summary>
    /// Lógica interna para TrocarSenhaView.xaml
    /// </summary>
    public partial class TrocarSenhaView : Window
    {
        private readonly UsuarioService _service = new();
        public TrocarSenhaView()
        {
            InitializeComponent();
        }
        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNovaSenha.Password != txtConfirmar.Password)
            {
                MessageBox.Show("As senhas não conferem.");
                return;
            }

            _service.TrocarSenha(
                SessaoUsuario.UsuarioLogado!.Id,
                txtNovaSenha.Password);

            MessageBox.Show("Senha alterada com sucesso!");
            Close();
        }
    }
}
