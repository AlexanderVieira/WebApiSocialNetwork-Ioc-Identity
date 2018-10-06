using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class ImageViewModel
    {        
        public Guid Id { get; set; }

        [Display(Name = "Imagem")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Nome")]
        public String Name { get; set; }

        [Display(Name = "Tipo")]
        public String Type { get; set; }

        [Display(Name = "Descrição")]
        public String Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Atualização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }
        public Guid GalleryId { get; set; }
        public virtual GalleryViewModel Gallery { get; set; }
        public Guid ProfileId { get; set; }
        public virtual ProfileViewModel Profile { get; set; }

    }
}