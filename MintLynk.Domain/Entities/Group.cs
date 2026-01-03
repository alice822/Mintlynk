using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string GroupName { get; set; }

        public string GroupKey { get; set; }    

        public int Status { get; set; }
    }
}
