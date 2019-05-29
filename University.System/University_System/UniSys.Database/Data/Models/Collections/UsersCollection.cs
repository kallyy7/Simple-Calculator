namespace UniSys.Database.Data.Models.Collections
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using UniSys.Database.Data.Models.Users;

    [XmlRoot("Users")]
    public class UsersCollection
    {
        [XmlElement("Student")]
        public List<Student> Students { get; set; }

        [XmlElement("Lecturer")]
        public List<Lecturer> Lecturers { get; set; }
    }
}
