using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BikeShop.Models.ViewModels
{
    public class ModelViewModel
    {
        //
        public Model Model { get; set; }
        //Used to display the list of makes in our drop down
        public IEnumerable<Make> Makes { get; set; }
        //rendering
        //
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Make> Items)
        {
            List<SelectListItem> Makelist = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem
            {
                Text = "----Select----",
                Value = "0"
            };
            Makelist.Add(sli);
            foreach (Make make in Items)
            {
                sli = new SelectListItem
                {
                    Text = make.Name,
                    Value = make.Id.ToString(),
                };
                Makelist.Add(sli);


            }
            return Makelist;

        }
    }
}
