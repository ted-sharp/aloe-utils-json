# Aloe.Utils.Json

[![English](https://img.shields.io/badge/Language-English-blue)](./README.md)
[![日本語](https://img.shields.io/badge/言語-日本語-blue)](./README.ja.md)

[![NuGet Version](https://img.shields.io/nuget/v/Aloe.Utils.Json.svg)](https://www.nuget.org/packages/Aloe.Utils.Json)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Aloe.Utils.Json.svg)](https://www.nuget.org/packages/Aloe.Utils.Json)
[![License](https://img.shields.io/github/license/ted-sharp/aloe-utils-json.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)

`Aloe.Utils.Json` は、.NETアプリケーションにおけるJSONシリアライズとフォーマットのための軽量ユーティリティです。

## 主な機能

* 拡張メソッドによる簡単なJSONシリアライズとデシリアライズ
* 適切なインデントによるJSON文字列のフォーマット
* オブジェクトとJSON間の型安全な変換

## 対応環境

* .NET 9 以降

## インストール

NuGet パッケージマネージャーからインストールします：

```cmd
Install-Package Aloe.Utils.Json
```

あるいは、.NET CLI で：

```cmd
dotnet add package Aloe.Utils.Json
```

## 使用方法

### 基本的な使い方

```csharp
using Aloe.Utils.Json;

// オブジェクトをJSONに変換
var person = new Person { Name = "John", Age = 30 };
string json = person.ToJson();

// JSONをオブジェクトに変換
var deserializedPerson = json.ToObj<Person>();

// JSON文字列を整形
string formattedJson = json.FormatJson();
```

### AOT/トリミング対応版の使い方

Native AOTやトリミングを使用する場合は、`JsonSerializerContext`を定義して`JsonTypeInfo`を渡すオーバーロードを使用します。

```csharp
using System.Text.Json.Serialization;
using Aloe.Utils.Json;

// JsonSerializerContextを定義
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(JsonElement))]
internal partial class AppJsonContext : JsonSerializerContext
{
}

// オブジェクトをJSONに変換（AOT対応）
var person = new Person { Name = "John", Age = 30 };
string json = person.ToJson(AppJsonContext.Default.Person);

// JSONをオブジェクトに変換（AOT対応）
var deserializedPerson = json.ToObj(AppJsonContext.Default.Person);

// JSON文字列を整形（AOT対応）
string formattedJson = json.FormatJson(AppJsonContext.Default.JsonElement);
```

## 注意事項

このライブラリは内部で`System.Text.Json`を使用し、.NETアプリケーションでJSONデータを扱うための便利な方法を提供します。

## ライセンス

MIT License

## 貢献

バグ報告や機能要望は、GitHub Issues でお願いします。プルリクエストも歓迎します。
