using System.ComponentModel;
using System.Windows.Forms;

namespace CompanyScheduler.Pages.Calendar.Appointments;

partial class AppointmentUpdateForm
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        this.Text = "Update Record";

        #region Time Pickers

        updateAppointmentLabel = new()
        {
            Name = "updateAppointmentLabel",
            Location = new Point(10, 10),
            AutoSize = true,
            Text = "Update An Appointment"
        };

        dateLabel = new()
        {
            Name = "dateLabel",
            Location = new Point(50, 20),
            AutoSize = true,
            Text = "Date"
        };
        datePicker = new()
        {
            Name = "datePicker",
            Format = DateTimePickerFormat.Custom,
            Location = new Point(50, 40),
            Size = new Size(100, 30),
        };

        timeLabel = new()
        {
            Name = "timeLabel",
            Location = new Point(50, 90),
            AutoSize = true,
            Text = "Time"
        };
        timePicker = new()
        {
            Name = "timePicker",
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "HH:mm:ss",
            Location = new Point(50, 110),
            Size = new Size(100, 30),
        };

        durationLabel = new()
        {
            Name = "durationLabel",
            Location = new Point(50, 140),
            AutoSize = true,
            Text = "Duration (Minutes)"
        };
        durationPicker = new()
        {
            Name = "durationPicker",
            Location = new Point(50, 160),
            Size = new Size(100, 30)
        };
        #endregion


        #region Text Info
        titleLabel = new()
        {
            Location = new Point(350, 40),
            AutoSize = true,
            Name = "titleLabel",
            Text = "Title"
        };
        titleTextBox = new()
        {
            Location = new Point(350, 60),
            Name = "titleTextBox",
        };

        descriptionLabel = new()
        {
            Name = "descriptionLabel",
            Text = "Description",
            AutoSize = true,
            Location = new Point(350, 90)
        };
        descriptionTextBox = new()
        {
            Name = "descritionTextBox",
            Location = new Point(350, 110)
        };

        locationLabel = new()
        {
            Name = "locationLabel",
            Location = new Point(350, 140),
            AutoSize = true,
            Text = "Location",
        };
        locationTextBox = new()
        {
            Name = "locationTextBox",
            Location = new Point(350, 160)
        };

        contactLabel = new()
        {
            Name = "contactLabel",
            Location = new Point(550, 40),
            AutoSize = true,
            Text = "Contact"
        };
        contactTextBox = new()
        {
            Name = "contactTextBox",
            Location = new Point(550, 60)
        };

        typeLabel = new()
        {
            Name = "typeLabel",
            Location = new Point(550, 90),
            AutoSize = true,
            Text = "Type"
        };
        typeTextBox = new()
        {
            Name = "typeTextBox",
            Location = new Point(550, 110)
        };
        urlLabel = new()
        {
            Name = "urlLabel",
            Location = new Point(550, 140),
            AutoSize = true,
            Text = "URL"
        };
        urlTextBox = new()
        {
            Name = "urlTextBox",
            Location = new Point(550, 160)
        };
        #endregion

        saveAppointmentButton = new()
        {
            Name = "saveAppointmentButton",
            Location = new Point(50, 400),
            Text = "Save Appointment",
            AutoSize = true
        };
        saveAppointmentButton.Click += SaveAppointmentButton_Clicked;

        quitAppointmentButton = new()
        {
            Name = "quitAppointmentButton",
            Location = new Point(600, 400),
            Text = "Quit",
            AutoSize = true
        };
        quitAppointmentButton.Click += QuitAppointmentButton_Clicked;

        #region Controls
        Controls.Add(updateAppointmentLabel);
        Controls.Add(dateLabel);
        Controls.Add(datePicker);
        Controls.Add(timeLabel);
        Controls.Add(timePicker);
        Controls.Add(durationLabel);
        Controls.Add(durationPicker);
        Controls.Add(titleLabel);
        Controls.Add(titleTextBox);
        Controls.Add(descriptionLabel);
        Controls.Add(descriptionTextBox);
        Controls.Add(locationLabel);
        Controls.Add(locationTextBox);
        Controls.Add(contactLabel);
        Controls.Add(contactTextBox);
        Controls.Add(typeLabel);
        Controls.Add(typeTextBox);
        Controls.Add(urlLabel);
        Controls.Add(urlTextBox);
        Controls.Add(saveAppointmentButton);
        Controls.Add(quitAppointmentButton);
        #endregion
    }


    Label updateAppointmentLabel;
    #region TimePickers
    Label dateLabel;
    DateTimePicker datePicker;
    Label timeLabel;
    DateTimePicker timePicker;
    Label durationLabel;
    NumericUpDown durationPicker;
    #endregion

    #region Text Info
    Label titleLabel;
    TextBox titleTextBox;
    Label descriptionLabel;
    TextBox descriptionTextBox;
    Label locationLabel;
    TextBox locationTextBox;
    Label contactLabel;
    TextBox contactTextBox;
    Label typeLabel;
    TextBox typeTextBox;
    Label urlLabel;
    TextBox urlTextBox;
    #endregion

    Button saveAppointmentButton;
    Button quitAppointmentButton;
}