using System.Windows;
using EscolaApp.ViewModels;


namespace EscolaApp.Views
{
    /// <summary>
    /// Lógica interna para AlunoView.xaml
    /// </summary>
    public partial class AlunoView : Window
    {
        public AlunoView()
        {
            InitializeComponent();
            DataContext = new AlunoViewModel();
        }
    }
}
