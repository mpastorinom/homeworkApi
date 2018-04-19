using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homeworks.WebApi.Models
{
    public class FormatModel
    {
        [Required]
        public string Text { get; set;  }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Background { get; set; }
        [Required]
        [Range(2, 50)]

        public int FontSize { get; set; }

    }
}