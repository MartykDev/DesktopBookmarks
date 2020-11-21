using AngleSharp.Html.Dom;
using System;
using System.Threading.Tasks;

namespace DesktopBookmarks.Core.Interfaces
{
    public interface IParserService : IDisposable
    {
        Task<IHtmlDocument> ParseAsync(string url);
        Task LoadIconAsync(IHtmlDocument htmlDocument);
    }
}
