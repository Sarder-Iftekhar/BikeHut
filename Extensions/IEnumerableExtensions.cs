using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> Items, int selectedValue = 0)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            List.Add(sli);
            foreach (var Item in Items)
            {
                sli = new SelectListItem
                {

                    //Text = Item.GetType().GetProperty("Name").GetValue(Item,null).ToString(),
                    //Value = Item.GetType().GetProperty("Id").GetValue(Item, null).ToString(),
                    //Selected = Item.GetPropertyValue("Id").Equals(selectedValue.ToString()),
                    //Too long,just make short using Reflection extension

                    Text = Item.GetPropertyValue("Name"),
                    Value = Item.GetPropertyValue("Id"),
                    //Selected = Item.GetPropertyValue("Id").Equals(selectedValue.ToString())

                };
                List.Add(sli);
            }
            return List;
        }
    }
}