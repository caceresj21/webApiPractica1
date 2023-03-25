using System.ComponentModel.DataAnnotations;

namespace webApiPractica.Models
{
    public class marca
    {
        [Key]
        public int id_marcas { get; set; }

        public string? nombre_marca { get; set; }

        public string? estados { get; set; }
    }
}
