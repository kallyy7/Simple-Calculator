namespace University_System.Helpers
{
    using Contracts;
    using ViewModel;
    using Views;

    public class ViewNavigation
    {
        #region Declaration
        private StudentView studentView;
        private StudentViewModel studentViewModel;
        private LecturerView lecturerView;
        private LecturerViewModel lecturerViewModel;
        private StudentsRegisteredTableView studentsRegisteredTableView;
        private StudentsRegisteredTableViewModel studentsRegisteredViewModel;
        private LecturersRegisteredTableView lecturerRegisteredTableView;
        private RegisteredLecturersViewModel lecturerRegisteredTableViewModel;
        #endregion

        #region Constuctor
        public ViewNavigation()
        {
            studentView = new StudentView();
            lecturerView = new LecturerView();
            studentsRegisteredTableView = new StudentsRegisteredTableView();
            studentsRegisteredViewModel = new StudentsRegisteredTableViewModel();
            lecturerRegisteredTableView = new LecturersRegisteredTableView();
            lecturerRegisteredTableViewModel = new RegisteredLecturersViewModel();
            lecturerViewModel = new LecturerViewModel();
            studentViewModel = new StudentViewModel();
        }
        #endregion

        #region Commands
        public BaseViewModel ViewNavigationButton(ButtonCommands? commandParam)
        {
            IView view = null;
            BaseViewModel viewModel = null;

            switch (commandParam)
            {
                case ButtonCommands.ShowStudentRegisterForm:
                    view = studentView;
                    viewModel = studentViewModel;
                    break;
                case ButtonCommands.ShowLecturerRegisterForm:
                    view = lecturerView;
                    viewModel = lecturerViewModel;
                    break;
                case ButtonCommands.ShowStudentsTable:
                    view = studentsRegisteredTableView;
                    viewModel = studentsRegisteredViewModel;
                    break;
                case ButtonCommands.ShowLecturerTable:
                    view = lecturerRegisteredTableView;
                    viewModel = lecturerRegisteredTableViewModel;
                    break;
            }

            view.DataContext = viewModel;
            viewModel.View = view;

            return viewModel;
        }
        #endregion
    }
}
