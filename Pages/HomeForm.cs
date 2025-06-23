using System.ComponentModel;
using System.Linq;

using CompanyScheduler.Data;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Customers;

using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages;

/// <summary>
/// Form handeling main activities
/// 
/// TODO:
///     Add Exception Handeling add update delete
///     Add report functions
/// </summary>


public partial class HomeForm : Form
{
    private BindingList<Appointment>? appointments;
    private Appointment? _selectedAppointment;

    private readonly DbContext? dbContext;


    private User _user;

    public HomeForm(User user)
    {
        InitializeComponent();
        LoadAppointments();
        _user = user;
    }

    private void LoadAppointments()
    {
        using (var context = new CompanyContext())
        {
            appointments = [.. context.Appointments.Include(a => a.User)];
        }
    }

    private void CreateCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null || dbContext is null)
            return;
        var CreateCustomer = new CustomerCreateForm(_user, this);

        using (var context = new CompanyContext())
        {
            CreateCustomer.CustomerCreated += (sender, customer) => _selectedAppointment.Customer = customer;
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

        var UpdateCustomer = new CustomerUpdateForm(_user, _selectedAppointment.Customer, this);

        using (var context = new CompanyContext())
        {
            context.Appointments.Remove(_selectedAppointment);
            UpdateCustomer.CustomerUpdated += (sender, customer) => _selectedAppointment.Customer = customer;
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

        using (var context = new CompanyContext())
        {
            context.Appointments.Remove(_selectedAppointment);
            _selectedAppointment.Customer = null;
            context.Appointments.Add(_selectedAppointment);
            context.SaveChanges();
        }
    }

    private void GenerateAppointmentTypesByMonth()
    {

    }

    private void GenerateSchedule()
    {

    }

    private void GeneratePerCity(City city)
    {

    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
