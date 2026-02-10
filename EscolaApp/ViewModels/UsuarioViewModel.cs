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
   public  class UsuarioViewModel
    {
        private readonly UsuarioService _service = new();

        public ObservableCollection<Usuario> Usuarios { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        private Usuario? _usuarioSelecionado;
        public Usuario? UsuarioSelecionado
        {
            get => _usuarioSelecionado;
            set
            {
                _usuarioSelecionado = value;
                if (value != null)
                {
                    Username = value.Username;
                    Senha = string.Empty; // nunca carregar senha
                }
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand ExcluirCommand { get; }

        public UsuarioViewModel()
        {
            Usuarios = new ObservableCollection<Usuario>(_service.Listar());

            SalvarCommand = new RelayCommand(Salvar);
            EditarCommand = new RelayCommand(Editar);
            ExcluirCommand = new RelayCommand(Excluir);
        }

        private void Salvar()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Senha))
            {
                MessageBox.Show("Usuário e senha são obrigatórios.");
                return;
            }

            var usuario = new Usuario
            {
                Username = Username.Trim(),
                Senha = Senha
            };

            _service.Inserir(usuario);
            Usuarios.Add(usuario);

            Limpar();
        }

        private void Editar()
        {
            if (UsuarioSelecionado == null)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }

            UsuarioSelecionado.Username = Username.Trim();
            _service.Atualizar(UsuarioSelecionado);
        }

        private void Excluir()
        {
            if (UsuarioSelecionado == null)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }

            var confirm = MessageBox.Show(
                "Deseja excluir este usuário?",
                "Confirmação",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            _service.Excluir(UsuarioSelecionado.Id);
            Usuarios.Remove(UsuarioSelecionado);

            Limpar();
        }

        private void Limpar()
        {
            Username = string.Empty;
            Senha = string.Empty;
        }

    }
}
