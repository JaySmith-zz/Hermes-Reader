using System;
using System.Collections.Generic;
using FluentNHibernate;
using hermes.core.Persistence;
using hermes.core.Domain.RssFeedAggregate;
using hermes.web;
using NUnit.Framework;
using FluentNHibernate.Framework;

namespace hermes.integrationtest.Core.Persistence
{
    [TestFixture]
    public class RssFeedPersistenceSpecification
    {
        private ISessionSource _sessionSource;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            var propDict = new Dictionary<string,string>
            {
                {"connection.provider", "NHibernate.Connection.DriverConnectionProvider" },
                {"connection.driver_class", "NHibernate.Driver.SqlClientDriver" },
                {"dialect", "NHibernate.Dialect.MsSql2000Dialect" },
                {"hibernate.dialect", "NHibernate.Dialect.MsSql2000Dialect" },
                {"use_outer_join", "true" },
                {"connection.connection_string", "Data Source=YOUNG\\SQLEXPRESS;Initial Catalog=hermes;Integrated Security=True;" },
                {"show_sql", "true" }
            };

            _sessionSource = new SessionSource(propDict, new HermesPersistenceModel());
        }

        [Test]
        public void RssFeed_mappings_should_work_correctly()
        {
            new PersistenceSpecification<RssFeed>(_sessionSource)
                .CheckProperty(r => r.CreateDate, new DateTime(2001, 1, 1))
                .CheckProperty(r => r.PublicationDate, new DateTime(2005, 5, 5))
                .CheckProperty(r => r.FeedUri, "Foo")
                .CheckProperty(r => r.ModifiedDate, new DateTime(2008, 6, 5))
                .VerifyTheMappings();
        }
    }
}