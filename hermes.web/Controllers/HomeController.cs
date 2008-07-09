using System.Linq;
using System.Web.Mvc;
using hermes.core.Domain.RssFeedAggregate;

namespace hermes.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssFeedRepository _feedRepo;

        public HomeController(IRssFeedRepository feedRepo)
        {
            _feedRepo = feedRepo;
        }

        public ActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            
            var feedList = _feedRepo.ListAllFeeds().OrderByDescending(x => x.PublicationDate);

            return View(feedList);
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }

        
    }
}