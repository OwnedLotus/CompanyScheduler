using System.ComponentModel;
using System.Windows.Forms;

namespace CompanyScheduler.Pages.Customers;

partial class CustomerUpdateForm
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
        this.Text = "Update Record";
        
        updateCustomerLabel = new()
        {
            Name = "addCustomerLabel",
            AutoSize = true,
            Text = "Add Customer!",
            Location = new Point(10, 10)
        };

        #region Customer UI

        updateCustomerNameLabel = new()
        {
            Name = "addCustomerNameLabel",
            AutoSize = true,
            Text = "Customer Name",
            Location = new Point(100, 40)
        };

        updateCustomerNameTextBox = new()
        {
            Name = "addCustomerNameTextBox",
            Location = new Point(100, 60)
        };

        #endregion

        #region Address UI

        updateAddress1Label = new()
        {
            Name = "addAddress1Label",
            AutoSize = true,
            Text = "Address",
            Location = new Point(100, 100)
        };
        updateAddress1TextBox = new()
        {
            Name = "addAddress1TextBox",
            Location = new Point(100, 120)
        };

        updateAddress2Label = new()
        {
            Name = "addAddress2Label",
            AutoSize = true,
            Text = "Secondary Address",
            Location = new Point(100, 160)
        };
        updateAddress2TextBox = new()
        {
            Name = "addAddress2TextBox",
            Location = new Point(100, 180)
        };

        updateAddressPostalCodeLabel = new()
        {
            Name = "addAddressPostalCodeLabel",
            AutoSize = true,
            Text = "PostalCode",
            Location = new Point(100, 220)
        };
        updateAddressPostalCodeTextBox = new()
        {
            Name = "addAddressPostalCodeTextBox",
            Location = new Point(100, 240)
        };

        updateAddressPhoneLabel = new()
        {
            Name = "addCustomerPhoneLabel",
            AutoSize = true,
            Text = "Customer Phone Number",
            Location = new Point(100, 280)
        };
        updateAddressPhoneTextBox = new()
        {
            Name = "addCustomerPhoneTextBox",
            Location = new Point(100, 300)
        };

        #endregion

        #region City UI

        updateCityNameLabel = new()
        {
            Name = "addCityNameLabel",
            AutoSize = true,
            Text = "City",
            Location = new Point(300, 40)
        };

        updateCityNameTextBox = new()
        {
            Name = "addCityNameTextBox",
            Location = new Point(300, 60)
        };

        #endregion

        #region Country UI

        updateCountryNameLabel = new()
        {
            Name = "addCountryNameLabel",
            AutoSize = true,
            Text = "Country",
            Location = new Point(300, 100)
        };
        updateCountryNameTextBox = new()
        {
            Name = "addCountryNameTextBox",
            Location = new Point(300, 120)
        };

        #endregion

        updateCustomerAddButton = new()
        {
            Name = "addCustomerAddButton",
            Text = "Add Customer",
            Location = new Point(600, 80)
        };
        updateCustomerAddButton.Click += UpdateCustomerAddButton_Click;

        updateCustomerQuitButton = new()
        {
            Name = "addCustomerQuitButton",
            Text = "Quit",
            Location = new Point(600, 240)
        };
        updateCustomerQuitButton.Click += UpdateCustomerQuitButton_Click;


        Controls.Add(updateCustomerLabel);

        Controls.Add(updateCustomerNameLabel);
        Controls.Add(updateCustomerNameTextBox);

        Controls.Add(updateAddress1Label);
        Controls.Add(updateAddress1TextBox);
        Controls.Add(updateAddress2Label);
        Controls.Add(updateAddress2TextBox);
        Controls.Add(updateAddressPostalCodeLabel);
        Controls.Add(updateAddressPostalCodeTextBox);
        Controls.Add(updateAddressPhoneLabel);
        Controls.Add(updateAddressPhoneTextBox);

        Controls.Add(updateCityNameLabel);
        Controls.Add(updateCityNameTextBox);

        Controls.Add(updateCountryNameLabel);
        Controls.Add(updateCountryNameTextBox);

        Controls.Add(updateCustomerAddButton);
        Controls.Add(updateCustomerQuitButton);
    }

    Label updateCustomerLabel;

    #region Customer Info

    Label updateCustomerNameLabel;
    TextBox updateCustomerNameTextBox;

    #endregion

    #region Address Info

    Label updateAddress1Label;
    TextBox updateAddress1TextBox;
    Label updateAddress2Label;
    TextBox updateAddress2TextBox;
    Label updateAddressPostalCodeLabel;
    TextBox updateAddressPostalCodeTextBox;
    Label updateAddressPhoneLabel;
    TextBox updateAddressPhoneTextBox;

    #endregion

    #region City Info

    Label updateCityNameLabel;
    TextBox updateCityNameTextBox;

    #endregion

    #region Country Info

    Label updateCountryNameLabel;
    TextBox updateCountryNameTextBox;

    #endregion

    Button updateCustomerAddButton;
    Button updateCustomerQuitButton;
}