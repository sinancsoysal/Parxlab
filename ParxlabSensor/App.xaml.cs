using System.Windows;
using ParxlabSensor.Cache;

namespace ParxlabSensor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Barrel.ApplicationId = "&%J2A5g)";
        }
    }
}
