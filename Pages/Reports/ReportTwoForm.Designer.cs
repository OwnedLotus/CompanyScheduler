using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyScheduler.Pages.Reports;

partial class ReportTwoForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Reports";

        reportsLabel = new()
        {
            Name = "reportsLabel",
            AutoSize = true,
            Location = new Point(10, 10)
        };

        userAppointmentsListView = new()
        {
            Name = "userAppointmentsGridView",
            AutoSize = true,
            Location = new Point(100, 30),
            Size = new Size(600, 300),
            GridLines = true,
            View = View.Details
        };

        okButton = new()
        {
            Name = "okButton",
            AutoSize = true,
            Location = new Point(420,400),
            Text = "OK"
        };
        okButton.Click += OkButton_Click;

        Controls.Add(reportsLabel);
        Controls.Add(userAppointmentsListView);
        Controls.Add(okButton);

    }


    Label reportsLabel;
    ListView userAppointmentsListView;
    Button okButton;
}