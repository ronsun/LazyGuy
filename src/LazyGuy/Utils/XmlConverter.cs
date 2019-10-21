using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LazyGuy.Utils
{
    public class XmlConverter
    {
        private XmlSerializer _serializer;

        public XmlConverter(XmlSerializer serializer = null)
        {
            _serializer = serializer;
        }

        public virtual string Serialize<T>(T obj,
            XmlWriterSettings xmlWriterSettings = null,
            XmlSerializerNamespaces ns = null)
        {
            InitSerializer<T>();

            if (xmlWriterSettings == null)
            {
                xmlWriterSettings = new XmlWriterSettings() { Encoding = new UTF8Encoding() };
            }

            if (ns == null)
            {
                ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            }

            var ms = new MemoryStream();
            using (var xmlWriter = XmlWriter.Create(ms, xmlWriterSettings))
            {
                _serializer.Serialize(xmlWriter, obj, ns);
                return xmlWriterSettings.Encoding.GetString(ms.ToArray());
            }
        }

        public virtual T Deserialize<T>(string xml, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            InitSerializer<T>();

            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                var result = _serializer.Deserialize(ms);
                if (result == null)
                {
                    return default(T);
                }

                return (T)result;
            }
        }

        private void InitSerializer<T>()
        {
            if (_serializer == null)
            {
                _serializer = new XmlSerializer(typeof(T));
            }
        }
    }
}
