using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyScheduler.Pages.Reports;

public partial class ReportOneForm : Form
{
    private readonly Form prevForm;

    public ReportOneForm(Form form, string reportName, Tuple<string, Tuple<string, int>[]>[]? data)
    {
        InitializeComponent();

        prevForm = form;

        reportsLabel.Text = reportName;
        reportGridView.Columns.Clear();
        reportGridView.Columns.Add("Month", 50);
        reportGridView.Columns.Add("Types", 150);

        foreach (var outer in data)
        {
            string month = outer.Item1;
            var innerCollection = outer.Item2;

            var isFirst = true;
            foreach (var inner in innerCollection)
            {
                var pairs = $"{inner.Item1}: {inner.Item2}";
                ListViewItem item;

                if (isFirst)
                {
                    item = new ListViewItem(month);
                    isFirst = false;
                }
                else
                {
                    item = new ListViewItem("");
                }

                item.SubItems.Add(pairs);
                reportGridView.Items.Add(item);
            }
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        prevForm.Show();
        Close();
    }
}
