using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extensions for <see cref="XmlSerializer"/>.
    /// </summary>
    public static class XmlSerializerExtensions
    {
        /// <summary>
        /// Serialize.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="serializer">Serializer.</param>
        /// <param name="obj">Target object.</param>
        /// <returns>Serialized string.</returns>
        public static string Serialize<T>(this XmlSerializer serializer, T obj)
        {
            var xmlWriterSettings = new XmlWriterSettings() { Encoding = new UTF8Encoding() };
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            return Serialize<T>(serializer, obj, xmlWriterSettings, ns);
        }

        /// <summary>
        /// Serialize.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="serializer">Serializer.</param>
        /// <param name="obj">Target object.</param>
        /// <returns>Serialized string.</returns>
        /// <param name="xmlWriterSettings"><see cref="XmlWriterSettings"/>.</param>
        /// <param name="ns"><see cref="XmlSerializerNamespaces"/>.</param>
        /// <returns>Serialized string.</returns>
        public static string Serialize<T>(
            this XmlSerializer serializer,
            T obj,
            XmlWriterSettings xmlWriterSettings,
            XmlSerializerNamespaces ns)
        {
            var ms = new MemoryStream();
            using (var xmlWriter = XmlWriter.Create(ms, xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, obj, ns);
                return xmlWriterSettings.Encoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Deserialize.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="serializer">Serializer.</param>
        /// <param name="xml">XML string.</param>
        /// <returns>Deserialized object.</returns>
        public static T Deserialize<T>(this XmlSerializer serializer, string xml)
        {
            return Deserialize<T>(serializer, xml, Encoding.UTF8);
        }

        /// <summary>
        /// Deserialize.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="serializer">Serializer.</param>
        /// <param name="xml">XML string.</param>
        /// <param name="encoding"><see cref="Encoding"/>.</param>
        /// <returns>Deserialized object.</returns>
        public static T Deserialize<T>(this XmlSerializer serializer, string xml, Encoding encoding)
        {
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            using (var reader = XmlReader.Create(ms))
            {
                var result = serializer.Deserialize(reader);
                if (result == null)
                {
                    return default;
                }

                return (T)result;
            }
        }
    }
}
