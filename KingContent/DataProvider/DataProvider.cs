using KingContent.DriverFactory;
using KingContent.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent.DataProvider
{
    public interface IDDLProvider
    {
        bool CreateTable(ITableSchema table);
        bool DropTable(string tableName);
        bool AlertTable(ITableSchema oldT, ITableSchema newT);
    }

    public interface IDataProvider
    {
        void AddRecord(TextContent item);
        void RemoveRecord(TextContent item);
        void UpdateRecord(TextContent newitem, TextContent olditem);

    }
    public class DataProvider : IDDLProvider, IDataProvider
    {
        public IDriver Driver { get; private set; }
        public string ConnectString { get; private set; }

        public DataProvider(string constr,IDriver driver)
        {
            //var employee = (Employee)Activator.CreateInstance(typeof(Employee));
            //employee = Activator.CreateInstance<Employee>();

            ConnectString = constr;
            Driver = driver;
        }
        public void AddRecord(TextContent item)
        {
            throw new NotImplementedException();
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

        public void RemoveRecord(TextContent item)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(TextContent newitem, TextContent olditem)
        {
            throw new NotImplementedException();
        }
    }
}
