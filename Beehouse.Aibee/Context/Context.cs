using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Beehouse.Aibee.Context
{
    public class Context
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("related")]
        public string Related { get; set; }

        [XmlAttribute("require-reset")]
        public bool RequireReset { get; set; } = false;

        [XmlElement("message")]
        public List<ContextMessage> Messages { get; set; }
    }
}