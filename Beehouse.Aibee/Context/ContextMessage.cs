using System.Xml;
using System.Xml.Serialization;

namespace Beehouse.Aibee.Context
{
    public class ContextMessage
    {
        [XmlAttribute("entities")]
        public string Entities { get; set; }

        [XmlAttribute("if")]
        public string If { get; set; }

        [XmlAttribute("not")]
        public string Not { get; set; }

        [XmlAttribute("not-history")]
        public string NotHistory { get; set; }

        [XmlText]
        public string Message { get; set; }
    }
}