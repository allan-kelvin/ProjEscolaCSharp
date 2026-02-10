using EscolaApp.Models;
using EscolaApp.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;


namespace EscolaApp.ViewModels
{
    public class AlunoViewModel
    {

        private readonly AlunoService _alunoService = new();
        private readonly CursoService _cursoService = new();

        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }


        public ObservableCollection<Aluno> Alunos { get; set; }
        public ObservableCollection<Curso> Cursos { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; } = DateTime.Today;

        public Curso? CursoSelecionado { get; set; }

        private Aluno? _alunoSelecionado;

        public Aluno? AlunoSelecionado
        {
            get => _alunoSelecionado;
            set
            {
                _alunoSelecionado = value;

                if (value != null)
                {
                    Nome = value.Nome;
                    Email = value.Email;
                    DataNascimento = value.DataNascimento;
                    CursoSelecionado = Cursos.FirstOrDefault(c => c.Id == value.CursoId);
                }
            }
        }


        public ICommand SalvarCommand { get; }

        public AlunoViewModel()
        {
            Alunos = new ObservableCollection<Aluno>(_alunoService.Listar());
            Cursos = new ObservableCollection<Curso>(_cursoService.Listar());
            SalvarCommand = new RelayCommand(Salvar);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);

        }

        private void Salvar()
        {
            if (string.IsNullOrWhiteSpace(Nome) || CursoSelecionado == null)
            {
                MessageBox.Show("Preencha o nome e selecione um curso.");
                return;
            }

            var aluno = new Aluno
            {
                Nome = Nome.Trim(),
                Email = Email.Trim(),
                DataNascimento = DataNascimento,
                CursoId = CursoSelecionado.Id,
                NomeCurso = CursoSelecionado.Nome
            };

            _alunoService.Inserir(aluno);
            Alunos.Add(aluno);

            Nome = string.Empty;
            Email = string.Empty;
            DataNascimento = DateTime.Today;
            CursoSelecionado = null;
        }

        private void Editar()
        {
            if (AlunoSelecionado == null)
            {
                MessageBox.Show("Selecione um aluno para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Nome) || CursoSelecionado == null)
            {
                MessageBox.Show("Preencha os campos obrigatórios.");
                return;
            }

            AlunoSelecionado.Nome = Nome.Trim();
            AlunoSelecionado.Email = Email.Trim();
            AlunoSelecionado.DataNascimento = DataNascimento;
            AlunoSelecionado.CursoId = CursoSelecionado.Id;
            AlunoSelecionado.NomeCurso = CursoSelecionado.Nome;

            _alunoService.Atualizar(AlunoSelecionado);
        }

        private void Excluir()
        {
            if (AlunoSelecionado == null)
            {
                MessageBox.Show("Selecione um aluno para excluir.");
                return;
            }

            var confirmar = MessageBox.Show(
                "Deseja realmente excluir este aluno?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmar != MessageBoxResult.Yes)
                return;

            _alunoService.Excluir(AlunoSelecionado.Id);
            Alunos.Remove(AlunoSelecionado);

            Nome = string.Empty;
            Email = string.Empty;
            DataNascimento = DateTime.Today;
            CursoSelecionado = null;
        }


    }
}
