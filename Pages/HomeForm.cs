using System.ComponentModel;

using CompanyScheduler.Models;
using CompanyScheduler.Pages.Customers;

namespace CompanyScheduler.Pages;

public partial class HomeForm : Form
{
    BindingList<Appointment> appointments = new();
    BindingList<Customer> customers = new();

    private User _user;

    public HomeForm(User user)
    {
        InitializeComponent();
        _user = user;

        appointments = LoadAppointments();
        customers = LoadCustomers();
    }

    public BindingList<Appointment> LoadAppointments()
    {
        BindingList<Appointment> appointments = [];
        return appointments;
    }

    public BindingList<Customer> LoadCustomers()
    {
        BindingList<Customer> customers = [];
        return customers;
    }

    private void CreateCustomerButton_Clicked(object sender, EventArgs e)
    {
        var CreateCustomer = new CustomerCreateForm(_user);
        CreateCustomer.Show();
        Hide();
    }

    private void UpdateCustomerButton_Clicked(object sender, EventArgs e)
    {
        var UpdateCustomer = new CustomerUpdateForm(_user);
        UpdateCustomer.Show();
        Hide();
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
    {
        var DeleteCustomer = new CustomerDeleteForm(_user);
        DeleteCustomer.Show();
        Hide();
    }

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
