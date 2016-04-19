
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingContent.Schema;

namespace KingContent.DriverFactory
{
    public abstract class BaseDriverDriver : IDriver
    {
        public virtual string Name
        {
            get
            {
                return "basedriver";
            }
        }
        public BaseDriverDriver()
        {

        }

    }
}
