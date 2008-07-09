namespace hermes.core.Domain.RssFeedAggregate
{
    public class RssFeedService
    {
        private readonly IRssFeedRepository _feedRepo;

        public RssFeedService(IRssFeedRepository feedRepo)
        {
            _feedRepo = feedRepo;
        }

        public RssFeed Create(string feedUri)
        {
            var feed = new RssFeed { FeedUri = feedUri };
            _feedRepo.SaveFeed(feed);

            return feed;
        }

        public void DeleteFeed(RssFeed feed)
        {
            _feedRepo.Delete(feed);
        }
    }
}