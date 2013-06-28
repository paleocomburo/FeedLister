/******************************************************************************\
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

using System.Linq;
using System.Xml.Linq;



namespace FeedLister
{
    public class Body : OpmlElement
    {
        public Outline[] Outlines { get; private set; } // At least one required.



        public Body(Outline[] outlines)
        {
            this.Outlines = outlines;
        }



        public static Body Parse(XElement bodyElement, bool strict = true)
        {
            var outlineElements = GetChildElements(bodyElement, "outline", true);
            var outlines = outlineElements.Select(x => Outline.Parse(x, false, strict)).ToArray();
            
            var body = new Body(outlines);
            return body;
        }
    }
}
