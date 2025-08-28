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
        TypeInfoResolver = JsonSerializerOptions.Default.TypeInfoResolver,
    };

    /// <summary>
    /// オブジェクトをJSON文字列に変換します。
    /// </summary>
    /// <typeparam name="T">変換するオブジェクトの型。</typeparam>
    /// <param name="obj">変換するオブジェクト。</param>
    /// <returns>JSON文字列。</returns>
    /// <exception cref="ArgumentNullException">objがnullの場合。</exception>
    /// <exception cref="InvalidOperationException">シリアル化に失敗した場合。</exception>
    public static string ToJson<T>(this T obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        
        try
        {
            return JsonSerializer.Serialize(obj, DefaultOptions);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to serialize object to JSON", ex);
        }
    }

    /// <summary>
    /// JSON文字列をオブジェクトに変換します。
    /// </summary>
    /// <typeparam name="T">変換先のオブジェクトの型。</typeparam>
    /// <param name="json">変換するJSON文字列。</param>
    /// <returns>変換されたオブジェクト。</returns>
    /// <exception cref="ArgumentException">jsonがnullまたは空文字列の場合。</exception>
    /// <exception cref="InvalidOperationException">デシリアル化に失敗した場合。</exception>
    public static T? FromJson<T>(this string json)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        
        try
        {
            return JsonSerializer.Deserialize<T>(json, DefaultOptions);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to deserialize JSON to object", ex);
        }
    }

    /// <summary>
    /// JSON文字列を整形します。
    /// </summary>
    /// <param name="json">整形するJSON文字列。</param>
    /// <returns>整形されたJSON文字列。</returns>
    /// <exception cref="ArgumentException">jsonがnullまたは空文字列の場合。</exception>
    /// <exception cref="InvalidOperationException">JSON整形に失敗した場合。</exception>
    public static string FormatJson(this string json)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        
        try
        {
            using var document = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(document.RootElement, DefaultOptions);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to format JSON string", ex);
        }
    }
}
