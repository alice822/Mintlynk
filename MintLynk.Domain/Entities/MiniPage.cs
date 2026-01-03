using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Entities
{
    public class MiniPage : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Guid Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string? PageContent { get; set; }

        public string Template { get; set; }
        public int Status { get; set; }
    }
}
