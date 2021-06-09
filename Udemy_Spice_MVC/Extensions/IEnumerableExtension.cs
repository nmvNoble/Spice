using Microsoft.AspNetCore.Mvc.Rendering;//added
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("name"),
                       Value = item.GetPropertyValue("id"),
                       Selected = item.GetPropertyValue("id").Equals(selectedValue.ToString())
                   };
        }
    }
}
