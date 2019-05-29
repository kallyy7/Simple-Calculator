namespace University_System.ViewModel
{
    using Prism.Commands;
    using Students;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using System.Xml.Linq;
    using UniSys.Database.Data;
    using UniSys.Database.Data.Models.Users;
    using UniSys.Database.Data.Xml.Services;
    using University_System.Helpers;

    public class StudentsRegisteredTableViewModel : BaseViewModel
    {
        #region Declaration
        private DelegateCommand<object> deleteStudent;
        private DelegateCommand<object> saveCurrent;
        private DelegateCommand<object> refresh;
        private Student selectedStudent;
        private BitmapImage image;
        private StudentInformationViewModel selectedStudentVM;
        private ObservableCollection<Student> students;
        #endregion

        #region Constructor
        public StudentsRegisteredTableViewModel()
        {
            SelectedStudent = Students.FirstOrDefault();
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

        public ObservableCollection<Student> Students
        {
            get
            {
                if (students == null)
                    students = new ObservableCollection<Student>(databaseService.GetStudentList());

                return students;
            }
            set
            {
                if (value == students)
                    return;

                students = new ObservableCollection<Student>(databaseService.GetStudentList());

                OnPropertyChanged(nameof(Students));
            }
        }
        public Student SelectedStudent
        {
            get
            {
                if (selectedStudent == null)
                    selectedStudent = new Student();
                return selectedStudent;
            }
            set
            {
                if (value == selectedStudent) return;

                selectedStudent = value;

                OnPropertyChanged(nameof(SelectedStudent));

                GetCurrentStudent(selectedStudent);
            }
        }

        public StudentInformationViewModel SelectedStudentVM
        {
            get
            {
                if (selectedStudentVM == null)
                    selectedStudentVM = new StudentInformationViewModel();

                return selectedStudentVM;
            }
            set
            {
                if (selectedStudentVM == value)
                    return;

                selectedStudentVM = value;

                OnPropertyChanged(nameof(selectedStudentVM));
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> DeleteStudent
        {
            get
            {
                if (deleteStudent == null)
                    deleteStudent = new DelegateCommand<object>(DeleteCurrentStudent);

                return deleteStudent;
            }
        }

        public DelegateCommand<object> SaveCurrent
        {
            get
            {
                if (saveCurrent == null)
                    saveCurrent = new DelegateCommand<object>(SaveCurrentStudent);

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

        /// <summary>
        /// взима конкретния студент
        /// </summary>
        /// <param name="selectedStudent">селектирания студент</param>
        #region Methods
        private void RefreshTable(object obj)
        {
            Students = new ObservableCollection<Student>(CollectionDataService.Instance.Students);
        }
        private void GetCurrentStudent(Student selectedStudent)
        {
            if (selectedStudent != null)
            {
                #region пол и регион от xml
                //// пол и регион от xml
                //XDocument xml = XDocument.Load(xmlService.PathSystData);
                //var users = xml.Element("Users").Elements("Student");
                //string gender = string.Empty;
                //string region = string.Empty;

                //foreach (var user in users)
                //{
                //    if (user.Element("PersonalNumber").Value == SelectedStudent.PersonalNumber)
                //    {
                //        gender = user.Element("Gender").Value;
                //        region = user.Element("Region").Value;
                //    }
                //}
                #endregion

                string img = databaseService.GetUsersList(selectedStudent)
                  .Where(s => s.PersonalNumber == selectedStudent.PersonalNumber)
                  .Select(s => s.Image)
                  .FirstOrDefault();

                // пол и регион заредени от базата
                string gender = databaseService.GetGender(selectedStudent);
                string region = databaseService.GetRegion(selectedStudent);


                SelectedStudentVM.FirstName = selectedStudent.FirstName;
                SelectedStudentVM.LastName = selectedStudent.LastName;
                SelectedStudentVM.PersonalNumber = selectedStudent.PersonalNumber;
                SelectedStudentVM.Gender = gender;
                SelectedStudentVM.Faculty = selectedStudent.Faculty;
                SelectedStudentVM.FacultyNumber = selectedStudent.FacultyNumber;
                SelectedStudentVM.Specialty = selectedStudent.Specialty;
                SelectedStudentVM.Region = region;
                SelectedStudentVM.BirthDate = selectedStudent.BirthDate;
                Image = ImageConvert.GetImage(img);
            }
        }

        /// <summary>
        /// изтрива конкретния студент
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteCurrentStudent(object obj)
        {
            // изтриване в базата
            databaseService.DeleteUser(selectedStudent);
            
            // изтриване в xml
            XDocument doc = XDocument.Load(XmlDatabase.PathSystData);

            doc.Descendants("Users")
               .Descendants("Student")
               .Where(x => (string)x.Element("PersonalNumber") == selectedStudent.PersonalNumber)
               .Remove();

            doc.Save(XmlDatabase.PathSystData);

            // изтриване от колекцията
            CollectionDataService.Instance.Students.Remove(selectedStudent);
            Students.Remove(selectedStudent);

            if (Students.Count < 1)
            {
                SelectedStudentVM.FirstName = string.Empty;
                SelectedStudentVM.LastName = string.Empty;
                SelectedStudentVM.PersonalNumber = string.Empty;
                SelectedStudentVM.Gender = string.Empty;
                SelectedStudentVM.Faculty = string.Empty;
                SelectedStudentVM.FacultyNumber = string.Empty;
                SelectedStudentVM.Specialty= string.Empty;
                SelectedStudentVM.Region = string.Empty;
                SelectedStudentVM.BirthDate = string.Empty;
                Image = null;
            }
        }

        /// <summary>
        /// запазва конкретния студент
        /// </summary>
        /// <param name="obj"></param>
        private void SaveCurrentStudent(object obj)
        {
            //  промяна в колекцията
            foreach (Student student in CollectionDataService.Instance.Students)
            {
                if (student.Equals(SelectedStudent))
                {
                    student.FirstName = SelectedStudentVM.FirstName;
                    student.LastName = SelectedStudentVM.LastName;
                    student.PersonalNumber = SelectedStudentVM.PersonalNumber;
                    student.Gender = SelectedStudentVM.Gender;
                    student.Faculty = SelectedStudentVM.Faculty;
                    student.FacultyNumber = SelectedStudentVM.FacultyNumber;
                    student.Specialty = SelectedStudentVM.Specialty;
                    student.Region = SelectedStudentVM.Region;
                    student.BirthDate = SelectedStudentVM.BirthDate;
                }
            }

            // промяна в базата 
            databaseService.SaveCurrentUser(SelectedStudent);

            Students = new ObservableCollection<Student>(databaseService.GetStudentList());

            // променя елементите в xml 
            XDocument doc = XDocument.Load(XmlDatabase.PathSystData);

            IEnumerable<XElement> users = doc
                .Element("Users")
                .Elements("Student");

            foreach (XElement user in users)
            {
                if (user.Element("PersonalNumber").Value == SelectedStudent.PersonalNumber)
                {
                    user.Element("Name").Value = SelectedStudentVM.FirstName;
                    user.Element("LastName").Value = SelectedStudentVM.LastName;
                    user.Element("PersonalNumber").Value = SelectedStudentVM.PersonalNumber;
                    user.Element("Gender").Value = SelectedStudentVM.Gender;
                    user.Element("Faculty").Value = SelectedStudentVM.Faculty;
                    user.Element("FacultyNumber").Value = SelectedStudentVM.FacultyNumber;
                    user.Element("Specialty").Value = SelectedStudentVM.Specialty;
                    user.Element("Region").Value = SelectedStudentVM.Region;
                    user.Element("BirthDate").Value = SelectedStudentVM.BirthDate;

                    doc.Save(XmlDatabase.PathSystData);
                }
            }
        }
        #endregion
    }
}
