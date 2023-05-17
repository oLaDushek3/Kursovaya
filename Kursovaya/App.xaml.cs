using Kursovaya.View;
using Kursovaya.ViewModel;
using System.Windows;
using System.Windows.Markup;

namespace Kursovaya
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                  var mainView = new MainView();
                  mainView.Show();
                  loginView.Close();
                  mainView.IsVisibleChanged += (s, ev) =>
                  {
                      if (mainView.IsVisible == false && mainView.IsLoaded)
                      {
                          System.Windows.Forms.Application.Restart();
                          Application.Current.Shutdown();
                      }
                  };
                }
            };
           
        }
    }
}
