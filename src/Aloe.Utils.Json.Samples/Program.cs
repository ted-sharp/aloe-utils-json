using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Aloe.Utils.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

// ========================================
// JsonExtensions の使い方デモ
// ========================================
Console.WriteLine("=== Aloe.Utils.Json Demo ===");
Console.WriteLine();

// 1. ToJson() - オブジェクトをJSONに変換
var person = new Person
{
    Name = "Alice",
    Age = 30,
    Email = "alice@example.com"
};

string json = person.ToJson();
Console.WriteLine("1. ToJson() - オブジェクト → JSON:");
Console.WriteLine(json);
Console.WriteLine();

// 2. ToObj() - JSONをオブジェクトに変換
var jsonString = """
    {
        "Name": "Bob",
        "Age": 25,
        "Email": "bob@example.com"
    }
    """;

var deserializedPerson = jsonString.ToObj<Person>();
Console.WriteLine("2. ToObj() - JSON → オブジェクト:");
Console.WriteLine($"Name: {deserializedPerson?.Name}, Age: {deserializedPerson?.Age}, Email: {deserializedPerson?.Email}");
Console.WriteLine();

// 3. FormatJson() - JSON文字列を整形
var compactJson = """{"Name":"Charlie","Age":35,"Email":"charlie@example.com"}""";
var formattedJson = compactJson.FormatJson();
Console.WriteLine("3. FormatJson() - JSON整形:");
Console.WriteLine(formattedJson);
Console.WriteLine();

// 4. ToJson(JsonTypeInfo) - AOT/トリミング対応版
var aotPerson = new Person
{
    Name = "David",
    Age = 28,
    Email = "david@example.com"
};
var aotJson = aotPerson.ToJson(AppJsonContext.Default.Person);
Console.WriteLine("4. ToJson(JsonTypeInfo) - AOT/トリミング対応:");
Console.WriteLine(aotJson);
Console.WriteLine();

// 5. ToObj(JsonTypeInfo) - AOT/トリミング対応版
var aotJsonString = """
    {
        "Name": "Eve",
        "Age": 32,
        "Email": "eve@example.com"
    }
    """;
var aotDeserializedPerson = aotJsonString.ToObj(AppJsonContext.Default.Person);
Console.WriteLine("5. ToObj(JsonTypeInfo) - AOT/トリミング対応:");
Console.WriteLine($"Name: {aotDeserializedPerson?.Name}, Age: {aotDeserializedPerson?.Age}, Email: {aotDeserializedPerson?.Email}");
Console.WriteLine();

Console.WriteLine("=== Configuration Manager Demo ===");
Console.WriteLine();

// 1. .NET 9以降の最小ホスト ビルダーを作成
var builder = Host.CreateApplicationBuilder(args);

// 2. ConfigurationManager に対してベースパスと JSON ファイルを設定
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile("appsettings.PostgreSQL.json", optional: true)
    .AddJsonFile("appsettings.Serilog.json", optional: true);

// 3. ビルドして IHost を生成
using var host = builder.Build();

// 4. IConfiguration を取得
var config = host.Services.GetRequiredService<IConfiguration>();

// 5. ConnectionStrings:DefaultConnection を表示
var defaultConn = config.GetConnectionString("DefaultConnection");
Console.WriteLine("=== ConnectionStrings:DefaultConnection ===");
Console.WriteLine(defaultConn);
Console.WriteLine();

// 6. PostgreSQL セクションを表示
var pg = config.GetSection("PostgreSQL");
Console.WriteLine("=== PostgreSQL Configuration ===");
Console.WriteLine($"Host     : {pg["Host"]}");
Console.WriteLine($"Database : {pg["Database"]}");
Console.WriteLine($"User     : {pg["Username"]}");
Console.WriteLine($"Password : {pg["Password"]}");
Console.WriteLine();

// 7. Serilog セクションを表示
var serilog = config.GetSection("Serilog");
Console.WriteLine("=== Serilog Configuration ===");
Console.WriteLine($"MinimumLevel : {serilog["MinimumLevel"]}");
Console.WriteLine("WriteTo      : " +
    String.Join(", ",
        serilog
          .GetSection("WriteTo")
          .GetChildren()
          .Select(c => c["Name"])));

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
Console.WriteLine();

// ========================================
// 型定義（トップレベルステートメントの後に配置）
// ========================================

/// <summary>
/// サンプル用のPersonクラス
/// </summary>
public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// AOT/トリミング対応用のJsonSerializerContext
/// </summary>
[JsonSerializable(typeof(Person))]
internal partial class AppJsonContext : JsonSerializerContext
{
}
