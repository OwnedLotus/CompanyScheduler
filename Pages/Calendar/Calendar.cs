using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Data;
using CompanyScheduler.OldModels;
using CompanyScheduler.Pages.Calendar.Appointments;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages.Calendar;

public partial class CalendarForm : Form
{
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Now);

    DateOnly? selectedDate;

    readonly GregorianCalendar calendar;

    private Form _homeForm;

    BindingList<Customer> _Customers;
    BindingList<Appointment> _appointments;

    User _currentUser;

    public CalendarForm(
        Form homeForm,
        GregorianCalendar cal,
        Customer[] customers,
        Appointment[] appointments,
        User user
    )
    {
        InitializeComponent();
        LoadCustomers();

        _homeForm = homeForm;
        _Customers = [.. customers];
        _appointments = [.. appointments];
        customerListBox.DataSource = _Customers;
        calendar = cal;
        _currentUser = user;
    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
        selectedDate = DateOnly.FromDateTime(datePicker.Value.ToUniversalTime());

        if (selectedDate is not null)
            using (var context = new CompanyContext())
            {
                _appointments =
                [
                    .. context.Appointments.Where(apt =>
                        DateOnly.FromDateTime(apt.Start.UtcDateTime) == selectedDate
                    )
                ];
            }

        appointmentListBox.DataSource = _appointments;
    }

    private void LoadCustomers()
    {
        //using (var context = new CompanyContext())
        //{
        //    Customers = [..context.Customers];
        //}
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        _homeForm.Show();
        Close();
    }

    private void AddAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var addAppointmentForm = new AppointmentCreateForm(
            this,
            new User(),
            new Customer(),
            [.. _appointments]
        );

        addAppointmentForm.Show();
        Hide();
    }

    private void updateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        var selectedAppointment = (Appointment?)appointmentListBox.SelectedValue;

        if (selectedAppointment is Appointment)
        {
            var updateAppointment = new AppointmentUpdateForm(
                this,
                selectedAppointment,
                [.. _appointments],
                _currentUser
            );
            updateAppointment.Show();
            Hide();
        }
    }
}
