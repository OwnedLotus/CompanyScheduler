using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerUpdateForm : Form
{
    public event EventHandler<Customer>? CustomerUpdated;
    public Customer Customer { get; private set; }
    public User User { get; private set; }
    private readonly Form _mainForm;
    public CustomerUpdateForm(User user, Customer customer, Form mainForm)
    {
        InitializeComponent();
        User = user;
        _mainForm = mainForm;
        Customer = customer;

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
        var customerName = updateCustomerNameTextBox.Text;
        var customerAddress1 = updateAddress1TextBox.Text;
        var customerAddress2 = updateAddress2TextBox.Text;
        var customerPostal = updateAddressPostalCodeTextBox.Text;
        var customerCityName = updateCityNameTextBox.Text;
        var customerCountryName = updateCountryNameTextBox.Text;
        var customerPhone = updateAddressPhoneTextBox.Text;

        string[] inputs = [
                customerName,
                customerAddress1,
                customerAddress2,
                customerPostal,
                customerCityName,
                customerCountryName
            ];

        if (
            Appointment.CheckTextBoxes(inputs) ||
            Address.OnlyDigitsAndDashes(customerPhone)
            )
        {
            var country = new Country()
            {
                CountryName = customerCountryName,
                CreateDate = DateTimeOffset.UtcNow,
                CreatedBy = User.UserName,
                LastUpdate = Appointment.UpdateFormat(),
                LastUpdateBy = User.UserName
            };
            var city = new City()
            {
                CityName = customerCityName,
                Country = country,
                CreateDate = DateTimeOffset.UtcNow,
                CreatedBy = User.UserName,
                LastUpdate = Appointment.UpdateFormat(),
                LastUpdateBy = User.UserName
            };
            var address = new Address()
            {
                Address1 = customerAddress1,
                Address2 = customerAddress2,
                City = city,
                PostalCode = customerPostal,
                Phone = customerPhone,
                CreateDate = DateTimeOffset.UtcNow,
                CreatedBy = User.UserName,
                LastUpdate = Appointment.UpdateFormat(),
                LastUpdateBy = User.UserName,
            };

            Customer.CustomerName = customerName;
            Customer.Address = address;
            Customer.Active = 0;
            Customer.CreatedDate = DateTime.UtcNow;
            Customer.LastUpdate = Appointment.UpdateFormat();
            Customer.LastUpdateBy = User.UserName;

            CustomerUpdated?.Invoke(this, Customer);
            _mainForm.Show();
            Close();
        }
    }

    private void UpdateCustomerQuitButton_Click(object sender, EventArgs e)
    {
        _mainForm.Show();
        Close();
    }
}
