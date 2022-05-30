// Генерування заданої к-сті запитів з наступним логуванням
// потраченого часу на цю к-ть запитів

using System.Diagnostics;
using MySqlConnector;

var RequestCount = 10000;

var dummyCheck = new List<int>();
for (int i = 0; i < RequestCount; i++)
{
    dummyCheck.Add(i);
}

var stopwatch = new Stopwatch();
stopwatch.Start();

await Parallel.ForEachAsync(dummyCheck, async (i, token) =>
{
    using var connection = new MySqlConnection("server=localhost;user=root;password=my-secret-pw;database=HLC8");

    await connection.OpenAsync();
    var query = "INSERT INTO Users2 (firstname, dob) VALUES (SUBSTRING(MD5(RAND()) FROM 1 FOR 10), FROM_UNIXTIME(UNIX_TIMESTAMP('1950-04-30 14:53:27') + FLOOR(0 + (RAND() * 63072000))))";

    using var command = new MySqlCommand(query, connection);

    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
    }
});


stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);
Console.ReadLine();