using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class TransportImageViewModel
    {
        public GalleryViewModel DetailsGVM { get; set; }
        public ImageViewModel DetailsIVM { get; set; }
    }
}