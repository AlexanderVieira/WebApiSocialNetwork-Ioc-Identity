using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Domain.Entities
{
    public class Address
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //[Column("Logradouro")]
        public String Street { get; set; }

        //[Column("Numero")]
        public int Number { get; set; }

        //[Column("Bairro")]
        public String Neighborhood { get; set; }

        //[Column("Cidade")]
        public String City { get; set; }        

        //[Column("Cep")]
        public String PostalCode { get; set; }       
        
        public Address()
        {
            Id = Guid.NewGuid();
        }
    }
}
