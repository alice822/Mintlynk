using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Interfaces
{
    public interface IEntityWithEntityId
    {
        string EntityId { get; set; }
    }

    public interface IEntityWithDestinationUrl
    {
        string DestinationUrl { get; set; }
    }

    public interface IEntityWithShortUrl
    {
        string ShortUrl { get; set; }
    }
}
