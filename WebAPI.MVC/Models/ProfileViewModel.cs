using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class ProfileViewModel
    {
        
        public Guid Id { get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo Nome é obrigatório!")]
        [StringLength(150, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 4)]
        public String FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "O Campo Sobrenome é obrigatório!")]
        [StringLength(150, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 4)]
        public String LastName { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public String Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Foto")]
        public String PhotoUrl { get; set; }
        public Guid CountryId { get; set; }
        [Display(Name = "País")]
        public virtual CountryViewModel Country { get; set; }
        public Guid StateId { get; set; }

        [Display(Name = "Estado")]
        public virtual StateViewModel State { get; set; }
        public Guid AddressId { get; set; }

        [Display(Name = "Endereço")]
        public virtual AddressViewModel Address { get; set; }
        public virtual MarketplaceViewModel Marketplace { get; set; }
        public virtual ICollection<FriendShipViewModel> FriendShips { get; set; }
        public virtual ICollection<FriendShipViewModel> Friends { get; set; }
        public virtual ICollection<GroupViewModel> Groups { get; set; }
        public virtual ICollection<GalleryViewModel> Galleries { get; set; }
        public virtual ICollection<ImageViewModel> Images { get; set; }
        public virtual ICollection<AnnouncementViewModel> Announcements { get; set; }
        public virtual ICollection<NotificationViewModel> Notifications { get; set; }
        public virtual ICollection<PostViewModel> Posts { get; set; }
        public virtual ICollection<ReactionViewModel> Reactions { get; set; }       
        
    }
}