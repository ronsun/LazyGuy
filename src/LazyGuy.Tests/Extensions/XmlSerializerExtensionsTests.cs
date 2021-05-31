using FluentAssertions;
using NUnit.Framework;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class XmlSerializerExtensionsTests
    {
        private static readonly string _utf8BOM = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

        public class FakeClass
        {
            public string FakeString { get; set; } = "A";
        }

        [Test()]
        public void SerializeTest_InputObject_ReturnCleanXmlWithDeclaration()
        {
            // arrange
            var stubClass = new FakeClass();
            var target = new XmlSerializer(typeof(FakeClass));
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_InputAllParameters_OmitXmlDeclaration_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            var stubXmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var target = new XmlSerializer(typeof(FakeClass));
            var expected = $"{_utf8BOM}<FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings, stubXmlSerializerNamespaces);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_InputAllParameters_IncludeBOM_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { Encoding = Encoding.UTF8 };
            var stubXmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var target = new XmlSerializer(typeof(FakeClass));
            var expected = $"{_utf8BOM}<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings, stubXmlSerializerNamespaces);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_InputAllParameters_WithoutBOM_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { Encoding = new UTF8Encoding() };
            var stubXmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var target = new XmlSerializer(typeof(FakeClass));
            var expected = $"<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings, stubXmlSerializerNamespaces);

            // assert
            actual.Should().Be(expected);
        }
    }
}