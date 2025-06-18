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

namespace CompanyScheduler.Pages;

/// <summary>
/// CRUD User Records
///  Validate:
///     Customer record includes name, address, and phone fields
///     Fields are trimmed and nonempty
///     Phone number filed allows dashes and digits
///
/// Exception Handling:
///     Add op
///     Update op
///     Delete DB op
///
/// Provide Ability to CRUD Appointments:
/// Validate:
///     Appointments 9:00am -> 5:00pm, Mon-Fri,EST
///     Prevent Schedule overlap
///
/// Exception Handling:
///     Add op
///     Update op
///     Delete DB op
/// </summary>
public partial class HomeWindow : Window
{
    public HomeWindow() { }
}
