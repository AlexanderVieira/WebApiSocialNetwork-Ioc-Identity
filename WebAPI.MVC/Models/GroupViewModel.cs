using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Imagem")]
        public String Name { get; set; }

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

        [Display(Name = "Ativado")]
        public bool Actived { get; set; }
        public Guid ProfileId { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
        public virtual ICollection<ProfileViewModel> Friends { get; set; }
    }
}