using System.IO;
using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    public class HttpHelper
    {
        public HttpWebRequest CurrentRequest { get; private set; }

        public HttpWebResponse CurrentResponse { get; private set; }

        public string Get(string url, RequestOptions options = null)
        {
            if (options == null)
            {
                options = new RequestOptions();
            }

            InitRequest(url, WebRequestMethods.Http.Get, options);

            return SendRequest(options.Encoding);
        }

        public string Post(string url, string content, RequestOptions options = null)
        {
            if (options == null)
            {
                options = new RequestOptions();
            }

            InitRequest(url, WebRequestMethods.Http.Post, options);

            var contentBytes = options.Encoding.GetBytes(content);

            using (var requestStream = CurrentRequest.GetRequestStream())
            {
                requestStream.Write(contentBytes, 0, contentBytes.Length);
            }

            return SendRequest(options.Encoding);
        }

        private void InitRequest(string url, string httpVerb, RequestOptions options)
        {
            CurrentRequest = (HttpWebRequest)WebRequest.Create(url);
            CurrentRequest.Method = httpVerb;
            CurrentRequest.Timeout = options.Timeout;
            CurrentRequest.ReadWriteTimeout = options.ReadWriteTimeout;
            CurrentRequest.ContentType = options.ContentType;

            if (options.Proxy != null)
            {
                CurrentRequest.Proxy = options.Proxy;
            }
        }

        private string SendRequest(Encoding encoding)
        {
            using (HttpWebResponse resopnse = (HttpWebResponse)CurrentRequest.GetResponse())
            using (Stream s = resopnse.GetResponseStream())
            using (StreamReader sr = new StreamReader(s, encoding))
            {
                CurrentResponse = resopnse;
                return sr.ReadToEnd();
            }
        }
    }
}
