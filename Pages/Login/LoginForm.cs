using System.Globalization;
using System.Linq;
using CompanyScheduler.Data;
using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Login;

/// <summary>
/// This is the login form
///
///     Last thing todo is the
///     Alert message translation
///
/// </summary>
public partial class LoginForm : Form
{
    static readonly string filePath = "Login_History.txt";
    private string welcomeEN = "Please Login!";
    private string welcomeJP = "ログインして下さい";
    private string locEn = "Current Location: ";
    private string locJp = "現在地:";
    private string loginEn = "Login: ";
    private string loginJp = "ログイン";
    private string passEn = "Password: ";
    private string passJp = "パスワード";
    private string quitEn = "Quit";

    private string quitJp = "終了";
    private string loginBtEn = "Login";
    private string loginBtJp = "入て";

    private string messageFailedEn = "Username or Password do not match";
    private string messageFailedJp = "ハンドルやパスワードは不正確です";

    private string captionFailedEn = "Failed Login";
    private string captionFailedJp = "ログイン失敗";

    int selection = 0;

    private User _user = new();
    public string LabelText { get; private set; } = RegionInfo.CurrentRegion.DisplayName;

    public LoginForm() => InitializeComponent();

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
#if !DEBUG
        // Check if user is registered
        using (var context = new CompanyContext())
        {
            var users = context.Users.AsParallel();

            foreach (var user in users)
            {
                if (userBox.Text == user.UserName && passBox.Text == user.Password)
                {
                    // Enter the application
                    UpdateLoginLog(userBox.Text);
                    var home = new HomeForm(new User());
                    home.Show();
                    Hide();
                }
                else
                {
                    switch (selection)
                    {
                        case 0:
                            MessageBox.Show(messageFailedEn, captionFailedEn, MessageBoxButtons.OK);
                            break;
                        case 1:
                            MessageBox.Show(messageFailedJp, captionFailedJp, MessageBoxButtons.OK);
                            break;
                    }
                }
            }
        }
#else
        if (userBox.Text == "test" && passBox.Text == "test")
        {
            // Enter the application
            UpdateLoginLog(userBox.Text);
            var home = new HomeForm(new User());
            home.Show();
            Hide();
        }
        else
        {
            switch (selection)
            {
                case 0:
                    MessageBox.Show(messageFailedEn, captionFailedEn, MessageBoxButtons.OK);
                    break;
                case 1:
                    MessageBox.Show(messageFailedJp, captionFailedJp, MessageBoxButtons.OK);
                    break;
            }
        }
#endif
    }

    public void LanguagesSelector_SelectionChanged(object sender, EventArgs e)
    {
        selection = selectionBox.SelectedIndex;

        // Change the UI language to the selected language
        switch (selection)
        {
            case 0:
                userLabel.Text = loginEn;
                passLabel.Text = passEn;
                locationLabel.Text = locEn;
                loginHeaderLabel.Text = welcomeEN;
                quitButton.Text = quitEn;
                loginButton.Text = loginBtEn;
                break;

            case 1:
                userLabel.Text = loginJp;
                passLabel.Text = passJp;
                locationLabel.Text = locJp;
                loginHeaderLabel.Text = welcomeJP;
                quitButton.Text = quitJp;
                loginButton.Text = loginBtJp;
                break;
            default:
                break;
        }
    }

    public void UpdateLoginLog(string username)
    {
        using (StreamWriter sw = new(filePath, append: true))
        {
            sw.WriteLine($"{DateTime.UtcNow}: {userBox.Text}");
        }
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}

public enum LoginLanguage
{
    English,
    Japanese
}
