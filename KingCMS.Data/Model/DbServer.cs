using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCMS.Data.Model
{
    public class DbServer
    {
        public string ServerName { get; set; }
        public string ServerPath { get; set; }
        public DbServerType ServerType { get; set; }

        public string DbName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }  

        public List<Table> Tables { get; set; }
        public List<View> Views { get; set; }
        public List<StoreFun> StoreFuns { get; set; }
    }

    public class View
    {
        public string ViewName { get; set; }
    }

    public class StoreFun
    {
        public string FunName { get; set; }
    }

    public enum DbServerType
    {
        Sqlite = 0,
        Sqlce = 1,
        MsSql = 2,
        MySql = 3,
        Oracle = 4
    }
}
