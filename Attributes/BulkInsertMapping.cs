using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert.Attributes
{
    public class BulkInsertMapping : Attribute
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public bool Include { get; set; }
        public BulkInsertMapping(string source, string destination, bool include = false)
        {
            Source = source;
            Destination = destination;
            Include = include;
        }
        public BulkInsertMapping(bool include = true)
        {
            Include = include;
        }
    }
}
