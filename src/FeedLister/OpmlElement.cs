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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;



namespace FeedLister
{
    public abstract class OpmlElement
    {
        protected static IEnumerable<XElement> GetChildElements(XElement rootElement, XName elementName, bool required)
        {
            var elements = rootElement.Elements(elementName);
            if(required && elements.Any() == false)
            {
                throw new Exception("The specified OPML document is not an OPML formatted document. Missing '" + elementName.LocalName + "' tag under the '" + rootElement.Name.LocalName + "' tag.");
            }

            return elements;
        }



        protected static DateTime? ParseDateTime(string value, string elementName)
        {
            DateTime? result = null;

            if(String.IsNullOrWhiteSpace(value) == false)
            {
                DateTime parsedDateTime;
                if(DateTime.TryParse(value, out parsedDateTime) == false)
                {
                    throw new Exception("The '" + elementName + "' value cannot be parsed.");
                }

                result = parsedDateTime;
            }

            return result;
        }
    }
}