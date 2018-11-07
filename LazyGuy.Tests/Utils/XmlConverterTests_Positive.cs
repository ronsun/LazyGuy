using NUnit.Framework;
using LazyGuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using System.Xml.Serialization;
using System.IO;
using FluentAssertions;
using System.Xml;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class XmlConverterTests_Positive
    {
        public class FakeClass
        {
            public string FakeString { get; set; } = "A";
        }

        private readonly string _urf8BOM = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        #region Serialize

        [Test()]
        public void SerializeTest_Default_ReturnCleanXmlWithDeclaration()
        {
            // arrange
            var stubClass = new FakeClass();
            var target = new XmlConverter();
            var expected = $"{_urf8BOM}<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";
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
            var expected = $"{_urf8BOM}<FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void SerializeTest_NoBOM_ReturnCleanXml()
        {
            // arrange
            var stubClass = new FakeClass();
            var stubXmlWriterSettings = new XmlWriterSettings() { Encoding = new UTF8Encoding() };
            var target = new XmlConverter();
            var expected = $"<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>A</FakeString></FakeClass>";

            // act
            var actual = target.Serialize(stubClass, stubXmlWriterSettings);

            // assert
            actual.Should().Be(expected);
        }
        #endregion

        #region DeSerialize

        [Test()]
        public void DeSerializeTest_Default_ReturnObjectWithExpectedValue()
        {
            // arrange
            var expectedFakeString = "A";
            var stubXml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?><FakeClass><FakeString>{expectedFakeString}</FakeString></FakeClass>";

            var target = new XmlConverter();

            // act
            var actual = target.DeSerialize<FakeClass>(stubXml);

            // assert
            actual.FakeString.Should().Be(expectedFakeString);
        }
        #endregion
    }
}