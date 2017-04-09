using KingCMS.Data.Operation.Driver; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCMS.Data
{
    public class DriverFactory
    { 
        public virtual IDriverProvider GetDriver()
        {
            //serviceCollection.Add()
            return new SqliteDriver();
        }

        public void RegisterDriver(Type type)
        {
            //serviceCollection.AddSingleton<IDriverProvider, type>();
        }
    }
}
