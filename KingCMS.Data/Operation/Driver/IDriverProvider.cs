using System;
using System.Collections.Generic;
using System.Text;

namespace KingCMS.Data.Operation.Driver
{
    public interface IDriverProvider
    {
        string DriverName();
    }

    public class BaseDriver:IDriverProvider
    { 
        public virtual string DriverName()
        {
           return "BaseDriver";
        }
    }

    public class SqliteDriver : BaseDriver
    {
        public override string DriverName()
        {
            return  "SqliteDriver";
        } 
    }

    public class MySqlDriver : BaseDriver
    {
        public override string DriverName()
        {
            return  "MySqlDriver";
        } 
    }
}
