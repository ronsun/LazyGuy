using System.Collections.Generic;
using System.Text;
using System.Xml;
using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class XmlConverterTests
    {
        public class FakeClass
        {
            public string FakeString { get; set; } = "A";
        }

        private static readonly string _utf8BOM = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        private static readonly string _utf32BOM = Encoding.UTF32.GetString(Encoding.UTF32.GetPreamble());

        #region Serialize

        [Test()]
        public void SerializeTest_Default_ReturnCleanXmlWithDeclaration()
        {
            // arrange
            var stubClass = new FakeClass();
            var target = new XmlConverter();
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";
            // act
            var actual = target.Serialize(stubClass);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_OmitXmlDeclaration_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            var target = new XmlConverter();
            var expected = $"{_utf8BOM}<FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_IncludeBOM_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { Encoding = Encoding.UTF8 };
            var target = new XmlConverter();
            var expected = $"{_utf8BOM}<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings);

            // assert
            actual.Should().Be(expected);
        }
        #endregion

        #region DeSerialize

        [Test()]
        [TestCaseSource(nameof(TestCase_DeSerializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue))]
        public void DeSerializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue(
            string stubXml,
            string expectedFakeString
            )
        {
            // arrange
            var target = new XmlConverter();
            var stubDefaultEncoding = Encoding.UTF8;
            byte[] expected = stubDefaultEncoding.GetBytes(expectedFakeString);

            // act
            var deserializedResult = target.Deserialize<FakeClass>(stubXml);
            byte[] actual = stubDefaultEncoding.GetBytes(deserializedResult.FakeString);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static List<object[]> TestCase_DeSerializeTest_XmlWithDefaultEncoding_ReturnObjectWithExpectedValue()
        {
            var expectedFakeString = "繁简A";
            var xml = $"<FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";
            var xmlWithDeclaration = $"<?xml version=\"1.0\" ?><FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";

            var cases = new List<object[]>()
            {
                new object[] { xml, expectedFakeString },
                new object[] { xmlWithDeclaration, expectedFakeString },
                new object[] { _utf8BOM + xmlWithDeclaration, expectedFakeString },
            };

            return cases;
        }

        [Test()]
        [TestCaseSource(nameof(TestCase_DeSerializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue))]
        public void DeSerializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue(
            string stubXml,
            string expectedFakeString,
            Encoding expectedEncoding)
        {
            // arrange
            var target = new XmlConverter();
            byte[] expected = expectedEncoding.GetBytes(expectedFakeString);

            // act
            var deserializedResult = target.Deserialize<FakeClass>(stubXml, expectedEncoding);
            byte[] actual = expectedEncoding.GetBytes(deserializedResult.FakeString);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static List<object[]> TestCase_DeSerializeTest_XmlWithSpecificEncoding_ReturnObjectWithExpectedValue()
        {
            var expectedFakeString = "繁简A";
            var xml = $"<FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";
            var xmlWithDeclaration = $"<?xml version=\"1.0\" ?><FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";

            var cases = new List<object[]>()
            {
                new object[] { xml, expectedFakeString, Encoding.ASCII },
                new object[] { xmlWithDeclaration, expectedFakeString, Encoding.ASCII },

                new object[] { xml, expectedFakeString, Encoding.UTF8 },
                new object[] { xmlWithDeclaration, expectedFakeString, Encoding.UTF8 },
                new object[] { _utf8BOM + xml, expectedFakeString, Encoding.UTF8 },

                new object[] { xml, expectedFakeString, Encoding.UTF32 },
                new object[] { xmlWithDeclaration, expectedFakeString, Encoding.UTF32 },
                new object[] { _utf32BOM + xml, expectedFakeString, Encoding.UTF32 },
            };

            return cases;
        }


        #endregion
    }
}