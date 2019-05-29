namespace UniSys.Database.Data.Models.Users
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Lecturer :  IUser
    {
        public Lecturer()
        {
            SubjectCollection = new List<Subject>();
        }

        public int LecturerId { get; set; }

        [XmlElement("Name")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("PersonalNumber")]
        public string PersonalNumber { get; set; }

        [XmlElement("Gender")]
        public string Gender { get; set; } 

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Faculty")]
        public string Faculty { get; set; }

        [XmlElement("Subjects")]
        public string Subjects { get; set; }

        public List<Subject> SubjectCollection { get; set; } 

        [XmlElement("Region")]
        public string Region { get; set; }

        [XmlElement("BirthDate")]
        public string BirthDate { get; set; }

        [XmlElement("Image")]
        public string Image { get; set; }

        [XmlElement("Students")]
        public string Students { get; set; }

        public string Names
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
