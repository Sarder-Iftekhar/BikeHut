using System.ComponentModel.DataAnnotations;

namespace BikeShop.Models
{
    public class Model
    {

        public int Id { get; set; }
        //[Required]
        //[StringLength(255)]
        //[DisplayName("Model")]
        //Apply constraint 
        //Data anoations
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Make Make { get; set; }

        //Foreign key can write this way 

        public int MakeID { get; set; }




    }
}
