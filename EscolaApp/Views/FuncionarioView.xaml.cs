using EscolaApp.ViewModels;
using System.Windows;


namespace EscolaApp.Views
{

    public partial class FuncionarioView : Window
    {
        public FuncionarioView()
        {
            InitializeComponent();
            DataContext = new FuncionarioViewModel();
        }
    }
}
