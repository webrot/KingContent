
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingContent.Schema;

namespace KingContent.DriverFactory
{
    public class DiskFileDriver : IDriver
    {
        public string Name
        {
            get
            {
               return "diskfile";
            }
        }

        public bool AlertTable(ITableSchema oldT, ITableSchema newT)
        {
            throw new NotImplementedException();
        }

        public bool CreateTable(ITableSchema table)
        {
            throw new NotImplementedException();
        }

        public bool DropTable(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}
