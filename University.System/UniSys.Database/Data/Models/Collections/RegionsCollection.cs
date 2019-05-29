namespace UniSys.Database.Data.Models.Collections
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("Regions")]
    public class RegionsCollection
    {
        [XmlElement("Region")]
        public List<string> Region { get; set; }
    }
}
