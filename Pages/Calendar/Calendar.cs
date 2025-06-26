using System.ComponentModel;
using System.Globalization;
using CompanyScheduler.Data;
using CompanyScheduler.Models;
using CompanyScheduler.Pages.Calendar.Appointments;
using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Pages.Calendar;

public partial class CalendarForm : Form
{
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Now);

    DateTimeOffset? selectedDate;

    readonly GregorianCalendar calendar = new();

    private Form _homeForm;

    BindingList<Customer> _Customers = new();
    BindingList<Appointment> appointments = new();

    public CalendarForm(Form homeForm)
    {
        InitializeComponent();
        LoadCustomers();

        _homeForm = homeForm;
        customerListBox.DataSource = _Customers;
    }

    private void DatePicker_ValueChanged(object sender, EventArgs e)
    {
        selectedDate = datePicker.Value.ToUniversalTime();

        // if (selectedDate is not null)
        //     using (var context = new CompanyContext())
        //     {
        //         appointments = [.. context.Appointments.Where(apt => apt.Start == selectedDate)];
        //     }

        appointmentListBox.DataSource = appointments;
    }

    // private void TimePicker_ValueChanged(object sender, EventArgs e)
    // {
    //    throw new NotImplementedException();
    // }

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
        var addAppointmentForm = new AppointmentCreate(this, new User(), new Customer());
        addAppointmentForm.Show();
        Hide();
    }

    private void updateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
