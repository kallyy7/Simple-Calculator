namespace UniSys.Database.Data.Models.Collections
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Users;

    [XmlRoot("Subjects")]
    public class SubjectsCollection
    {
        [XmlElement("Subject")]
        public List<Subject> Subjects { get; set; }
    }
}
