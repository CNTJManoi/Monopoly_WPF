using System.Globalization;
using System.Windows;
using Monopoly.Properties;

namespace Monopoly;

/// <summary>
///     a command layout that implements ICommand.
/// </summary>
public partial class App
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(Settings.Default.Language);
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(Settings.Default.Language);
    }
}