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
        var customerPhoneNumber = addAddressPhoneTextBox.Text;

        Customer.Address = new Address();



        string[] inputs = [customerName, customerPhoneNumber];

        if (
            Address.CheckTextBoxes(inputs) ||
            Address.OnlyDigitsAndDashes(customerPhoneNumber)
            )
        {
            Customer.CustomerName = customerName;
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
