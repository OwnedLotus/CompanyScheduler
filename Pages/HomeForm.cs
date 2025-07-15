using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Models.Errors;
using CompanyScheduler.Pages.Calendar;
using CompanyScheduler.Pages.Customers;
using CompanyScheduler.Pages.Reports;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages;

/// <summary>
/// Form handling main activities
/// </summary>
public partial class HomeForm : Form
{
    private readonly DateTime currentTime = DateTime.UtcNow;
    private readonly GregorianCalendar calendar = new();
    private BindingList<Appointment> appointments = new();
    private BindingList<Customer> customers = new();
    private Appointment? _selectedAppointment;
    private Customer? _selectedCustomer;

    private User? _user;

    public HomeForm(User user)
    {
        InitializeComponent();
        LoadData();
        CheckSoonAppointments(15);
        _user = user;

        customerDataGrid.DataSource = customers;
        appointmentDataGrid.DataSource = appointments;
    }

    private void LoadData()
    {
        using var context = new ClientScheduleContext();

        foreach (var customer in context.Customers)
        {
            customers.Add(customer);
        }

        foreach (var appointment in context.Appointments)
        {
            appointments.Add(appointment);
        }
    }

    private void CheckSoonAppointments(int span)
    {
        using var context = new ClientScheduleContext();

        foreach (var appointment in context.Appointments)
        {
            var minutesLeft = (int)(appointment.Start - currentTime).TotalMinutes;

            if (minutesLeft <= span && minutesLeft >= 0)
            {
                string message = $"Appointment {appointment.Title} starts in {(int)minutesLeft}";
                string caption = $"Appointment Soon!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }
    }

    private void CustomerDataGrid_Changed(object sender, EventArgs e)
    {
        Int32 selectedCellCount = customerDataGrid.GetCellCount(DataGridViewElementStates.Selected);

        if (selectedCellCount > 0)
        {
            if (customerDataGrid.AreAllCellsSelected(true))
            {
                MessageBox.Show("Too many cells selected!", "Selected Cells");
            }
            else
            {
                var selectedRow = customerDataGrid.SelectedRows;

                if (selectedRow.Count > 0)
                {
                    _selectedCustomer = selectedRow[0].DataBoundItem as Customer;
                }
            }
        }
    }

    private void AppointmentDataGrid_IndexChanged(object sender, EventArgs e)
    {
        Int32 selectedCellCount = appointmentDataGrid.GetCellCount(
            DataGridViewElementStates.Selected
        );

        if (selectedCellCount > 0)
        {
            if (appointmentDataGrid.AreAllCellsSelected(true))
            {
                MessageBox.Show("Too many cells selected!", "Selected Cells");
            }
            else
            {
                _selectedAppointment = appointmentDataGrid.SelectedCells[0].Value as Appointment;
            }
        }
    }

    private void CreateCustomerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_user is null)
                return;
            var CreateCustomer = new CustomerCreateForm(_user, this);

            CreateCustomer.CustomerCreated += (sender, customer) =>
            {
                customers.Add(customer);
            };

            CreateCustomer.Show();
            Hide();
        }
        catch (FailedCustomerCreateException) { }
    }

    private void UpdateCustomerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_selectedCustomer is null || _user is null)
                return;

            var UpdateCustomer = new CustomerUpdateForm(_user, _selectedCustomer, this);
            UpdateCustomer.CustomerUpdated += (sender, customer) =>
            {
                customers.Add(customer);
            };

            UpdateCustomer.Show();
            Hide();
        }
        catch (FailedCustomerUpdatedException) { }
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            using var context = new ClientScheduleContext();
            if (_selectedCustomer is null || !context.Customers.Contains(_selectedCustomer))
            {
                string message = "Failed to Delete Customer";
                string caption = "Missing Customer";
                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, messageBoxButtons);
                return;
            }

            context.Customers.Remove(_selectedCustomer);
            customers.Remove(_selectedCustomer);
            context.SaveChanges();
        }
        catch (FailedCustomerDeleteException) { }
    }

    private void AppointmentsButton_Clicked(object sender, EventArgs e)
    {
        if (_user is null)
            return;
        var calForm = new CalendarForm(this, calendar, _user);
        calForm.Show();

        calForm.ScheduleUpdated += (sender, cal) =>
        {
            LoadData();
        };

        Hide();
    }

    private void GenReportOneLabel_Click(object sender, EventArgs e) =>
        new ReportOneForm(
            this,
            "Generate Appointments For this Month",
            [.. GenerateAppointmentTypesByMonth()]
        ).Show();

    // Report generating functions
    private IQueryable<Tuple<string,int>>? GenerateAppointmentTypesByMonth() =>
         new ClientScheduleContext().Appointments.GroupBy(appointment => appointment.Start.Month)
        .Select(appointment => new Tuple<string,int>(
            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(appointment.Key), 
            appointment.Count()));

    private void GenReportTwoLabel_Click(object sender, EventArgs e) =>
        new ReportTwoForm(
            this,
            "Schedule By Customer",
            [.. GenerateSchedule()?.SelectMany(list => list)]
        ).Show();

    private IQueryable<List<string>>? GenerateSchedule() =>
        new ClientScheduleContext()
            .Appointments.GroupBy(appointment => appointment.User)
            .Select(appointments =>
                appointments
                    .Select(x => $"Customer: {x.Customer.CustomerName} has appointment {x.Title}")
                    .ToList()
            );

    private void GenReportThreeLabel_Click(object sender, EventArgs e) =>
        new ReportThreeForm(
            this,
            "All Customers With Appointments",
            [.. GenerateAllCustomerWithAppointments()]
        ).Show();

    private IQueryable<Customer>? GenerateAllCustomerWithAppointments() =>
        new ClientScheduleContext()
            .Customers.Where(customer => customer.Appointments.Count() != 0)
            .Select(customer => customer);

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
