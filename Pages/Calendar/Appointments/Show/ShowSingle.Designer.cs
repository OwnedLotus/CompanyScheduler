using System.ComponentModel;

namespace CompanyScheduler.Pages.Calendar.Appointments.Show;

partial class ShowSingleForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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

    private void InitializeComponent()
    {
        this.components = new Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 450);
        this.Text = "Show Single Appointment";

        singleLabel = new()
        {
            Name = "singleLabel",
            AutoSize = true,
            Location = new Point(10, 10)
        };

        singleGridView = new()
        {
            Name = "singleGridView",
            AutoSize = true,
            Location = new Point(200, 30),
            Size = new Size(400, 200)
        };

        okButton = new()
        {
            Name = "okButton",
            AutoSize = true,
            Location = new Point(420, 400),
            Text = "OK"
        };
        okButton.Click += OkButton_Click;

        Controls.Add(singleLabel);
        Controls.Add(singleGridView);
        Controls.Add(okButton);
    }


    Label singleLabel;
    PropertyGrid singleGridView;
    Button okButton;
}