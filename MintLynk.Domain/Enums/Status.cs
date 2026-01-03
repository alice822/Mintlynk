using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Enums
{
    public enum Status
    {
        Draft = 0,
        Active = 1,
        Inactive = 2,
        Expired = 3,
        Deleted = 4,
        Archived = 5,
    }

    public enum UserStatus
    {
        Deleted = 0,
        AwaitingConfirmation = 1,
        Active = 2,
        Inactive = 3,
        Suspended = 4,
        Banned = 5,
        Pending = 6,
        AwaitingApproval = 7,
        AwaitingVerification = 8,
        AwaitingActivation = 9,
        AwaitingDeactivation = 10,
        AwaitingDeletion = 11
    }

    public static class StatusExtensions
    {
        public static string ToFriendlyString(this Status status)
        {
            return status switch
            {
                Status.Draft => "Draft",
                Status.Active => "Active",
                Status.Inactive => "Inactive",
                Status.Expired => "Expired",
                Status.Deleted => "Deleted",
                Status.Archived => "Archived",
                _ => "Unknown"
            };
        }
    }
}
