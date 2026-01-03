using Microsoft.AspNetCore.Mvc;

namespace MintLynk.Web.Controllers
{
    public class BlogController: Controller
    {
        [Route("blog")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
