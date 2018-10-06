using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Region.Models
{
    public class AddressBindingModel
    {
        public Guid Id { get; set; }        
        public String Street { get; set; }       
        public int Number { get; set; }        
        public String Neighborhood { get; set; }        
        public String City { get; set; }        
        public String PostalCode { get; set; }
    }
}