using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    public class RequestOptions
    {
        public Encoding Encoding { get; private set; } = Encoding.UTF8;

        public int Timeout { get; private set; } = 100000;

        public int ReadWriteTimeout { get; private set; } = 300000;

        public string ContentType { get; private set; } = "application/x-www-form-urlencoded";

        public IWebProxy Proxy { get; private set; } = null;

        private RequestOptions() { }

        /// <summary>
        /// Create new self with required parameters for any request.
        /// </summary>
        /// <returns></returns>
        public static RequestOptions Create()
        {
            return new RequestOptions();
        }

        /// <summary>
        /// Set HttpWebRequest.Timeout.
        /// </summary>
        /// <param name="milliseconds">Timeout in milliseconds</param>
        /// <returns>Current HttpHelper</returns>
        public RequestOptions WithTimeout(int milliseconds)
        {
            Timeout = milliseconds;
            return this;
        }

        /// <summary>
        /// Set HttpWebRequest.ReadWriteTimeout
        /// </summary>
        /// <param name="milliseconds">ReadWriteTimeout in milliseconds</param>
        /// <returns>Current HttpHelper</returns>
        public RequestOptions WithReadWriteTimeout(int milliseconds)
        {
            ReadWriteTimeout = milliseconds;
            return this;
        }

        /// <summary>
        /// Set HttpWebRequest.ContentType
        /// </summary>
        /// <param name="contentType">Content-type HTTP header</param>
        /// <returns>Current HttpHelper</returns>
        public RequestOptions WithContentType(string contentType)
        {
            ContentType = contentType;
            return this;
        }

        /// <summary>
        /// Set the encoding for content of POST and response data.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public RequestOptions WithEncoding(Encoding encoding)
        {
            Encoding = encoding;
            return this;
        }

        /// <summary>
        /// Set proxy information as default that
        /// configured with the Internet Explorer settings of the currently impersonated user and
        /// system credentials of the application
        /// </summary>
        /// <returns>Current HttpHelper</returns>
        public RequestOptions WithProxy()
        {
            //default proxy
            IWebProxy defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;
            Proxy = defaultProxy;
            return this;
        }

        /// <summary>
        /// Set customeize proxy
        /// </summary>
        /// <param name="address">The URI of the proxy server</param>
        /// <param name="userName">The user name associated with the credentials</param>
        /// <param name="password">The password for the user name associated with the credentials.</param>
        /// <returns>Current HttpHelper</returns>
        public RequestOptions WithProxy(string address, string userName, string password)
        {
            IWebProxy webProxy = new WebProxy(address);
            webProxy.Credentials = new NetworkCredential(userName, password);
            Proxy = webProxy;
            return this;
        }
    }
}
