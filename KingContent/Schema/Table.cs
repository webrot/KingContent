﻿

namespace KingContent
{
    public class TableCommand : ISchemaBuilderCommand
    {
        public string TableName { get; private set; }

        public TableCommand(string tableName)
        {
            TableName = tableName;
        }


    }
}
