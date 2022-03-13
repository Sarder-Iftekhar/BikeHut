using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Models
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Office phone")]
        public string PhoneNumber2 { get; set; }

        //As we do not need to edit to database
        //so use not mapped data anotation

        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}
