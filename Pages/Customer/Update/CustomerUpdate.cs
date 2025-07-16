using CompanyScheduler.Models;
using CompanyScheduler.Models.Errors;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerUpdateForm : Form
{
    public event EventHandler<Customer>? CustomerUpdated;
    private Customer _customer;
    private Customer _updatedCustomer;
    public User User { get; private set; }
    private readonly Form _mainForm;

    public CustomerUpdateForm(User user, Customer customer, Form mainForm)
    {
        InitializeComponent();
        User = user;
        _mainForm = mainForm;
        _customer = customer;
        _updatedCustomer = customer;

        updateCustomerNameTextBox.Text = customer.CustomerName;
        updateAddress1TextBox.Text = customer.Address?.Address1;
        updateAddress2TextBox.Text = customer.Address?.Address2;
        updateAddressPhoneTextBox.Text = customer.Address?.Phone;
        updateAddressPostalCodeTextBox.Text = customer.Address?.PostalCode;

        updateCityNameTextBox.Text = customer.Address?.City?.City1;

        updateCountryNameTextBox.Text = customer.Address?.City?.Country?.Country1;
    }

    private void UpdateCustomerAddButton_Click(object sender, EventArgs e)
    {
        try
        {
            var customerName = updateCustomerNameTextBox.Text;
            var customerAddress1 = updateAddress1TextBox.Text;
            var customerAddress2 = updateAddress2TextBox.Text;
            var customerPostal = updateAddressPostalCodeTextBox.Text;
            var customerCityName = updateCityNameTextBox.Text;
            var customerCountryName = updateCountryNameTextBox.Text;
            var customerPhone = updateAddressPhoneTextBox.Text;

            string[] inputs =
            [
                customerName,
                customerAddress1,
                customerAddress2,
                customerPostal,
                customerCityName,
                customerCountryName,
            ];

            var message = "Faild to Update Customer";

            if (!Appointment.CheckTextBoxes(inputs))
            {
                message = "Failed to parse Text boxes";
                throw new FailedCustomerUpdatedException(message);
            }
            if (!Address.OnlyDigitsAndDashes(customerPhone))
            {
                message = "Failed to parse Digits and Dashes in Phone number";
                throw new FailedCustomerUpdatedException(message);
            }

            var country = new Country()
            {
                Country1 = customerCountryName,
                CreateDate = DateTime.Now,
                CreatedBy = User.UserName,
                LastUpdate = DateTime.Now,
                LastUpdateBy = User.UserName,
            };
            var city = new City()
            {
                City1 = customerCityName,
                Country = country,
                CreateDate = DateTime.Now,
                CreatedBy = User.UserName,
                LastUpdate = DateTime.Now,
                LastUpdateBy = User.UserName,
            };
            var address = new Address()
            {
                Address1 = customerAddress1,
                Address2 = customerAddress2,
                City = city,
                PostalCode = customerPostal,
                Phone = customerPhone,
                CreateDate = DateTime.Now,
                CreatedBy = User.UserName,
                LastUpdate = DateTime.Now,
                LastUpdateBy = User.UserName,
            };

            _updatedCustomer.CustomerName = customerName;
            _updatedCustomer.Address = address;
            _updatedCustomer.Active = true;
            _updatedCustomer.LastUpdate = DateTime.Now;
            _updatedCustomer.LastUpdateBy = User.UserName;

            using (var context = new ClientScheduleContext())
            {
                context.Customers.Remove(_customer);
                context.Customers.Add(_updatedCustomer);
                context.SaveChanges();
            }

            CustomerUpdated?.Invoke(this, _updatedCustomer);
            _mainForm.Show();
            Close();
        }
        catch (FailedCustomerUpdatedException error)
        {
            var message = error.ToString();
            string caption = "Malformed Input";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }

    private void UpdateCustomerQuitButton_Click(object sender, EventArgs e)
    {
        _mainForm.Show();
        Close();
    }
}
