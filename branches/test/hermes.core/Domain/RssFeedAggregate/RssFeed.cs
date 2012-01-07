using System;

namespace hermes.core.Domain.RssFeedAggregate
{
    public class RssFeed : DomainEntity
    {
        public string FeedUri { get; set; }
        public DateTime PublicationDate { get; set; }
    }

    
}