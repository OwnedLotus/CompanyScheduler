using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Customers;

public partial class CustomerUpdateForm : Form
{
    public event EventHandler<Customer>? CustomerCreated;
    private Customer? _customer;
    private readonly User _user;
    public CustomerUpdateForm(User user)
    {
        InitializeComponent();
        _user = user;
    }

}
