using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using Perfumaria.Models;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Perfumaria.DB
{
    public class ClientesFunctions
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Perfumaria;Uid=root;Pwd=admin;");

        
        List<Clientes> listaClientes = new List<Clientes>();

        List<Clientes> clienteById = new List<Clientes>();
        public List<Clientes> GetClient()
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM Clientes";

                MySqlCommand comando = new MySqlCommand(query, conn);
                MySqlDataReader leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    Clientes cliente = new Clientes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        sobrenome = leitor["sobrenome"].ToString(),
                        email = leitor["email"].ToString(),
                        cpf = leitor["cpf"].ToString(),
                        endereco = leitor["endereco"].ToString()
                    };

                    listaClientes.Add(cliente);
                }
                leitor.Close();
                conn.Close();
                return listaClientes;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }

        public List<Clientes> GetClientById(int id)
        {
            try
            {
                conn.Open();

                string query = $"select * from Clientes where id={id}";
                MySqlCommand comando = new MySqlCommand(query, conn);
                MySqlDataReader leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    Clientes cliente = new Clientes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        sobrenome = leitor["sobrenome"].ToString(),
                        email = leitor["email"].ToString(),
                        cpf = leitor["cpf"].ToString(),
                        endereco = leitor["endereco"].ToString()
                    };
                    clienteById.Add(cliente);
                }
                leitor.Close();
                return clienteById;
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

        public bool SaveClient(Clientes cliente)
        {

            try
            {
                conn.Open();

                var query = $"insert into Clientes (name,sobrenome,email,cpf,endereco) " +
                            $"values ('{cliente.nome}','{cliente.sobrenome}','{cliente.email}','{cliente.cpf}','{cliente.endereco}')";

                MySqlCommand command = new MySqlCommand(query, conn);

                var linhasAfetadas = command.ExecuteNonQuery();

                return linhasAfetadas > 0;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }

        public List<Clientes> EditClient(int id,Clientes cliente)
        {
            try
            {
                conn.Open();

                var query = $"update Clientes set " +
                    $"name ='{cliente.nome}', sobrenome = '{cliente.sobrenome}', email='{cliente.email}', cpf='{cliente.cpf}', endereco='{cliente.endereco}'" +
                    $"where id = {id}";
                MySqlCommand command = new MySqlCommand(query, conn);

                var query2 = $"select * from Clientes where id={id}";
                MySqlCommand command2 = new MySqlCommand(query2, conn);
                MySqlDataReader leitor = command2.ExecuteReader();

                if (leitor.Read())
                {
                    Clientes clientes = new Clientes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        nome = leitor["name"].ToString(),
                        sobrenome = leitor["sobrenome"].ToString(),
                        email = leitor["email"].ToString(),
                        cpf = leitor["cpf"].ToString(),
                        endereco = leitor["endereco"].ToString()
                    };
                    clienteById.Add(clientes);
                }
                leitor.Close();
                return clienteById;
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("Erro ao executar o comando SQL: " + ex.Message);
            }
        }
    }
}
