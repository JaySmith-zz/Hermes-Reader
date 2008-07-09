using hermes.core.Domain.RssFeedAggregate;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace hermes.test
{
    [TestFixture]
    public class When_adding_an_rss_feed : RssFeedSpecification
    {
        [Test]
        public void the_RssFeedService_should_add_a_new_rss_feed_to_repository()
        {
            _repo.Expect(r => r.SaveFeed(null))
                .Constraints(Property.Value("FeedUri", "TESTURI"));

            _feedService.Create("TESTURI");

            _repo.VerifyAllExpectations();
        }

        [Test]
        public void the_RessFeedService_should_return_a_new_populated_rss_feed_object()
        {
            _feedService.Create("TESTURI").FeedUri.ShouldEqual("TESTURI");
        }
    }

    [TestFixture]
    public class When_deleting_an_rss_feed : RssFeedSpecification
    {
        [Test]
        public void the_RssFeedService_should_delete_the_feed_from_repository()
        {
            var feed = new RssFeed();
            _repo.Expect(r => r.Delete(feed));

            _feedService.DeleteFeed(feed);

            _repo.VerifyAllExpectations();
        }
    }


    public abstract class RssFeedSpecification
    {
        protected MockRepository _mocks;
        protected IRssFeedRepository _repo;
        protected RssFeedService _feedService;

        [SetUp]
        public void BaseSetup()
        {
            _mocks = new MockRepository();
            _repo = _mocks.DynamicMock<IRssFeedRepository>();
            _feedService = new RssFeedService(_repo);
            SetUp();
        }

        protected virtual void SetUp()
        {
        }
    }
}