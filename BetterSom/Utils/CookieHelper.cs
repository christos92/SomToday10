using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace BetterSom.Utils
{
    public class CookieHelper
    {
        public CookieHelper()
        {

        }
        internal static string JSESSIONID = null;
        internal static string username = null;
        internal static string password = null;

        internal static string afkorting = "merewa-elo";
        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        static String mAfkorting = "merewa"; //my school
        public async Task<string> getLogCookies(string username, string password)
        {//Init httpClient
            HttpClientHandler myClientHandler = new HttpClientHandler();
            HttpClient httpClient;
            myClientHandler.CookieContainer = new CookieContainer();
            httpClient = new HttpClient(myClientHandler);
            httpClient.MaxResponseContentBufferSize = 9556000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");


            //Begin code
            String result = "";
            String JSESSIONID_COOKIE = "";
            String loggedInJSESSIONID = "";

            var request = await httpClient.GetAsync("https://" + mAfkorting + ".somtoday.nl");
            var cookieCollection = myClientHandler.CookieContainer.GetCookies(new Uri("https://" + mAfkorting + ".somtoday.nl"));

            List<Cookie> cookies = cookieCollection.Cast<Cookie>().ToList();
            JSESSIONID_COOKIE = getJSESSIONIDfromList(cookies);
            Debug.WriteLine("JSESSIONID_before_login = " + request.Headers.GetValues("Set-Cookie").First(x => x.StartsWith("JSESSIONID")));


            String url = "https://" + mAfkorting + ".somtoday.nl/?-1.IFormSubmitListener-content-upp-signInForm";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cookie", "JSESSIONID=" + JSESSIONID_COOKIE);

                var values = new Dictionary<string, string>
                {
                    { "id2_hf_0", "" },
                    { "loginLink", "Submit+Query" },
                    { "usernameFieldPanel:usernameFieldPanel_body:usernameField", username },
                    { "passwordFieldPanel:passwordFieldPanel_body:passwordField", password }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    String line;
                    StringBuilder builder = new StringBuilder();

                    StreamReader reader = new StreamReader(await httpClient.GetStreamAsync(url), Encoding.UTF8);

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        builder.AppendLine(line);
                    }
                    String html = builder.ToString();
                    Debug.Write(html);
                    string data = getBetween(html, "<form id", "?");
                    string data2 = data.Replace("=\"id1\" method=\"post\" action=\"./;jsessionid=", "");
                    loggedInJSESSIONID = data2;

                }
            }

            return loggedInJSESSIONID;
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}