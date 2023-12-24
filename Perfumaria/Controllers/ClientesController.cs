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
        public List<Clientes> Get(int id)
        {
            var cliente = new ClientesFunctions();
            var clientes = cliente.GetClientById(id);
            return clientes;
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
