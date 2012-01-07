using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using hermes.core.Domain.RssFeedAggregate;
using hermes.web.Controllers;
using NUnit.Framework;
using Rhino.Mocks;


namespace hermes.test
{
    [TestFixture]
    public class When_listing_rss_feeds
    {
        private MockRepository _mocks;
        private IRssFeedRepository _repo;
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();
            _repo = _mocks.DynamicMock<IRssFeedRepository>();
            _controller = new HomeController(_repo);
        }

        [Test]
        public void should_retrieve_all_rss_feeds()
        {
            using (_mocks.Record())
            {
                Expect.Call(_repo.ListAllFeeds()).Return(new List<RssFeed>());
            }

            ViewResult result = null;

            using (_mocks.Playback())
            {
                result = _controller.Index() as ViewResult;
            }

            Assert.IsNotNull(result);
        }

        [Test]
        public void should_order_feeds_by_publication_date_descending()
        {
            List<RssFeed> feedList = new List<RssFeed>
                                         {
                                             new RssFeed{PublicationDate = new DateTime(2008, 05, 13)},
                                             new RssFeed{PublicationDate = new DateTime(2009, 01, 01)},
                                             new RssFeed{PublicationDate = new DateTime(2000, 01, 01)}
                                         };
            using(_mocks.Record())
            {
                Expect.Call(_repo.ListAllFeeds()).Return(feedList);
            }

            ViewResult result = null;
            using(_mocks.Playback())
            {
                result = _controller.Index() as ViewResult;
            }
            
            Assert.IsNotNull(result);
            var viewData = result.ViewData.Model as IEnumerable<RssFeed>;
            Assert.That(viewData.First().PublicationDate == new DateTime(2009, 01, 01));

        }

        [Test]
        public void should_provide_list_of_feeds_to_view()
        {
            var feedList = new List<RssFeed>();

            using (_mocks.Record())
            {
                Expect.Call(_repo.ListAllFeeds()).Return(feedList);
            }

            ViewResult result = null;

            using (_mocks.Playback())
            {
                result = _controller.Index() as ViewResult;
            }

            Assert.IsNotNull(result);

            var viewData = result.ViewData.Model as IEnumerable<RssFeed>;

            Assert.That(viewData.Count() == feedList.Count());
        }
    }
}