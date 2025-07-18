using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Models.Errors;
using CompanyScheduler.Pages.Calendar.Appointments;
using CompanyScheduler.Pages.Calendar.Appointments.Show;
using Microsoft.Win32;

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

    private System.Windows.Forms.Timer _timer;
    private TimeZoneInfo currentTimeZone = TimeZoneInfo.Local;

    // todo enable selection of customer

    public CalendarForm(Form homeForm, GregorianCalendar cal, User user)
    {
        InitializeComponent();
        LoadData();

        _timer = new();
        _timer.Interval = 500;
        _timer.Tick += _timer_Tick;
        _timer.Start();

        _homeForm = homeForm;
        customerDataGrid.DataSource = _customers;
        appointmentDataGrid.DataSource = _appointments;
        calendar = cal;
        _currentUser = user;

        SystemEvents.UserPreferenceChanged += PreferenceChanged;
    }

    private void _timer_Tick(object? sender, EventArgs e)
    {
        TimeZoneInfo.ClearCachedData();

        if(!currentTimeZone.Equals(TimeZoneInfo.Local))
        {
            LoadData();
            currentTimeZone = TimeZoneInfo.Local;
        }
    }

    private void PreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        LoadData();
    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
        selectedDate = DateOnly.FromDateTime(datePicker.Value.ToUniversalTime());

        if (selectedDate is not null)
            using (var context = new ClientScheduleContext())
            {
                var daysAppointments = context.Appointments.Where(apt =>
                                        DateOnly.FromDateTime(apt.Start) == selectedDate.Value).ToList();

                if (daysAppointments.Count != 0)
                    _appointments =
                    [
                        .. context.Appointments.Where(apt =>
                            DateOnly.FromDateTime(apt.Start) == selectedDate.Value
                        ),
                    ];
                else
                    _appointments = [];

                _appointments.ResetBindings();
            }

        appointmentDataGrid.DataSource = _appointments;
    }

    private void ShowSingleAppButton_Click(object sender, EventArgs e)
    {
        if (_selectedAppointment is null)
            return;
        var showSingle = new ShowSingleForm(this, _selectedAppointment);
        showSingle.Show();
        Hide();
    }

    private void LoadData()
    {
        if (_customers.Count == 0 || _appointments.Count == 0)
        {
            _customers.Clear();
            _appointments.Clear();
        }

        using var context = new ClientScheduleContext();

        _customers = [.. context.Customers];
        _appointments = [.. context.Appointments];
    }

    private void AppointmentDataGrid_SelectionChanged(object sender, EventArgs e)
    {
        Int32 selectedCellCount = appointmentDataGrid.GetCellCount(
            DataGridViewElementStates.Selected
        );

        if (selectedCellCount > 0)
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
        if (customerDataGrid.CurrentRow is not null)
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
        if(_selectedCustomer is null)
        {
            MessageBox.Show("Please Select Customer", "No Customer", MessageBoxButtons.OK);
            return;
        }


        var addAppointmentForm = new AppointmentCreateForm(this, _currentUser, _selectedCustomer);

        _timer.Stop();

        addAppointmentForm.AppointmentCreated += (sender, appointment) =>
        {
            var message = "Appointment saved successfully";
            var caption = "Appointment Saved!";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
            _appointments?.Add(appointment);
            _appointments?.ResetBindings();
            _timer.Start();
        };

        addAppointmentForm.Show();
        Hide();
    }

    private void UpdateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        _timer.Stop();

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
                _appointments?.ResetBindings();
                _timer.Start();
            };
            updateAppointment.Show();
            Hide();
        }
    }

    private void DeleteAppointmentButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            using var context = new ClientScheduleContext();

            if (
                _selectedAppointment is not null
                && context.Appointments.Contains(_selectedAppointment)
            )
            {
                context.Appointments.Remove(_selectedAppointment);
                _appointments.Remove(_selectedAppointment);
                _appointments.ResetBindings();
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
        catch (FailedAppointmentDeleteException error)
        {
            var message = error.ToString();
            var caption = "Failed to Delete Appointment";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }
    }
}
