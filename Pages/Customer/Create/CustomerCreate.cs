using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerCreateForm : Form
{
    public event EventHandler<Customer>? CustomerCreated;
    private Customer? _customer;
    private readonly User _user;
    public CustomerCreateForm(User user) 
    { 
        InitializeComponent();
        _user = user;
    }
}
