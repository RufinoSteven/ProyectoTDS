using System.ComponentModel.DataAnnotations;

namespace MainService.Models
{
    public class Detalle
    {
        [Key]
        public int id { get; set; }
        public int analisis_id { get; set; }
        public int item_id { get; set; }
        public long qty { get; set; }
    }
}
