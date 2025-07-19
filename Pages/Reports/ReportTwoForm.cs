using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyScheduler.Models;

namespace CompanyScheduler.Pages.Reports;

public partial class ReportTwoForm : Form
{
    private Form prevForm;
    private User? selectedUser;

    public ReportTwoForm(Form form, string reportName, Tuple<string, Appointment[]>[] users)
    {
        InitializeComponent();

        prevForm = form;

        reportsLabel.Text = reportName;
        userAppointmentsListView.Columns.Clear();
        userAppointmentsListView.Columns.Add("User");
        userAppointmentsListView.Columns.Add("Appointments",500);

        foreach (var outer in users)
        {
            string userName = outer.Item1;
            var innerCollection = outer.Item2;

            var isFirst = true;
            foreach (var appointment in innerCollection)
            {
                var value = $"Appointment ID: {appointment.AppointmentId}," +
                    $" Appointment Title: {appointment.Title}," +
                    $"Appointment Time: {appointment.Start}";
                ListViewItem item;

                if (isFirst)
                {
                    item = new ListViewItem(userName);
                    isFirst = false;
                }
                else
                {
                    item = new ListViewItem("");
                }

                item.SubItems.Add(value);
                userAppointmentsListView.Items.Add(item);
            }
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        prevForm.Show();
        Close();
    }
}
