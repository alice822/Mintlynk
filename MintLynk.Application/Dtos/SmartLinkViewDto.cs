using MintLynk.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Dtos
{
    public class SmartLinkViewDto
    {

        public long Id { get; set; }
        public string? EntityId { get; set; }

        [MaxLength(255)]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Destination Link")]
        [Required(ErrorMessage = "Destination link is required.")]
        [Url(ErrorMessage = "Please enter a valid link.")]
        public string DestinationUrl { get; set; }

        [Display(Name = "Short URL")]
        public string? ShortUrl { get; set; }

        [Display(Name = "Tags")]
        public string? Tags { get; set; }

        public string? FavIconUrl { get; set; }

        public string? QrCode { get; set; }

        public long TotalEngagement { get; set; }

        public UtmParameters UtmParameters { get; set; } = new UtmParameters();

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
