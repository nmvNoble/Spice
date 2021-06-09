using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class PagingInfo
    {
        public int totalItem { get; set; }

        public int itemsPerPage { get; set; }

        public int currentPage { get; set; }

        public int totalPage => (int)Math.Ceiling((decimal)totalItem / itemsPerPage);

        public string urlParam { get; set; }
    }
}
