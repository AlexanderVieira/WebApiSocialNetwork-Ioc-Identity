using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BLL.Entities
{
    public class FriendShip
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RequestedById { get; set; }
        
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RequestedToId { get; set; }
        public DateTime? RequestTime { get; set; }
        public StatusEnum Status { get; set; }
        public virtual Profile RequestedBy { get; set; }
        public virtual Profile RequestedTo { get; set; }       

        public FriendShip()
        {
            Id = Guid.NewGuid();
        }
		
	}

    public enum StatusEnum
    {
        Pendent = 0,
        Accepted = 1,
        Rejected = 2,
        Blocked = 3        
    };
}