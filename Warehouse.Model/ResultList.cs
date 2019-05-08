
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Model
{
    public class ResultList<T>
    {
        public List<T> List { get; set; }
        public int TotalCount { get; set; }
        public ResultList()
        {
            List = new List<T>();
            TotalCount = 0;
        }
        public ResultList(List<T> list , int totalCount)
        {
            List = list;
            TotalCount = totalCount;
        }
    }
}
