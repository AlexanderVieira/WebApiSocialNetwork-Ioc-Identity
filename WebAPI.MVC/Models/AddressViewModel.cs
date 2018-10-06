using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Logradouro")]
        public String Street { get; set; }

        [Display(Name = "Número")]
        public int Number { get; set; }

        [Display(Name = "Bairro")]
        public String Neighborhood { get; set; }

        [Display(Name = "Cidade")]
        public String City { get; set; }

        [Display(Name = "Código Postal")]
        public String PostalCode { get; set; }
    }
}