using EscolaApp.ViewModels;
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
    /// Lógica interna para UsuarioView.xaml
    /// </summary>
    public partial class UsuarioView : Window
    {
        public UsuarioView()
        {
            InitializeComponent();
            DataContext = new UsuarioViewModel();
        }

        private void SalvarSenha_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsuarioViewModel vm)
                vm.Senha = txtSenha.Password;
        }
    }
}
