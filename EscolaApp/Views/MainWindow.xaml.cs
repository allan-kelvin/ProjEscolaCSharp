using EscolaApp.Services;
using EscolaApp.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscolaApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UsuarioLogado => $"Usuário: {SessaoUsuario.UsuarioLogado?.Username}";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            if (SessaoUsuario.UsuarioLogado?.Perfil != "ADMIN")
                menuUsuarios.Visibility = Visibility.Collapsed;

        }

        private void AbrirCursos_Click(object sender, RoutedEventArgs e)
        {
            var telaCursos = new CursoView();
            telaCursos.ShowDialog();
        }

        private void AbrirAlunos_Click(object sender, RoutedEventArgs e)
        {
            new AlunoView().ShowDialog();
        }

        private void AbrirFuncionarios_Click(object sender, RoutedEventArgs e)
        {
            new FuncionarioView().ShowDialog();
        }

        private void AbrirUsuarios_Click(object sender, RoutedEventArgs e)
        {
            new UsuarioView().ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show(
                "Deseja sair do sistema?",
                "Logout",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            var login = new LoginView();
            login.Show();
            this.Close();
        }

        private void TrocarSenha_Click(object sender, RoutedEventArgs e)
        {
            new TrocarSenhaView().ShowDialog();
        }

    }
}