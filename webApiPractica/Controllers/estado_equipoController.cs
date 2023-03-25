using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;
using Microsoft.AspNetCore.Http;



namespace webApiPractica.Controllers
{
    public class estado_equipoController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class estados_equipoController : ControllerBase
        {
            private readonly equiposContext _equiposContexto;

            public estados_equipoController(equiposContext estados_equipoContexto)
            {
                _equiposContexto = estados_equipoContexto;
            }
            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                List<estados_equipos> listadoestados_equipo = (from e in _equiposContexto.estados_equipo
                                                               select e).ToList();
                if (listadoestados_equipo.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoestados_equipo);
            }
            
            [HttpPost]
            [Route("Add")]
            public IActionResult Guardarcomentario([FromBody] estados_equipos equi)
            {
                try
                {

                    _equiposContexto.estados_equipo.Add(equi);
                    _equiposContexto.SaveChanges();
                    return Ok(equi);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            
            [HttpPut]
            [Route("actualizar/{id}")]
            public IActionResult actualizarcomentario(int id, [FromBody] estados_equipos estados_equipoModificar)
            {
                estados_equipos? carre = (from e in _equiposContexto.estados_equipo
                                          where e.id_estados_equipo == id
                                          select e).FirstOrDefault();
                if (carre == null) return NotFound();


                carre.descripcion = estados_equipoModificar.descripcion;
                carre.estado = estados_equipoModificar.estado;


                _equiposContexto.Entry(carre).State = EntityState.Modified;
                _equiposContexto.SaveChanges();

                return Ok(estados_equipoModificar);




            }
           
            [HttpPut]
            [Route("eliminar/{id}")]
            public IActionResult Eliminarestados_equipo(int id)
            {
                estados_equipos? carre = (from e in _equiposContexto.estados_equipo
                                          where e.id_estados_equipo == id
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
