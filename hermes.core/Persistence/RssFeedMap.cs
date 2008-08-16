using hermes.core.Domain.RssFeedAggregate;

namespace hermes.core.Persistence
{
    public class RssFeedMap : DomainEntityMap<RssFeed>
    {
        public RssFeedMap()
        {
            Map(r => r.FeedUri);
            Map(r => r.PublicationDate);
        }
    }
}