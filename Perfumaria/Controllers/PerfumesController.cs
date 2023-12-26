using Microsoft.AspNetCore.Mvc;
using Perfumaria.DB;
using Perfumaria.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Perfumaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumesController : ControllerBase
    {
        // GET: api/<PerfumesController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var perfume = new PerfumesFunctions();
                var perfumes = perfume.GetPerfumes();
                return StatusCode(200, new { sucess = "Sucesso ao realizar consulta", perfumes });
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
                var perfume = new PerfumesFunctions();
                var perfumesId = perfume.GetPerfumesById(id);
                if (perfumesId.Count > 0)
                {
                    return StatusCode(200, new { sucess = "Sucesso ao realizar consulta", perfumesId });
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
        public ActionResult Post(Perfumes perfumes)
        {
            try
            {
                var perfume = new PerfumesFunctions();
                var perfumesDb = perfume.SavePerfumes(perfumes);

                if (perfumesDb == true)
                {
                    return StatusCode(200, new { sucess = "Sucesso ao gravar dados", perfumesDb });
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
        public ActionResult Put(int id, Perfumes perfumes)
        {
            try
            {
                var perfume = new PerfumesFunctions();
                var perfumesDb = perfume.EditPerfume(id, perfumes);

                return StatusCode(200, new { sucess = "Sucesso ao alterar dados", perfumesDb });
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
                var perfume = new PerfumesFunctions();
                var perfumesDb= perfume.DeletePerfume(id);

                if (perfumesDb == true)
                {
                    return StatusCode(200, new { sucess = "Cliente deletado com sucesso!", perfumesDb });
                }

                return StatusCode(500, new { msg = "Erro desconhecido!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro de banco de dados!", message = ex.Message });
            }
        }
    }
}
