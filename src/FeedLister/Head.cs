using System;



namespace FeedLister
{
    public class Head
    {
        public string Title { get; private set; }
        public DateTime? DateCreated { get; private set; } // Must conform to RFC 822.
        public DateTime? DateModified { get; private set; } // Must conform to RFC 822.
        public string OwnerName { get; private set; }
        public string OwnerEmail { get; private set; }
        public Uri OwnerId { get; private set; } // OPML 2.0
        public Uri Documentation { get; private set; } // OPML 2.0
        public int[] ExpansionState { get; private set; }
        public int? VerticalScrollState { get; private set; }
        public int? WindowTop { get; private set; }
        public int? WindowLeft { get; private set; }
        public int? WindowBottom { get; private set; }
        public int? WindowRight { get; private set; }



        public Head(string title, DateTime? dateCreated = null, DateTime? dateModified = null, string ownerName = null, string ownerEmail = null, int[] expansionState = null, int? verticalScrollState = null,
            int? windowTop = null, int? windowLeft = null, int? windowBottom = null, int? windowRight = null, Uri ownerId = null, Uri documentation = null)
        {
            this.Title = title;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.OwnerEmail = ownerEmail;
            this.OwnerName = ownerName;
            this.ExpansionState = expansionState;
            this.VerticalScrollState = verticalScrollState;
            this.WindowTop = windowTop;
            this.WindowLeft = windowLeft;
            this.WindowBottom = windowBottom;
            this.WindowRight = windowRight;

            // OPML 2.0 fields.
            this.OwnerId = ownerId;
            this.Documentation = documentation;
        }
    }
}
