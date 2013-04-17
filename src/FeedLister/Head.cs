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
using System.Linq;
using System.Xml.Linq;


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



        public static Head Parse(XElement headElement)
        {
            var title = GetStringElementValue(headElement, "title");
            var ownerName = GetStringElementValue(headElement, "ownerName");
            var ownerEmail = GetStringElementValue(headElement, "ownerEmail");

            var dateCreated = GetDateTimeElementValue(headElement, "dateCreated");
            var dateModified = GetDateTimeElementValue(headElement, "dateModified");

            var expansionStateElement = headElement.Descendants("expansionState").FirstOrDefault();
            var expansionStateValue = GetSafeValue(expansionStateElement);
            int[] expansionState = null;
            if (String.IsNullOrWhiteSpace(expansionStateValue) == false)
            {
                var values = expansionStateValue.Split(',');
                expansionState = values.Select(x =>
                {
                    var parsedInt = ParseInt(x, "expansionState");
                    if (parsedInt == null)
                    {
                        throw new Exception("The 'expansionState' value cannot be parsed.");
                    }

                    return parsedInt.Value;
                }).ToArray();
            }

            var verticalScrollState = GetIntElementValue(headElement, "vertScrollState");
            var windowTop = GetIntElementValue(headElement, "windowTop");
            var windowLeft = GetIntElementValue(headElement, "windowLeft");
            var windowBottom = GetIntElementValue(headElement, "windowBottom");
            var windowRight = GetIntElementValue(headElement, "windowRight");

            var ownerId = GetUriElementValue(headElement, "ownerId");
            var documentation = GetUriElementValue(headElement, "docs");

            var head = new Head(title, dateCreated, dateModified, ownerName, ownerEmail, expansionState, verticalScrollState, windowTop, windowLeft, windowBottom, windowRight, ownerId, documentation);
            return head;
        }



        private static Uri GetUriElementValue(XElement rootElement, XName elementName)
        {
            var element = rootElement.Descendants(elementName).FirstOrDefault();
            var elementValue = GetSafeValue(element);
            var value = ParseUri(elementValue, elementName.ToString());

            return value;
        }



        private static int? GetIntElementValue(XElement rootElement, XName elementName)
        {
            var element = rootElement.Descendants(elementName).FirstOrDefault();
            var elementValue = GetSafeValue(element);
            var value = ParseInt(elementValue, elementName.ToString());

            return value;
        }



        private static DateTime? GetDateTimeElementValue(XElement rootElement, XName elementName)
        {
            var element = rootElement.Descendants(elementName).FirstOrDefault();
            var elementValue = GetSafeValue(element);
            var value = ParseDateTime(elementValue, elementName.ToString());

            return value;
        }



        private static string GetStringElementValue(XElement rootElement, XName elementName)
        {
            var element = rootElement.Descendants(elementName).FirstOrDefault();
            var value = GetSafeValue(element);

            return value;
        }



        private static Uri ParseUri(string value, string elementName)
        {
            Uri result = null;

            if (String.IsNullOrWhiteSpace(value) == false)
            {
                Uri parsedUri;
                if (Uri.TryCreate(value, UriKind.Absolute, out parsedUri) == false)
                {
                    throw new Exception("The '" + elementName + "' value cannot be parsed.");
                }

                result = parsedUri;
            }

            return result;
        }



        private static int? ParseInt(string value, string elementName)
        {
            int? result = null;

            if (String.IsNullOrWhiteSpace(value) == false)
            {
                int parsedInt;
                if (Int32.TryParse(value, out parsedInt) == false)
                {
                    throw new Exception("The '" + elementName + "' value cannot be parsed.");
                }

                result = parsedInt;
            }

            return result;
        }



        private static DateTime? ParseDateTime(string value, string elementName)
        {
            DateTime? result = null;

            if (String.IsNullOrWhiteSpace(value) == false)
            {
                DateTime parsedDateTime;
                if (DateTime.TryParse(value, out parsedDateTime) == false)
                {
                    throw new Exception("The '" + elementName + "' value cannot be parsed.");
                }

                result = parsedDateTime;
            }

            return result;
        }



        private static string GetSafeValue(XElement element)
        {
            string value = null;
            if (element != null)
            {
                value = element.Value;
            }

            return value;
        }
    }
}
