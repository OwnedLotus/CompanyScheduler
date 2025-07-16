using CompanyScheduler.Models;
using CompanyScheduler.Models.Errors;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerCreateForm : Form
{
    public EventHandler<Customer>? CustomerCreated;
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
        try
        {
            var customerName = addCustomerNameTextBox.Text;
            var customerAddress1 = addAddress1TextBox.Text;
            var customerAddress2 = addAddress2TextBox.Text;
            var customerPostal = addAddressPostalCodeTextBox.Text;
            var customerCityName = addCityNameTextBox.Text;
            var customerCountryName = addCountryNameTextBox.Text;
            var customerPhone = addAddressPhoneTextBox.Text;

            string[] inputs =
            [
                customerName,
                customerAddress1,
                customerAddress2,
                customerPostal,
                customerCityName,
                customerCountryName,
            ];

            string message = "Failed to Create Customer";

            if (!Appointment.CheckTextBoxes(inputs))
            {
                message = "Failed to Parse text boxes";
                throw new FailedCustomerCreateException(message);
            }
            if (!Address.OnlyDigitsAndDashes(customerPhone))
            {
                message = "Failed to Parse Digits and Dashes in program";
                throw new FailedCustomerCreateException(message);
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

            Customer.CustomerName = customerName;
            Customer.Address = address;
            Customer.Active = true;
            Customer.LastUpdate = DateTime.Now;
            Customer.LastUpdateBy = User.UserName;
            Customer.CreatedBy = User.UserName;

            using (var context = new ClientScheduleContext())
            {
                context.Customers.Add(Customer);
                context.SaveChanges();
            }

            CustomerCreated?.Invoke(this, Customer);
            _mainForm.Show();
            Close();
        }
        catch (FailedCustomerCreateException error)
        {
            var message = error.ToString();
            var caption = "Failed Customer Create";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }

    private void AddCustomerQuitButton_Click(object sender, EventArgs e)
    {
        _mainForm.Show();
        Close();
    }
}
