using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class BulkCopySettings
    {
        //   copyOptions:
        //     A combination of values from the System.Data.SqlClient.SqlBulkCopyOptions enumeration
        //     that determines which data source rows are copied to the destination table.
        public SqlBulkCopyOptions copyOptions { get; set; }
        //
        // Summary:
        //     Number of seconds for the operation to complete before it times out.
        //
        // Returns:
        //     The integer value of the System.Data.SqlClient.SqlBulkCopy.BulkCopyTimeout property.
        //     The default is 300 seconds. A value of 0 indicates no limit; the bulk copy will
        //     wait indefinitely.
        public int BulkCopyTimeout { get; set; } = 300;
        //
        // Summary:
        //     Number of rows in each batch. At the end of each batch, the rows in the batch
        //     are sent to the server.
        //
        // Returns:
        //     The integer value of the System.Data.SqlClient.SqlBulkCopy.BatchSize property,
        //     or 50K if no value has been set.
        public int BatchSize { get; set; } = 50000;
    }
}
