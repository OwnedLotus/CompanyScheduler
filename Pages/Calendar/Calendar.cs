using System.Globalization;

namespace CompanyScheduler.Pages.Calendar;

public partial class CalendarForm : Form
{
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Now);
    readonly GregorianCalendar calendar = new();
    private DayCalendar[,] days = new DayCalendar[5,7];
    private Form _homeForm;


    public CalendarForm(Form homeForm)
    {
        _homeForm = homeForm;

        InitializeComponent();

        today = today.AddMonths(-1);

        monthLabel.Text = DateTime.Now.ToString("MMMM");

        LoadCalendar();
        DrawDays();
    }

    private void LoadCalendar()
    {
        uint counter = 1;
        for (int i = 0; i < days.GetLength(0); i++)
        {
            for (int j = 0; j < days.GetLength(1); j++)
            {
                days[i, j] = new(new Point((j+1) * 50, (i+1) * 50), new Size(40, 40), $"{counter}");
                counter++;
            }
        }
    }

    private void DrawDays()
    {
        foreach (DayCalendar day in days)
        {
            day.DrawDay(Controls);
        }
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AddAppointmentButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void updateAppointmentButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}

public class DayCalendar
{
    public string Name { get; set; }
    public string DateNum { get; set; }
    public Point Pos { get; set; }
    public Panel Panel { get; set; }
    public Label Date { get; set; }
    public bool Selected { get; set; }

    public DayCalendar(Point position,Size size, string dateNum)
    {
        Name = dateNum + "date";
        DateNum = dateNum;

        Pos = position;

        Panel = new()
        {
            Size = size,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White,
            Location = position
        };
        Panel.Click += PanelDay_Clicked;
        Date = new()
        {
            Location = position,
            Text = dateNum,
            AutoSize = true
        };
    }

    private void PanelDay_Clicked(object? sender, EventArgs e)
    {
        Selected = true;
        Panel.BackColor = Color.Blue;
    }

    internal void DrawDay(Control.ControlCollection controls)
    {
        controls.Add(Date);
        controls.Add(Panel);
    }
}
