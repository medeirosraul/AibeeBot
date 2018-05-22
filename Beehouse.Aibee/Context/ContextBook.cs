using System.Collections.Generic;
using System.Xml.Serialization;

namespace Beehouse.Aibee.Context
{
    [XmlRoot("root")]
    public class ContextBook
    {
        [XmlElement("context")]
        public List<Context> Contexts { get; set; }
    }
}