using CompanyScheduler.Models;
using CompanyScheduler.Models.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages.Calendar.Appointments;

public partial class AppointmentCreateForm : Form
{
    public EventHandler<Appointment>? AppointmentCreated;
    private Appointment newAppointment = new();
    private readonly Form previousForm;
    private readonly Customer _customer;
    private readonly User _user;

    public AppointmentCreateForm(Form prevForm, User user, Customer customer)
    {
        InitializeComponent();

        previousForm = prevForm;
        _customer = customer;
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
    private bool ValidateScheduleConflict(DateOnly date, TimeOnly time, int duration)
    {
        DateTime start = new DateTime(date, time);
        DateTime end = start.AddMinutes(duration);

        using var context = new ClientScheduleContext();
        var appointments = context
            .Appointments.Include(a => a.Customer)
            .Where(a => a.CustomerId == _customer.CustomerId);

        foreach (var appointment in appointments)
        {
            var intersect1 = appointment.Start <= start && appointment.End <= end && start <= appointment.End;
            var intersect2 = start <= appointment.Start && end <= appointment.End && appointment.Start <= end;
            var intersect3 = start <= appointment.Start && appointment.End <= end; // Interior
            var intersect4 = appointment.Start <= start && end <= appointment.End; // Exterior

            if (intersect1 || intersect2 || intersect3 || intersect4)
                return false;
        }
        return true;
    }

    private void SaveAppointmentButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var title = titleTextBox.Text.Trim();
            var description = descriptionTextBox.Text.Trim();
            var location = locationTextBox.Text.Trim();
            var contact = contactTextBox.Text.Trim();
            var type = typeTextBox.Text.Trim();
            var url = urlTextBox.Text.Trim();

            string[] inputs = [title, description, location, contact, type, url];

            var selectedDate = DateOnly.FromDateTime(datePicker.Value);
            var selectedTime = TimeOnly.FromDateTime(timePicker.Value);
            var selectedDateTime = new DateTime(selectedDate, selectedTime, DateTimeKind.Local);

            var parsed = int.TryParse(durationPicker.Text.Trim(), out int selectedDuration);

            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var utcDate = TimeZoneInfo.ConvertTimeToUtc(selectedDateTime);

            var estDT = TimeZoneInfo.ConvertTimeFromUtc(utcDate, est);

            var estDate = DateOnly.FromDateTime(estDT);
            var estTime = TimeOnly.FromDateTime(estDT);

            string message = "Failed to Parse Appointment";

            if (!parsed)
            {
                message = "Failed to parse input from duration selector";
                throw new FailedAppointmentCreateException(message);
            }
            if (!Appointment.CheckTextBoxes(inputs))
            {
                message = "Failed to input strings into textboxes";
                throw new FailedAppointmentCreateException(message);
            }
            if (!ValidateTime(estTime))
            {
                message = "Time falls outside 9 am to 5pm EST";
                throw new FailedAppointmentCreateException(message);
            }
            if (!ValidateDate(estDate))
            {
                message = "Date selected falls outside Monday - Friday EST";
                throw new FailedAppointmentCreateException(message);
            }
            if (
                !ValidateScheduleConflict(
                    DateOnly.FromDateTime(utcDate),
                    TimeOnly.FromDateTime(utcDate),
                    selectedDuration
                )
            )
            {
                message = "Scheduled appointment conflicts with customer's available appointment";
                throw new FailedAppointmentCreateException(message);
            }

            newAppointment.Title = title;
            newAppointment.Description = description;
            newAppointment.Location = location;
            newAppointment.Contact = contact;
            newAppointment.Type = type;
            newAppointment.Url = url;
            newAppointment.Start = utcDate;
            newAppointment.End = utcDate.AddMinutes(selectedDuration);
            newAppointment.CreateDate = DateTime.UtcNow;
            newAppointment.LastUpdate = DateTime.UtcNow;
            newAppointment.LastUpdateBy = _user.UserName;
            newAppointment.CreatedBy = _user.UserName;

            using (var context = new ClientScheduleContext())
            {
                newAppointment.User = context.Users.Find(_user.UserId)!;
                newAppointment.Customer = context.Customers.Find(_customer.CustomerId)!;

                context.Appointments.Add(newAppointment);
                context.SaveChanges();
            }

            AppointmentCreated?.Invoke(this, newAppointment);
            previousForm.Show();
            Close();
        }
        catch (FailedAppointmentCreateException error)
        {
            var message = error.ToString();
            var caption = "Failed Creation";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }

    private void QuitAppointmentButton_Clicked(object sender, EventArgs e)
    {
        previousForm.Show();
        Hide();
    }
}
