using System;
using System.Web.Mvc;
using hermes.core.Domain.RssFeedAggregate;

namespace hermes.web.Controllers
{
    public class RssFeedController : Controller
    {
        private readonly IRssFeedRepository _feedRepo;

        public RssFeedController(IRssFeedRepository repo)
        {
            _feedRepo = repo;
        }

        public ActionResult Create(string feedUri)
        {
           

            return RedirectToRoute("Default");
        }
    }
}