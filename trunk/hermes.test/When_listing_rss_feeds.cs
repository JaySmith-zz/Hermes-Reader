using System.Collections.Generic;
using hermes.Controllers;
using hermes.core.Domain.RssFeedAggregate;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace hermes.test
{
    [TestFixture]
    public class When_listing_rss_feeds
    {
        [Test]
        public void should_retrieve_all_rss_feeds()
        {
            var mocks = new MockRepository();
            var repo = mocks.DynamicMock<IRssFeedRepository>();
            var controller = new HomeController(repo);

            using (mocks.Record())
            {
                Expect.Call(repo.ListAllFeeds()).Return(new List<RssFeed>());
            }

            using (mocks.Playback())
            {
                controller.Index();
            }
        }

        [Test]
        public void should_order_feeds_by_publication_date()
        {
        }

        [Test]
        public void should_provide_list_of_feeds_to_view()
        {
            var mocks = new MockRepository();
            var repo = mocks.DynamicMock<IRssFeedRepository>();
            var controller = new HomeController(repo);
            var feedList = new List<RssFeed>();
            object viewData = null;

            using (mocks.Record())
            {
                Expect.Call(repo.ListAllFeeds()).Return(feedList);
                controller.RenderViewWithDataAction = 
                    (viewName, data) => { viewData = data; };
            }

            using (mocks.Playback())
            {
                controller.Index();
            }

            Assert.AreSame(viewData, feedList);
        }
    }
}