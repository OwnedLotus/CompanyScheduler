using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerUpdateForm : Form
{
    public event EventHandler<Customer>? CustomerCreated;
    private Customer _customer;
    private readonly User _user;
    private readonly Form _mainForm;
    public CustomerUpdateForm(User user, Customer customer, Form mainForm)
    {
        InitializeComponent();
        _user = user;
        _mainForm = mainForm;
        _customer = customer;

        updateCustomerNameTextBox.Text = customer.CustomerName;
        updateAddress1TextBox.Text = customer.Address?.Address1;
        updateAddress2TextBox.Text = customer.Address?.Address2;
        updateAddressPhoneTextBox.Text = customer.Address?.Phone;
        updateAddressPostalCodeTextBox.Text = customer.Address?.PostalCode;

        updateCityNameTextBox.Text = customer.Address?.City?.CityName;

        updateCountryNameTextBox.Text = customer.Address?.City?.Country?.CountryName;
    }

    private void UpdateCustomerAddButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void UpdateCustomerQuitButton_Click(object sender, EventArgs e)
    {
        _mainForm.Show();
        Close();
    }
}
