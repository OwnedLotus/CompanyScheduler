using CompanyScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyScheduler.Pages.Reports;

public partial class ReportThreeForm : Form
{
    private Form prevForm;

    public ReportThreeForm(Form form, string reportName, params Customer[] values)
    {
        InitializeComponent();

        prevForm = form;

        reportsLabel.Text = reportName;
        reportGridView.DataSource = values;
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        prevForm.Show();
        Close();
    }
}
