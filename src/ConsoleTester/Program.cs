using System;
using System.Xml.Linq;
using FeedLister;


namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var opmlDocumentPath = args[0];
            var opmlData = XDocument.Load(opmlDocumentPath, LoadOptions.SetLineInfo);

            var opmlDocument = OpmlDocument.Create(opmlData);
        }
    }
}
