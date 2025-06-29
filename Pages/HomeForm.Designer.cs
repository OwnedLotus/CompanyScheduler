namespace CompanyScheduler.Pages;
partial class HomeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "HomePage";

        customerGridLabel = new()
        {
            AutoSize = true,
            Name = "customerGridLabel",
            Location = new Point(50, 10),
            Text = "Available Customers"
        };
        customerGrid = new()
        {
            Name = "customerGrid",
            Location = new Point(50, 30),
        };

        createCustomerButton = new()
        {
            Name = "createCustomerButton",
            Location = new Point(50, 200),
            Text = "Create Customer",
            AutoSize = true
        };
        createCustomerButton.Click += CreateCustomerButton_Clicked;
        updateCustomerButton = new()
        {
            Name = "updateCustomerButton",
            Location = new Point(50, 230),
            Text = "Update Customer",
            AutoSize = true
        };
        updateCustomerButton.Click += UpdateCustomerButton_Clicked;
        deleteCustomerButton = new()
        {
            Name = "deleteCustomerButton",
            Location = new Point(50, 260),
            Text = "Delete Customer",
            AutoSize = true
        };
        deleteCustomerButton.Click += DeleteCustomerButton_Clicked;

        appointmentListViewLabel = new()
        {
            AutoSize = true,
            Name = "appointmentListViewLabel",
            Location = new Point(400, 10),
            Text = "Customer Appointments"
        };
        appointmentListView = new()
        {
            Name = "appointmentListView",
            Location = new Point(400, 30)
        };
        appointmentListView.SelectedIndexChanged += AppointmentListView_IndexChanged;

        appointmentsButton = new()
        {
            Name = "appointmentsButton",
            Location = new Point(400, 150),
            AutoSize = true,
            Text = "Appointments"
        };
        appointmentsButton.Click += AppointmentsButton_Clicked;

        quitButton = new()
        {
            Name = "quitButton",
            Location = new Point(600, 400),
            Text = "Quit"
        };
        quitButton.Click += QuitButton_Clicked;

        Controls.Add(customerGridLabel);
        Controls.Add(customerGrid);
        Controls.Add(createCustomerButton);
        Controls.Add(updateCustomerButton);
        Controls.Add(deleteCustomerButton);

        Controls.Add(appointmentListViewLabel);
        Controls.Add(appointmentListView);

        Controls.Add(appointmentsButton);

        Controls.Add(quitButton);
    }

    #endregion

    Label customerGridLabel;
    DataGridView customerGrid;
    Button createCustomerButton;
    Button updateCustomerButton;
    Button deleteCustomerButton;

    Label appointmentListViewLabel;
    ListView appointmentListView;

    Button appointmentsButton;

    Button quitButton;
}