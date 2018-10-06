using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Address
    {
        [Key, ForeignKey("Profile")]
        public Guid Id { get; set; }

        [Column("Logradouro")]
        public String Street { get; set; }

        [Column("Numero")]
        public int Number { get; set; }

        [Column("Bairro")]
        public String Neighborhood { get; set; }

        [Column("Cidade")]
        public String City { get; set; }        

        [Column("Cep")]
        public String PostalCode { get; set; }
        public virtual Profile Profile { get; set; }

        public Address()
        {
            Id = Guid.NewGuid();
        }
    }
}
