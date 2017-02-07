using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingCMS.Data.Model
{
    public class Table
    {
        public string TableName { get; set; }
        public List<Column> Columns { get; private set; }

        public Table(string tablename)
        {
            Columns = new List<Column>();
            TableName = tablename;
        }

        #region override object

        public static bool operator == (Table obj1,Table obj2)
        {
            if (object.Equals(obj1, obj2) == true)
                return true;
            if (object.Equals(obj1, null) == true || object.Equals(obj2, null) == true) 
                return false; 

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Table obj1, Table obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        { 
            if (!(obj is Table))
            {
                return false;
            }
            if (obj != null)
            {
                var table = (Table)obj;
                if (string.Compare(this.TableName, table.TableName, true) == 0)
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.TableName))
            {
                return base.GetHashCode();
            }
            else
            {
                return this.TableName.ToLower().GetHashCode();
            }
        }
        public override string ToString()
        {
            return this.TableName;
        }
        #endregion

        public Table DeepClone()
        {
            var table = new Table(this.TableName);
            if (this.Columns != null)
            {
                foreach (var item in this.Columns)
                {
                    table.Columns.Add(item.DeepClone());
                }
            }
            return table;
        }

        public List<Column> Indexs
        {
            get
            {
                return Columns.Where(i => i.Isindex).ToList();
            }
        }
    }


}
