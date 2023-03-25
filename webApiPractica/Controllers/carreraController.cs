using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using webApiPractica.Models;


using Microsoft.EntityFrameworkCore;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carreraController : Controller
    {
        private readonly equiposContext _equiposContexto;

        public carreraController(equiposContext carrerasContexto)
        {
            _equiposContexto = carrerasContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listadocarreras = (from c in _equiposContexto.carreras
                                   join f in _equiposContexto.facultades on c.facultad_id equals f.facultad_id

                                   select new
                                   {
                                       c.carrera_id,
                                       c.nombre_carrera,
                                       c.facultad_id,
                                       f.nombre_facultad,
                                       c.estado
                                   }).ToList();
            if (listadocarreras.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadocarreras);
        }
        
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] carreras come)
        {
            try
            {

                _equiposContexto.carreras.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] carreras carrerasModificar)
        {
            carreras? carre = (from e in _equiposContexto.carreras
                               where e.carrera_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.nombre_carrera = carrerasModificar.nombre_carrera;
            carre.facultad_id = carrerasModificar.facultad_id;
            carre.estado = carrerasModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carrerasModificar);




        }
        
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarcarreras(int id)
        {
            carreras? carre = (from e in _equiposContexto.carreras
                               where e.carrera_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }
}
