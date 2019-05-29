using System.Collections.Generic;

namespace University_System.ViewModel
{
    public class LecturerInformationViewModel : BaseViewModel
    {
        #region Declaration
        private string firstName;
        private string lastName;
        private string personalNumber;
        private string gender;
        private string title;
        private string faculty;
        private List<string> subjects;
        private string region;
        private string birthDate;
        private string students;
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
                if (value == personalNumber)
                    return;

                personalNumber = value;

                OnPropertyChanged(nameof(PersonalNumber));
            }
        }

        public string Gender
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

        public List<string> Subjects
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

        public string Region
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
        #endregion
    }
}
