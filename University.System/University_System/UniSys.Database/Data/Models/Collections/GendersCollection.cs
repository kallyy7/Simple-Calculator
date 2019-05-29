namespace UniSys.Database.Data.Models.Collections
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("GenderType")]
    public class GendersCollection
    {
        [XmlElement("Gender")]
        public List<string> Gender { get; set; }
    }
}
