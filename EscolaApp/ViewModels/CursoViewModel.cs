using CommunityToolkit.Mvvm.Input;
using EscolaApp.Models;
using EscolaApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EscolaApp.ViewModels
{
    class CursoViewModel
    {
        private readonly CursoService _service = new();
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }


        // Lista exibida no DataGrid
        public ObservableCollection<Curso> Cursos { get; set; }

        // Campos do formulário
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }

        // Command do botão Salvar
        public ICommand SalvarCommand { get; }

        public CursoViewModel()
        {
            Cursos = new ObservableCollection<Curso>(_service.Listar());
            SalvarCommand = new RelayCommand(Salvar);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);

        }

        private void Salvar()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                MessageBox.Show(
                    "Informe o nome do curso.",
                    "Validação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                    );
                return;
            }
            var curso = new Curso
            {
                Nome = Nome.Trim(),
                CargaHoraria = CargaHoraria
            };

            _service.Inserir(curso );

            Cursos.Add(curso);

            //Limpa os campos

            Nome = string.Empty;
            CargaHoraria = 0;
        }

        private void Editar()
        {
            if (CursoSelecionado == null)
            {
                MessageBox.Show("Selecione um curso para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Nome) || CargaHoraria <= 0)
            {
                MessageBox.Show("Preencha corretamente os campos.");
                return;
            }

            CursoSelecionado.Nome = Nome;
            CursoSelecionado.CargaHoraria = CargaHoraria;

            _service.Atualizar(CursoSelecionado);
        }

        private void Excluir()
        {
            if (CursoSelecionado == null)
            {
                MessageBox.Show("Selecione um curso para excluir.");
                return;
            }

            var confirmar = MessageBox.Show(
                "Deseja realmente excluir este curso?",
                "Confirmação",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmar != MessageBoxResult.Yes)
                return;

            _service.Excluir(CursoSelecionado.Id);
            Cursos.Remove(CursoSelecionado);

            Nome = string.Empty;
            CargaHoraria = 0;
        }


        private Curso _cursoSelecionado;
        public Curso CursoSelecionado
        {
            get => _cursoSelecionado;
            set
            {
                _cursoSelecionado = value;

                if (value != null)
                {
                    Nome = value.Nome;
                    CargaHoraria = value.CargaHoraria;
                }
            }
        }

    }
}
