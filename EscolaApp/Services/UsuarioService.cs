using EscolaApp.Data;
using EscolaApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaApp.Services
{
    public class UsuarioService
    {
        private readonly MySqlContext _context = new();

        public Usuario? Login(string user, string senha)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                SELECT id, username, perfil
                FROM usuarios
                WHERE username = @user
                  AND senha = SHA2(@senha, 256)", conn);

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@senha", senha);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new Usuario
            {
                Id = reader.GetInt32("id"),
                Username = reader.GetString("username"),
                Perfil = reader.GetString("perfil")
            };
        }

        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();

            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "SELECT id, username FROM usuarios", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username")
                });
            }

            return lista;
        }

        public void Inserir(Usuario usuario)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO usuarios (username, senha)
                VALUES (@user, SHA2(@senha, 256))", conn);

            cmd.Parameters.AddWithValue("@user", usuario.Username);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);

            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Usuario usuario)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                UPDATE usuarios 
                SET username = @user
                WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@user", usuario.Username);
            cmd.Parameters.AddWithValue("@id", usuario.Id);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "DELETE FROM usuarios WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void TrocarSenha(int id, string novaSenha)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
        UPDATE usuarios 
        SET senha = SHA2(@senha, 256)
        WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@senha", novaSenha);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

    }
}
