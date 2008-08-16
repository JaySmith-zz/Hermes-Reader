using FluentNHibernate;

namespace hermes.core.Persistence
{
    public class HermesPersistenceModel : PersistenceModel
    {
        public HermesPersistenceModel()
        {
            addMappingsFromThisAssembly();
            
        }
    }
}