using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Domain.Entities
{
    public class State
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //[Column("Nome")]
        public String Name { get; set; }

        //[Column("Bandeira")]
        public String Flag { get; set; }

        public State()
        {
            Id = Guid.NewGuid();
        }
    }
}
