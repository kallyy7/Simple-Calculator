namespace University_System.ViewModel
{
    using Models.Users;
    using Prism.Commands;
    using System.Windows.Data;

    public class StudentsRegisteredTableViewModel : StudentViewModel
    {
        #region Declaration
        private DelegateCommand<object> deleteStudent;
        private DelegateCommand<object> editStudent;
        private DelegateCommand<object> saveCurrent;
        #endregion

        #region Constructor
        public StudentsRegisteredTableViewModel()
        {
            this.CurrentStudent = new StudentModel();
        }
        #endregion

        #region Properties
        public StudentModel CurrentStudent { get; set; }
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

        public DelegateCommand<object> EditStudent
        {
            get
            {
                if (editStudent == null)
                    editStudent = new DelegateCommand<object>(EditCurrentStudent);

                return editStudent;
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
        #endregion

        #region Methods
        private void DeleteCurrentStudent(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Students);

            StudentModel selectedStudent = view.CurrentItem as StudentModel;

            Students.Remove(selectedStudent);
        }


        private void EditCurrentStudent(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Students);

            StudentModel selectedStudent = view.CurrentItem as StudentModel;

            this.CurrentStudent.FirstName = selectedStudent.FirstName;
            this.CurrentStudent.LastName = selectedStudent.LastName;
            this.CurrentStudent.PersonalNumber = selectedStudent.PersonalNumber;
            this.CurrentStudent.Gender = selectedStudent.Gender;
            this.CurrentStudent.Faculty = selectedStudent.Faculty;
            this.CurrentStudent.FacultyNumber = selectedStudent.FacultyNumber;
            this.CurrentStudent.Specialty = selectedStudent.Specialty;
            this.CurrentStudent.Region = selectedStudent.Region;
            this.CurrentStudent.BirthDate = selectedStudent.BirthDate;
        }

        private void SaveCurrentStudent(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Students);

            StudentModel selectedStudent = view.CurrentItem as StudentModel;

            selectedStudent.FirstName = this.CurrentStudent.FirstName;
            selectedStudent.LastName = this.CurrentStudent.LastName;
            selectedStudent.PersonalNumber = this.CurrentStudent.PersonalNumber;
            selectedStudent.Gender = this.CurrentStudent.Gender;
            selectedStudent.Faculty = this.CurrentStudent.Faculty;
            selectedStudent.FacultyNumber = this.CurrentStudent.FacultyNumber;
            selectedStudent.Specialty = this.CurrentStudent.Specialty;
            selectedStudent.Region = this.CurrentStudent.Region;
            selectedStudent.BirthDate = this.CurrentStudent.BirthDate;
        }

        #endregion

    }
}
