using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameShop.ViewModels.Catalog.Publishers
{
    public class PublisherCreateDTO
    {
        [Required]
        public string Name { get; set; }
        
    }
}
