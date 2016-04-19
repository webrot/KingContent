
using KingContent.Schema;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent.DriverFactory
{
    public class DriverFactory
    {
        private static readonly Dictionary<string, IDriver> Drivers = new Dictionary<string, IDriver>
        {
            {"sqlite", new SqliteDriver()},
            {"sqlce", new SqlCeDriver()},
            {"diskfile",new DiskFileDriver() }
        };

        public static void RegisterSchemaBuilder(string providername, IDriver dataProvider)
        {
            Drivers[providername] = dataProvider;
        }

        public static IDriver For(string providername)
        {  
            if (!Drivers.ContainsKey(providername))
            {
                throw new ArgumentException("Unknown connection name: " + providername);
            }
            
            return Drivers[providername];
        }
    }

    public interface IDriver
    {
        string Name { get;  }
    }

}
