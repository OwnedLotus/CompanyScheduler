using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyScheduler.Models;

namespace CompanyScheduler;

/// <summary>
/// Interaction logic for MainWindow.xaml
///
///     TODO! Translate Login to different Language
/// </summary>
public partial class MainWindow : Window
{
    static readonly string filePath = "Login_History.txt";
    private string locEn = "Current Location: ";
    private string locJp = "!!!!";
    private string loginEn = "Login";
    private string loginJp = "!!!!";
    private string passEn = "Password";
    private string passJp = "!!!!";
    public string LabelText { get; private set; } = RegionInfo.CurrentRegion.DisplayName;
    private LoginLanguage loginLanguage = LoginLanguage.English;

    public MainWindow()
    {
        InitializeComponent();
        LocalText.Text = LabelText;

        currLocationText.Text = locEn;
        userText.Text = loginEn;
        passText.Text = passEn;
    }

    public void LoginButton_Clicked(Object sender, RoutedEventArgs e)
    {
        // Check if user is registered
        if (userTextBox.Text == "test" && passTextBox.Text == "test")
        {
            // Enter the application
            //this.Navigate(new Uri("New.xaml", UriKind.Relative));
            UpdateLoginLog(userTextBox.Text);
        }
        else
        {
            // deny user entry
        }
    }

    public void QuitButton_Clicked(Object sender, RoutedEventArgs e) => Environment.Exit(0);

    public void LanguagesSelector_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        if (e is null || e.AddedItems is null || e.AddedItems[0] is null)
            return;

        loginLanguage = (LoginLanguage)e.AddedItems[0];

        // Change the UI language to the selected language
        switch (loginLanguage)
        {
            case LoginLanguage.English:
                break;

            case LoginLanguage.Japanese:
                break;
        }
    }

    public void UpdateLoginLog(string username)
    {
        using (StreamWriter sw = new(filePath, append: true))
        {
            sw.WriteLine($"{DateTime.UtcNow}: {userTextBox.Text}");
        }
    }
}

public enum LoginLanguage
{
    English,
    Japanese
}
