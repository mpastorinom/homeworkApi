using Homeworks.WebApi.Binders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Homeworks.WebApi.Models
{
    [ModelBinder(typeof(FormatModelBinder))]
    public class Format
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


        public static bool TryParse(string s, out Format result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 4)
            {
                return false;
            }

            string text = parts[0];
            string color = parts[1];
            string background = parts[2];
            int size; 
            if (int.TryParse(parts[3], out size))
            {
                result = new Format() { Text = text, Color = color, Background = background, FontSize = size };
                return true;
            }
            return false;
        }
    }
}