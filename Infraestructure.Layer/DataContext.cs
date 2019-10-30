using System.Data.ORM.Context;
using System.Data.ORM.MapCfg;

namespace Infraestructure.Layer
{
    public class DataContext : DbContext
    {
        public DataContext(ISQLConfiguration configuration) : base(configuration)
        {
            
        }
    }
}
