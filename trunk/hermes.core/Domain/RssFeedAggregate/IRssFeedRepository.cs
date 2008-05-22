using System.Collections.Generic;

namespace hermes.core.Domain.RssFeedAggregate
{
    public interface IRssFeedRepository
    {
        void SaveFeed(RssFeed feed);
        IList<RssFeed> ListAllFeeds();
    }
}