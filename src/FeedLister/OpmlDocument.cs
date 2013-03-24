using System;


//NOTE: Based on specs at: http://dev.opml.org/spec1.html


namespace FeedLister
{
    public class OpmlDocument
    {
        public Head Head { get; private set; }
        public Body Body { get; private set; }
        public string Version { get; private set; }



        private OpmlDocument(string version, Head head, Body body)
        {
            this.Version = version;
            this.Head = head;
            this.Body = body;
        }
    }
}
