using System.ComponentModel;
using System.Windows.Forms;


namespace CompanyScheduler.Pages.Customers;

public partial class CustomerCreateForm
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


        addCustomerLabel = new()
        {
            Name = "addCustomerLabel",
            AutoSize = true,
            Text = "Add Customer!",
            Location = new Point(10,10)
        };

        #region Customer UI

        addCustomerNameLabel = new()
        {
            Name = "addCustomerNameLabel",
            AutoSize = true,
            Text = "Customer Name",
            Location = new Point(100, 40)
        };

        addCustomerNameTextBox = new()
        {
            Name = "addCustomerNameTextBox",
            Location = new Point(100, 60)
        };

        #endregion

        #region Address UI

        addAddress1Label = new()
        {
            Name = "addAddress1Label",
            AutoSize = true,
            Text = "Address",
            Location = new Point(100,100)
        };
        addAddress1TextBox = new() 
        {
            Name = "addAddress1TextBox",
            Location = new Point(100, 120)
        };

        addAddress2Label = new()
        {
            Name = "addAddress2Label",
            AutoSize = true,
            Text = "Secondary Address",
            Location = new Point(100, 160)
        };
        addAddress2TextBox = new()
        {
            Name = "addAddress2TextBox",
            Location = new Point(100, 180)
        };

        addAddressPostalCodeLabel = new()
        {
            Name = "addAddressPostalCodeLabel",
            AutoSize = true,
            Text = "PostalCode",
            Location = new Point(100, 220)
        };
        addAddressPostalCodeTextBox = new()
        {
            Name = "addAddressPostalCodeTextBox",
            Location = new Point(100, 240)
        };

        addAddressPhoneLabel = new()
        {
            Name = "addCustomerPhoneLabel",
            AutoSize = true,
            Text = "Customer Phone Number",
            Location = new Point(100, 280)
        };
        addAddressPhoneTextBox = new()
        {
            Name = "addCustomerPhoneTextBox",
            Location = new Point(100, 300)
        };

        #endregion

        #region City UI

        addCityNameLabel = new()
        {
            Name = "addCityNameLabel",
            AutoSize = true,
            Text = "City",
            Location = new Point(300, 40)
        };

        addCityNameTextBox = new()
        {
            Name = "addCityNameTextBox",
            Location = new Point(300, 60)
        };

        #endregion

        #region Country UI

        addCountryNameLabel = new()
        {
            Name = "addCountryNameLabel",
            AutoSize = true,
            Text = "Country",
            Location = new Point(300, 100)
        };
        addCountryNameTextBox = new()
        {
            Name = "addCountryNameTextBox",
            Location = new Point(300, 120)
        };

        #endregion

        addCustomerAddButton = new()
        {
            Name = "addCustomerAddButton",
            Text = "Add Customer",
            Location = new Point(600, 80)
        };
        addCustomerAddButton.Click += AddCustomerAddButton_Click;

        addCustomerQuitButton = new()
        {
            Name = "addCustomerQuitButton",
            Text = "Quit",
            Location = new Point(600, 240)
        };
        addCustomerQuitButton.Click += AddCustomerQuitButton_Click;


        Controls.Add(addCustomerLabel);

        Controls.Add(addCustomerNameLabel);
        Controls.Add(addCustomerNameTextBox);

        Controls.Add(addAddress1Label);
        Controls.Add(addAddress1TextBox);
        Controls.Add(addAddress2Label);
        Controls.Add(addAddress2TextBox);
        Controls.Add(addAddressPostalCodeLabel);
        Controls.Add(addAddressPostalCodeTextBox);
        Controls.Add(addAddressPhoneLabel);
        Controls.Add(addAddressPhoneTextBox);

        Controls.Add(addCityNameLabel);
        Controls.Add(addCityNameTextBox);

        Controls.Add(addCountryNameLabel);
        Controls.Add(addCountryNameTextBox);

        Controls.Add(addCustomerAddButton);
        Controls.Add(addCustomerQuitButton);
    }

    Label addCustomerLabel;

    #region Customer Info

    Label addCustomerNameLabel;
    TextBox addCustomerNameTextBox;

    #endregion

    #region Address Info

    Label addAddress1Label;
    TextBox addAddress1TextBox;
    Label addAddress2Label;
    TextBox addAddress2TextBox;
    Label addAddressPostalCodeLabel;
    TextBox addAddressPostalCodeTextBox;
    Label addAddressPhoneLabel;
    TextBox addAddressPhoneTextBox;

    #endregion

    #region City Info

    Label addCityNameLabel;
    TextBox addCityNameTextBox;

    #endregion

    #region Country Info

    Label addCountryNameLabel;
    TextBox addCountryNameTextBox;

    #endregion

    Button addCustomerAddButton;
    Button addCustomerQuitButton;
}