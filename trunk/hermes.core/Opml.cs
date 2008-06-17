using System;
using System.Collections.Generic;
using System.Xml;

namespace hermes.core
{
    internal class Opml
    {
        #region Private Declarations

        private DateTime _dateCreated = DateTime.MinValue;
        private DateTime _dateModified = DateTime.MinValue;
        private List<OpmlOutline> _outlines;
        private string _ownerEmail = string.Empty;
        private string _ownerName = string.Empty;
        private string _title = string.Empty;
        private XmlDocument opmlDoc;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets or sets a date-time, indicating when the document was created .
        /// </summary>
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        /// <summary>
        /// Gets or sets a date-time, indicating when the document was last modified 
        /// </summary>
        public DateTime DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }

        /// <summary>
        /// Gets or sets the owner of the document.
        /// </summary>
        public string OwnerName
        {
            get { return _ownerName; }
            set { _ownerName = value; }
        }

        /// <summary>
        /// Gets or sets the email address of the owner of the document.
        /// </summary>
        public string OwnerEmail
        {
            get { return _ownerEmail; }
            set { _ownerEmail = value; }
        }

        /// <summary>
        /// Gets or sets the collection of outline elements
        /// </summary>
        public List<OpmlOutline> Outlines
        {
            get { return _outlines; }
            set { _outlines = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        public Opml()
        {
            _outlines = new List<OpmlOutline>();
        }

        #endregion

        #region AddFeed

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddOutline(OpmlOutline item)
        {
            _outlines.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public void AddOutline(string title, string description)
        {
            AddOutline(title, description, null, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="htmlUrl"></param>
        /// <param name="xmlUrl"></param>
        /// <param name="outlines"></param>
        public void AddOutline(string title, string description, Uri htmlUrl, Uri xmlUrl, List<OpmlOutline> outlines)
        {
            var item = new OpmlOutline
                           {
                               Title = title,
                               Description = description,
                               XmlUrl = xmlUrl,
                               HtmlUrl = xmlUrl,
                               Outlines = outlines
                           };
            _outlines.Add(item);
        }

        #endregion

        #region GetXml

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            return opmlDoc.OuterXml;
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves this Opml to a file
        /// </summary>
        /// <param name="fileName">file name</param>
        public void Save(string fileName)
        {
            opmlDoc = new XmlDocument();
            /*
            XmlDeclaration xml = opmlDoc.CreateXmlDeclaration("1.0", "", "");
            opmlDoc.AppendChild(xml);
             */
            XmlElement opml = opmlDoc.CreateElement("opml");
            opml.SetAttribute("version", "1.1");
            opmlDoc.AppendChild(opml);

            // create head    
            XmlElement head = opmlDoc.CreateElement("head");
            opml.AppendChild(head);

            // set Title
            XmlElement title = opmlDoc.CreateElement("title");
            title.InnerText = Title;
            head.AppendChild(title);

            // set date crated
            XmlElement dateCreated = opmlDoc.CreateElement("dateCreated");
            dateCreated.InnerText = DateCreated != DateTime.MinValue
                                        ? DateCreated.ToString("r", null)
                                        : DateTime.Now.ToString("r", null);
            head.AppendChild(dateCreated);

            // set date modified
            XmlElement dateModified = opmlDoc.CreateElement("dateModified");
            dateCreated.InnerText = DateModified != DateTime.MinValue
                                        ? DateModified.ToString("r", null)
                                        : DateTime.Now.ToString("r", null);
            head.AppendChild(dateModified);

            // set owner email
            XmlElement ownerEmail = opmlDoc.CreateElement("ownerEmail");
            ownerEmail.InnerText = OwnerEmail;
            head.AppendChild(ownerEmail);

            // set owner name
            XmlElement ownerName = opmlDoc.CreateElement("ownerName");
            ownerName.InnerText = OwnerName;
            head.AppendChild(ownerName);

            // create body
            XmlElement opmlBody = opmlDoc.CreateElement("body");
            opml.AppendChild(opmlBody);

            foreach (OpmlOutline outline in _outlines)
            {
                opmlBody.AppendChild(OutlineToXML(outline));
            }
            opmlDoc.Save(fileName);
        }

        private XmlElement OutlineToXML(OpmlOutline outline)
        {
            XmlElement outlineNode = opmlDoc.CreateElement("outline");
            if (!String.IsNullOrEmpty(outline.Title))
                outlineNode.SetAttribute("text", outline.Title);
            if (!String.IsNullOrEmpty(outline.Description))
                outlineNode.SetAttribute("description", outline.Description);
            if (!String.IsNullOrEmpty(outline.Text))
                outlineNode.SetAttribute("title", outline.Text);
            if (!String.IsNullOrEmpty(outline.Type))
                outlineNode.SetAttribute("type", outline.Type);
            if (outline.HtmlUrl != null)
                outlineNode.SetAttribute("htmlUrl", outline.HtmlUrl.ToString());
            if (outline.XmlUrl != null)
                outlineNode.SetAttribute("xmlUrl", outline.XmlUrl.ToString());

            foreach (OpmlOutline childOutline in outline.Outlines)
            {
                outlineNode.AppendChild(OutlineToXML(childOutline));
            }

            return outlineNode;
        }

        #endregion

        #region Parse

        /// <summary>
        /// Parses a given opml file
        /// </summary>
        /// <param name="opmlFile">opml file to parse</param>
        /// <returns><see cref="Opml"/> object</returns>
        public static Opml Parse(string opmlFile)
        {
            var opmlDoc = new XmlDocument();
            opmlDoc.Load(opmlFile);

            var _out = new Opml();

            // Parse head
            XmlNode head = opmlDoc.GetElementsByTagName("head")[0];
            XmlNode title = head.SelectSingleNode("./title");
            XmlNode dateCreated = head.SelectSingleNode("./dateCreated");
            XmlNode dateModified = head.SelectSingleNode("./dateModified");
            XmlNode ownerName = head.SelectSingleNode("./ownerName");
            XmlNode ownerEmail = head.SelectSingleNode("./ownerEmail");

            if (title != null)
            {
                _out.Title = title.InnerText;
            }
            if (dateCreated != null)
            {
                _out.DateCreated = DateTime.Parse(dateCreated.InnerText);
            }
            if (dateModified != null)
            {
                _out.DateModified = DateTime.Parse(dateModified.InnerText);
            }
            if (ownerName != null)
            {
                _out.OwnerName = ownerName.InnerText;
            }
            if (ownerEmail != null)
            {
                _out.OwnerEmail = ownerEmail.InnerText;
            }

            // Parse body
            XmlNode body = opmlDoc.GetElementsByTagName("body")[0];
            XmlNodeList outlineList = body.SelectNodes("./outline");
            if (outlineList != null)
                foreach (XmlElement outline in outlineList)
                {
                    _out.Outlines.Add(ParseContent(outline));
                }


            return _out;
        }

        /// <summary>
        /// Parse the outline content and its children
        /// </summary>
        /// <param name="xmlNode">outline xml node to parse</param>
        /// <returns><see cref="OpmlOutline"/> content for the current node</returns>
        private static OpmlOutline ParseContent(XmlElement xmlNode)
        {
            var newOutline = new OpmlOutline();

            string title = xmlNode.GetAttribute("title");
            string text = xmlNode.GetAttribute("text");
            newOutline.Title = !String.IsNullOrEmpty(title) ? title : string.Empty;
            newOutline.Text = !String.IsNullOrEmpty(text) ? text : string.Empty;
            string url = xmlNode.GetAttribute("htmlUrl");
            newOutline.HtmlUrl = !String.IsNullOrEmpty(url) ? new Uri(url) : null;
            string link = xmlNode.GetAttribute("xmlUrl");
            newOutline.XmlUrl = !String.IsNullOrEmpty(link) ? new Uri(link) : null;
            newOutline.Description = xmlNode.GetAttribute("description");

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlElement childNode in xmlNode.SelectNodes("./outline"))
                {
                    newOutline.Outlines.Add(ParseContent(childNode));
                }
            }
            return newOutline;
        }

        #endregion
    }
}