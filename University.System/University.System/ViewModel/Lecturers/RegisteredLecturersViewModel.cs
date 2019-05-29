namespace University_System.ViewModel
{
    using Prism.Commands;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using System.Xml.Linq;
    using UniSys.Database.Data;
    using UniSys.Database.Data.Models.Users;
    using UniSys.Database.Data.Xml.Services;
    using University_System.Helpers;

    public class RegisteredLecturersViewModel : BaseViewModel
    {
        #region Declaration
        private DelegateCommand<object> deleteLecturer;
        private DelegateCommand<object> saveCurrent;
        private DelegateCommand<object> refresh;
        private Lecturer selectedLecturer;
        private BitmapImage image;
        private LecturerInformationViewModel selectedLectVM;
        private ObservableCollection<Lecturer> lecturers;
        private ObservableCollection<Subject> sbjWithStudents;
        #endregion

        #region Constructor
        public RegisteredLecturersViewModel()
        {
            SelectedLecturer = Lecturers.FirstOrDefault();
        }
        #endregion

        #region Properties
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value == image)
                    return;

                image = value;

                OnPropertyChanged(nameof(Image));
            }
        }

        public ObservableCollection<Subject> SbjWithStudents
        {
            get
            {
                if (sbjWithStudents == null)
                    sbjWithStudents = new ObservableCollection<Subject>();

                return sbjWithStudents;
            }
            set
            {
                if (value == sbjWithStudents)
                    return;

                sbjWithStudents = new ObservableCollection<Subject>();

                OnPropertyChanged(nameof(SbjWithStudents));
            }
        }

        public ObservableCollection<Lecturer> Lecturers
        {
            get
            {
                if (lecturers == null)
                    lecturers = new ObservableCollection<Lecturer>(databaseService.GetLecturersList());

                return lecturers;
            }
            set
            {
                if (value == lecturers)
                    return;

                lecturers = new ObservableCollection<Lecturer>(databaseService.GetLecturersList());

                OnPropertyChanged(nameof(Lecturers));
            }
        }

        public List<string> LecturerStudents { get; set; } = new List<string>();

        public Lecturer SelectedLecturer
        {
            get
            {
                if (selectedLecturer == null)
                    selectedLecturer = new Lecturer();

                return selectedLecturer;
            }
            set
            {
                if (value == selectedLecturer)
                    return;

                selectedLecturer = value;

                OnPropertyChanged(nameof(SelectedLecturer));

                GetCurrentLecturer(selectedLecturer);
            }
        }

        public LecturerInformationViewModel SelectedLectViewModel
        {
            get
            {
                if (selectedLectVM == null)
                    selectedLectVM = new LecturerInformationViewModel();

                return selectedLectVM;
            }
            set
            {
                if (selectedLectVM == value)
                    return;

                selectedLectVM = value;
                OnPropertyChanged(nameof(SelectedLectViewModel));
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> DeleteLecturer
        {
            get
            {
                if (deleteLecturer == null)
                    deleteLecturer = new DelegateCommand<object>(DeleteCurrentLecturer);

                return deleteLecturer;
            }
        }

        public DelegateCommand<object> SaveCurrent
        {
            get
            {
                if (saveCurrent == null)
                    saveCurrent = new DelegateCommand<object>(SaveCurrentLecturer);

                return saveCurrent;
            }
        }
        public DelegateCommand<object> Refresh
        {
            get
            {
                if (refresh == null)
                    refresh = new DelegateCommand<object>(RefreshTable);

                return refresh;
            }
        }
        #endregion

        #region Methods
        private void RefreshTable(object obj)
        {
            Lecturers = new ObservableCollection<Lecturer>(databaseService.GetLecturersList());
        }
        /// <summary>
        /// взима конкретния преподавател
        /// </summary>
        /// <param name="selectedStudent">селектирания студент</param>
        private void GetCurrentLecturer(Lecturer selectedLecturer)
        {
            if (selectedLecturer != null)
            {
                #region пол и регион от xml
                // пол и регион от xml
                //XDocument xml = XDocument.Load(xmlService.PathSystData);
                //var users = xml.Element("Users").Elements("Lecturer");
                //string gender = string.Empty;
                //string region = string.Empty;

                //foreach (var user in users)
                //{
                //    if (user.Element("PersonalNumber").Value == SelectedLecturer.PersonalNumber)
                //    {
                //        gender = user.Element("Gender").Value;
                //        region = user.Element("Region").Value;
                //    }
                //}
                #endregion

                // пол и регион заредени от базата
                string gender = databaseService.GetGender(selectedLecturer);
                string region = databaseService.GetRegion(selectedLecturer);

                // добавяне на студенти за таблицата във View-то, които са при този преподавател
                SbjWithStudents = new ObservableCollection<Subject>();

                //List<string> subjectList = selectedLecturer.Subjects.Split(", ".ToCharArray()).ToList();
                var subjectList = CollectionDataService.Instance.Lecturers
                    .Where(l => l.PersonalNumber == selectedLecturer.PersonalNumber)
                    .Select(l => l.Subjects)
                    .ToList();

                foreach (var subject in subjectList)
                {
                    Subject currentSubject = new Subject();
                    currentSubject.Name = subject;

                    bool currentSbjWithSt = CollectionDataService.Instance.SubjWithStudents
                        .Select(s => s.Name == currentSubject.Name)
                        .FirstOrDefault();

                    if (selectedLecturer != null && currentSbjWithSt)
                    {
                        List<Student> studentList = CollectionDataService.Instance.SubjWithStudents
                              .Where(s => s.Name == subject)
                              .Select(s => s.Students)
                              .FirstOrDefault();

                        currentSubject.Students = studentList;
                    }

                    SbjWithStudents.Add(currentSubject);
                }
                // изображение от базата
                string img = databaseService.GetUsersList(selectedLecturer)
                   .Where(l => l.PersonalNumber == selectedLecturer.PersonalNumber)
                   .Select(l => l.Image)
                   .FirstOrDefault();

                SelectedLectViewModel.FirstName = selectedLecturer.FirstName;
                SelectedLectViewModel.LastName = selectedLecturer.LastName;
                SelectedLectViewModel.PersonalNumber = selectedLecturer.PersonalNumber;
                SelectedLectViewModel.Gender = gender;
                SelectedLectViewModel.Title = selectedLecturer.Title;
                SelectedLectViewModel.Faculty = selectedLecturer.Faculty;
                SelectedLectViewModel.Region = region;
                SelectedLectViewModel.BirthDate = selectedLecturer.BirthDate;
                LecturerStudents = new List<string>();
                Image = ImageConvert.GetImage(img);
            }
        }
        /// <summary>
        /// изтрива конкретния преподавател
        /// </summary>
        private void DeleteCurrentLecturer(object obj)
        {
            // изтриване в базата
            databaseService.DeleteUser(selectedLecturer);

            // изтриване в xml
            XDocument doc = XDocument.Load(XmlDatabase.PathSystData);

            doc.Descendants("Users")
              .Descendants("Lecturer")
              .Where(x => (string)x.Element("PersonalNumber") == selectedLecturer.PersonalNumber)
              .Remove();

            doc.Save(XmlDatabase.PathSystData);

            // изтриване от колекцията
            CollectionDataService.Instance.Lecturers.Remove(selectedLecturer);
            Lecturers.Remove(selectedLecturer);

            if (Lecturers.Count < 1)
            {
                SelectedLectViewModel.FirstName = string.Empty;
                SelectedLectViewModel.LastName = string.Empty;
                SelectedLectViewModel.PersonalNumber = string.Empty;
                SelectedLectViewModel.Gender = string.Empty;
                SelectedLectViewModel.Title = string.Empty;
                SelectedLectViewModel.Faculty = string.Empty;
                SelectedLectViewModel.Region = string.Empty;
                SelectedLectViewModel.BirthDate = string.Empty;
                Image = null;
            }
        }
        /// <summary>
        /// запазва конкретния преподавател
        /// </summary>
        private void SaveCurrentLecturer(object obj)
        {
            //  промяна в колекцията
            foreach (Lecturer lecturer in CollectionDataService.Instance.Lecturers)
            {
                if (lecturer.Equals(SelectedLecturer))
                {
                    lecturer.FirstName = SelectedLectViewModel.FirstName;
                    lecturer.LastName = SelectedLectViewModel.LastName;
                    lecturer.PersonalNumber = SelectedLectViewModel.PersonalNumber;
                    lecturer.Title = SelectedLecturer.Title;
                    lecturer.Gender = SelectedLectViewModel.Gender;
                    lecturer.Faculty = SelectedLectViewModel.Faculty;
                    lecturer.Region = SelectedLectViewModel.Region;
                    lecturer.BirthDate = SelectedLectViewModel.BirthDate;

                    break;
                }
            }
            Lecturers = new ObservableCollection<Lecturer>(databaseService.GetLecturersList());

            // промяна в базата 
            databaseService.SaveCurrentUser(selectedLecturer);

            // промяна в xml 
            XDocument doc = XDocument.Load(XmlDatabase.PathSystData);

            IEnumerable<XElement> users = doc.Element("Users").Elements("Lecturer");

            foreach (XElement user in users)
            {
                if (user.Element("PersonalNumber").Value == SelectedLecturer.PersonalNumber)
                {
                    user.Element("Name").Value = SelectedLectViewModel.FirstName;
                    user.Element("LastName").Value = SelectedLectViewModel.LastName;
                    user.Element("PersonalNumber").Value = SelectedLectViewModel.PersonalNumber;
                    user.Element("Gender").Value = SelectedLectViewModel.Gender;
                    user.Element("Title").Value = SelectedLectViewModel.Title;
                    user.Element("Faculty").Value = SelectedLectViewModel.Faculty;
                    if (selectedLecturer.Subjects != null)
                        user.Element("Subjects").Value = string.Join(", ", selectedLecturer.Subjects);
                    user.Element("Region").Value = SelectedLectViewModel.Region;
                    user.Element("BirthDate").Value = SelectedLectViewModel.BirthDate;

                    doc.Save(XmlDatabase.PathSystData);
                    break;
                }
            }
        }
        #endregion
    }
}
