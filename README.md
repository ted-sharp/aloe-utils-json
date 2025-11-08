# Aloe.Utils.Json

[![English](https://img.shields.io/badge/Language-English-blue)](./README.md)
[![日本語](https://img.shields.io/badge/言語-日本語-blue)](./README.ja.md)

[![NuGet Version](https://img.shields.io/nuget/v/Aloe.Utils.Json.svg)](https://www.nuget.org/packages/Aloe.Utils.Json)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Aloe.Utils.Json.svg)](https://www.nuget.org/packages/Aloe.Utils.Json)
[![License](https://img.shields.io/github/license/ted-sharp/aloe-utils-json.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)

`Aloe.Utils.Json` is a lightweight utility for JSON serialization and formatting in .NET applications.

## Main Features

* Simple JSON serialization and deserialization with extension methods
* JSON string formatting with proper indentation
* Type-safe conversion between objects and JSON

## Supported Environments

* .NET 9 and later

## Install

Install via NuGet Package Manager:

```cmd
Install-Package Aloe.Utils.Json
```

Or using .NET CLI:

```cmd
dotnet add package Aloe.Utils.Json
```

## Usage

### Basic Usage

```csharp
using Aloe.Utils.Json;

// Serialize an object to JSON
var person = new Person { Name = "John", Age = 30 };
string json = person.ToJson();

// Deserialize JSON to an object
var deserializedPerson = json.ToObj<Person>();

// Format a JSON string
string formattedJson = json.FormatJson();
```

### AOT/Trimming-Compatible Usage

When using Native AOT or trimming, define a `JsonSerializerContext` and use the overloads that accept `JsonTypeInfo`.

```csharp
using System.Text.Json.Serialization;
using Aloe.Utils.Json;

// Define a JsonSerializerContext
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(JsonElement))]
internal partial class AppJsonContext : JsonSerializerContext
{
}

// Serialize an object to JSON (AOT-compatible)
var person = new Person { Name = "John", Age = 30 };
string json = person.ToJson(AppJsonContext.Default.Person);

// Deserialize JSON to an object (AOT-compatible)
var deserializedPerson = json.ToObj(AppJsonContext.Default.Person);

// Format a JSON string (AOT-compatible)
string formattedJson = json.FormatJson(AppJsonContext.Default.JsonElement);
```

## Note

The library uses `System.Text.Json` under the hood and provides a convenient way to work with JSON data in your .NET applications.

## License

MIT License

## Contributing

Bug reports and feature requests are welcome on GitHub Issues. Pull requests are also appreciated.
