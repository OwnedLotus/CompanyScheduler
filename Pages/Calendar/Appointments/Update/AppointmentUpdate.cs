using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Calendar.Appointments;

public partial class AppointmentUpdateForm : Form
{
    public EventHandler<Appointment>? AppointmentCreated;
    private Appointment _appointment;
    private Appointment[] allAppointments;
    private readonly Form previousForm;
    private Customer? _customer;
    private User? _user;

    private DateOnly selectedDate = new();
    private TimeOnly selectedTime = new();
    private TimeOnly selectedDuration = new();

    public AppointmentUpdateForm(Form prevForm, Appointment appointment, Appointment[] appointments, User user)
    {
        InitializeComponent();

        previousForm = prevForm;
        allAppointments = appointments;

        _appointment = appointment;

        titleTextBox.Text = appointment.Title;
        descriptionTextBox.Text = appointment.Description;
        locationTextBox.Text = appointment.Location;
        contactTextBox.Text = appointment.Contact;
        typeTextBox.Text = appointment.Type;
        urlTextBox.Text = appointment.Url?.ToString();

        datePicker.Value = appointment.Start.ToLocalTime().LocalDateTime;
        timePicker.Value = appointment.Start.ToLocalTime().LocalDateTime;
        durationPicker.Value = appointment.End.Subtract(appointment.Start).Minutes;

        _customer = appointment.Customer;
        _user = user;
    }

    /// <summary>
    /// Validates local time within Mon->Fri
    /// </summary>
    /// <param name="date">Local Time</param>
    private bool ValidateDate(DateOnly date)
    {
        if (date.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday)
            return false;

        return true;
    }

    /// <summary>
    /// Validate Local time within 9->5
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool ValidateTime(TimeOnly time)
    {
        if (
            // 9 am
            time.ToTimeSpan() < new TimeSpan(9, 0, 0)
            // 5 pm
            || time.ToTimeSpan() > new TimeSpan(17, 0, 0)
        )
            return false;
        return true;
    }

    /// <summary>
    /// Validates whether the appointment conflicts with other appointments
    /// </summary>
    /// <param name="date">Date of appointment</param>
    /// <param name="time">Start time of appointment</param>
    /// <param name="duration">The length of the appointment</param>
    /// <returns></returns>
    private bool ValidateScheduleConflict(DateOnly date, TimeOnly time, decimal duration)
    {
        // all appointments are in utc
        var start = new DateTimeOffset(date, time, TimeSpan.Zero).ToUniversalTime();
        var end = new DateTimeOffset(date,time, TimeSpan.FromMinutes((double)duration)).ToUniversalTime();

        foreach (var appointment in allAppointments)
        {
            // if is the same customer and the schedules conflict
            if ( appointment.Customer == _customer 
                && 
                ((appointment.Start <= start && start <= appointment.End)
                || (appointment.Start <= end && end <= appointment.End)
                || (appointment.Start <= start && end <= appointment.End)
                || (start <= appointment.Start &&  appointment.End <= end))
                )
                return false;
        }
        return true;
    }

    private void SaveAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var title = titleTextBox.Text;
        var description = descriptionTextBox.Text;
        var location = locationTextBox.Text;
        var contact = contactTextBox.Text;
        var type = typeTextBox.Text;
        var url = urlTextBox.Text;

        string[] inputs = [title, description, location, contact, type, url];

        var selectedDate = DateOnly.FromDateTime(datePicker.Value);
        var selectedTime = TimeOnly.FromDateTime(timePicker.Value);
        var selectedDuration = durationPicker.Value;

        if (
            Appointment.CheckTextBoxes(inputs)
            || ValidateTime(selectedTime)
            || ValidateDate(selectedDate)
            || ValidateScheduleConflict(selectedDate, selectedTime, selectedDuration)
        ) {
            _appointment.Title = title;
            _appointment.Description = description;
            _appointment.Location = location;
            _appointment.Contact = contact;
            _appointment.Type = type;
            _appointment.Url = new Uri(url);
            _appointment.Start = new DateTime(selectedDate, selectedTime).ToUniversalTime();
            _appointment.End = _appointment.Start.AddMinutes((double)selectedDuration).ToUniversalTime();
            _appointment.CreateDate = DateTimeOffset.UtcNow;
            _appointment.LastUpdate = Appointment.UpdateFormat();
            _appointment.LastUpdateBy = _user?.UserName;

        }
    }

    private void QuitAppointmentButton_Clicked(object sender, EventArgs e)
    {
        previousForm.Show();
        Hide();
    }
}
