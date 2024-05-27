// Copyright (c) Alexander Bocharov.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System.Text;

namespace AlexBocharov.Xml.Tests;

public class XmlConvertTests
{
    [Fact]
    public void Serialize_NullValue_ReturnsNull()
    {
        // Arrange
        object? value = null;

        // Act
        var result = XmlConvert.Serialize(value);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Serialize_NonNullValue_ReturnsByteArray()
    {
        // Arrange
        var value = new TestObject { Name = "John", Age = 30 };

        // Act
        var result = XmlConvert.Serialize(value);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<byte[]>(result);
    }

    [Fact]
    public void Deserialize_NullValue_ReturnsDefault()
    {
        // Arrange
        byte[]? value = null;

        // Act
        var result = XmlConvert.Deserialize<TestObject>(value!);

        // Assert
        Assert.Equal(default(TestObject), result);
    }

    [Fact]
    public void Deserialize_EmptyValue_ReturnsDefault()
    {
        // Arrange
        byte[] value = new byte[0];

        // Act
        var result = XmlConvert.Deserialize<TestObject>(value);

        // Assert
        Assert.Equal(default(TestObject), result);
    }

    [Fact]
    public void Deserialize_NonEmptyValue_ReturnsDeserializedObject()
    {
        // Arrange
        var value = Encoding.UTF8.GetBytes("<TestObject><Name>John</Name><Age>30</Age></TestObject>");

        // Act
        var result = XmlConvert.Deserialize<TestObject>(value);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
        Assert.Equal(30, result.Age);
    }

    public class TestObject
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
    }
}
