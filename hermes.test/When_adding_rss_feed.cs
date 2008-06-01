using hermes.Controllers;
using hermes.core.Domain.RssFeedAggregate;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace hermes.test
{
    [TestFixture]
    public class When_adding_rss_feed
    {
        //private MockRepository _mocks;

        //[SetUp]
        //public void Setup()
        //{
        //    _mocks = new MockRepository();
        //}

        [Test]
        public void should_add_new_rss_feed_to_repository()
        {
            var mocks = new MockRepository();
            var repo = mocks.DynamicMock<IRssFeedRepository>();
            var controller = new HomeController(repo);

            using(mocks.Record())
            {
                Expect.Call(() => repo.SaveFeed(null))
                    .Constraints(Is.NotNull());
            }

            using (mocks.Playback())
            {
                controller.AddRssFeed("TESTURI");
            }
        }

        

    }
}
