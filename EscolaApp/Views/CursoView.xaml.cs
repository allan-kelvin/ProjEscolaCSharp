using EscolaApp.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace EscolaApp.Views
{
    /// <summary>
    /// Lógica interna para CursoView.xaml
    /// </summary>
    public partial class CursoView : Window
    {
        public CursoView()
        {
            InitializeComponent();
            DataContext = new CursoViewModel();

        }

        private void SoNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
