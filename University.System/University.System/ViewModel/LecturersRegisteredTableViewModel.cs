namespace University_System.ViewModel
{
    using Models.Users;
    using Prism.Commands;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Data;

    public class LecturersRegisteredTableViewModel : LecturerViewModel
    {
        #region Declaration
        private DelegateCommand<object> deleteLecturer;
        private DelegateCommand<object> editLecturer;
        private DelegateCommand<object> saveCurrent;
        #endregion

        #region Constructor
        public LecturersRegisteredTableViewModel()
        {
            this.CurrentLecturer = new LecturerModel();
            this.LecturerStudents = new List<string>();
        }
        #endregion

        #region Properties
        public LecturerModel CurrentLecturer{ get; set; }
        public List<string> LecturerStudents { get; set; }

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

        public DelegateCommand<object> EditLecturer
        {
            get
            {
                if (editLecturer == null)
                    editLecturer = new DelegateCommand<object>(EditCurrentLecturer);

                return editLecturer;
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
        #endregion

        #region Methods
        private void DeleteCurrentLecturer(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Lecturers);

            LecturerModel selectedLecturer = view.CurrentItem as LecturerModel;

            Lecturers.Remove(selectedLecturer);
        }

        private void EditCurrentLecturer(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Lecturers);

            LecturerModel selectedLecturer = view.CurrentItem as LecturerModel;

            foreach (var subject in SpecialtiesCollection.Instance.SpecWithStudents)
            {
                if (selectedLecturer.Subjects.Contains(subject.Key))
                {
                    LecturerStudents.AddRange(subject.Value);
                }
            }

            this.CurrentLecturer.FirstName = selectedLecturer.FirstName;
            this.CurrentLecturer.LastName = selectedLecturer.LastName;
            this.CurrentLecturer.PersonalNumber = selectedLecturer.PersonalNumber;
            this.CurrentLecturer.Gender = selectedLecturer.Gender;
            this.CurrentLecturer.Title = selectedLecturer.Title;
            this.CurrentLecturer.Faculty = selectedLecturer.Faculty;
            this.CurrentLecturer.CurrentSubjects = string.Join(", ", selectedLecturer.Subjects);
            this.CurrentLecturer.Region = selectedLecturer.Region;
            this.CurrentLecturer.BirthDate = selectedLecturer.BirthDate;
            this.CurrentLecturer.Students = string.Join(", ", LecturerStudents);
        }

        private void SaveCurrentLecturer(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(Lecturers);

            LecturerModel selectedLecturer = view.CurrentItem as LecturerModel;

            selectedLecturer.FirstName = this.CurrentLecturer.FirstName;
            selectedLecturer.LastName = this.CurrentLecturer.LastName;
            selectedLecturer.PersonalNumber = this.CurrentLecturer.PersonalNumber;
            selectedLecturer.Gender = this.CurrentLecturer.Gender;
            selectedLecturer.Title = this.CurrentLecturer.Title;
            selectedLecturer.Faculty = this.CurrentLecturer.Faculty;
            selectedLecturer.Subjects = this.CurrentLecturer.CurrentSubjects.Split(", ".ToArray()).ToList();
            selectedLecturer.Region = this.CurrentLecturer.Region;
            selectedLecturer.BirthDate = this.CurrentLecturer.BirthDate;            
        }
        #endregion
    }
}
