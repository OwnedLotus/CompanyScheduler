using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyScheduler.Models;

namespace CompanyScheduler;

/// <summary>
/// View Calendar Events (Appointments) by selecting a Day of the month
///
/// Provide the ability to automatically adjust appointment times based on user time zones and dst
///
/// Create a function that allows users to generate thr three reports using collection classes:
///     The number of appointment types by month
///     The schedule for each user
///     one additional report
/// </summary>
public partial class CalendarWindow : Window
{
    public CalendarWindow() { }
}
