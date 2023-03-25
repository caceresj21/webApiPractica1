using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public marcasController(equiposContext marcasContexto)
        {
            _equiposContexto = marcasContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<marca> listadomarcas = (from e in _equiposContexto.marcas
                                         select e).ToList();
            if (listadomarcas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadomarcas);
        }
        
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] marca come)
        {
            try
            {

                _equiposContexto.marcas.Add(come);
                _equiposContexto.SaveChanges();
                return Ok(come);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarcomentario(int id, [FromBody] marca marcasModificar)
        {
            marca? carre = (from e in _equiposContexto.marcas
                            where e.id_marcas == id
                            select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.nombre_marca = marcasModificar.nombre_marca;
            carre.estados = marcasModificar.estados;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(marcasModificar);




        }
        
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarmarcas(int id)
        {
            marca? carre = (from e in _equiposContexto.marcas
                            where e.id_marcas == id
                            select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estados = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }
    }
}
