using MySql.Data.MySqlClient;
using Perfumaria.Models;

namespace Perfumaria.DB
{
    public class PerfumesFunctions
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Perfumaria;Uid=root;Pwd=admin;");

        List<Perfumes> listaPerfumes = new List<Perfumes>();
        public List<Perfumes> GetPerfumes()
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM Produtos";

                MySqlCommand comando = new MySqlCommand(query, conn);
                MySqlDataReader leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    Perfumes perfumes = new Perfumes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        descricao = leitor["description"].ToString(),
                        valor = Convert.ToDecimal(leitor["valor"]),
                        quantidade = Convert.ToInt32(leitor["quantidade"]),
                    };

                    listaPerfumes.Add(perfumes);
                }
                leitor.Close();
                conn.Close();
                return listaPerfumes;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }

        public List<Perfumes> GetPerfumesById(int id)
        {
            try
            {
                conn.Open();

                string query = $"select * from Produtos where id={id}";

                MySqlCommand comando = new MySqlCommand(query, conn);
                MySqlDataReader leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    Perfumes perfumes = new Perfumes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        descricao = leitor["description"].ToString(),
                        valor = Convert.ToDecimal(leitor["valor"]),
                        quantidade = Convert.ToInt32(leitor["quantidade"])
                    };
                    listaPerfumes.Add(perfumes);
                }
                leitor.Close();
                return listaPerfumes;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SavePerfumes(Perfumes perfumes)
        {
            
            try
            {
                conn.Open();

                string query = "insert into Produtos (name, description, quantidade, valor) " +
                            $"values ('{perfumes.nome}','{perfumes.descricao}',{perfumes.quantidade},";

                string valor = (perfumes.valor).ToString();
                valor = valor.Replace(',','.');

                query = query + $"'{valor}')";

                MySqlCommand command = new MySqlCommand(query, conn);

                var linhasAfetadas = command.ExecuteNonQuery();

                return linhasAfetadas > 0;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }

        public List<Perfumes> EditPerfume(int id, Perfumes perfumes)
        {
            try
            {
                conn.Open();

                string valor = (perfumes.valor).ToString();
                valor = valor.Replace(',', '.');

                var query = $"update Produtos set " +
                    $"name ='{perfumes.nome}', description = '{perfumes.descricao}', valor='{valor}', quantidade='{perfumes.quantidade}'" +
                    $"where id = {id}";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();

                var query2 = $"select * from Produtos where id={id}";
                MySqlCommand command2 = new MySqlCommand(query2, conn);
                MySqlDataReader leitor = command2.ExecuteReader();

                if (leitor.Read())
                {
                    Perfumes perfume = new Perfumes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        descricao = leitor["description"].ToString(),
                        valor = Convert.ToDecimal(leitor["valor"]),
                        quantidade = Convert.ToInt32(leitor["quantidade"])
                    };
                    listaPerfumes.Add(perfume);
                }
                leitor.Close();
                return listaPerfumes;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }

        public bool DeletePerfume(int id)
        {
            try
            {
                conn.Open();

                var query = $"delete from Produtos where id = {id}";

                MySqlCommand command = new MySqlCommand(query, conn);

                int linhasAfetadas = command.ExecuteNonQuery();

                return linhasAfetadas > 0;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }
    }
}
