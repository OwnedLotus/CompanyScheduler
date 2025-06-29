using CompanyScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyScheduler.Pages.Calendar.Appointments.Show;

public partial class ShowSingleForm : Form
{
    private Form prevForm;
    private Appointment _appointment;

    public ShowSingleForm(Form form, Appointment appointment)
    {
        InitializeComponent();

        prevForm = form;
        _appointment = appointment;

        singleGridView.SelectedObject = appointment;
    }


    private void OkButton_Click(object sender, EventArgs e)
    {
        prevForm.Show();
        Close();
    }
}
