using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    public class RequestOptions
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public int Timeout { get; set; } = 100000;

        public int ReadWriteTimeout { get; set; } = 300000;

        public string ContentType { get; set; } = "application/x-www-form-urlencoded";

        public IWebProxy Proxy { get; private set; } = null;

        public void SetDefaultProxy()
        {
            //default proxy
            IWebProxy defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;
            Proxy = defaultProxy;
        }

        public void SetProxy(string address, string userName, string password)
        {
            IWebProxy webProxy = new WebProxy(address);
            webProxy.Credentials = new NetworkCredential(userName, password);
            Proxy = webProxy;
        }
    }
}
