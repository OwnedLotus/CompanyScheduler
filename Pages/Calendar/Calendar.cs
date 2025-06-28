using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Calendar.Appointments;

namespace CompanyScheduler.Pages.Calendar;

public partial class CalendarForm : Form
{
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Now);

    DateOnly? selectedDate;

    readonly GregorianCalendar calendar;

    private Form _homeForm;

    BindingList<Customer>? _Customers;
    BindingList<Appointment>? _appointments;

    User _currentUser;
    Customer _selectedCustomer = new();
    // todo enable selection of customer

    public CalendarForm(Form homeForm, GregorianCalendar cal, User user)
    {
        InitializeComponent();
        LoadData();

        _homeForm = homeForm;
        customerListBox.DataSource = _Customers;
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
                    )
                ];
            }

        appointmentListBox.DataSource = _appointments;
    }

    private void LoadData()
    {
        using var context = new ClientScheduleContext();
        _Customers = [.. context.Customers];
        _appointments = [.. context.Appointments];
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        _homeForm.Show();
        Close();
    }

    private void AddAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var addAppointmentForm = new AppointmentCreateForm(this, _currentUser, _selectedCustomer);

        addAppointmentForm.Show();
        Hide();
    }

    private void UpdateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var selectedAppointment = (Appointment?)appointmentListBox.SelectedValue;

        if (selectedAppointment is not null)
        {
            var updateAppointment = new AppointmentUpdateForm(
                this,
                selectedAppointment,
                _currentUser
            );
            updateAppointment.Show();
            Hide();
        }
    }

    private void DeleteAppointmentButton_Clicked(object sender, EventArgs e)
    {
        using var context = new ClientScheduleContext();

        var selectedAppointment = (Appointment?)appointmentListBox.SelectedValue;

        if (selectedAppointment is not null && context.Appointments.Contains(selectedAppointment))
        {
            context.Appointments.Remove(selectedAppointment);
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
