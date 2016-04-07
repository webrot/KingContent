using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingContent
{
    public class TreeObject<T>
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public int OrderIndex { get; set; }
        public List<TreeObject<T>> Childrens = new List<TreeObject<T>>();
        public List<T> Objs = new List<T>();

        public void AddChildren(string name)
        {
            Childrens.Add(
                new TreeObject<T>()
                {
                    UUID = Guid.NewGuid().ToString(),
                    Name = name,
                    OrderIndex = Childrens.Count()
                });
        }

        public void RemoveChildren(string id)
        {
            /*
            Childrens.Where(i => i.UUID.Equals(id))
                .ToList()
                .ForEach(x => Childrens.Remove(x));
            */
            int index = 0;
            foreach(var x in Childrens)
            {
                if (x.UUID.Equals(id))
                {
                    Childrens.Remove(x);
                    continue;
                }
                x.OrderIndex = index;
                index++;    
            }
        }
         
    }
}
