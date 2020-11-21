using AngleSharp.Html.Dom;
using DesktopBookmarks.Core.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DesktopBookmarks.Core.Services
{
    public class WebParserService : IParserService
    {
        private readonly HttpClient _httpClient;

        public WebParserService()
        {
            _httpClient = new HttpClient();
        }

        private async Task<string> LoadDataAsync(string url)
        {
            var responce = await _httpClient.GetAsync(url);

            if (responce == null || !responce.IsSuccessStatusCode)
                throw new Exception("Can't load data");

            return await responce.Content.ReadAsStringAsync();
        }

        public async Task<IHtmlDocument> ParseAsync(string url)
        {
            var source = await LoadDataAsync(url);
            var parser = new HtmlParser();
            return await parser.ParseDocumentAsync(source);
        }

        public async Task LoadIconAsync(IHtmlDocument htmlDocument)
        {
            var items = htmlDocument.GetElementsByTagName("link");
            var item = items.Where(x => x.Attributes.Any(a => a.Value == "icon")).FirstOrDefault();
            var iconUrl = item.Attributes.Where(x => x.Name == "href").FirstOrDefault()?.Value;

            var source = await LoadDataAsync(iconUrl);

            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadData("");

                using (var stream = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(stream))
                    {
                        yourImage.Save("icon.ico", ImageFormat.Bmp);
                    }
                }

            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}