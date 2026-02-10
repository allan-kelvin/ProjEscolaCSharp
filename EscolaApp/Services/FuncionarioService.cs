using EscolaApp.Data;
using EscolaApp.Models;
using MySqlConnector;


namespace EscolaApp.Services
{
    class FuncionarioService
    {

        private readonly MySqlContext _context = new();

        public List<Funcionario> Listar()
        {
            var lista = new List<Funcionario>();

            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM funcionarios", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Funcionario
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Cargo = reader.GetString("cargo"),
                    Email = reader.GetString("email")
                });
            }

            return lista;
        }

        public void Inserir(Funcionario funcionario)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO funcionarios (nome, cargo, email)
                VALUES (@nome, @cargo, @email)", conn);

            cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            cmd.Parameters.AddWithValue("@email", funcionario.Email);

            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Funcionario funcionario)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
                UPDATE funcionarios 
                SET nome = @nome, cargo = @cargo, email = @email
                WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            cmd.Parameters.AddWithValue("@email", funcionario.Email);
            cmd.Parameters.AddWithValue("@id", funcionario.Id);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _context.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "DELETE FROM funcionarios WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
