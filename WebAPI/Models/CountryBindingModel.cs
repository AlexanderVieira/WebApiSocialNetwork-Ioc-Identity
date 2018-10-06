using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebAPI.Models
{
    public class CountryBindingModel
    {
        public Guid Id { get; set; }        
        public String Name { get; set; }        
        public String Flag { get; set; }       
        public virtual ICollection<StateBindingModel> States { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
    }
}