using System;
using System.Collections.Generic;
using System.Linq;
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
                _controller.RenderViewWithDataAction =
                    (viewName, data) => { int x = 0; };
            }

            using (_mocks.Playback())
            {
                _controller.Index();
            }
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
            object viewData = null;

            using(_mocks.Record())
            {
                Expect.Call(_repo.ListAllFeeds()).Return(feedList);
                
                _controller.RenderViewWithDataAction =
                    (viewName, data) =>{ viewData = data; };
            }

            using(_mocks.Playback())
            {
                _controller.Index();
            }


            Assert.That(((IEnumerable<RssFeed>)viewData).First().PublicationDate == new DateTime(2009, 01, 01));

        }

        [Test]
        public void should_provide_list_of_feeds_to_view()
        {
            var feedList = new List<RssFeed>();
            object viewData = null;

            using (_mocks.Record())
            {
                Expect.Call(_repo.ListAllFeeds()).Return(feedList);
                _controller.RenderViewWithDataAction = 
                    (viewName, data) => { viewData = data; };
            }

            using (_mocks.Playback())
            {
                _controller.Index();
            }

            Assert.AreSame(viewData, feedList);
        }
    }
}