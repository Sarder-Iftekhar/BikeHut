using System.ComponentModel.DataAnnotations;

namespace BikeShop.Models
{
    public class Make
    {
        public int Id { get; set; }
        //Apply conmstraint
        //data anotations
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        //public ICollection<Model> Models { get; set; }
    }
}
