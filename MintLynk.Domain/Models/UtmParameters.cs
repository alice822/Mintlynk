using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Models
{
    public class UtmParameters
    {
        [Display(Name = "Source")]
        public string? Source { get; set; } = string.Empty;

        [Display(Name = "Medium")]
        public string? Medium { get; set; } = string.Empty;

        [Display(Name = "Campaign")]
        public string? Campaign { get; set; } = string.Empty;

        [Display(Name = "Content")]
        public string? Content { get; set; } = string.Empty;

        [Display(Name = "Term")]
        public string? Term { get; set; } = string.Empty;
    }
}
