namespace University_System.ViewModel
{
    using Microsoft.Win32;
    using Prism.Commands;
    using System;
    using System.Collections.Generic;
    using UniSys.Database.Data.Models.Users;
    using UniSys.Database.Data.Xml.Services;
    using University_System.Helpers;

    public class LecturerViewModel : BaseViewModel
    {
        #region Declaration
        private string firstName;
        private string lastName;
        private string personalNumber;
        private object gender;
        private string title;
        private string faculty;
        private string subjects;
        private object region;
        private string birthDate;
        private string photoPath;
        private string students;
        private string newSubject;
        private DelegateCommand<object> addLecturer;
        private DelegateCommand<object> addSubject;
        private DelegateCommand<object> addLecturerPhoto;
        private readonly List<string> currentSubjectsCollection;
        #endregion

        #region Constructor
        public LecturerViewModel()
        {
            currentSubjectsCollection = new List<string>();
        }
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
                    // Генериране на пол
                    Gender = PersonalNumberValidation.GetGender(personalNumber);
                    // Генериране на рожденна дата
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

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value == title)
                    return;

                title = value;

                OnPropertyChanged(nameof(Title));
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

        public string Subjects
        {
            get
            {
                return subjects;
            }
            set
            {
                if (value == subjects)
                    return;

                subjects = value;

                OnPropertyChanged(nameof(Subjects));
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
                // Проверка дали е въведено име, фамилия и ЕГН
                AddLecturer.RaiseCanExecuteChanged();

                photoPath = value;

                OnPropertyChanged(nameof(PhotoPath));
            }
        }

        public string Students
        {
            get
            {
                return students;
            }
            set
            {
                if (value == students)
                    return;

                students = value;

                OnPropertyChanged(nameof(Students));
            }
        }

        public string NewSubject
        {
            get
            {
                return newSubject;
            }
            set
            {
                if (value == newSubject)
                    return;

                newSubject = value;
                // Проверка дали е въведен предмет
                AddSubject.RaiseCanExecuteChanged();

                OnPropertyChanged(nameof(NewSubject));
            }
        }
        #endregion

        #region Commands 
        public DelegateCommand<object> AddLecturer
        {
            get
            {
                if (addLecturer == null)
                    addLecturer = new DelegateCommand<object>(AddNewLecturer, CanAddLecturer);

                return addLecturer;
            }
        }

        public DelegateCommand<object> AddSubject
        {
            get
            {
                if (addSubject == null)
                    addSubject = new DelegateCommand<object>(AddNewSubject, CanAddSubject);

                return addSubject;
            }
        }

        public DelegateCommand<object> AddLecturerPhoto
        {
            get
            {
                if (addLecturerPhoto == null)
                    addLecturerPhoto = new DelegateCommand<object>(AddNewLecturerPhoto);

                return addLecturerPhoto;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Отваряне на прозорчето за добавяне на снимка
        /// </summary>
        private void AddNewLecturerPhoto(object obj)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.ShowDialog();

            PhotoPath = openFileDlg.FileName;
        }
        /// <summary>
        /// Добавяне на нов предмет
        /// </summary>
        private void AddNewSubject(object obj)
        {
            currentSubjectsCollection.Add(NewSubject);

            Subjects = string.Join(", ", currentSubjectsCollection);

            NewSubject = null;
        }
        /// <summary>
        /// Добавяне на нов преподавател
        /// </summary>
        private void AddNewLecturer(object obj)
        {
            List<Student> lecturerStudents = new List<Student>();
            List<Subject> subjectsCollectionDb = new List<Subject>();

            // конвертиране на изображението
            string imgFromStringToByte = Convert.ToBase64String(ImageConvert.GetImageBytes(PhotoPath));

            // създаване на нов преподавател
            Lecturer lecturer = new Lecturer()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                PersonalNumber = this.PersonalNumber,
                BirthDate = this.BirthDate,
                Gender = this.Gender.ToString(),
                Title = this.Title,
                Faculty = this.Faculty,
                Subjects = string.Join(", ", currentSubjectsCollection),
                Students = string.Join(", ", students),
                Region = this.Region.ToString(),
                Image = imgFromStringToByte
            };

            // добавяне на предмета в xml 
            xmlService.AddSubject(lecturer);

            // добавяне на студенти според предмета
            foreach (var subject in CollectionDataService.Instance.SubjWithStudents)
            {
                if (currentSubjectsCollection.Contains(subject.Name))
                {
                    // колекция от студентите с общ предмет от базата
                    var students = databaseService.GetStudents(subject.Name);

                    // създаване на нов предмет
                    Subject currentSubject = new Subject();
                    currentSubject.Name = subject.Name;
                    currentSubject.Students = students;

                    // добавяне в базата
                    subjectsCollectionDb.Add(currentSubject);

                    // добавяне за xml
                    lecturerStudents.AddRange(subject.Students);
                }
            }
            // валидиране на ЕГН-то и добавяне на преподавателя към колекцията и xml 
            if (PersonalNumberValidation.GetValid(lecturer))
            {
                // добавяне към колекцията
                CollectionDataService.Instance.Lecturers.Add(lecturer);

                // добавяне към БД
                databaseService.AddUser(lecturer, subjectsCollectionDb);

                // добавяне към xml
                xmlService.AddUser(lecturer);

                // Clear
                Clear();
            }
            else // ако ЕГН-то е невалидно
            {
                PersonalNumber = "Невалидно ЕГН!";
            }
        }

        private bool CanAddLecturer(object arg)
        {
            if (FirstName != null &&
                LastName != null &&
                PersonalNumber.Length == 10 &&
                PhotoPath != null)
                return true;

            return false;
        }

        private bool CanAddSubject(object arg)
        {
            if (NewSubject != null)
                return true;

            return false;
        }

        private void Clear()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PersonalNumber = string.Empty;
            BirthDate = string.Empty;
            Gender = this.GenderTypes[0];
            Title = string.Empty;
            Faculty = string.Empty;
            Students = string.Empty;
            Subjects = string.Empty;
            Region = this.RegionCollection[0];
            PhotoPath = string.Empty;
        }
        #endregion
    }
}