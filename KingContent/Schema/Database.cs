
using KingContent.DataProvider;
using KingContent.DriverFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent.Schema
{ 
    public class DatabaseSchema
    {
        public string DatabaseName { get; private set;}
        public string ConnectString { get; private set; }

        public IDriver Driver { get; private set; }

        public DataProvider.DataProvider DProvider { get; private set; }

        public DatabaseSchema(string name,string constr,string drivername)
        {
            DatabaseName = name;
            ConnectString = constr;
            Driver = DriverFactory.DriverFactory.For(drivername);
            DProvider = new DataProvider.DataProvider(constr, Driver);
        }
        public List<TableSchema> Tables { get; private set; }
        public List<ForeignKeySchema> ForeignKeys { get; private set; }

        public bool ExecuteDDL(ExecuteDDLType type, TableSchema table, TableSchema oldtable = null)
        {


            return true;
        }
    }
    public enum ExecuteDDLType
    {
        CreateTable,
        DropTable,
        AlterTable
    }

    public static class DatabaseSchemaExtensions
    {
        public static DatabaseSchema AddTable(this DatabaseSchema database, string tablename,Action<TableSchema> tablefun)
        {
            TableSchema table = new TableSchema(tablename);

            tablefun(table);

           if( database.ExecuteDDL(ExecuteDDLType.CreateTable,table))
               database.Tables.Add(table);

            return database;
        }
        /// <summary>
        /// 从模板添加 table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
        public static DatabaseSchema AddTableFrom(this DatabaseSchema database,TableSchema table,string newname)
        {
            TableSchema newtable = table.DeepClone();
            newtable.TableName = newname;

            if (database.ExecuteDDL(ExecuteDDLType.CreateTable, newtable))
                database.Tables.Add(newtable);

            return database;
        }
        /// <summary>
        /// 删除 table 
        /// delete table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static DatabaseSchema DropTable(this DatabaseSchema database,string tablename)
        { 
            var table = database.Tables.Find(i => i.TableName.Equals(tablename));

            if(table!=default(TableSchema))
                if (database.ExecuteDDL(ExecuteDDLType.DropTable, table))
                    database.Tables.Remove(table);

            return database;
        }
        /// <summary>
        /// 修改 table
        /// update table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="tablefun"></param>
        /// <returns></returns>
        public static DatabaseSchema AlterTable(this DatabaseSchema database, string tableName, Action<TableSchema> tablefun)
        {
            TableSchema table = database.Tables.Find(i => i.TableName.Equals(tableName));
            if (table != default(TableSchema))
            {
                var oldtable = table.DeepClone();
                tablefun(table);
                database.ExecuteDDL(ExecuteDDLType.AlterTable, table,oldtable);
            } 
            return database;
        }
    }
}
