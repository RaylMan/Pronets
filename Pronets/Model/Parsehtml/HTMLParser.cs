using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pronets.Model.Parsehtml
{
    public static class HTMLParser
    {
        public static async Task<string> GetPonSerial(string source)
        {
            string output = "";
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            var scripts = document.All.Where(i => i.LocalName == "script");
            var element = scripts?.FirstOrDefault(i => i.InnerHtml.Contains("454C54"));
            if (element != null)
            {
                string[] words = element.InnerHtml.Split(' ');
                foreach (var item in words)
                {
                    if (item.Contains("454C54"))
                    {

                        string pattern = @"\w{16}";
                        Regex rx = new Regex(@"\w{16}");
                        foreach (Match match in Regex.Matches(item, pattern))
                        {
                            return match.Value;
                        }
                    }
                }
            }
            return output;
        }
        public static async Task<string> GetPonSerialNTE(string source)
        {
            string output = "";
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            var scripts = document.All.Where(i => i.LocalName == "script");
            var element = scripts?.FirstOrDefault(i => i.InnerHtml.Contains("02:00"));
            if (element != null)
            {
                string[] words = element.InnerHtml.Split(' ');
                foreach (var item in words)
                {
                    if (item.Contains("02:00"))
                    {

                        string pattern = @"\w{2}\:\w{2}\:\w{2}\:\w{2}\:\w{2}\:\w{2}";
                        Regex rx = new Regex(@"\w{2}\:\w{2}\:\w{2}\:\w{2}\:\w{2}\:\w{2}");
                        foreach (Match match in Regex.Matches(item, pattern))
                        {
                            return match.Value;
                        }
                    }
                }
            }
            return output;
        }
        public static async Task<string> GetSerial(string source)
        {
            string output = "";
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            var scripts= document.All.Where(i => i.LocalName == "script");
            var element = scripts?.FirstOrDefault(i => i.InnerHtml.Contains("GP") || i.InnerHtml.Contains("TG"));
            if (element != null)
            {
                string[] words = element.InnerHtml.Split(' ');
                foreach (var item in words)
                {
                    if (item.Contains("GP") || item.Contains("TG"))
                    {

                        string pattern = @"\w{17}";
                        Regex rx = new Regex(@"\w{17}");
                        foreach (Match match in Regex.Matches(item, pattern))
                        {
                            return match.Value;
                        }
                    }
                }
            }

            var td = document.All.Where(i => i.LocalName == "td");
            
            foreach (var item in td)
            {
                if (item.InnerHtml.Length == 10 && item.InnerHtml.Contains("GP") || item.InnerHtml.Contains("TG"))
                    return item.InnerHtml;
            }


            return output;
        }
        public static async Task<string> GetMac(string source)
        {
            string output = "";
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            var tr = document.All.Where(i => i.LocalName == "td");

            foreach (var item in tr)
            {
                if (item.InnerHtml.Length == 17)
                    return item.InnerHtml;
            }
            return output;
        }
        public static async Task<string> GetNomenclature(string source)
        {
            string output = "";
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            var tr = document.All.Where(i => i.LocalName == "td");

            foreach (var item in tr)
            {
                foreach (DevicePrefix prefix in Enum.GetValues(typeof(DevicePrefix)))
                {
                    if (item.InnerHtml.Contains(prefix.ToString()))
                        return item.InnerHtml;
                }
            }
            return output;
        }
    }
}
