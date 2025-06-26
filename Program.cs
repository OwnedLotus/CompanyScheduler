using CompanyScheduler.Pages;
using CompanyScheduler.Pages.Calendar;

namespace CompanyScheduler;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new CalendarForm(new HomeForm(new Models.User())));
    }
}
