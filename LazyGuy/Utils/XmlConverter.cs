using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LazyGuy.Utils
{
    // 目前看來能取得最佳平衡的作法
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

            using (var ms = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(ms, xmlWriterSettings))
            {
                _serializer.Serialize(xmlWriter, obj, ns);
                return xmlWriterSettings.Encoding.GetString(ms.ToArray());
            }
        }

        public virtual T DeSerialize<T>(string xml, Encoding encoding = null)
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
    /*
    // 一個型別的轉換要手動建立一個實體, 不好用, 即使不用泛型也一樣, 因為初始化需要知道型別
    public class HandyXmlSerializer<T> : XmlSerializer
    {
        public HandyXmlSerializer() : base(typeof(T))
        {
        }

        public virtual string Serialize(T obj, XmlSerializerNamespaces namespaces = null, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream())
            {
                if (namespaces == null)
                {
                    Serialize(ms, obj);
                }
                else
                {
                    Serialize(ms, obj, namespaces);
                }

                return encoding.GetString(ms.ToArray());
            }
        }

        public virtual T DeSerialize(string xml, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                var result = Deserialize(ms);

                if (result == null)
                {
                    return default(T);
                }

                return (T)result;
            }
        }
    }

    // 有擴充方法的缺點, 且一個型別的轉換要手動建立一個實體, 不好用
    public static class XmlSerializerExtensions
    {
        public static string Serialize<T>(this XmlSerializer serializer, T obj, XmlSerializerNamespaces namespaces = null, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream())
            {
                if (namespaces == null)
                {
                    serializer.Serialize(ms, obj);
                }
                else
                {
                    serializer.Serialize(ms, obj, namespaces);
                }

                return encoding.GetString(ms.ToArray());
            }
        }

        public static T DeSerialize<T>(this XmlSerializer serializer, string xml, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                var result = serializer.Deserialize(ms);

                if (result == null)
                {
                    return default(T);
                }

                return (T)result;
            }
        }
    }
*/
}
