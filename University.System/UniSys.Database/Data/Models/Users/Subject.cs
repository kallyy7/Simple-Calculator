namespace UniSys.Database.Data.Models.Users
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Subject
    {
        public Subject()
        {
            Students = new List<Student>();
        }

        public int SubjectId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Student")]
        public List<Student> Students { get; set; }
    }
}
