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

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Home";

        customerGridLabel = new()
        {
            AutoSize = true,
            Name = "customerGridLabel",
            Location = new Point(50, 10),
            Text = "Available Customers"
        };
        customerDataGrid = new()
        {
            Name = "customerListBox",
            Location = new Point(50, 30),
        };
        customerDataGrid.SelectionChanged += CustomerDataGrid_Changed;

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
        appointmentDataGrid = new()
        {
            Name = "appointmentListView",
            Location = new Point(400, 30)
        };
        appointmentDataGrid.SelectionChanged += AppointmentDataGrid_IndexChanged;

        appointmentsButton = new()
        {
            Name = "appointmentsButton",
            Location = new Point(400, 200),
            AutoSize = true,
            Text = "Appointments"
        };
        appointmentsButton.Click += AppointmentsButton_Clicked;

        genReportOneLabel = new()
        {
            Name = "genReportOneLabel",
            Location = new Point(10, 360),
            AutoSize = true,
            Text = "Click to Generate Appointment Types By Month"
        };
        genReportOneLabel.Click += GenReportOneLabel_Click;

        genReportTwoLabel = new()
        {
            Name = "getReportTwoLabel",
            Location = new Point(10, 380),
            AutoSize = true,
            Text = "Click to Generate Schedule for Each User"
        };
        genReportTwoLabel.Click += GenReportTwoLabel_Click;

        genReportThreeLabel = new()
        {
            Name = "genReportThreeLabel",
            Location = new Point(10, 400),
            AutoSize = true,
            Text = "Click to Generate All Customers With Appointments"
        };
        genReportThreeLabel.Click += GenReportThreeLabel_Click;

        quitButton = new()
        {
            Name = "quitButton",
            Location = new Point(600, 400),
            Text = "Quit"
        };
        quitButton.Click += QuitButton_Clicked;

        Controls.Add(customerGridLabel);
        Controls.Add(customerDataGrid);
        Controls.Add(createCustomerButton);
        Controls.Add(updateCustomerButton);
        Controls.Add(deleteCustomerButton);

        Controls.Add(appointmentListViewLabel);
        Controls.Add(appointmentDataGrid);

        Controls.Add(appointmentsButton);


        Controls.Add(genReportOneLabel);
        Controls.Add(genReportTwoLabel);
        Controls.Add(genReportThreeLabel);

        Controls.Add(quitButton);
    }



    Label customerGridLabel;
    DataGridView customerDataGrid;
    Button createCustomerButton;
    Button updateCustomerButton;
    Button deleteCustomerButton;

    Label appointmentListViewLabel;
    DataGridView appointmentDataGrid;

    Button appointmentsButton;

    Label genReportOneLabel;
    Label genReportTwoLabel;
    Label genReportThreeLabel;

    Button quitButton;
}