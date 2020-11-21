using DesktopBookmarks.Core.Interfaces;
using DesktopBookmarks.Core.Services;
using System;
using System.Threading.Tasks;

namespace DesktopBookmarks.ConsoleUi
{
    class Program
    {
        private static readonly string SampleUrl = "";
        private static async Task Main(string[] args)
        {
            using (IParserService parser = new WebParserService())
            {
                try
                {
                    var htmlDOM = await parser.ParseAsync(SampleUrl);
                    await parser.LoadIconAsync(htmlDOM);
                }
                catch (Exception ex)
                {
                    WriteError(ex.Message);
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}
