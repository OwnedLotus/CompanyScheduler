using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerDeleteForm : Form
{
    public event EventHandler<Customer>? CustomerCreated;
    private Customer? _customer;
    private readonly User _user;
    public CustomerDeleteForm(User user)
    {
        InitializeComponent();
        _user = user;
    }
}
