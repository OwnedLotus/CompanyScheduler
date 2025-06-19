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
using CompanyScheduler.Pages;

namespace CompanyScheduler;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// This is the login page for the application
/// </summary>
public partial class MainWindow : Window
{
    private Frame _mainFrame = new();
    static readonly string filePath = "Login_History.txt";
    private string welcomeEN = "Please Login!";
    private string welcomeJP = "roguinshitekudasai";
    private string locEn = "Current Location: ";
    private string locJp = "現在地:";
    private string loginEn = "Login";
    private string loginJp = "ログイン";
    private string passEn = "Password";
    private string passJp = "パスワード";
    public string LabelText { get; private set; } = RegionInfo.CurrentRegion.DisplayName;
    private LoginLanguage loginLanguage = LoginLanguage.English;

    public MainWindow()
    {
        InitializeComponent();

        LocalText.Text = LabelText;

        currLocationText.Text = locEn;
        userText.Text = loginEn;
        passText.Text = passEn;
        welcomeText.Text = welcomeEN;
    }

    public void LoginButton_Clicked(Object sender, RoutedEventArgs e)
    {
        // Check if user is registered
        if (userTextBox.Text == "test" && passTextBox.Text == "test")
        {
            // Enter the application
            //this.Navigate(new Uri("New.xaml", UriKind.Relative));
            UpdateLoginLog(userTextBox.Text);
            NavigationService navigationService = NavigationService.GetNavigationService(layoutRoot);
            navigationService.Navigate(new HomeWindow());
        }
        else
        {
            // deny user entry
        }
    }

    public void QuitButton_Clicked(Object sender, RoutedEventArgs e) => Environment.Exit(0);

    public void LanguagesSelector_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        var selection = LanguagesSelector.SelectedIndex;

        // Change the UI language to the selected language
        switch (selection)
        {
            case 0:
                this.userText.Text = loginEn;
                this.passText.Text = passEn;
                this.currLocationText.Text = locEn;
                this.welcomeText.Text = welcomeEN;

                break;

            case 1:
                this.userText.Text = loginJp;
                this.passText.Text = passJp;
                this.currLocationText.Text = locJp;
                this.welcomeText.Text = welcomeJP;
                break;
            default:
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
