namespace UniSys.Database.Data.Models.Users
{
    using Interfaces;
    using System.Xml.Serialization;

    public class Student : IUser
    {
        #region Properties
        public int StudentId { get; set; }

        [XmlElement("Name")]
        public string FirstName { get; set; }
        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("PersonalNumber")]
        public string PersonalNumber { get; set; }

        [XmlElement("Gender")]
        public string Gender { get; set; } 

        [XmlElement("Faculty")]
        public string Faculty { get; set; }

        [XmlElement("FacultyNumber")]
        public string FacultyNumber { get; set; }

        [XmlElement("Specialty")]
        public string Specialty { get; set; }

        [XmlElement("Region")]
        public string Region { get; set; }

        [XmlElement("BirthDate")]
        public string BirthDate { get; set; }

        [XmlElement("Image")]
        public string Image { get; set; }

        public string Names
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        #endregion
    }
}
