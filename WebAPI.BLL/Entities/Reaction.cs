using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Reaction
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ReactionOwnerId { get; set; }

        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ReactionPostId { get; set; }
        public string Text { get; set; }
        public string IconUri { get; set; }
        public virtual Profile ReactionOwner { get; set; }
        public virtual Post ReactionPost { get; set; }

        public Reaction()
        {
            Id = Guid.NewGuid();
        }
    }
}