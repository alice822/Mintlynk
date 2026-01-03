using MintLynk.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Entities
{
    public abstract  class SmartLinkBase : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column(TypeName = "varchar(8)")]
        public string EntityId { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string DestinationUrl { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string ShortUrl { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Tags { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string UtmParameters { get; set; } = string.Empty;

        [Required]
        public int LinkType { get; set; } = (int)SmartLinkType.None;

        [Required]
        public bool HasExpirationDate { get; set; } = false;

        public DateTimeOffset ExpirationDate { get; set; }

        [Required]
        public int Status { get; set; } = (int)MintLynk.Domain.Enums.Status.Archived;
    }
}
