namespace UniSys.Database.Data.Services
{
    using Models.Users;
    using Models.Users.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    internal class DbService : IDbService
    {
        #region Declaration
        private UniSystDbContext context;
        #endregion

        #region Constructor
        public DbService()
        {
            context = new UniSystDbContext();
            context.Database.EnsureCreated();
        }
        #endregion

        #region Properties
        public List<Lecturer> GetLecturersList()
        {
            List<Lecturer> lecturers = context.Lecturers.ToList();
            return lecturers;
        }

        public List<Student> GetStudentList()
        {
            List<Student> students = context.Students.ToList();
            return students;
        }

        public List<IUser> GetUsersList(IUser user)
        {
            IQueryable<IUser> users = GetUsers(user);

            List<IUser> students = users.ToList();

            return students;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Запазване на промените в базата
        /// </summary>
        public void SaveCurrentUser(IUser user)
        {
            IQueryable<IUser> users = GetUsers(user);

            IUser currentUser = users
                .FirstOrDefault(a => a.PersonalNumber == user.PersonalNumber);

            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Gender = user.Gender;
            currentUser.PersonalNumber = user.PersonalNumber;
            currentUser.BirthDate = user.BirthDate;
            currentUser.Faculty = user.Faculty;
            currentUser.Region = user.Region;
            currentUser.Image = user.Image;

            if(currentUser is Student)
            {
                Student student = user as Student;
                Student dbStudent = currentUser as Student;
                dbStudent.FacultyNumber = student.FacultyNumber;
                dbStudent.Specialty = student.Specialty;
            }
            else
            {
                Lecturer lecturer = user as Lecturer;
                Lecturer dbLecturer = currentUser as Lecturer;
                dbLecturer.SubjectCollection = lecturer.SubjectCollection;
                dbLecturer.Title = lecturer.Title;
            }

            context.SaveChanges();
        }

        /// <summary>
        /// изтрива юзър
        /// </summary>
        public void DeleteUser(IUser user)
        {
            IQueryable<IUser> users = GetUsers(user);

            IUser currentUser = users
                .FirstOrDefault(s => s.PersonalNumber == user.PersonalNumber);

            context.Remove(currentUser); 

            context.SaveChanges();
        }
        /// <summary>
        /// връща пола на юзъра
        /// </summary>
        public string GetGender(IUser user)
        {
            IQueryable<IUser> users = GetUsers(user);

            //IEnumerable<IUser> u = context.Students
            //    .Where(s => s.PersonalNumber == user.PersonalNumber)
            //    .Select(s => s.Gender);

            string gender = users
                .Where(s => s.PersonalNumber == user.PersonalNumber)
                .Select(s => s.Gender)
                .FirstOrDefault();

            return gender;
        }
        /// <summary>
        /// връща града на юзъра
        /// </summary>
        public string GetRegion(IUser user)
        {
            IQueryable<IUser> users = GetUsers(user);

            string region = users
                .Where(s => s.PersonalNumber == user.PersonalNumber)
                .Select(s => s.Region)
                .FirstOrDefault();

            return region;
        }
        /// <summary>
        /// Взима студентите с общ предмет
        /// </summary>
        public List<Student> GetStudents(string subject)
        {
            List<Student> students = context
                           .Students
                           .Where(s => s.Specialty == subject)
                           .ToList();

            return students;
        }

        /// <summary>
        /// Добавяне на нов юзър към базата
        /// </summary>   
        /// 
        public void AddUser(Student student)
        {
            context.Students.Add(new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                PersonalNumber = student.PersonalNumber,
                BirthDate = student.BirthDate,
                Gender = student.Gender.ToString(),
                Faculty = student.Faculty,
                FacultyNumber = student.FacultyNumber,
                Specialty = student.Specialty,
                Region = student.Region,
                Image = student.Image
            });

            context.SaveChanges();
        }

        public void AddUser(Lecturer lecturer, List<Subject> subjectsCollectionDb)
        {     
            context.Lecturers.Add(new Lecturer()
            {
                FirstName = lecturer.FirstName,
                LastName = lecturer.LastName,
                PersonalNumber = lecturer.PersonalNumber,
                BirthDate = lecturer.BirthDate,
                SubjectCollection = new List<Subject>(subjectsCollectionDb),
                Gender = lecturer.Gender,
                Title = lecturer.Title,
                Faculty = lecturer.Faculty,
                Region = lecturer.Region,
                Image = lecturer.Image
            });

            context.SaveChanges();
        }

        private IQueryable<IUser> GetUsers(IUser user)
        {         
            IQueryable<IUser> users =  user is Student ?
                context.Students :
                users = context.Lecturers;

            return users;
        }
        #endregion
    }
}
