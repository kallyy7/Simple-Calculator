namespace University_System.ViewModel.Students
{
    public class StudentInformationViewModel : BaseViewModel
    {
        #region Declaration
        private string firstName;
        private string lastName;
        private string personalNumber;
        private string gender;
        private string faculty;
        private string facultyNumber;
        private string specialty;
        private string region;
        private string birthDate;
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
        #endregion
    }
}
