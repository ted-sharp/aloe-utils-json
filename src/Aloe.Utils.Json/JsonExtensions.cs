// <copyright file="JsonExtensions.cs" company="ted-sharp">
// Copyright (c) ted-sharp. All rights reserved.
// </copyright>

using System.Text.Json;

namespace Aloe.Utils.Json;

/// <summary>
/// JSON操作のための拡張メソッドを提供します。
/// </summary>
public static class JsonExtensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        WriteIndented = true,
        TypeInfoResolver = JsonSerializer.IsReflectionEnabledByDefault
            ? JsonSerializerOptions.Default.TypeInfoResolver
            : JsonSerializerOptions.Default.TypeInfoResolver,
    };

    /// <summary>
    /// オブジェクトをJSON文字列に変換します。
    /// </summary>
    /// <typeparam name="T">変換するオブジェクトの型。</typeparam>
    /// <param name="obj">変換するオブジェクト。</param>
    /// <returns>JSON文字列。</returns>
    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, DefaultOptions);
    }

    /// <summary>
    /// JSON文字列をオブジェクトに変換します。
    /// </summary>
    /// <typeparam name="T">変換先のオブジェクトの型。</typeparam>
    /// <param name="json">変換するJSON文字列。</param>
    /// <returns>変換されたオブジェクト。</returns>
    public static T? FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }

    /// <summary>
    /// JSON文字列を整形します。
    /// </summary>
    /// <param name="json">整形するJSON文字列。</param>
    /// <returns>整形されたJSON文字列。</returns>
    public static string FormatJson(this string json)
    {
        var element = JsonSerializer.Deserialize<JsonElement>(json);
        return JsonSerializer.Serialize(element, DefaultOptions);
    }
}
