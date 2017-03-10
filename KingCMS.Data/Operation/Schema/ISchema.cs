using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCMS.Data.Operation.Schema
{
    interface ISchema
    {
        bool CreateTable();
        bool DropTable();
        bool AlertTable();

        bool TableIsExist();

        bool ViewIsExist();
    }
}
