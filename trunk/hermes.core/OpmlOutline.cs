using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hermes.core
{
    class OpmlOutline
    {
        private Uri _htmlUrl = null;
        private Uri _xmlUrl = null;
        private List<OpmlOutline> _outlines = null;
        //private OpmlOutlines _outlines = null;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private string _text;
        private string _type;

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or sets the html url
        /// </summary>
        public Uri HtmlUrl
        {
            get { return _htmlUrl; }
            set { _htmlUrl = value; }
        }

        /// <summary>
        /// Gets or sets the xml url
        /// </summary>
        public Uri XmlUrl
        {
            get { return _xmlUrl; }
            set { _xmlUrl = value; }
        }

        /// <summary>
        /// Gets or sets the collection of outline elements
        /// </summary>
        public List<OpmlOutline> Outlines
        {
            get { return _outlines; }
            set { _outlines = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OpmlOutline()
            : base()
        {
            _outlines = new List<OpmlOutline>();
        }
    }
}
