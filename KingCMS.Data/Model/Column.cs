using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCMS.Data.Model
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
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
        public override bool Equals(object obj)
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

    public enum DbType
    {
        //
        // 摘要:
        //     非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
        AnsiString = 0,
        //
        // 摘要:
        //     二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。
        Binary = 1,
        //
        // 摘要:
        //     一个 8 位无符号整数，范围在 0 到 255 之间。
        Byte = 2,
        //
        // 摘要:
        //     简单类型，表示 true 或 false 的布尔值。
        Boolean = 3,
        //
        // 摘要:
        //     货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。
        Currency = 4,
        //
        // 摘要:
        //     表示日期值的类型。
        Date = 5,
        //
        // 摘要:
        //     表示一个日期和时间值的类型。
        DateTime = 6,
        //
        // 摘要:
        //     简单类型，表示从 1.0 x 10 -28 到大约 7.9 x 10 28 且有效位数为 28 到 29 位的值。
        Decimal = 7,
        //
        // 摘要:
        //     浮点型，表示从大约 5.0 x 10 -324 到 1.7 x 10 308 且精度为 15 到 16 位的值。
        Double = 8,
        //
        // 摘要:
        //     全局唯一标识符（或 GUID）。
        Guid = 9,
        //
        // 摘要:
        //     整型，表示值介于 -32768 到 32767 之间的有符号 16 位整数。
        Int16 = 10,
        //
        // 摘要:
        //     整型，表示值介于 -2147483648 到 2147483647 之间的有符号 32 位整数。
        Int32 = 11,
        //
        // 摘要:
        //     整型，表示值介于 -9223372036854775808 到 9223372036854775807 之间的有符号 64 位整数。
        Int64 = 12,
        //
        // 摘要:
        //     常规类型，表示任何没有由其他 DbType 值显式表示的引用或值类型。
        Object = 13,
        //
        // 摘要:
        //     整型，表示值介于 -128 到 127 之间的有符号 8 位整数。
        SByte = 14,
        //
        // 摘要:
        //     浮点型，表示从大约 1.5 x 10 -45 到 3.4 x 10 38 且精度为 7 位的值。
        Single = 15,
        //
        // 摘要:
        //     表示 Unicode 字符串的类型。
        String = 16,
        //
        // 摘要:
        //     一个表示 SQL Server DateTime 值的类型。如果要使用 SQL Server time 值，请使用 System.Data.SqlDbType.Time。
        Time = 17,
        //
        // 摘要:
        //     整型，表示值介于 0 到 65535 之间的无符号 16 位整数。
        UInt16 = 18,
        //
        // 摘要:
        //     整型，表示值介于 0 到 4294967295 之间的无符号 32 位整数。
        UInt32 = 19,
        //
        // 摘要:
        //     整型，表示值介于 0 到 18446744073709551615 之间的无符号 64 位整数。
        UInt64 = 20,
        //
        // 摘要:
        //     变长数值。
        VarNumeric = 21,
        //
        // 摘要:
        //     非 Unicode 字符的固定长度流。
        AnsiStringFixedLength = 22,
        //
        // 摘要:
        //     Unicode 字符的定长串。
        StringFixedLength = 23,
        //
        // 摘要:
        //     XML 文档或片段的分析表示。
        Xml = 25,
        //
        // 摘要:
        //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //     100 毫微秒。
        DateTime2 = 26,
        //
        // 摘要:
        //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
        DateTimeOffset = 27
    }
}
