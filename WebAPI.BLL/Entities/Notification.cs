using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Notification
    {
        [Key,Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid NotificationIssuerId { get; set; }

        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid NotifiedProfileId { get; set; }
        public string Message { get; set; }
        public DateTime? NotificationTime { get; set; }
        public bool WasRead { get; set; }

        public virtual Profile NotificationIssuer { get; set; }
        public virtual Profile NotifiedProfile { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid();
        }
    }
}