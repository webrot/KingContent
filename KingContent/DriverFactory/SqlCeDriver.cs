using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent.DriverFactory
{
    public class SqlCeDriver : BaseDriverDriver
    {
        public override string Name
        {
            get
            {
                return "sqlce";
            }
        }
    }
}
