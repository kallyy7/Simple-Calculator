namespace UniSys.Database.Data.Xml.Services
{
    using Data.Services.Interfaces;
    using Models.Users;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using UniSys.Database.Data.Models.Users.Interfaces;

    internal class XmlService : IXmlService
    {
        public string PathSystData = "..\\..\\Data_Source\\UniversitySystemData.xml";
        public string PathSubjWithStudents = "..\\..\\Data_Source\\SubjectsWithStudentsData.xml";

        /// <summary>
        /// Xml десериализация
        /// </summary>
        public T Deserialize<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T deserializer;

            using (XmlReader reader = XmlReader.Create(new StreamReader(path)))
            {
                return deserializer = (T)xmlSerializer.Deserialize(reader);
            }
        }
        /// <summary>
        /// Добавяне на нов юзър
        /// </summary>
        public void AddUser(Student student)
        {
            XDocument xdoc = XDocument.Load(PathSystData);

            xdoc.Root.Add(
                 new XElement("Student",
                 new XAttribute("ID", student.PersonalNumber),
                 new XElement("Name", student.FirstName),
                 new XElement("LastName", student.LastName),
                 new XElement("PersonalNumber", student.PersonalNumber),
                 new XElement("Gender", student.Gender.ToString()),
                 new XElement("Faculty", student.Faculty),
                 new XElement("FacultyNumber", student.FacultyNumber),
                 new XElement("Specialty", student.Specialty),
                 new XElement("Region", student.Region.ToString()),
                 new XElement("BirthDate", student.BirthDate),
                 new XElement("PhotoPath", student.Image)));
            xdoc.Save(PathSystData);
        }

        public void AddUser(Lecturer lecturer)
        {
            XDocument xdoc = XDocument.Load(PathSystData);

            xdoc.Root.Add(
                   new XElement("Lecturer",
                   new XElement("Name", lecturer.FirstName),
                   new XElement("LastName", lecturer.LastName),
                   new XElement("PersonalNumber", lecturer.PersonalNumber),
                   new XElement("Gender", lecturer.Gender.ToString()),
                   new XElement("BirthDate", lecturer.BirthDate),
                   new XElement("Title", lecturer.Title),
                   new XElement("Faculty", lecturer.Faculty),
                   new XElement("Subjects", lecturer.Subjects),
                   new XElement("Students", lecturer.Students),
                   new XElement("Region", lecturer.Region.ToString()),
                   new XElement("Image", lecturer.Image)));
            xdoc.Save(PathSystData);
        }
        /// <summary>
        /// Добавяне на предмет 
        /// </summary>
        public void AddSubject(IUser user)
        {
            XDocument xdoc = XDocument.Load(PathSubjWithStudents);

            if (user is Lecturer)
            {
                Lecturer lecturer = user as Lecturer;
                string[] subjects = lecturer.Subjects.Split(", ".ToCharArray());

                foreach (var subject in subjects)
                {
                    xdoc.Root.Add(
                 new XElement("Subject",
                 new XAttribute("name", subject)));
                }
            }
            else
            {
                Student student = user as Student;

                xdoc.Root.Add(
                 new XElement("Subject",
                 new XAttribute("name", student.Specialty),
                 new XElement("Student", 
                 new XElement("Name", user.FirstName),
                 new XElement("LastName", user.LastName),
                 new XElement("Faculty", user.Faculty))));
            }
            xdoc.Save(PathSubjWithStudents);
        }
        /// <summary>
        /// Добавяне на студент към колекцията от предмети
        /// </summary>
        public void AddStudentToSbjCollection(Student student)
        {
            XDocument xdoc = XDocument.Load(PathSubjWithStudents);

            XElement element = xdoc.Descendants()
                    .Where(x => (string)x.Attribute("name") == student.Specialty)
                    .FirstOrDefault();

            element.Add(new XElement("Student",
                 new XElement("Name", student.FirstName),
                 new XElement("LastName", student.PersonalNumber),
                 new XElement("Faculty", student.Faculty)));

            xdoc.Save(PathSubjWithStudents);
        }
    }
}
