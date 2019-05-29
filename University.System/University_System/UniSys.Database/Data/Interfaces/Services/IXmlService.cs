namespace UniSys.Database.Data.Services.Interfaces
{
    using Models.Users;
    using UniSys.Database.Data.Models.Users.Interfaces;

    public interface IXmlService
    {
        T Deserialize<T>(string path);

        void AddUser(Student student);

        void AddUser(Lecturer lecturer);

        void AddSubject(IUser user);

        void AddStudentToSbjCollection(Student student);
    }
}
