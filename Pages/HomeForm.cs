using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Calendar;
using CompanyScheduler.Pages.Customers;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages;

/// <summary>
/// Form handling main activities
/// </summary>
public partial class HomeForm : Form
{
    private readonly DateTime currentTime = DateTime.UtcNow;
    private readonly GregorianCalendar calendar = new();
    private BindingList<Appointment>? appointments;
    private Appointment? _selectedAppointment;

    public User User { get; private set; }

    public HomeForm(User user)
    {
        InitializeComponent();
        LoadAppointments();
        CheckSoonAppointments(15);
        User = user;
    }

    private void LoadAppointments()
    {
        using var context = new ClientScheduleContext();
        appointments = [.. context.Appointments.Include(a => a.User)];
    }

    private void CheckSoonAppointments(int span)
    {
        using var context = new ClientScheduleContext();

        foreach (var appointment in context.Appointments)
        {
            var minutesLeft = Math.Abs((appointment.Start - currentTime).TotalMinutes);

            if (minutesLeft <= span)
            {
                string message = $"Appointment {appointment.Title} starts in {minutesLeft}";
                string caption = $"Appointment Soon!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }
    }

    private void AppointmentListView_IndexChanged(object sender, EventArgs e)
    {
        if (appointmentListView.SelectedItems.Count > 0)
        {
            _selectedAppointment = appointmentListView.SelectedItems[0].Tag as Appointment;
        }
    }

    private void CalendarButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null)
            return;

        var CalendarForm = new CalendarForm(this, calendar, User);

        CalendarForm.Show();
        Hide();
    }

    private void CreateCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null)
            return;
        var CreateCustomer = new CustomerCreateForm(User, this);

        using (var context = new ClientScheduleContext())
        {
            CreateCustomer.CustomerCreated += (sender, customer) =>
                _selectedAppointment.Customer = customer;
            context.Appointments.Add(_selectedAppointment);
            context.SaveChanges();
        }

        CreateCustomer.Show();
        Hide();
    }

    private void UpdateCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null || _selectedAppointment.Customer is null)
            return;

        var tbdAppointment = _selectedAppointment;

        var UpdateCustomer = new CustomerUpdateForm(User, _selectedAppointment.Customer, this);
        UpdateCustomer.CustomerUpdated += (sender, customer) =>
        {
            using var context = new ClientScheduleContext();
            context.Appointments.Remove(_selectedAppointment);

            _selectedAppointment.Customer = customer;
            context.Appointments.Add(_selectedAppointment);
            context.SaveChanges();
        };

        UpdateCustomer.Show();
        Hide();
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
    {
        using var context = new ClientScheduleContext();
        if (
            _selectedAppointment is null
            || _selectedAppointment.User is null
            || !context.Appointments.Contains(_selectedAppointment)
        )
        {
            string message = "Failed to Delete Customer";
            string caption = "Missing Customer";
            MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, messageBoxButtons);
            return;
        }

        context.Appointments.Remove(_selectedAppointment);
        context.SaveChanges();
    }

    private void AppointmentsButton_Clicked(object sender, EventArgs e) { }

    // Report generating functions
    private IQueryable<string>? GenerateAppointmentTypesByMonth(DateOnly date) =>
        new ClientScheduleContext()
            .Appointments.Where(appointment => appointment.Start.Month == date.Month)
            .Select(appointment => appointment.Type)
            .Distinct();

    private IQueryable<List<Appointment>>? GenerateSchedule() =>
        new ClientScheduleContext()
            .Appointments.GroupBy(appointment => appointment.User)
            .Select(appointments => appointments.ToList());

    private IQueryable<bool>? GenerateAllCustomerWithAppointments() =>
        new ClientScheduleContext()
            .Customers.Select(customer => customer.Appointments.Count != 0)
            .Distinct();

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
