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
    class CursoService
    {
        private readonly MySqlContext _context = new();

        public List<Curso> Listar()
        {
            var lista = new List<Curso>();

            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM cursos", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Curso
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    CargaHoraria = reader.GetInt32("carga_horaria")
                });
            }

            return lista;
        }

        public void Inserir(Curso curso)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO cursos (nome, carga_horaria) VALUES (@nome, @carga)",
                conn);

            cmd.Parameters.AddWithValue("@nome", curso.Nome);
            cmd.Parameters.AddWithValue("@carga", curso.CargaHoraria);

            cmd.ExecuteNonQuery();

        }

        public void Atualizar(Curso curso)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "UPDATE cursos SET nome = @nome, carga_horaria = @carga WHERE id = @id",
                conn);

            cmd.Parameters.AddWithValue("@nome", curso.Nome);
            cmd.Parameters.AddWithValue("@carga", curso.CargaHoraria);
            cmd.Parameters.AddWithValue("@id", curso.Id);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "DELETE FROM cursos WHERE id = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
