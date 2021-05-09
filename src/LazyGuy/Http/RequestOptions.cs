using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    /// <summary>
    /// Options of http request.
    /// </summary>
    public class RequestOptions
    {
        /// <summary>
        /// Gets or sets encoding, default as UTF-8.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Gets or sets timeout in millisecond, default as 100000 (100 seconds).
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// Gets or sets readWriteTimeout in millisecond, default as 300000 (300 seconds).
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 300000;

        /// <summary>
        /// Gets or sets contentType, default as "application/x-www-form-urlencoded".
        /// </summary>
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";

        /// <summary>
        /// Gets or sets proxy, default as null (no need to request via proxy).
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Set <see cref="Proxy"/> to system web proxy.
        /// </summary>
        public void SetDefaultProxy()
        {
            IWebProxy defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;
            Proxy = defaultProxy;
        }

        /// <summary>
        /// Set <see cref="Proxy"/>.
        /// </summary>
        /// <param name="address">Addres.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public void SetProxy(string address, string userName, string password)
        {
            IWebProxy webProxy = new WebProxy(address);
            webProxy.Credentials = new NetworkCredential(userName, password);
            Proxy = webProxy;
        }
    }
}
