using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;
using Microsoft.AspNetCore.Http;
namespace webApiPractica.Controllers
{
    public class tipo_equiposController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class tipo_equipoController : ControllerBase
        {
            private readonly equiposContext _equiposContexto;

            public tipo_equipoController(equiposContext tipo_equipoContexto)
            {
                _equiposContexto = tipo_equipoContexto;
            }
            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                List<tipo_equipos> listadotipo_equipo = (from e in _equiposContexto.tipo_equipo
                                                         select e).ToList();
                if (listadotipo_equipo.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadotipo_equipo);
            }
           
            [HttpPost]
            [Route("Add")]
            public IActionResult Guardarcomentario([FromBody] tipo_equipos come)
            {
                try
                {

                    _equiposContexto.tipo_equipo.Add(come);
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
            public IActionResult actualizarcomentario(int id, [FromBody] tipo_equipos tipo_equipoModificar)
            {
                tipo_equipos? carre = (from e in _equiposContexto.tipo_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();
                if (carre == null) return NotFound();


                carre.descripcion = tipo_equipoModificar.descripcion;
                carre.estado = tipo_equipoModificar.estado;


                _equiposContexto.Entry(carre).State = EntityState.Modified;
                _equiposContexto.SaveChanges();

                return Ok(tipo_equipoModificar);




            }
            [HttpPut]
            [Route("eliminar/{id}")]
            public IActionResult Eliminartipo_equipo(int id)
            {
                tipo_equipos? carre = (from e in _equiposContexto.tipo_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();
                if (carre == null) return NotFound();

                carre.estado = "I";
                _equiposContexto.Entry(carre).State = EntityState.Modified;
                _equiposContexto.SaveChanges();

                return Ok(carre);
            }
        }
    }
}
