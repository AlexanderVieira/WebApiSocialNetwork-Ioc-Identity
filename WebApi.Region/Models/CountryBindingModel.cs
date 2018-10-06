using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Region.Models
{
    public class CountryBindingModel
    {
        public Guid Id { get; set; }        
        public String Name { get; set; }        
        public String Flag { get; set; }
    }
}