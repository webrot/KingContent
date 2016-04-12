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
        public string ConnectString { get; set; }

        public string ProviderName { get; set; }
        
        public List<TableSchema> Tables { get; private set; }
        public List<ForeignKeySchema> ForeignKeys { get; private set; }
    }


    public static class DatabaseSchemaExtensions
    {
        public static DatabaseSchema AddTable(this DatabaseSchema database, string tablename,Action<TableSchema> tablefun = null)
        {
            TableSchema table = new TableSchema(tablename);
            if(tablefun!=null)
            {
                tablefun(table);
            }
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
            database.Tables.Add(newtable);
            return database;
        }

        public static DatabaseSchema DropTable(this DatabaseSchema database,string tablename)
        { 
            int index = database.Tables.FindIndex(i => i.TableName.Equals(tablename));
            database.Tables.RemoveAt(index);
            return database;
        }

        public static DatabaseSchema AlterTable(this DatabaseSchema database, string tableName, Action<TableSchema> tablefun)
        {
            TableSchema table = database.Tables.Find(i => i.TableName.Equals(tableName));
            if (table != default(TableSchema))
            {
                tablefun(table);
            } 
            return database;
        }
    }
}
