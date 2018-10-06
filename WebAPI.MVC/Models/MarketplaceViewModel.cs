using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.MVC.Models
{
    public class MarketplaceViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Título")]
        public String Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Atualização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }
        public Guid ProfileId { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
        public virtual ICollection<AnnouncementViewModel> Announcements { get; set; }
        
    }
}