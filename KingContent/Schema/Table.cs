using System;
using System.Data;
using System.Collections.Generic;

namespace KingContent.Schema
{
    public class ForeignKeySchema
    {
        public string Name { get; private set; }
        public string[] DestColumns { get; private set; }

        public string DestTable { get; private set; }

        public string[] SrcColumns { get; private set; }

        public string SrcTable { get; private set; }

        public ForeignKeySchema(string name, string srcTable, string[] srcColumns, string destTable, string[] destColumns) 
        {
            Name = name;
            SrcColumns = srcColumns;
            DestTable = destTable;
            DestColumns = destColumns;
            SrcTable = srcTable;
        }
    }
    public class TableSchema
    {
        public string TableName { get; set; }

        public TableSchema(string tableName)
        {
            Columns = new List<Column>();
            TableName = tableName;
        }
        public List<Column> Columns { get; private set; }

        #region override object
        public static bool operator ==(TableSchema obj1, TableSchema obj2)
        {
            if (object.Equals(obj1, obj2) == true)
            {
                return true;
            }
            if (object.Equals(obj1, null) == true || object.Equals(obj2, null) == true)
            {
                return false;
            }
            return obj1.Equals(obj2);
        }
        public static bool operator !=(TableSchema obj1, TableSchema obj2)
        {
            return !(obj1 == obj2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is TableSchema))
            {
                return false;
            }
            if (obj != null)
            {
                var table = (TableSchema)obj;
                if ( string.Compare(this.TableName, table.TableName, true) == 0)
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        { 
            if (string.IsNullOrEmpty(this.TableName))
            {
                return base.GetHashCode();
            }
            else
            {
                return this.TableName.ToLower().GetHashCode();
            } 
        }
        public override string ToString()
        {
            return this.TableName;
        }
        #endregion

        public TableSchema DeepClone()
        {
            var table = (TableSchema)this.MemberwiseClone();
            if (this.Columns != null)
            {
                table.Columns = new List<Column>();

                foreach (var item in this.Columns)
                {
                    table.Columns.Add(item.DeepClone());
                }
            }
            return table;
        }
    }

    public static class TableExtensions
    {
        public static TableSchema AddColumn(this TableSchema table, string columnName,  Action<Column> columnfun = null)
        {
            var column = new Column(columnName);
            if (columnfun != null)
            {
                columnfun(column);
            }
            table.Columns.Add(column);
            return table;
        }
        public static TableSchema AddColumn(this TableSchema table, string columnName, DbType dbType, Action<Column> columnfun = null)
        {
            var column = new Column(columnName);
            column.WithType(dbType);
            if (columnfun != null)
            {
                columnfun(column);
            }
            table.Columns.Add(column);
            return table;
        }

        public static TableSchema AddColumn<T>(this TableSchema table, string columnName, Action<Column> columnfun = null)
        {
            var dbType = SchemaUtils.ToDbType(typeof(T));
            return table.AddColumn(columnName, dbType, columnfun); 
        }
         

        public static TableSchema DropColumn(this TableSchema table, string columnName)
        {
            int index = table.Columns.FindIndex(i => i.ColumnName.Equals(columnName));
            table.Columns.RemoveAt(index);
            return table;
        }

        public static TableSchema AlterColumn<T>(this TableSchema table,string columnName, Action<Column> columnfun)
        {
            Column column = table.Columns.Find(i => i.ColumnName.Equals(columnName));
            if (column != default(Column))
            {
                var dbType = SchemaUtils.ToDbType(typeof(T));
                column.WithType(dbType);
                columnfun(column);
            }
            return table;
        }
        public static TableSchema AlterColumn(this TableSchema table, string columnName, Action<Column> columnfun)
        {
            Column column = table.Columns.Find(i => i.ColumnName.Equals(columnName));
            if (column != default(Column))
            {
                columnfun(column);
            }
            return table;
        }
                 
    } 

}
