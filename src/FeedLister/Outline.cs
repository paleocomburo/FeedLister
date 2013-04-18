﻿/******************************************************************************\
    Copyright 2013 Jeroen-bart Engelen

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
\******************************************************************************/

using System;
using System.Linq;
using System.Xml.Linq;



namespace FeedLister
{
    public class Outline : OpmlElement
    {
        public Outline[] Outlines { get; private set; }
        public string Text { get; private set; } // Required, may contain HTML markup.
        public string Type { get; private set; } // Type 'rss' defined by OPML 2.0.
        public bool IsComment { get; private set; }
        public bool IsBreakPoint { get; private set; }

        public DateTime? Created { get; private set; } // // Must conform to RFC 822. OPML 2.0.
        public string Categories { get; private set; } // OPML 2.0.
        
        // All fields below are for OPML 2.0 type 'rss'
        public string Title { get; private set; }
        public Uri XmlUrl { get; private set; } // Required.
        public string Description { get; private set; }
        public Uri HtmlUrl { get; private set; }
        public string Language { get; private set; }
        public string Version { get; private set; } // RSS1 for RSS 1.0; RSS for 0.91, 0.92 or 2.0; scriptingNews for scriptingNews format.

        // All fields below are for OPML 2.0 type 'link' or 'include'.
        public Uri Url { get; private set; } // For type 'include', must point to an OPML file that will be included in-place.

        
        
        public Outline(Outline[] childOutlines, string text, string type, bool isComment = false, bool isBreakPoint = false)
        {
            this.Outlines = childOutlines;
            this.Text = text;
            this.Type = type;
            this.IsComment = isComment;
            this.IsBreakPoint = isBreakPoint;
        }



        public static Outline Parse(XElement outlineElement)
        {
            Outline[] childOutlines = null;
            var outlineElements = GetChildElements(outlineElement, "outline", false);
            if(outlineElements.Any())
            {
                childOutlines = outlineElements.Select(x => Outline.Parse(x)).ToArray();
            }

            var textAttribute = outlineElement.Attribute("text");
            if(textAttribute == null)
            {
                // Strictly speaking the text attribute is only required for OPML 2.0, but we'll be a little more strict.
                throw new Exception("The specified OPML document is not an OPML formatted document. There is a missing 'text' attribute in an 'outline' element.");
            }

            var text = textAttribute.Value;
            if(String.IsNullOrWhiteSpace(text))
            {
                // Strictly speaking the text attribute is only required for OPML 2.0, but we'll be a little more strict.
                throw new Exception("The specified OPML document is not an OPML formatted document. There is an empty 'text' attribute in an 'outline' element.");
            }

            string type = null;
            var outlineAttributes = outlineElement.Attributes();
            if (outlineAttributes.Any())
            {
                foreach(var attribute in outlineAttributes)
                {
                    var attributeName = attribute.Name.ToString();
                    switch (attributeName)
                    {
                        case "text":
                            // Skip the 'text' attribute, we already processed it.
                            break;

                        case "type":
                            type = String.IsNullOrWhiteSpace(attribute.Value) ? null : attribute.Value;
                            break;
                    }
                }
            }

            var outline = new Outline(childOutlines, text, type);
            return outline;
        }
    }
}
