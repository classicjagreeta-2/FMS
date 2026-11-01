using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppFileMange.Models
{
    public class Parameter
    {
        public int[] MyIntArrayData { get; set; }
    }
    public class ResultList<T>
    {
        public List<T> items { get; set; }
        public int total_count { get; set; }
    }
    public class ModelTextId
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}