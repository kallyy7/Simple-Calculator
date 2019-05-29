namespace University_System.ViewModel
{
    using Helpers;
    using Microsoft.Win32;
    using Prism.Commands;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UniSys.Database.Data.Models.Users;
    using UniSys.Database.Data.Xml.Services;

    public class StudentViewModel : BaseViewModel
    {
        #region Declaration
        private string firstName;
        private string lastName;
        private string personalNumber;
        private object gender;
        private string faculty;
        private string facultyNumber;
        private string specialty;
        private object region;
        private string birthDate;
        private string photoPath;
        private DelegateCommand<object> addStudent;
        private DelegateCommand<object> addStudentPhoto;
        private ObservableCollection<Student> students;
        #endregion

        #region Properties
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value == firstName)
                    return;

                firstName = value;

                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value == lastName)
                    return;

                lastName = value;

                OnPropertyChanged(nameof(LastName));
            }
        }

        public string PersonalNumber
        {
            get
            {
                return personalNumber;
            }
            set
            {
                personalNumber = value;

                // валидация на ЕГН-то
                if (PersonalNumberValidation.IsValidPersonalNumber(personalNumber))
                {
                    // генериране на пол
                    Gender = PersonalNumberValidation.GetGender(personalNumber);
                    // генериране на рожденна дата
                    BirthDate = PersonalNumberValidation.GetBirthDate(personalNumber);
                }
                else if (personalNumber == "") // ако полето е празно
                {
                    personalNumber = string.Empty;
                }
                else // ако е невалидно ЕГН
                {
                    personalNumber = "Невалидно ЕГН!";
                }

                OnPropertyChanged(nameof(PersonalNumber));
            }
        }

        public object Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value == gender)
                    return;

                gender = value;

                OnPropertyChanged(nameof(Gender));
            }
        }

        public string Faculty
        {
            get
            {
                return faculty;
            }
            set
            {
                if (value == faculty)
                    return;

                faculty = value;

                OnPropertyChanged(nameof(Faculty));
            }
        }

        public string FacultyNumber
        {
            get
            {
                return facultyNumber;
            }
            set
            {
                if (value == facultyNumber)
                    return;

                facultyNumber = value;

                OnPropertyChanged(nameof(FacultyNumber));
            }
        }

        public string Specialty
        {
            get
            {
                return specialty;
            }
            set
            {
                if (value == specialty)
                    return;

                specialty = value;

                OnPropertyChanged(nameof(Specialty));
            }
        }

        public object Region
        {
            get
            {
                return region;
            }
            set
            {
                if (value == region)
                    return;

                region = value;

                OnPropertyChanged(nameof(Region));
            }
        }

        public string BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (value == birthDate)
                    return;

                birthDate = value;

                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string PhotoPath
        {
            get
            {
                return photoPath;
            }
            set
            {
                if (value == photoPath)
                    return;

                photoPath = value;
                // проверка дали е въведено име, фалимия и ЕГН
                AddStudent.RaiseCanExecuteChanged();

                OnPropertyChanged(nameof(PhotoPath));
            }
        }

        public ObservableCollection<Student> Students
        {
            get
            {
                if (students == null)
                    students = new ObservableCollection<Student>();

                List<Student> listOfStudents = CollectionDataService.Instance.Students;
                students.AddRange(listOfStudents);

                return students;
            }
        }
        #endregion

        #region Commands 
        public DelegateCommand<object> AddStudent
        {
            get
            {
                if (addStudent == null)
                    addStudent = new DelegateCommand<object>(AddNewStudent, CanAddStudent);

                return addStudent;
            }
        }

        public DelegateCommand<object> AddStudentPhoto
        {
            get
            {
                if (addStudentPhoto == null)
                    addStudentPhoto = new DelegateCommand<object>(AddNewStudentPhoto);

                return addStudentPhoto;
            }
        }
        #endregion

        #region Methods   
        /// <summary>
        /// Добавяне на снимка
        /// </summary>
        private void AddNewStudentPhoto(object obj)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.ShowDialog();

            PhotoPath = openFileDlg.FileName;
        }
        /// <summary>
        /// Добавяне на нов студент
        /// </summary>
        private void AddNewStudent(object obj)
        {
            Subject newSubject = new Subject();
            newSubject.Name = Specialty;
            newSubject.Students = new List<Student>();

            // конвертиране на изображението
            string imgFromStringToByte = Convert.ToBase64String(ImageConvert.GetImageBytes(PhotoPath));

            // създаване на студента
            Student student = new Student()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                PersonalNumber = this.PersonalNumber,
                BirthDate = this.BirthDate,
                Gender = this.Gender.ToString(),
                Faculty = this.Faculty,
                FacultyNumber = this.FacultyNumber,
                Specialty = this.Specialty,
                Region = this.Region.ToString(),
                Image = imgFromStringToByte
            };

            // проверка дали съществува текущия предмет
            bool containsSubject = CollectionDataService.Instance.SubjWithStudents
                .Exists(x => x.Name == Specialty);

            // добавяне на предмети към колекцията
            if (!containsSubject) // ако предмета не съществува 
            {
                // добавяне в колекцията
                newSubject.Students.Add(student);

                // добавяне в xml
                xmlService.AddSubject(student);
            }
            else // aко не съществува
            {
                foreach (Subject subject in CollectionDataService.Instance.SubjWithStudents)
                {
                    // добавяне на студент в съществуващ вече предмет
                    newSubject.Students.Add(student);               

                    // добавяне в xml 
                    xmlService.AddStudentToSbjCollection(student);

                    break;
                }
            }
            // добавяне на новия предмет в колекцията
            CollectionDataService.Instance.SubjWithStudents.Add(newSubject);

            // валидиране на ЕГН-то и добавяне на студента към колекцията и xml
            if (PersonalNumberValidation.GetValid(student))
            {
                // добавяне към колекцията
                CollectionDataService.Instance.Students.Add(student);

                // добавяне към xml            
                xmlService.AddUser(student);

                // добавяне към БД
                databaseService.AddUser(student);

                // Clear
                Clear();
            }
            else
            {                
                this.PersonalNumber = "Невалидно ЕГН!";
            }
        }
        // активация на бутона
        private bool CanAddStudent(object arg)
        {
            if (!string.IsNullOrEmpty(FirstName) &&
                LastName != null &&
                PersonalNumber.Length == 10 &&
                PhotoPath != null) return true;

            return false;
        }

        private void Clear()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.PersonalNumber = string.Empty;
            this.Gender = this.GenderTypes[0];
            this.Faculty = string.Empty;
            this.FacultyNumber = string.Empty;
            this.Specialty = string.Empty;
            this.Region = this.RegionCollection[0];
            this.PhotoPath = string.Empty;
            this.BirthDate = string.Empty;
        }
        #endregion
    }
}
