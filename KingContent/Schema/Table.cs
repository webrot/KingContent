using System;
using System.Data;

namespace KingContent.Schema
{
    public class TableCommand : ISchemaBuilderCommand
    {
        public string TableName { get; private set; }

        public TableCommand(string tableName)
        {
            TableName = tableName;
        }  
    }
    public class AlterTableCommand : SchemaCommand
    {
        public AlterTableCommand(string name)
            : base(name, SchemaCommandType.AlterTable)
        {
        }

        public void AddColumn(string columnName, DbType dbType, Action<AddColumnCommand> column = null)
        {
            var command = new AddColumnCommand(Name, columnName);
            command.WithType(dbType);

            if (column != null)
            {
                column(command);
            }

            TableCommands.Add(command);
        }

        public void AddColumn<T>(string columnName, Action<AddColumnCommand> column = null)
        {
            var dbType = SchemaUtils.ToDbType(typeof(T));
            AddColumn(columnName, dbType, column);
        }

        public void DropColumn(string columnName)
        {
            var command = new DropColumnCommand(Name, columnName);
            TableCommands.Add(command);
        }

        public void AlterColumn(string columnName, Action<AlterColumnCommand> column = null)
        {
            var command = new AlterColumnCommand(Name, columnName);

            if (column != null)
            {
                column(command);
            }

            TableCommands.Add(command);
        }

        public void CreateIndex(string indexName, params string[] columnNames)
        {
            var command = new AddIndexCommand(Name, indexName, columnNames);
            TableCommands.Add(command);
        }

        public void DropIndex(string indexName)
        {
            var command = new DropIndexCommand(Name, indexName);
            TableCommands.Add(command);
        }
    }

    public class CreateTableCommand : SchemaCommand
    {
        public CreateTableCommand(string name)
            : base(name, SchemaCommandType.CreateTable)
        {
        }

        public CreateTableCommand Column(string columnName, DbType dbType, Action<CreateColumnCommand> column = null)
        {
            var command = new CreateColumnCommand(Name, columnName);
            command.WithType(dbType);

            if (column != null)
            {
                column(command);
            }
            TableCommands.Add(command);
            return this;
        }

        public CreateTableCommand Column<T>(string columnName, Action<CreateColumnCommand> column = null)
        {
            var dbType = SchemaUtils.ToDbType(typeof(T));
            return Column(columnName, dbType, column);
        }

        /// <summary>
        /// Defines a primary column as for content parts
        /// </summary>
        public CreateTableCommand ContentPartRecord()
        {
            Column<int>("Id", column => column.PrimaryKey().NotNull());

            return this;
        }

        /// <summary>
        /// Defines a primary column as for versionnable content parts
        /// </summary>
        public CreateTableCommand ContentPartVersionRecord()
        {
            Column<int>("Id", column => column.PrimaryKey().NotNull());
            Column<int>("ContentItemRecord_id");
            return this;
        } 
    }
    public class DropTableCommand : SchemaCommand
    {
        public DropTableCommand(string name)
            : base(name, SchemaCommandType.DropTable)
        {
        }
    }
    public class AddIndexCommand : TableCommand
    {
        public string IndexName { get; set; }

        public AddIndexCommand(string tableName, string indexName, params string[] columnNames)
            : base(tableName)
        {
            ColumnNames = columnNames;
            IndexName = indexName;
        }

        public string[] ColumnNames { get; private set; }
    }
    public class DropIndexCommand : TableCommand
    {
        public string IndexName { get; set; }

        public DropIndexCommand(string tableName, string indexName)
            : base(tableName)
        {
            IndexName = indexName;
        }
    }

}
