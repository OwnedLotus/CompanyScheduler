using System.Globalization;

namespace CompanyScheduler;

/// <summary>
/// This is the login form
/// </summary>
public partial class LoginForm : Form
{
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

    public LoginForm()
    {
        InitializeComponent();

        // LocalText.Text = LabelText;
        // currLocationText.Text = locEn;
        // userText.Text = loginEn;
        // passText.Text = passEn;
        // welcomeText.Text = welcomeEN;
    }

    public void LoginButton_Clicked(Object sender)
    {
        /*
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
        */
    }

    public void QuitButton_Clicked(Object sender) => Environment.Exit(0);

    public void LanguagesSelector_SelectionChanged(Object sender)
    {
        /*
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
        */
    }

    public void UpdateLoginLog(string username)
    {
        // using (StreamWriter sw = new(filePath, append: true))
        {
            //sw.WriteLine($"{DateTime.UtcNow}: {userTextBox.Text}");
        }
    }
}

public enum LoginLanguage
{
    English,
    Japanese
}
