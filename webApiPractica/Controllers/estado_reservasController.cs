using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;
using Microsoft.AspNetCore.Http;


namespace webApiPractica.Controllers
{
    public class estado_reservasController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class estados_reservaController : ControllerBase
        {
            private readonly equiposContext _equiposContexto;

            public estados_reservaController(equiposContext estado_reservasContexto)
            {
                _equiposContexto = estado_reservasContexto;
            }
            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                List<estado_reservas> listadoestados_reserva = (from e in _equiposContexto.estados_reserva
                                                                select e).ToList();
                if (listadoestados_reserva.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoestados_reserva);
            }
            
            [Route("Add")]
            public IActionResult Guardarcomentario([FromBody] estado_reservas come)
            {
                try
                {

                    _equiposContexto.estados_reserva.Add(come);
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
            public IActionResult actualizarcomentario(int id, [FromBody] estado_reservas estados_reservaModificar)
            {
                estado_reservas? carre = (from e in _equiposContexto.estados_reserva
                                          where e.estado_res_id == id
                                          select e).FirstOrDefault();
                if (carre == null) return NotFound();


                carre.estado = estados_reservaModificar.estado;


                _equiposContexto.Entry(carre).State = EntityState.Modified;
                _equiposContexto.SaveChanges();

                return Ok(estados_reservaModificar);




            }
            
            [HttpPut]
            [Route("eliminar/{id}")]
            public IActionResult Eliminarestados_reserva(int id)
            {
                estado_reservas? carre = (from e in _equiposContexto.estados_reserva
                                          where e.estado_res_id == id
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
