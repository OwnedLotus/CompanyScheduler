using CompanyScheduler.Models;

using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages.Calendar.Appointments;

public partial class AppointmentUpdateForm : Form
{
    public EventHandler<(Appointment, Appointment)>? AppointmentUpdated;
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
        durationPicker.Text = appointment.End.Subtract(appointment.Start).Minutes.ToString();
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
    private bool ValidateScheduleConflict(DateOnly date, TimeOnly time, int duration)
    {
        DateTime start = new DateTime(date, time);
        DateTime end = start.AddMinutes(duration);

        List<Appointment> allAppointments = [];

        using var context = new ClientScheduleContext();
        var appointments = context.Appointments
            .Include(a => a.Customer)
            .Where(a => a.CustomerId == _customer!.CustomerId);


        foreach (var appointment in allAppointments)
        {
            if (appointment.AppointmentId == _appointment.AppointmentId) continue;

            var intersect1 = appointment.Start <= start && appointment.End <= end;
            var intersect2 = start <= appointment.Start && end <= appointment.End;
            var intersect3 = start <= appointment.Start && appointment.End <= end;
            var intersect4 = appointment.Start <= start && end <= appointment.End;

            if (intersect1 || intersect2 || intersect3 || intersect4)
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
        var selectedDateTime = new DateTime(selectedDate, selectedTime);
        selectedDateTime = DateTime.SpecifyKind(selectedDateTime, DateTimeKind.Local);
        
        var parsed = int.TryParse(durationPicker.Text, out int selectedDuration);

        TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard time");

        var utcDate = TimeZoneInfo.ConvertTimeToUtc(selectedDateTime);

        var estDate = DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeToUtc(selectedDateTime));
        var estTime = TimeOnly.FromDateTime(TimeZoneInfo.ConvertTimeToUtc(selectedDateTime));

        if (
            parsed
            && Appointment.CheckTextBoxes(inputs)
            && ValidateTime(estTime)
            && ValidateDate(estDate)
            && ValidateScheduleConflict(DateOnly.FromDateTime(utcDate), TimeOnly.FromDateTime(utcDate), selectedDuration)
        )
        {
            using (var context = new ClientScheduleContext())
            {
                var _newAppointment = context.Appointments.FirstOrDefault(app => app.AppointmentId == _appointment.AppointmentId);

                _newAppointment!.Title = title;
                _newAppointment.Description = description;
                _newAppointment.Location = location;
                _newAppointment.Contact = contact;
                _newAppointment.Type = type;
                _newAppointment.Url = url;
                _newAppointment.Start = new DateTime(selectedDate, selectedTime).ToUniversalTime();
                _newAppointment.End = _newAppointment
                    .Start.AddMinutes(selectedDuration)
                    .ToUniversalTime();
                _newAppointment.LastUpdate = DateTime.UtcNow;
                _newAppointment.LastUpdateBy = _user?.UserName!;

           
                _newAppointment.Customer = context.Customers!.Find(_customer.CustomerId);

                context.SaveChanges();
                AppointmentUpdated?.Invoke(this, (_appointment, _newAppointment));

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
