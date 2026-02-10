using EscolaApp.Models;
using EscolaApp.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EscolaApp.ViewModels
{
    public class FuncionarioViewModel
    {

        private readonly FuncionarioService _service = new();

        public ObservableCollection<Funcionario> Funcionarios { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        private Funcionario? _funcionarioSelecionado;
        public Funcionario? FuncionarioSelecionado
        {
            get => _funcionarioSelecionado;
            set
            {
                _funcionarioSelecionado = value;

                if (value != null)
                {
                    Nome = value.Nome;
                    Cargo = value.Cargo;
                    Email = value.Email;
                }
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public FuncionarioViewModel()
        {
            Funcionarios = new ObservableCollection<Funcionario>(_service.Listar());

            SalvarCommand = new RelayCommand(Salvar);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);
        }

        private void Salvar()
        {
            if (string.IsNullOrWhiteSpace(Nome) || string.IsNullOrWhiteSpace(Cargo))
            {
                MessageBox.Show("Nome e cargo são obrigatórios.");
                return;
            }

            var funcionario = new Funcionario
            {
                Nome = Nome.Trim(),
                Cargo = Cargo.Trim(),
                Email = Email.Trim()
            };

            _service.Inserir(funcionario);
            Funcionarios.Add(funcionario);

            Limpar();
        }

        private void Editar()
        {
            if (FuncionarioSelecionado == null)
            {
                MessageBox.Show("Selecione um funcionário.");
                return;
            }

            FuncionarioSelecionado.Nome = Nome.Trim();
            FuncionarioSelecionado.Cargo = Cargo.Trim();
            FuncionarioSelecionado.Email = Email.Trim();

            _service.Atualizar(FuncionarioSelecionado);
        }

        private void Excluir()
        {
            if (FuncionarioSelecionado == null)
            {
                MessageBox.Show("Selecione um funcionário.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja excluir este funcionário?",
                "Confirmação",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            _service.Excluir(FuncionarioSelecionado.Id);
            Funcionarios.Remove(FuncionarioSelecionado);

            Limpar();
        }

        private void Limpar()
        {
            Nome = string.Empty;
            Cargo = string.Empty;
            Email = string.Empty;
        }
    }
}
