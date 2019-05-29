namespace UniSys.Database.Data.Xml.Services
{
    using Models.Collections;
    using Models.Users;
    using System.Collections.Generic;

    public sealed class CollectionDataService
    {
        #region Declarations
        private static CollectionDataService instance;
        private static readonly object padlock = new object();
        private XmlService context = new XmlService();
        #endregion

        #region Properties
        public List<Subject> SubjWithStudents { get; set; } 
            = new List<Subject>();
        public List<Lecturer> Lecturers { get; set; }
            = new List<Lecturer>();
        public List<Student> Students { get; set; } 
            = new List<Student>();
        public List<string> GenderTypes { get; set; } 
            = new List<string>();
        public List<string> RegionCollection { get; set; }
            = new List<string>();
        #endregion

        #region Constructor
        private CollectionDataService()
        {
            var xmlRegionCollection = context
                .Deserialize<RegionsCollection>("..\\..\\Data_Source\\RegionsData.xml")
                .Region;
            RegionCollection.AddRange(xmlRegionCollection);

            var xmlGendersType = context
                .Deserialize<GendersCollection>("..\\..\\Data_Source\\GenderData.xml")
                .Gender;
            GenderTypes.AddRange(xmlGendersType);

            var xmlSubjects = context
                .Deserialize<SubjectsCollection>("..\\..\\Data_Source\\SubjectsWithStudentsData.xml")
                .Subjects;
            SubjWithStudents.AddRange(xmlSubjects);

            var users = context
                .Deserialize<UsersCollection>("..\\..\\Data_Source\\UniversitySystemData.xml");

            List<Lecturer> xmlLecturers = users.Lecturers;
            Lecturers.AddRange(xmlLecturers);

            List<Student> xmlStudents = users.Students;
            Students.AddRange(xmlStudents);
        }
        #endregion

        #region Methods
        public static CollectionDataService Instance
        {
            get
            {
                lock (padlock) // execute code on only one thread
                {
                    if (instance == null)
                        instance = new CollectionDataService();
                    return instance;
                }
            }
        }
        #endregion
    }
}
