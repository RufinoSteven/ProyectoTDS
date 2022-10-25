using System.ComponentModel.DataAnnotations;

namespace MainService.Models
{
    public class Items 
    {
        [Key]
        public int id  { get; set; }

        public string itemName { get; set; }
        public string unidad { get; set; }
        public decimal price { get; set; }
        public string descripcion { get; set; }
        public string familia { get ; set; }
        public int codSap { get; set; }




    }
}
