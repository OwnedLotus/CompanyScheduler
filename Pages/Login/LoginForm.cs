using System.Globalization;
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
    private readonly string welcomeEN = "Please Login!";
    private readonly string welcomeJP = "ログインして下さい";
    private readonly string locEn = "Current Location: ";
    private readonly string locJp = "現在地:";
    private readonly string loginEn = "Login: ";
    private readonly string loginJp = "ログイン";
    private readonly string passEn = "Password: ";
    private readonly string passJp = "パスワード";
    private readonly string quitEn = "Quit";

    private readonly string quitJp = "終了";
    private readonly string loginBtEn = "Login";
    private readonly string loginBtJp = "入て";

    private readonly string messageFailedEn = "Username or Password do not match";
    private readonly string messageFailedJp = "ハンドルやパスワードは不正確です";

    private readonly string captionFailedEn = "Failed Login";
    private readonly string captionFailedJp = "ログイン失敗";

    int selection = 0;

    public string LabelText { get; private set; } = RegionInfo.CurrentRegion.DisplayName;

    public LoginForm() => InitializeComponent();

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        // Check if user is registered
        using var context = new ClientScheduleContext();
        var users = context.Users.AsParallel();

        foreach (var user in users)
        {
            if (user is not null)
                AuthenticateUser(user);
        }
    }

    private void AuthenticateUser(User user)
    {
        if (userBox.Text == user.UserName && passBox.Text == user.Password)
        {
            // Enter the application
            UpdateLoginLog(userBox.Text);

            var home = new HomeForm(user);
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
        using StreamWriter sw = new(filePath, append: true);
        sw.WriteLine($"{DateTime.UtcNow}: {userBox.Text}");
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}

public enum LoginLanguage
{
    English,
    Japanese
}
