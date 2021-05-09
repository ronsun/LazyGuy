using System.IO;
using System.Net;
using System.Text;

namespace LazyGuy.Http
{
    /// <summary>
    /// Heper to send HTTP request.
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// Gets current <see cref="HttpWebRequest"/>.
        /// </summary>
        public HttpWebRequest CurrentRequest { get; private set; }

        /// <summary>
        /// Gets current <see cref="CurrentResponse"/>.
        /// </summary>
        public HttpWebResponse CurrentResponse { get; private set; }

        /// <summary>
        /// Send request by HTTP GET.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="options">Optional parameters for the request.</param>
        /// <returns>Response body from target URL in string type.</returns>
        public string Get(string url, RequestOptions options = null)
        {
            if (options == null)
            {
                options = new RequestOptions();
            }

            InitRequest(url, WebRequestMethods.Http.Get, options);

            return SendRequest(options.Encoding);
        }

        /// <summary>
        ///  Send request by HTTP POST.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="content">The request body.</param>
        /// <param name="options">Optional parameters for the request.</param>
        /// <returns>Response body from target URL in string type.</returns>
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
            {
                Stream s = resopnse.GetResponseStream();
                using (StreamReader sr = new StreamReader(s, encoding))
                {
                    CurrentResponse = resopnse;
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
