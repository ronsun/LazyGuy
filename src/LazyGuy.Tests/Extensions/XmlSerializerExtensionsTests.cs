using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class XmlSerializerExtensionsTests
    {
        private static readonly string _utf8BOM = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        private static readonly string _utf32BOM = Encoding.UTF32.GetString(Encoding.UTF32.GetPreamble());

        public class FakeClass
        {
            public string FakeString { get; set; } = "A";
        }

        #region Serialize

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

        #endregion

        #region DeSerialize

        [Test()]
        [TestCaseSource(nameof(TestCase_DeserializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue))]
        public void DeserializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue(
            string stubXml,
            string expectedFakeString
            )
        {
            // arrange
            var target = new XmlSerializer(typeof(FakeClass));
            var stubDefaultEncoding = Encoding.UTF8;
            byte[] expected = stubDefaultEncoding.GetBytes(expectedFakeString);

            // act
            var deserializedResult = target.Deserialize<FakeClass>(stubXml);
            byte[] actual = stubDefaultEncoding.GetBytes(deserializedResult.FakeString);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable TestCase_DeserializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue()
        {
            var expectedFakeString = "繁简A";
            var xml = $"<FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";
            var xmlWithDeclaration = $"<?xml version=\"1.0\" ?><FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";

            yield return new TestCaseData(xml, expectedFakeString);
            yield return new TestCaseData(xmlWithDeclaration, expectedFakeString);
            yield return new TestCaseData(_utf8BOM + xmlWithDeclaration, expectedFakeString);
        }

        [Test()]
        [TestCaseSource(nameof(TestCase_DeserializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue))]
        public void DeserializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue(
            string stubXml,
            string expectedFakeString,
            Encoding expectedEncoding)
        {
            // arrange
            var target = new XmlSerializer(typeof(FakeClass));
            byte[] expected = expectedEncoding.GetBytes(expectedFakeString);

            // act
            var deserializedResult = target.Deserialize<FakeClass>(stubXml, expectedEncoding);
            byte[] actual = expectedEncoding.GetBytes(deserializedResult.FakeString);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable TestCase_DeserializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue()
        {
            var expectedFakeString = "繁简A";
            var xml = $"<FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";
            var xmlWithDeclaration = $"<?xml version=\"1.0\" ?><FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";

            yield return new TestCaseData(xml, expectedFakeString, Encoding.ASCII);
            yield return new TestCaseData(xmlWithDeclaration, expectedFakeString, Encoding.ASCII);

            yield return new TestCaseData(xml, expectedFakeString, Encoding.UTF8);
            yield return new TestCaseData(xmlWithDeclaration, expectedFakeString, Encoding.UTF8);
            yield return new TestCaseData(_utf8BOM + xml, expectedFakeString, Encoding.UTF8);

            yield return new TestCaseData(xml, expectedFakeString, Encoding.UTF32);
            yield return new TestCaseData(xmlWithDeclaration, expectedFakeString, Encoding.UTF32);
            yield return new TestCaseData(_utf32BOM + xml, expectedFakeString, Encoding.UTF32);
        }

        #endregion
    }
}