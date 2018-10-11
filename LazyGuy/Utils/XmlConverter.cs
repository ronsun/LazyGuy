using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LazyGuy.Utils
{
    public class XmlConverter
    {
        public virtual string Serialize(object obj, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var serializer = new XmlSerializer(obj.GetType());

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, obj);
                return encoding.GetString(ms.ToArray());
            }
        }

        public virtual T DeSerialize<T>(string xml, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                var xs = new XmlSerializer(typeof(T));
                var result = xs.Deserialize(ms);
                if (result == null)
                {
                    return default(T);
                }

                return (T)result;
            }
        }
    }
}
