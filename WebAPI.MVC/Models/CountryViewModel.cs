using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class CountryViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public String Name { get; set; }

        [Display(Name = "Bandeira")]
        public String Flag { get; set; }
        public virtual ICollection<StateViewModel> States { get; set; }
        public virtual ProfileViewModel Profile { get; set; }

    }
}