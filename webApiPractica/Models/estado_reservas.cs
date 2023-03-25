using System.ComponentModel.DataAnnotations;

namespace webApiPractica.Models
{
    public class estado_reservas
    {
        [Key]
        public int estado_res_id { get; set; }

        public string? estado { get; set; }
    }
}
