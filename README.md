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

```csharp
using Aloe.Utils.Json;

// Serialize an object to JSON
var person = new Person { Name = "John", Age = 30 };
string json = person.ToJson();

// Deserialize JSON to an object
var deserializedPerson = json.FromJson<Person>();

// Format a JSON string
string formattedJson = json.FormatJson();
```

## Note

The library uses `System.Text.Json` under the hood and provides a convenient way to work with JSON data in your .NET applications.

## License

MIT License

## Contributing

Bug reports and feature requests are welcome on GitHub Issues. Pull requests are also appreciated.
