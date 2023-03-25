using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;
using Microsoft.AspNetCore.Http;
namespace webApiPractica.Controllers
{
    public class usuarioController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class usuariosController : ControllerBase
        {
            private readonly equiposContext _equiposContexto;

            public usuariosController(equiposContext usuariosContexto)
            {
                _equiposContexto = usuariosContexto;
            }
            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                var listausuarios = (from u in _equiposContexto.usuarios
                                     join c in _equiposContexto.carreras on u.carrera_id equals c.carrera_id
                                     select new
                                     {
                                         u.usuario_id,
                                         u.nombre,
                                         u.documento,
                                         u.tipo,
                                         u.carnet,
                                         u.carrera_id,
                                         c.nombre_carrera,
                                         u.estado
                                     }).ToList();
                if (listausuarios.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listausuarios);
            }
           
            [HttpPost]
            [Route("Add")]
            public IActionResult Guardarcomentario([FromBody] usuario come)
            {
                try
                {

                    _equiposContexto.usuarios.Add(come);
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
            public IActionResult actualizarcomentario(int id, [FromBody] usuario usuariosModificar)
            {
                usuario? carre = (from e in _equiposContexto.usuarios
                                   where e.usuario_id == id
                                   select e).FirstOrDefault();
                if (carre == null) return NotFound();

                carre.nombre = usuariosModificar.nombre;
                carre.documento = usuariosModificar.documento;
                carre.tipo = usuariosModificar.tipo;
                carre.carnet = usuariosModificar.carnet;
                carre.carrera_id = usuariosModificar.carrera_id;
                carre.estado = usuariosModificar.estado;


                _equiposContexto.Entry(carre).State = EntityState.Modified;
                _equiposContexto.SaveChanges();

                return Ok(usuariosModificar);




            }
            
            [HttpPut]
            [Route("eliminar/{id}")]
            public IActionResult Eliminarusuarios(int id)
            {
                usuario? carre = (from e in _equiposContexto.usuarios
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
}
