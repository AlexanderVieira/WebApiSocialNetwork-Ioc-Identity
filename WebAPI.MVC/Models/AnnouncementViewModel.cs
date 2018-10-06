using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class AnnouncementViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Título")]
        public String Title { get; set; }

        [Display(Name = "Foto")]
        public String PhotoUrl { get; set; }

        [Display(Name = "Descrição")]
        public String Description { get; set; }

        [Display(Name = "Preço")]
        public Decimal Price { get; set; }

        [Display(Name = "Ativado")]
        public bool Actived { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Atualização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }
        public virtual ImageViewModel Image { get; set; }
        public Guid MarketplaceId { get; set; }
        public virtual MarketplaceViewModel Marketplace { get; set; }
        public Guid ProfileId { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
    }
}