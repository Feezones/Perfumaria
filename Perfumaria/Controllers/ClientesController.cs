using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Perfumaria.DB;
using Perfumaria.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Perfumaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // GET: api/<ClientesController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var cliente = new ClientesFunctions();
                var clientes = cliente.GetClient();
                return StatusCode(200, new { sucess = "Sucesso ao realizar consulta", clientes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro de banco de dados", message = ex.Message });

            }
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var cliente = new ClientesFunctions();
                var clientes = cliente.GetClientById(id);
                if(clientes.Count > 0)
                {
                    return StatusCode(200, new { sucess = "Sucesso ao realizar consulta", clientes });
                }
                return StatusCode(404, new { msg = "Não encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro de banco de dados", message = ex.Message });
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post(Clientes clientes)
        {
            try
            {
                var cliente = new ClientesFunctions();
                var clientesDb = cliente.SaveClient(clientes);

                if (clientesDb == true)
                {
                    return StatusCode(200, new { sucess = "Sucesso ao gravar dados", clientesDb });
                }

                return StatusCode(500, new { msg = "Erro desconhecido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro de banco de dados", message = ex.Message });
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id,Clientes clientes)
        {
            try
            {
                var cliente = new ClientesFunctions();
                var clientesDb = cliente.EditClient(id,clientes);

                return StatusCode(200, new { sucess = "Sucesso ao alterar dados", clientesDb });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro de banco de dados", message = ex.Message });
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var cliente = new ClientesFunctions();
                var clientesDb = cliente.DeleteClient(id);

                if (clientesDb == true)
                {
                    return StatusCode(200, new { sucess = "Cliente deletado com sucesso!", clientesDb });
                }

                return StatusCode(500, new { msg = "Erro desconhecido!" });

            }
            catch(Exception ex) 
            {
                return StatusCode(500, new { error = "Erro de banco de dados!", message = ex.Message });
            }
        }
    }
}
