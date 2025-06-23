using System.ComponentModel;
using System.Linq;

using CompanyScheduler.Data;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Customers;

using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages;

public partial class HomeForm : Form
{
    List<Appointment>? appointments;
    Appointment? _selectedAppointment;

    DbContext? dbContext = null;


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
        var CreateCustomer = new CustomerCreateForm(_user, this);
        CreateCustomer.Show();
        Hide();
    }

    private void UpdateCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null || _selectedAppointment.Customer is null)
            return;

        var UpdateCustomer = new CustomerUpdateForm(_user, _selectedAppointment.Customer, this);
        UpdateCustomer.Show();
        Hide();
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
    {
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
