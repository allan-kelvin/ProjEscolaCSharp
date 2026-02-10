using EscolaApp.Data;
using EscolaApp.Models;
using MySqlConnector;

namespace EscolaApp.Services
{
    public class AlunoService
    {
        private readonly MySqlContext _context = new();

        public List<Aluno> Listar()
        {
            var lista = new List<Aluno>();

            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                SELECT a.id, a.nome, a.email, a.data_nascimento, a.curso_id, c.nome AS nome_curso
                FROM alunos a
                JOIN cursos c ON c.id = a.curso_id", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Aluno
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    DataNascimento = reader.GetDateTime("data_nascimento"),
                    CursoId = reader.GetInt32("curso_id"),
                    NomeCurso = reader.GetString("nome_curso")
                });
            }

            return lista;
        }

        public void Inserir(Aluno aluno)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO alunos (nome, email, data_nascimento, curso_id)
                VALUES (@nome, @email, @data, @curso)", conn);

            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@email", aluno.Email);
            cmd.Parameters.AddWithValue("@data", aluno.DataNascimento);
            cmd.Parameters.AddWithValue("@curso", aluno.CursoId);

            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Aluno aluno)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
        UPDATE alunos 
        SET nome = @nome,
            email = @email,
            data_nascimento = @data,
            curso_id = @curso
        WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@email", aluno.Email);
            cmd.Parameters.AddWithValue("@data", aluno.DataNascimento);
            cmd.Parameters.AddWithValue("@curso", aluno.CursoId);
            cmd.Parameters.AddWithValue("@id", aluno.Id);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "DELETE FROM alunos WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

    }
}
