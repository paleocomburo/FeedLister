using System;



namespace FeedLister
{
    public class Outline
    {
        public Outline[] Outlines { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }
        public string Title { get; private set; }
        public bool IsComment { get; private set; }
        public bool IsBreakPoint { get; private set; }
    }
}
