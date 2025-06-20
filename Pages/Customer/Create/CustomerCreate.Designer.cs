using System.ComponentModel;
using System.Windows.Forms;


namespace CompanyScheduler.Pages.Customers;

partial class CustomerCreateForm
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        this.Text = "Create Record";

        // _customer.CustomerName;
        // _customer.CustomerId;
        // _customer.
    }
}