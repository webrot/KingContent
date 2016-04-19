using KingContent.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent.DataProvider
{
    public interface ISession : IDisposable
    {

    }
    public class Session:ISession
    {
        public DatabaseSchema Database { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
