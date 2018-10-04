using BulkInsert.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BulkInsert
{
    public class BulkInsert
    {
        private SqlConnection Connection { get; set; }
        private DataTable DataTable { get; set; }
        private SqlBulkCopy BulkCopy { get; set; }
        private BulkCopySettings BulkCopySettings { get; set; }
        private string TableName { get; set; }

        public BulkInsert(SqlConnection connection, string table)
        {
            BulkCopySettings = new BulkCopySettings();
            Connection = connection;
            TableName = table;
            BulkCopy = new SqlBulkCopy(connection);
        }

        public BulkInsert(SqlConnection connection, string table, BulkCopySettings bulkCopySettings)
        {
            BulkCopySettings = bulkCopySettings;
            Connection = connection;
            TableName = table;
            BulkCopy = new SqlBulkCopy(connection.ConnectionString, bulkCopySettings.copyOptions);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            var mappings = new List<BulkInsertMapping>();
            DataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var mapping = prop.GetCustomAttribute<BulkInsertMapping>();
                mappings.Add(mapping);
                if (mapping != null && !mapping.Include) continue;
                if (mapping != null)
                    BulkCopy.ColumnMappings.Add(mapping.Source, mapping.Destination);
                else
                    BulkCopy.ColumnMappings.Add(prop.Name, prop.Name);
                DataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[DataTable.Columns.Count];
                int j = 0;
                for (int i = 0; i < Props.Length; i++)
                {
                    if (mappings[i] != null && !mappings[i].Include) continue;
                    values[j++] = Props[i].GetValue(item, null);
                }
                DataTable.Rows.Add(values);
            }
            return DataTable;
        }

        public void Insert()
        {
            BulkCopy.BulkCopyTimeout = BulkCopySettings.BulkCopyTimeout;
            BulkCopy.BatchSize = BulkCopySettings.BatchSize;
            BulkCopy.DestinationTableName = TableName;
            Connection.Open();
            BulkCopy.WriteToServer(DataTable);
            Connection.Close();
        }
    }
}
