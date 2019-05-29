namespace University_System
{
    using System.Windows;
    using ViewModel;
    using Views;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);          
            MainWindow view = new MainWindow();
            var viewModel = new MainWindowViewModel();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
