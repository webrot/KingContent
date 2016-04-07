using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Xml.Serialization;


namespace KingContent.Schema
{ 
    public interface ISchemaBuilderCommand
    {
    }

    public abstract class SchemaCommand : ISchemaBuilderCommand
    {
        protected SchemaCommand(string name, SchemaCommandType type)
        {
            TableCommands = new List<TableCommand>();
            Type = type;
            WithName(name);
        }

        public string Name { get; private set; }
        public List<TableCommand> TableCommands { get; private set; }

        public SchemaCommandType Type { get; private set; }

        public SchemaCommand WithName(string name)
        {
            Name = name;
            return this;
        }
    }
    public class CreateForeignKeyCommand : SchemaCommand
    {
        public string[] DestColumns { get; private set; }

        public string DestTable { get; private set; }

        public string[] SrcColumns { get; private set; }

        public string SrcTable { get; private set; }

        public CreateForeignKeyCommand(string name, string srcTable, string[] srcColumns, string destTable, string[] destColumns) : base(name, SchemaCommandType.CreateForeignKey)
        {
            SrcColumns = srcColumns;
            DestTable = destTable;
            DestColumns = destColumns;
            SrcTable = srcTable;
        }
    }

    public class DropForeignKeyCommand : SchemaCommand
    {
        public string SrcTable { get; private set; }

        public DropForeignKeyCommand(string srcTable, string name)
            : base(name, SchemaCommandType.DropForeignKey)
        {
            SrcTable = srcTable;
        }
    }
    public enum SchemaCommandType
    {
        CreateTable,
        DropTable,
        AlterTable,
        SqlStatement,
        CreateForeignKey,
        DropForeignKey
    }
}
