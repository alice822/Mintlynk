using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Enums
{
    public enum NotificationStatusEnum
    {
        Deleted = 0,
        Unread = 1,
        Read = 2,
        Delivered = 3,
        DeliveryFailed = 4,
        Closed = 5,
        Active = 6
    }
    public enum NotificationTypeEnum
    {
        JustNote = 1,
        Reminders = 2,
        Messages = 3,
        Task = 4,
    }
    public enum ChannelTypeEnum
    {
        System = 1,
        Email = 2,
        SMS = 3
    }

    public enum MessageTemplateTypeEnum
    {
        [Description("Notification")] Notification = 1,

        [Description("Email")] Email = 2,

    }

    public enum EmailTypeEnum
    {
        [Description("Confirm your account")]
        ConfirmAccount = 1,

        [Description("Reset Password")]
        ResetPassword = 2,

        [Description("Contract")]
        Contract = 3,

        [Description("Payment complete")]
        PaymentComplete = 4,

        [Description("Review")]
        Review = 5,

        [Description("Approved")]
        Approved = 5
    }
    public enum ApplicationAreaEnum
    {
        Registration = 1,
        TDeals = 2,
        Shipment = 3,
        Wallet = 4,
        Payments = 5,
        Documents = 6,
        Users = 7,
        Quotation = 8,
        TeamMembers = 9,
        Order = 10,
        Community = 11,
        Course = 12,
    }
}
