using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Calendar.Appointments;

public partial class AppointmentCreateForm : Form
{
    public EventHandler<Appointment>? AppointmentCreated;
    private Appointment newAppointment = new();
    private Appointment[] allAppointments;
    private readonly Form previousForm;
    private Customer _customer;
    private User _user;

    private DateOnly selectedDate = new();
    private TimeOnly selectedTime = new();
    private TimeOnly selectedDuration = new();

    public AppointmentCreateForm(
        Form prevForm,
        User user,
        Customer customer,
        Appointment[] appointments
    )
    {
        InitializeComponent();

        previousForm = prevForm;
        _customer = customer;
        _user = user;
        allAppointments = appointments;
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
        var start = new DateTimeOffset(date, time, TimeSpan.Zero).ToUniversalTime();
        var end = new DateTimeOffset(date, time, TimeSpan.FromMinutes((double)duration)).ToUniversalTime();

        foreach (var appointment in allAppointments)
        {
            // if is the same customer and the schedules conflict
            if (appointment.Customer == _customer
                &&
                ((appointment.Start <= start && start <= appointment.End)
                || (appointment.Start <= end && end <= appointment.End)
                || (appointment.Start <= start && end <= appointment.End)
                || (start <= appointment.Start && appointment.End <= end))
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
        ) 
        {
            newAppointment.Title = title;
            newAppointment.Description = description;
            newAppointment.Location = location;
            newAppointment.Contact = contact;
            newAppointment.Type = type;
            newAppointment.Url = new Uri(url);
            newAppointment.Start = new DateTime(selectedDate, selectedTime).ToUniversalTime();
            newAppointment.End = newAppointment.Start.AddMinutes((double)selectedDuration).ToUniversalTime();
            newAppointment.CreateDate = DateTimeOffset.UtcNow;
            newAppointment.LastUpdate = Appointment.UpdateFormat();
            newAppointment.LastUpdateBy = _user.UserName;


            AppointmentCreated?.Invoke(this, newAppointment);
        }
    }

    private void QuitAppointmentButton_Clicked(object sender, EventArgs e)
    {
        previousForm.Show();
        Hide();
    }
}
