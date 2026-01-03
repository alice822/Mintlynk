using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        [Column(TypeName = "varchar(256)")]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset LastModified { get; set; }

        [Column(TypeName = "varchar(256)")]
        public string? LastModifiedBy { get; set; }
    }
}
