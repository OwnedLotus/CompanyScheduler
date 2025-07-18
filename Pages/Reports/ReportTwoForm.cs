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
        userGridView.DataSource = users;
    }

    private void userGridView_SelectionChanged(object sender, EventArgs e)
    {
        Int32 selectedCellCount = userGridView.GetCellCount(DataGridViewElementStates.Selected);

        if (selectedCellCount > 0)
        {
                selectedUser = userGridView.SelectedCells[0].Value as User;
                userAppointmentsGridView.DataSource = selectedUser?.Appointments;
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        prevForm.Show();
        Close();
    }
}
