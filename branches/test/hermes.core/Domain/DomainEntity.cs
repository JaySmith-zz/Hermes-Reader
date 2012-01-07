using System;
using FluentNHibernate.Framework;

namespace hermes.core.Domain
{
    public abstract class DomainEntity : Entity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool Equals(DomainEntity obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DomainEntity)) return false;
            return Equals((DomainEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}