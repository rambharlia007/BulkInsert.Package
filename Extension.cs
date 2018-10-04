using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public static class Extension
    {
        /// <summary>
        /// Inserts list of data into database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">Destination table</param>
        /// <param name="list"></param>
        public static void BulkInsert<T>(this SqlConnection connection, string destinationTable, List<T> list)
        {
            var bulkInsert = new BulkInsert(connection, destinationTable); 
            bulkInsert.ToDataTable(list);
            bulkInsert.Insert();
        }

        public static void BulkInsert<T>(this SqlConnection connection, string destinationTable, List<T> list, BulkCopySettings settings)
        {
            var bulkInsert = new BulkInsert(connection, destinationTable, settings);
            bulkInsert.ToDataTable(list);
            bulkInsert.Insert();
        }
    }
}
