// Copyright (c) Alexander Bocharov.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace AlexBocharov.Xml.Tests;

public class SerializableDictionaryTests
{
    [Fact]
    public void ReadXml_ShouldDeserializeXmlToDictionary()
    {
        // Arrange
        string xml = """<?xml version="1.0" encoding="utf-16"?><SerializableDictionary><key1>value1</key1><key2>value2</key2></SerializableDictionary>""";

        var serializer = new XmlSerializer(typeof(SerializableDictionary));
        var reader = XmlReader.Create(new StringReader(xml));

        // Act
        var dictionary = (SerializableDictionary?)serializer.Deserialize(reader);

        // Assert
        Assert.NotNull(dictionary);
        Assert.Equal("value1", dictionary["key1"]);
        Assert.Equal("value2", dictionary["key2"]);
    }

    [Fact]
    public void WriteXml_ShouldSerializeDictionaryToXml()
    {
        // Arrange
        string xml = """<?xml version="1.0" encoding="utf-16"?><SerializableDictionary><key1>value1</key1><key2>value2</key2></SerializableDictionary>""";

        var dictionary = new SerializableDictionary
        {
            { "key1", "value1" },
            { "key2", "value2" },
            { "key3", null }
        };

        var serializer = new XmlSerializer(typeof(SerializableDictionary));
        using var writer = new StringWriter();
        using var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            Indent = false,
            OmitXmlDeclaration = false
        });

        // Act
        serializer.Serialize(xmlWriter, dictionary);

        // Assert
        Assert.Equal(xml, writer.ToString());
    }
}
