using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Calendar;
using CompanyScheduler.Pages.Customers;
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

    public User User { get; private set; }

    public HomeForm(User user)
    {
        InitializeComponent();
        LoadData();
        CheckSoonAppointments(15);
        User = user;

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
            var minutesLeft = Math.Abs((appointment.Start - currentTime).TotalMinutes);

            if (minutesLeft <= span)
            {
                string message = $"Appointment {appointment.Title} starts in {minutesLeft}";
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

    private void CalendarButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is null)
            return;

        var CalendarForm = new CalendarForm(this, calendar, User);

        CalendarForm.Show();
        Hide();
    }

    private void CreateCustomerButton_Clicked(object sender, EventArgs e)
    {
        var CreateCustomer = new CustomerCreateForm(User, this);

        CreateCustomer.CustomerCreated += (sender, customer) =>
        {
            customers.Add(customer);
        };

        CreateCustomer.Show();
        Hide();
    }

    private void UpdateCustomerButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedCustomer is null)
            return;

        var UpdateCustomer = new CustomerUpdateForm(User, _selectedCustomer, this);
        UpdateCustomer.CustomerUpdated += (sender, customer) =>
        {
            customers.Add(customer);
        };

        UpdateCustomer.Show();
        Hide();
    }

    private void DeleteCustomerButton_Clicked(object sender, EventArgs e)
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

    private void AppointmentsButton_Clicked(object sender, EventArgs e)
    {
        var calForm = new CalendarForm(this, calendar, User);
        calForm.Show();

        calForm.ScheduleUpdated += (sender, cal) =>
        {
            LoadData();
        };

        Hide();
    }

    // Report generating functions
    private IQueryable<string>? GenerateAppointmentTypesByMonth(DateOnly date) =>
        new ClientScheduleContext()
            .Appointments.Where(appointment => appointment.Start.Month == date.Month)
            .Select(appointment => appointment.Type)
            .Distinct();

    private IQueryable<List<Appointment>>? GenerateSchedule() =>
        new ClientScheduleContext()
            .Appointments.GroupBy(appointment => appointment.User)
            .Select(appointments => appointments.ToList());

    private IQueryable<bool>? GenerateAllCustomerWithAppointments() =>
        new ClientScheduleContext()
            .Customers.Select(customer => customer.Appointments.Count != 0)
            .Distinct();

    private void QuitButton_Clicked(object sender, EventArgs e) => Environment.Exit(0);
}
