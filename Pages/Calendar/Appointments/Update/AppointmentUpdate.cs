using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Calendar.Appointments;

public partial class AppointmentUpdateForm : Form
{
    public EventHandler<Appointment>? AppointmentCreated;
    private Appointment _appointment;
    private Appointment updatedAppointment;
    private readonly Form previousForm;
    private readonly Customer? _customer;
    private User? _user;

    public AppointmentUpdateForm(Form prevForm, Appointment appointment, User user)
    {
        InitializeComponent();

        previousForm = prevForm;
        _appointment = appointment;
        updatedAppointment = appointment;
        titleTextBox.Text = appointment.Title;
        descriptionTextBox.Text = appointment.Description;
        locationTextBox.Text = appointment.Location;
        contactTextBox.Text = appointment.Contact;
        typeTextBox.Text = appointment.Type;
        urlTextBox.Text = appointment.Url?.ToString();
        datePicker.Value = appointment.Start.ToLocalTime();
        timePicker.Value = appointment.Start.ToLocalTime();
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
    /// According to EST Mon->Fri 9am->5pm
    /// </summary>
    /// <param name="date">Date of appointment</param>
    /// <param name="time">Start time of appointment</param>
    /// <param name="duration">The length of the appointment</param>
    /// <returns></returns>
    private bool ValidateScheduleConflict(DateOnly date, TimeOnly time, decimal duration)
    {
        TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        // all appointments are in utc
        var start = new DateTime(date, time).ToUniversalTime();
        start = TimeZoneInfo.ConvertTimeFromUtc(start, est);
        var end = start.AddMinutes((double)duration);
        end = TimeZoneInfo.ConvertTimeFromUtc(end, est);

        List<Appointment> allAppointments = [];

        using (var context = new ClientScheduleContext())
        {
            allAppointments = [.. context.Appointments];
        }

        foreach (var appointment in allAppointments)
        {
            var appointmentStart = TimeZoneInfo.ConvertTimeFromUtc(appointment.Start, est);
            var appointmentEnd = TimeZoneInfo.ConvertTimeFromUtc(appointment.End, est);

            // if is the same customer and the schedules conflict
            if (
                appointment.Customer == _customer
                && (
                    (appointmentStart <= start && start <= appointmentEnd)
                    || (appointmentStart <= end && end <= appointmentEnd)
                    || (appointmentStart <= start && end <= appointmentEnd)
                    || (start <= appointmentStart && appointmentEnd <= end)
                )
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
            updatedAppointment.Title = title;
            updatedAppointment.Description = description;
            updatedAppointment.Location = location;
            updatedAppointment.Contact = contact;
            updatedAppointment.Type = type;
            updatedAppointment.Url = url;
            updatedAppointment.Start = new DateTime(selectedDate, selectedTime).ToUniversalTime();
            updatedAppointment.End = _appointment
                .Start.AddMinutes((double)selectedDuration)
                .ToUniversalTime();
            updatedAppointment.CreateDate = DateTime.UtcNow;
            updatedAppointment.LastUpdate = DateTime.UtcNow;
            updatedAppointment.LastUpdateBy = _user?.UserName!;

            using (var context = new ClientScheduleContext())
            {
                context.Appointments.Remove(_appointment);
                context.Appointments.Add(updatedAppointment);
                context.SaveChanges();
            }

            previousForm.Show();
            Close();
        }
        else
        {
            string message = "Failed to Parse Appointment";
            string caption = "Input Value Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }
    }

    private void QuitAppointmentButton_Clicked(object sender, EventArgs e)
    {
        previousForm.Show();
        Hide();
    }
}
