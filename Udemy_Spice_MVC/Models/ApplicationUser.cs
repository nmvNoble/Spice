﻿using Microsoft.AspNetCore.Identity;//added
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy_Spice_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}
