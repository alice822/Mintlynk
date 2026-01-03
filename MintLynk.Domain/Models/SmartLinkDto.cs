using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Models
{
    public class SmartLinkDto
    {
        public long Id { get; set; }
        public string EntityId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string DestinationUrl { get; set; }

        [Display(Name = "Short URL")]
        public string ShortUrl { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }

        public string UtmParameters { get; set; }

        public int LinkType { get; set; }
        public bool HasExpirationDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
