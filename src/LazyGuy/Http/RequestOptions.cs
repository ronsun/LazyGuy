using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    public class RequestOptions
    {
        /// <summary>
        /// Encoding, default as UTF-8.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Timeout in millisecond, default as 100000 (100 seconds). 
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// ReadWriteTimeout in millisecond, default as 300000 (300 seconds). 
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 300000;

        /// <summary>
        /// ContentType, default as "application/x-www-form-urlencoded". 
        /// </summary>
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";

        /// <summary>
        /// Proxy, default as null (no need to request via proxy).  
        /// </summary>
        public IWebProxy Proxy { get; set; } = null;

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
        /// <param name="password">Password</param>
        public void SetProxy(string address, string userName, string password)
        {
            IWebProxy webProxy = new WebProxy(address);
            webProxy.Credentials = new NetworkCredential(userName, password);
            Proxy = webProxy;
        }
    }
}
