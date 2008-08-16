using FluentNHibernate.Mapping;
using hermes.core.Domain;

namespace hermes.core.Persistence
{
    public abstract class DomainEntityMap<ENTITY> : ClassMap<ENTITY> 
        where ENTITY : DomainEntity
    {
        protected DomainEntityMap()
        {
            UseIdentityForKey(e => e.Id, "Id");
            Map(e => e.CreateDate);
            Map(e => e.ModifiedDate);
        }
    }
}