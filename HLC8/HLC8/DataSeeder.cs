// Генерування sql запиту, який додасть в базу близько 40 мільйонів записів

using System.Diagnostics;
using MySqlConnector;

var dummyCont = new List<int>();

for (int i = 0; i < 150; i++)
{
    dummyCont.Add(i);
}

await Parallel.ForEachAsync(dummyCont, new ParallelOptions()
{
    MaxDegreeOfParallelism = 3
}, async (coasdfunter, token) =>
 {
     var watch = new Stopwatch();
     watch.Start();

     using var connection = new MySqlConnection("server=localhost;user=root;password=my-secret-pw;database=HLC8");

     await connection.OpenAsync();

     var query = "INSERT INTO Users2 (firstname, dob) VALUES ";

     var baseStr = " ,(SUBSTRING(MD5(RAND()) FROM 1 FOR 10), FROM_UNIXTIME(UNIX_TIMESTAMP('1950-04-30 14:53:27') + FLOOR(0 + (RAND() * 63072000))))";
     
     var internalQuery = baseStr;

     var counter = 1;

     for (int i = 0; i < 18; i++)
     {
         internalQuery += internalQuery;
         counter += counter;
     }

     query += internalQuery;

     query = query.Replace("VALUES  ,", "VALUES ");
     query += ";";

     using var command = new MySqlCommand(query, connection);
     
     using var reader = await command.ExecuteReaderAsync();
     while (await reader.ReadAsync())
     {
     }
 });
