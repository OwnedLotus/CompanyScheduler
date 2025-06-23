using System.Text.RegularExpressions;

using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerCreateForm : Form
{
    public event EventHandler<Customer>? CustomerCreated;
    public Customer Customer { get; private set; } = new();
    public User User { get; private set; }
    private readonly Form _mainForm;
    public CustomerCreateForm(User user, Form mainForm) 
    { 
        InitializeComponent();
        User = user;
        _mainForm = mainForm;
    }

    private void AddCustomerAddButton_Click(object sender, EventArgs e)
    {
        var customerName = addCustomerNameTextBox.Text;
        var customerAddress1 = addAddress1TextBox.Text;
        var customerAddress2 = addAddress2TextBox.Text;
        var customerPostal = addAddressPostalCodeTextBox.Text;
        var customerCityName = addCityNameTextBox.Text;
        var customerCountryName = addCountryNameTextBox.Text;
        var customerPhone = addAddressPhoneTextBox.Text;

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

            CustomerCreated?.Invoke(this, Customer);
            _mainForm.Show();
            Close();
        }
    }



    private void AddCustomerQuitButton_Click(object sender, EventArgs e)
    {
        _mainForm.Show();
        Close();
    }


}
