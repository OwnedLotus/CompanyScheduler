using System.ComponentModel;
using System.Linq;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Customers;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages;

/// <summary>
/// Form handling main activities
///
/// TODO:
///     Add Exception Handling add update delete
///     Add report functions
/// </summary>


public partial class HomeForm : Form
{
    private BindingList<Appointment>? appointments;
    private Appointment? _selectedAppointment = null;

    public User User { get; private set; }

    public HomeForm(User user)
    {
        InitializeComponent();
        LoadAppointments();
        User = user;
    }

    private void LoadAppointments()
    {
        using var context = new ClientScheduleContext();
        appointments = [.. context.Appointments.Include(a => a.User)];
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

        var UpdateCustomer = new CustomerUpdateForm(User, _selectedAppointment.Customer, this);

        using (var context = new ClientScheduleContext())
        {
            context.Appointments.Remove(_selectedAppointment);
            UpdateCustomer.CustomerUpdated += (sender, customer) =>
                _selectedAppointment.Customer = customer;
            context.Appointments.Add(_selectedAppointment);
            context.SaveChanges();
        }

        UpdateCustomer.Show();
        Hide();
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null || _selectedAppointment.User is null)
            return;

        using var context = new ClientScheduleContext();
        context.Appointments.Remove(_selectedAppointment);
        context.SaveChanges();
    }

    private void AppointmentsButton_Clicked(object sender, EventArgs e) { }

    private int GenerateAppointmentTypesByMonth(DateOnly date)
    {
        throw new NotImplementedException();
    }

    private void GenerateSchedule()
    {
        throw new NotImplementedException();
    }

    private void GeneratePerCity(City city)
    {
        throw new NotImplementedException();
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
