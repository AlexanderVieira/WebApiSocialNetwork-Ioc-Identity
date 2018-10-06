using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class State
    {
        [Key, ForeignKey("Profile")]
        public Guid Id { get; set; }

        [Column("Nome")]
        public String Name { get; set; }

        [Column("Bandeira")]
        public String Flag { get; set; }        
        public virtual Profile Profile { get; set; }

        public State()
        {
            Id = Guid.NewGuid();
        }
    }
}
