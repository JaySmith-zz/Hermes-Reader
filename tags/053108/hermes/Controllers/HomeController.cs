using System;
using System.Linq;
using System.Web.Mvc;
using hermes.core.Domain.RssFeedAggregate;

namespace hermes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssFeedRepository _feedRepo;

        public Action<string> RenderViewAction{ get; set; }
        public Action<string, object> RenderViewWithDataAction { get; set; }

        public HomeController(IRssFeedRepository feedRepo)
        {
            _feedRepo = feedRepo;
            RenderViewAction = RenderView;  // RenderView(string)
            RenderViewWithDataAction = RenderView; // RenderView(string, object)
        }

        public void Index()
        {
            var feeds = _feedRepo.ListAllFeeds();
            var sortedFeeds = feeds.OrderByDescending(f => f.PublicationDate);
            RenderViewWithDataAction("Index", sortedFeeds);
        }

        public void About()
        {
            RenderViewAction("About");
        }

        public void AddRssFeed(string feedUri)
        {
            var feed = new RssFeed { FeedUri = feedUri };
            _feedRepo.SaveFeed(feed);
        }
    }
}