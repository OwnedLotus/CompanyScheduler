using System.ComponentModel;
using System.Globalization;

namespace CompanyScheduler.Pages.Login;

partial class LoginForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Login";

        loginHeaderLabel = new()
        {
            AutoSize = true,
            Location = new Point(1, 1),
            Name = "loginHeaderLabel",
            TabIndex = 0,
            Text = "Login"
        };

        locationLabel = new()
        {
            AutoSize = true,
            Location = new Point(250, 40),
            Name = "locationLabel",
            Text = "Current Location: "
        };

        currentLocationLabel = new()
        {
            AutoSize = true,
            Location = new Point(400, 40),
            Name = "currentLocationLabel",
            Text = RegionInfo.CurrentRegion.DisplayName.ToString()
        };
        Console.WriteLine(RegionInfo.CurrentRegion.CurrencyNativeName.ToString());

        userLabel = new()
        {
            AutoSize = true,
            Location = new Point(300, 80),
            Name = "userLabel",
            Text = "Login: "
        };

        userBox = new()
        {
            Location = new Point(300, 100),
            Name = "userBox"
        };

        passLabel = new()
        {
            AutoSize = true,
            Location = new Point(300, 130),
            Name = "passLabel",
            Text = "Password: "
        };

        passBox = new()
        {
            Location = new Point(300, 150),
            Name = "passBox"

        };

        loginButton = new()
        {
            Location = new Point(320, 200),
            Name = "loginButton",
            Text = "Login"
        };
        loginButton.Click += LoginButton_Clicked;

        selectionBox = new()
        {
            Location = new Point(500, 130),
            Name = "selectionBox",
            Height = 35,
            Width = 80
        };
        selectionBox.BeginUpdate();
        selectionBox.Items.Add("English");
        selectionBox.Items.Add("NIHONGO");
        selectionBox.EndUpdate();
        selectionBox.SelectedIndexChanged += LanguagesSelector_SelectionChanged;
        SuspendLayout();
        quitButton = new()
        {
            Location = new Point(500, 200),
            Name = "quitBox",
            Text = "Quit"
        };
        quitButton.Click += QuitButton_Clicked;

        Controls.Add(loginHeaderLabel);
        Controls.Add(locationLabel);
        Controls.Add(currentLocationLabel);
        Controls.Add(userLabel);
        Controls.Add(userBox);
        Controls.Add(passLabel);
        Controls.Add(passBox);
        Controls.Add(loginButton);
        Controls.Add(selectionBox);
        Controls.Add(quitButton);
    }

    #endregion


    Label loginHeaderLabel;
    Label locationLabel;
    Label currentLocationLabel;
    Label userLabel;
    TextBox userBox;
    Label passLabel;
    TextBox passBox;
    Button loginButton;
    ListBox selectionBox;
    Button quitButton;
}
