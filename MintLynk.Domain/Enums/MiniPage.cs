using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Enums
{
    public enum MiniPageStatus
    {
        Deleted = 0,
        Draft = 1,
        Published = 2,
        Suspended = 3,
        Archived = 4,
    }

    public enum MiniPageType
    {
        Influencer = 0,
        Professional = 1,
        ProfessionalService = 2,
        Tourism = 3,
        Restaurant = 4,
        Event = 5,
        Business = 6,
        Personal = 7,
        Organization = 8,
        NonProfit = 9,
        Educational = 10,
        Government = 11,
        RealEstate = 12,
        HealthCare = 13,
        Entertainment = 14,
        Sports = 15,
        Technology = 16,
        Fashion = 17,
        Automotive = 18,
        FoodAndBeverage = 19,
        TravelAndLeisure = 20,
        ArtsAndCulture = 21,
        FinanceAndBanking = 22,
        Legal = 23,
        MediaAndPublishing = 24,
        Other = 25,
    }

    public enum SocialPlatform
    {
        Facebook,
        Instagram,
        LinkedIn,
        X,
        YouTube,
        TikTok,
        GitHub,
        PersonalWebsite
    }
}
