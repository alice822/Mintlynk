using MintLynk.Domain.Models;

namespace MintLynk.Web.Models
{
    public class LinkDetailViewModel
    {
        public string? EntityId { get; set; }

        public string Title { get; set; }

        public string ShortUrl { get; set; }

        public string DestinationUrl { get; set; }

        public string QrCode { get; set; }

        public string FavIconUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long TotalEngagement { get; set; }

        public long TotalQrScan { get; set; }

        public long TotalLinkClick { get; set; }

        public long SevenDaysEngagement { get; set; }

        public ReportViewModel Reports { get; set; } = new ReportViewModel();

        public UtmParameters UtmParameters { get; set; } = new UtmParameters();
    }

    public class TopLinkViewModel
    {
        public string? EntityId { get; set; }

        public string Title { get; set; }

        public string ShortUrl { get; set; }

        public string DestinationUrl { get; set; }

        public string QrCode { get; set; }

        public string FavIconUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long TotalEngagement { get; set; }
    }

    public class ReportViewModel
    {
        public long TotalEngagement { get; set; }

        public long TotalQrScan { get; set; }

        public long TotalLinkClick { get; set; }

        public long SevenDaysEngagement { get; set; }

        public long LastWeekEngageent { get; set; }

        public long ThisWeekEngageent { get; set; }

        public decimal WeeklyGrowthRate { get; set; }

        public List<BrowserReportViewModel> BrowserReports { get; set; } = new List<BrowserReportViewModel>();

        public List<OperatingSystemReportViewModel> OsReport { get; set; } = new List<OperatingSystemReportViewModel>();

        public List<DeviceReportViewModel> DeviceReport { get; set; } = new List<DeviceReportViewModel>();

        public List<CountryReportViewModel> CountryReport { get; set; } = new List<CountryReportViewModel>();

        public List<CityReportViewModel> CityReport { get; set; } = new List<CityReportViewModel>();

        public List<DateReportViewModel> DateReport { get; set; } = new List<DateReportViewModel>();

        public List<DateReportViewModel> SevenDaysReport { get; set; } = new List<DateReportViewModel>();
    }

    public class DateReportViewModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string Date { get; set; }

        public long Count { get; set; }

        public long QrScan { get; set; }

        public long LinkClick { get; set; }
    }

    public class BrowserReportViewModel: ReportBaseModel
    {

    }

    public class OperatingSystemReportViewModel: ReportBaseModel
    {

    }

    public class DeviceReportViewModel: ReportBaseModel
    {

    }

    public class CountryReportViewModel: ReportBaseModel
    {

    }

    public class CityReportViewModel:ReportBaseModel
    {

    }

    public class ReportBaseModel
    {
        public string Name { get; set; }

        public long Engagements { get; set; }

        public decimal Percentage { get; set; }
    }
}
