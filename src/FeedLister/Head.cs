using System;



namespace FeedLister
{
    public class Head
    {
        public string Title { get; private set; }
        public DateTime? DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string OwnerName { get; private set; }
        public string OwnerEmail { get; private set; }
        public int[] ExpansionState { get; private set; }
        public int? VertScrollState { get; private set; }
        public int? WindowTop { get; private set; }
        public int? WindowLeft { get; private set; }
        public int? WindowBottom { get; private set; }
        public int? WindowRight { get; private set; }
    }
}
