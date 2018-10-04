# SqlBulkInsert Extension
Inserts List of data into the database using sql bulk insert. Internally maps the List to datatable and column mappings

```    
	Install-Package Rmb.Sql.Extension.BulkInsert
```

## Code Example
Sample usage example

```C#
// Add Sql Column mapping using BulkInsertMapping Attribute
// To Exclude property from column mapping use [BulkInsertMapping(false)]
// If BulkInsertMapping attribute is not present, both source and destination column name will be equivalent to property name
// [BulkInsertMapping("Source", "Destination")]
 public class Movie
    {
        [BulkInsertMapping(false)]
        public string Id {get;set;}
        [BulkInsertMapping("Nam", "Name")]
        public string Nam { get; set; }
        [BulkInsertMapping("DirNam", "DirectorName")]
        public string DirNam { get; set; }
        [BulkInsertMapping("Pic", "Picture")]
        public string Pic { get; set; }
        [BulkInsertMapping("ProdNam", "ProducerName")]
        public string ProdNam { get; set; }
        public string ActorName { get; set; }
    }


var movies = new List<Movie>();
using (var connection = new SqlConnection("connectionString")
{
	// BulkInsert("DestinationTableName", ListData)
  connection.BulkInsert("Movies", movies);
}

```	

