// Copyright (c) Alexander Bocharov.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace AlexBocharov.Xml;

/// <summary>
/// Provides methods for converting between .NET types and XML types.
/// </summary>
/// <remarks>
/// This class provides methods for converting between .NET types and XML types.
/// </remarks>
public static class XmlConvert
{
    /// <summary>
    /// Serializes the specified objects of <typeparamref name="T"/> type and writes the XML document to a byte array.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize to.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <returns>A byte array containing the XML document.</returns>
    public static byte[]? Serialize<T>(T value)
    {
        if (value == null)
        {
            return null;
        }

        var xmlSerializer = new XmlSerializer(typeof(T));

        using var memoryStream = new MemoryStream();
        using var xmlWriter = XmlWriter.Create(memoryStream, GetXmlWriterSettings());
        xmlSerializer.Serialize(xmlWriter, value);

        memoryStream.Flush();
        return memoryStream.ToArray();

        static XmlWriterSettings GetXmlWriterSettings()
            => new()
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                OmitXmlDeclaration = false
            };
    }

    /// <summary>
    /// Deserializes the XML document contained by the specified byte array.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="value">The byte array containing the XML document to deserialize.</param>
    /// <returns>The object of <typeparamref name="T"/> type being deserialized.</returns>
    public static T? Deserialize<T>(byte[] value)
    {
        if (value == null || value.Length == 0)
        {
            return default;
        }

        var xmlSerializer = new XmlSerializer(typeof(T));
        using var memoryStream = new MemoryStream(value);

        return (T?)xmlSerializer.Deserialize(memoryStream);
    }
}