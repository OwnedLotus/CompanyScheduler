using System.ComponentModel;
using System.Windows.Forms;

namespace CompanyScheduler.Pages.Calendar;

partial class CalendarForm
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
        this.Text = "Calendar Form";

        titleLabel = new()
        {
            Location = new Point(10, 10),
            Name = "titleLabel",
            AutoSize = true,
            Text = "Appointments"
        };

        monthLabel = new()
        {
            Location = new Point(550, 10),
            Name = "monthLabel",
            AutoSize = true,
            Text = "Month"
        };

        appointmentListBox = new()
        {
            Location = new Point(450, 50),
            Size = new Size(150, 200),
            Name = "appointmentListBox"
        };

        quitButton = new()
        {
            Location = new Point(600, 350),
            Text = "Return"
        };
        quitButton.Click += QuitButton_Click;

        addAppointmentButton = new()
        {
            Name = "addAppointmentButton",
            Text = "Add Appointment",
            Location = new Point(50, 350)
        };
        addAppointmentButton.Click += AddAppointmentButton_Clicked;

        updateAppointmentButton = new()
        {
            Name = "updateAppointmentButton",
            Text = "Update Appointment",
            Location = new Point(325, 350),
        };
        updateAppointmentButton.Click += updateAppointmentButton_Clicked;

        Controls.Add(titleLabel);
        Controls.Add(monthLabel);
        Controls.Add(appointmentListBox);
        Controls.Add(quitButton);
        Controls.Add(addAppointmentButton);
        Controls.Add(updateAppointmentButton);
    }


    Label titleLabel;
    Label monthLabel;
    Button addAppointmentButton;
    Button updateAppointmentButton;
    Button quitButton;

    ListBox appointmentListBox;
}