namespace UniSys.Database.Data.Services.Interfaces
{
    using Models.Users;
    using Models.Users.Interfaces;
    using System.Collections.Generic;

    public interface IDbService
    {
        List<Lecturer> GetLecturersList();

        List<Student> GetStudentList();

        List<IUser> GetUsersList(IUser user);

        void SaveCurrentUser(IUser user);

        void DeleteUser(IUser user);

        string GetGender(IUser user);

        string GetRegion(IUser user);

        //void GetSubjects(int lecturerId);

        List<Student> GetStudents(string subject);

        void AddUser(Student student);

        void AddUser(Lecturer lecturer, List<Subject> subjectsCollectionDb);
    }
}
