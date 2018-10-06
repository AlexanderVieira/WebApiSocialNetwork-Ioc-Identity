using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebApi.Models
{
    public class StateBindingModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Flag { get; set; }
        public Guid CountryId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
    }
}