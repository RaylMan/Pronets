using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Parsehtml
{
    public class HTTPClient
    {
        private CookieCollection Cooks = new CookieCollection();
        public HTTPClient(string session_cookiename)
        {
        }
        public void ClearCookie()
        {
        }
        public HttpWebResponse Request(string sUrl)
        {
            return Request(sUrl, true);
        }
        public HttpWebResponse Request_Post(string path, string post_body)
        {
            return Request_Post(path, post_body, true);
        }
        public HttpWebResponse Request_Post(string path, string post_body, bool bAutoRedirect)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(path);
                httpWebRequest.AllowAutoRedirect = bAutoRedirect;
                httpWebRequest.CookieContainer = new CookieContainer();
                if (Cooks != null)
                {
                    httpWebRequest.CookieContainer.Add(Cooks);
                }
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                byte[] ByteQuery = System.Text.Encoding.ASCII.GetBytes(post_body);
                httpWebRequest.ContentLength = ByteQuery.Length;
                Stream QueryStream = httpWebRequest.GetRequestStream();
                QueryStream.Write(ByteQuery, 0, ByteQuery.Length);
                QueryStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebResponse.Cookies = httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri);
                if (httpWebResponse.Cookies != null)
                {
                    Cooks.Add(httpWebResponse.Cookies);
                }
                return httpWebResponse;
            }
            catch (WebException ex)
            {
                return ex.Response as HttpWebResponse;
            }
        }
        public HttpWebResponse Request(string sUrl, bool bAutoRedirect)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sUrl);
                httpWebRequest.AllowAutoRedirect = bAutoRedirect;
                httpWebRequest.CookieContainer = new CookieContainer();
                if (Cooks != null)
                {
                    httpWebRequest.CookieContainer.Add(Cooks);
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebResponse.Cookies = httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri);
                if (httpWebResponse.Cookies != null)
                {
                    Cooks.Add(httpWebResponse.Cookies);
                }
                return httpWebResponse;
            }
            catch (WebException ex)
            {
                return ex.Response as HttpWebResponse;
            }
        }
    }
}
