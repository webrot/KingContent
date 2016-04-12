using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data;

namespace KingContent.Schema
{
    public class Column
    {
        public Column(string name)
        {
            ColumnName = name;
            DbType = DbType.Object;
            Default = null;
            Length = null;
            IsNotNull = false;
            IsUnique = false;
            Isindex = false;
        }
        public string ColumnName { get; private set; }

        public byte Scale { get; set; }

        public byte Precision { get; set; }

        public DbType DbType { get; set; }

        public object Default { get; set; }

        public int? Length { get; set; }

        #region  CreateColumn
        public bool IsUnique { get; set; }

        public bool IsNotNull { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsIdentity { get; set; }

        public bool Isindex { get; set; }
        #endregion
         
        #region override object
        public override bool Equals( object obj)
        {
            if (!(obj is Column))
            {
                return false;
            }
            if (obj == null)
            {
                return false;
            }
            if (string.Compare(this.ColumnName, ((Column)obj).ColumnName, true) == 0)
            {
                return true;
            }
            return base.Equals(obj);
        }

        public static bool operator ==(Column obj1, Column obj2)
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
        public static bool operator !=(Column obj1, Column obj2)
        {
            return !(obj1 == obj2);
        }
        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.ColumnName))
            {
                return base.GetHashCode();
            }
            else
            {
                return this.ColumnName.ToLower().GetHashCode();
            }
        }
        #endregion
        #region Clone
        public Column DeepClone()
        {
            var column = (Column)this.MemberwiseClone();
            return column;
        }
        #endregion
    }

    public static class ColumnExtensions
    {
        public static Column WithType(this Column column, DbType dbType)
        {
            column.DbType = dbType;
            return column;
        }

        public static Column WithType(this Column column, DbType dbType, int? length)
        {
            column.WithType(dbType).WithLength(length);
            return column;
        }

        public static Column WithType(this Column column, DbType dbType, byte precision, byte scale)
        {
            column.WithType(dbType);
            column.Precision = precision;
            column.Scale = scale;
            return column;
        }

        public static Column WithDefault(this Column column, object @default)
        {
            column.Default = @default;
            return column;
        }

        public static Column WithLength(this Column column, int? length)
        {
            column.Length = length;
            return column;
        }
        public static Column Unlimited(this Column column)
        {
            return column.WithLength(10000);
        }

        #region  CreateColumnCommand
        public static Column PrimaryKey(this Column column)
        {
            column.IsPrimaryKey = true;
            column.IsUnique = false;
            return column;
        }
        public static Column Identity(this Column column)
        {
            column.IsIdentity = true;
            column.IsUnique = false;
            return column;
        }

        public static Column WithPrecision(this Column column, byte precision)
        {
            column.Precision = precision;
            return column;
        }
        public static Column WithScale(this Column column, byte scale)
        {
            column.Scale = scale;
            return column;
        }
        public static Column NotNull(this Column column)
        {
            column.IsNotNull = true;
            return column;
        }

        public static Column Nullable(this Column column)
        {
            column.IsNotNull = false;
            return column;
        }
        public static Column Unique(this Column column)
        {
            column.IsUnique = true;
            column.IsPrimaryKey = false;
            column.IsIdentity = false;
            return column;
        }
        public static Column NotUnique(this Column column)
        {
            column.IsUnique = false;
            return column;
        }

        public static Column Index(this Column column)
        {
            column.Isindex = true;
            return column;
        }
        public static Column NotIndex(this Column column)
        {
            column.Isindex = false;
            return column;
        }
        #endregion


    }
}
     
 
