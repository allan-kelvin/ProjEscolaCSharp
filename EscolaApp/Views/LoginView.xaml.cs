using EscolaApp.Services;
using System.Windows;
using System.Windows.Input;


namespace EscolaApp.Views
{
    public partial class LoginView : Window
    {
        private readonly UsuarioService _service = new();

        public LoginView()
        {
            InitializeComponent();
        }
        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            var usuario = txtUsuario.Text;
            var senha = txtSenha.Password;

            var user = _service.Login(usuario, senha);

            if (user != null)
            {
                SessaoUsuario.UsuarioLogado = user;

                new MainWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show(
                    "Usuário ou senha inválidos.",
                    "Login",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Entrar_Click(btnEntrar, null!);
            }
        }
    }
}
