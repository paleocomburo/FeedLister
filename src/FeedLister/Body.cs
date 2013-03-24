using System;



namespace FeedLister
{
    public class Body
    {
        public Outline[] Outlines { get; private set; } // At least one required.



        public Body(Outline[] outlines)
        {
            this.Outlines = outlines;
        }
    }
}
