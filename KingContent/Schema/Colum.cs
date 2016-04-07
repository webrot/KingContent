using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data;

namespace KingContent.Schema
{
    public class ColumnCommand : TableCommand
    {
        public string ColumnName { get; set; }

        public ColumnCommand(string tableName, string name)
            : base(tableName)
        {
            ColumnName = name;
            DbType = DbType.Object;
            Default = null;
            Length = null;
        }
        public byte Scale { get; protected set; }

        public byte Precision { get; protected set; }

        public DbType DbType { get; private set; }

        public object Default { get; private set; }

        public int? Length { get; private set; }

        public ColumnCommand WithType(DbType dbType)
        {
            DbType = dbType;
            return this;
        }

        public ColumnCommand WithDefault(object @default)
        {
            Default = @default;
            return this;
        }


        public ColumnCommand WithLength(int? length)
        {
            Length = length;
            return this;
        }

        public ColumnCommand Unlimited()
        {
            return WithLength(10000);
        }
    }

    public class CreateColumnCommand : ColumnCommand
    {
        public CreateColumnCommand(string tableName, string name) : base(tableName, name)
        {
            IsNotNull = false;
            IsUnique = false;
        }

        public bool IsUnique { get; protected set; }

        public bool IsNotNull { get; protected set; }

        public bool IsPrimaryKey { get; protected set; }

        public bool IsIdentity { get; protected set; }

        public CreateColumnCommand PrimaryKey()
        {
            IsPrimaryKey = true;
            IsUnique = false;
            return this;
        }

        public CreateColumnCommand Identity()
        {
            IsIdentity = true;
            IsUnique = false;
            return this;
        }

        public CreateColumnCommand WithPrecision(byte precision)
        {
            Precision = precision;
            return this;
        }

        public CreateColumnCommand WithScale(byte scale)
        {
            Scale = scale;
            return this;
        }

        public CreateColumnCommand NotNull()
        {
            IsNotNull = true;
            return this;
        }

        public CreateColumnCommand Nullable()
        {
            IsNotNull = false;
            return this;
        }

        public CreateColumnCommand Unique()
        {
            IsUnique = true;
            IsPrimaryKey = false;
            IsIdentity = false;
            return this;
        }

        public CreateColumnCommand NotUnique()
        {
            IsUnique = false;
            return this;
        }

        public new CreateColumnCommand WithLength(int? length)
        {
            base.WithLength(length);
            return this;
        }

        public new CreateColumnCommand Unlimited()
        {
            return WithLength(10000);
        }

        public new CreateColumnCommand WithType(DbType dbType)
        {
            base.WithType(dbType);
            return this;
        }

        public new CreateColumnCommand WithDefault(object @default)
        {
            base.WithDefault(@default);
            return this;
        }
    }

    public class AddColumnCommand : CreateColumnCommand
    {
        public AddColumnCommand(string tableName, string name) : base(tableName, name)
        {
        }
    }

    public class AlterColumnCommand : ColumnCommand
    {
        public AlterColumnCommand(string tableName, string columnName)
            : base(tableName, columnName)
        {
        }

        public new AlterColumnCommand WithType(DbType dbType)
        {
            base.WithType(dbType);
            return this;
        }

        public AlterColumnCommand WithType(DbType dbType, int? length)
        {
            base.WithType(dbType).WithLength(length);
            return this;
        }

        public AlterColumnCommand WithType(DbType dbType, byte precision, byte scale)
        {
            base.WithType(dbType);
            Precision = precision;
            Scale = scale;
            return this;
        }

        public new AlterColumnCommand WithLength(int? length)
        {
            base.WithLength(length);
            return this;
        }

        public new AlterColumnCommand Unlimited()
        {
            return WithLength(10000);
        }

    }

    public class DropColumnCommand : ColumnCommand
    { 
        public DropColumnCommand(string tableName, string columnName)
            : base(tableName, columnName)
        {
        }
    }
}
