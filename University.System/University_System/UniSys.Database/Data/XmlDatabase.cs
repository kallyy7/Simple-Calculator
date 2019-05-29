namespace UniSys.Database.Data
{
    using Services.Interfaces;
    using Xml.Services;

    public static class XmlDatabase
    {
        public static string PathSystData = "..\\..\\Data_Source\\UniversitySystemData.xml";
        public static string PathSubjWithStudents = "..\\..\\Data_Source\\SubjectsWithStudentsData.xml";

        public static IXmlService GetXmlDatabase()
        {
            return Context = new XmlService();         
        }

        public static IXmlService Context { get; set; }
    }
}
