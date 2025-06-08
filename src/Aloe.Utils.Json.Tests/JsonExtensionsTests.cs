// <copyright file="JsonExtensionsTests.cs" company="Aloe">
// Copyright (c) Aloe. All rights reserved.
// </copyright>

using System.Text.Json;

namespace Aloe.Utils.Json.Tests;

/// <summary>
/// JsonExtensionsのテストクラスですわ。
/// </summary>
public class JsonExtensionsTests
{
    /// <summary>
    /// テスト用のサンプルクラスですわ。
    /// </summary>
    private class SampleClass
    {
        public string Name { get; set; } = String.Empty;
        public int Age { get; set; }
    }

    [Fact(DisplayName = "ToJson_正常系_シリアライズできること")]
    public void ToJson_正常系_シリアライズできること()
    {
        // Arrange
        var sample = new SampleClass { Name = "Test", Age = 20 };
        var expected = """
            {
              "Name": "Test",
              "Age": 20
            }
            """;

        // Act
        var actual = sample.ToJson();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "FromJson_正常系_デシリアライズできること")]
    public void FromJson_正常系_デシリアライズできること()
    {
        // Arrange
        var json = """
            {
                "Name": "Test",
                "Age": 20
            }
            """;
        var expected = new SampleClass { Name = "Test", Age = 20 };

        // Act
        var actual = json.FromJson<SampleClass>();

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Age, actual.Age);
    }

    [Fact(DisplayName = "FromJson_異常系_不正なJSONの場合は例外が発生すること")]
    public void FromJson_異常系_不正なJSONの場合は例外が発生すること()
    {
        // Arrange
        var invalidJson = "{invalid json}";

        // Act & Assert
        Assert.Throws<JsonException>(() => invalidJson.FromJson<SampleClass>());
    }

    [Fact(DisplayName = "FormatJson_正常系_JSONが整形されること")]
    public void FormatJson_正常系_JSONが整形されること()
    {
        // Arrange
        var input = """
            {
                "Name": "Test",
                "Age": 20
            }
            """;
        var expected = """
            {
              "Name": "Test",
              "Age": 20
            }
            """;

        // Act
        var actual = input.FormatJson();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "FormatJson_異常系_不正なJSONの場合は例外が発生すること")]
    public void FormatJson_異常系_不正なJSONの場合は例外が発生すること()
    {
        // Arrange
        var invalidJson = "{invalid json}";

        // Act & Assert
        Assert.Throws<JsonException>(() => invalidJson.FormatJson());
    }
}
