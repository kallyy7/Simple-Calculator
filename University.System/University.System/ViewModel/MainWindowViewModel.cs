namespace University_System.ViewModel
{
    using Helpers;
    using Prism.Commands;

    public class MainWindowViewModel : BaseViewModel
    {
        #region Declaration
        private ViewNavigation viewNavigation = new ViewNavigation();
        private BaseViewModel currentViewModel;
        private DelegateCommand<ButtonCommands?> viewChange;
        #endregion

        #region Properties
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<ButtonCommands?> ViewChange
        {
            get
            {
                if (viewChange == null)
                    viewChange = new DelegateCommand<ButtonCommands?>(ViewChanger);

                return viewChange;
            }
        }
        #endregion

        #region Methods
        private void ViewChanger(ButtonCommands? button)
        {
            if (button != null)
                CurrentViewModel = viewNavigation.ViewNavigationButton(button);
        }
        #endregion
    }
}
