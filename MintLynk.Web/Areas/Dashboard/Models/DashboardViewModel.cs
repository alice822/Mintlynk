using MintLynk.Application.Dtos;
using MintLynk.Web.Models;

namespace MintLynk.Web.Areas.Dashboard.Models
{
    public class DashboardViewModel
    {
        public SmartLinkViewDto SmartLink { get; set; } = new SmartLinkViewDto();

        public ReportViewModel Reports { get; set; } = new ReportViewModel();

        public List<TopLinkViewModel> TopLinks { get; set; } = new List<TopLinkViewModel>();
    }
}