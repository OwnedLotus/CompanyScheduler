using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Calendar.Appointments;

namespace CompanyScheduler.Pages.Calendar;

public partial class CalendarForm : Form
{
    public EventHandler? ScheduleUpdated;
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Now);

    DateOnly? selectedDate;

    readonly GregorianCalendar calendar;

    private Form _homeForm;

    BindingList<Customer> _customers = new();
    BindingList<Appointment> _appointments = new();

    private Customer? selectedCustomer;
    private Appointment? selectedAppointment;

    User _currentUser;
    Customer _selectedCustomer = new();
    Appointment _selectedAppointment = new();

    // todo enable selection of customer

    public CalendarForm(Form homeForm, GregorianCalendar cal, User user)
    {
        InitializeComponent();
        LoadData();

        _homeForm = homeForm;
        customerDataGrid.DataSource = _customers;
        appointmentDataGrid.DataSource = _appointments;
        calendar = cal;
        _currentUser = user;
    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
        selectedDate = DateOnly.FromDateTime(datePicker.Value.ToUniversalTime());

        if (selectedDate is not null)
            using (var context = new ClientScheduleContext())
            {
                _appointments =
                [
                    .. context.Appointments.Where(apt =>
                        DateOnly.FromDateTime(apt.Start.ToUniversalTime()) == selectedDate
                    ),
                ];
            }

        appointmentDataGrid.DataSource = _appointments;
    }

    private void LoadData()
    {
        using var context = new ClientScheduleContext();

        foreach (var customer in context.Customers)
        {
            _customers.Add(customer);
        }

        foreach (var appointment in context.Appointments)
        {
            _appointments.Add(appointment);
        }
    }

    private void AppointmentDataGrid_SelectionChanged(object sender, EventArgs e)
    {
        Int32 selectedCellCount = appointmentDataGrid.GetCellCount(DataGridViewElementStates.Selected);

        if(selectedCellCount > 0)
        {
            if (appointmentDataGrid.AreAllCellsSelected(true))
            {
                MessageBox.Show("Too many cells selected!", "Selected Cells", MessageBoxButtons.OK);
            }
            else
            {
                var selectedRow = appointmentDataGrid.SelectedRows;

                if (selectedRow.Count > 0)
                {
                    _selectedAppointment = (selectedRow[0].DataBoundItem as Appointment)!;
                }
            }
        }
    }

    private void CustomerDataGrid_SelectionChanged(object sender, EventArgs e)
    {
        if(customerDataGrid.CurrentRow is not null)
        {
            _selectedCustomer = (customerDataGrid.CurrentRow.DataBoundItem as Customer)!;
        }
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        ScheduleUpdated?.Invoke(this, EventArgs.Empty);
        _homeForm.Show();
        Close();
    }

    private void AddAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var addAppointmentForm = new AppointmentCreateForm(this, _currentUser, _selectedCustomer);

        addAppointmentForm.AppointmentCreated += (sender, appointment) =>
            _appointments?.Add(appointment);

        addAppointmentForm.Show();
        Hide();
    }

    private void UpdateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedAppointment is not null && _selectedCustomer is not null)
        {
            var updateAppointment = new AppointmentUpdateForm(
                this,
                _selectedAppointment,
                _currentUser
            );
            updateAppointment.AppointmentUpdated += (sender, appointments) =>
            {
                var (old, app) = appointments;

                _appointments?.Remove(old);
                _appointments?.Add(app);
            };
            updateAppointment.Show();
            Hide();
        }
    }

    private void DeleteAppointmentButton_Clicked(object sender, EventArgs e)
    {
        using var context = new ClientScheduleContext();

        if (_selectedAppointment is not null && context.Appointments.Contains(_selectedAppointment))
        {
            context.Appointments.Remove(_selectedAppointment);
            _appointments.Remove(_selectedAppointment);
            context.SaveChanges();
        }
        else
        {
            string message = "Failed to Find Appointment";
            string caption = "Appointment Not Found";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);
        }
    }
}
