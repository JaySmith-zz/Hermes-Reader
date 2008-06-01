using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hermes.core.Domain.RssFeedAggregate;

namespace hermes.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssFeedRepository _feedRepo;
        
        public Action<string> RenderViewAction { get; set; }
        public Action<string, object> RenderViewWithDataAction { get; set; }

        public HomeController(IRssFeedRepository feedRepo)
        {
            _feedRepo = feedRepo;
            //RenderViewAction = RenderView;  // RenderView(string)
            //RenderViewWithDataAction = RenderView; // RenderView(string, object)
        }
        public ActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }

        public void AddRssFeed(string feedUri)
        {
            var feed = new RssFeed { FeedUri = feedUri };
            _feedRepo.SaveFeed(feed);
        }
    }
}
