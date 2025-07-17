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

    private CultureInfo currentCulture = CultureInfo.CurrentCulture;
    private System.Windows.Forms.Timer _timer;

    public string LabelText { get; private set; } = RegionInfo.CurrentRegion.DisplayName;

    public LoginForm()
    {
        InitializeComponent();

        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 1000;
        _timer.Tick += _timer_Tick;
        _timer.Start();

        if (
            RegionInfo.CurrentRegion.TwoLetterISORegionName.Equals(
                "JP",
                StringComparison.OrdinalIgnoreCase
            )
        )
        {
            selection = 1;
        }
        selectionBox.SelectedIndex = selection;

    }

    private void _timer_Tick(object? sender, EventArgs e)
    {
        CultureInfo.CurrentCulture.ClearCachedData();

        if (currentCulture != CultureInfo.CurrentCulture)
        {
            currentCulture = CultureInfo.CurrentCulture;
            selectionBox.SelectedIndex = selectionBox.SelectedIndex == 0 ? 1 : 0;
        }

        LanguagesSelector_SelectionChanged(null, EventArgs.Empty);
    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        // Check if user is registered
        using var context = new ClientScheduleContext();

        var user = context.Users.FirstOrDefault(user =>
            user.UserName == userBox.Text && user.Password == passBox.Text
        );

        if (user is not null)
        {
            UpdateLoginLog(user.UserName);

            var home = new HomeForm(user);
            home.Show();
            Hide();
            _timer.Dispose();
        }
        else
        {
            var message = selection == 0 ? messageFailedEn : messageFailedJp;
            var caption = selection == 0 ? captionFailedEn : captionFailedJp;

            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }

    public void LanguagesSelector_SelectionChanged(object? sender, EventArgs e)
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
        sw.WriteLine($"{DateTime.Now}: {userBox.Text}");
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}

public enum LoginLanguage
{
    English,
    Japanese,
}
