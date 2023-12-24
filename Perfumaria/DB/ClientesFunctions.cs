using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using Perfumaria.Models;

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
                        name = leitor["name"].ToString(),
                        sobrenome = leitor["sobrenome"].ToString(),
                        email = leitor["email"].ToString(),
                        cpf = leitor["cpf"].ToString(),
                        endereco = leitor["endereco"].ToString()
                    };

                    listaClientes.Add(cliente);
                }
                leitor.Close();
                return listaClientes;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao executar o comando SQL: " + ex.Message);
                return listaClientes;
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Clientes> GetClientById(int id)
        {
            try
            {
                conn.Open() ;

                string query = $"select * from Clientes where id={id}";
                MySqlCommand comando = new MySqlCommand(query, conn);
                MySqlDataReader leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    Clientes cliente = new Clientes
                    {
                        id = Convert.ToInt32(leitor["id"]),
                        name = leitor["name"].ToString(),
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
            catch
            {
                return clienteById;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
