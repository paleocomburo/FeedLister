using System;



namespace FeedLister
{
    public class Outline
    {
        public Outline[] Outlines { get; private set; }
        public string Text { get; private set; } // Required, may contain HTML markup.
        public string Type { get; private set; } // Type 'rss' defined by OPML 2.0.
        public string Title { get; private set; }
        public bool IsComment { get; private set; }
        public bool IsBreakPoint { get; private set; }

        public DateTime? Created { get; private set; } // // Must conform to RFC 822. OPML 2.0.
        public string Categories { get; private set; } // OPML 2.0.
        
        // All fields below are for OPML 2.0 type 'rss'
        public Uri XmlUrl { get; private set; } // Required.
        public string Description { get; private set; }
        public Uri HtmlUrl { get; private set; }
        public string Language { get; private set; }
        public string Version { get; private set; } // RSS1 for RSS 1.0; RSS for 0.91, 0.92 or 2.0; scriptingNews for scriptingNews format.

        // All fields below are for OPML 2.0 type 'link' or 'include'.
        public Uri Url { get; private set; } // For type 'include', must point to an OPML file that will be included in-place.



        
        public Outline(Outline[] childOutlines, string text, string type, string title, bool isComment = false, bool isBreakPoint = false)
        {
            this.Outlines = childOutlines;
            this.Text = text;
            this.Type = type;
            this.Title = title;
            this.IsComment = isComment;
            this.IsBreakPoint = isBreakPoint;
        }
    }
}
