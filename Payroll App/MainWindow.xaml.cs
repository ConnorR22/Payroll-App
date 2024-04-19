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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Payroll_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Tuple<DateTime, float>> hoursLogged = new List<Tuple<DateTime,float>>();
        public float totalHours = 0f;

        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = dateSelected.SelectedDates[0];
            float toSubmit =  float.Parse(hours.Text);
            totalHours += toSubmit;
            Tuple<DateTime, float> toAdd = new Tuple<DateTime, float>(date, toSubmit);
            if (hoursLogged.Any(d => d.Item1.Month == date.Month && d.Item1.Day == date.Day))
            {
                Tuple<DateTime, float>? tuple = hoursLogged.Find(d => d.Item1.Month == date.Month && d.Item1.Day == date.Day);
                Tuple<DateTime, float>? toRemove = tuple;
                hoursLogged.Remove(tuple);
                totalHours -= tuple.Item2;
            }    
            hoursLogged.Add(toAdd);
            hoursLogged.Sort();

            submittedHours.Text = string.Empty;

            foreach (Tuple<DateTime, float> day in hoursLogged)
            {
                string[] str = day.ToString().Split(" ");
                
                submittedHours.Text += str[0].Split("(")[1] + " - " + day.Item2 + "h\n";
            }

            submittedHours.Text += "\nTotal Hours: " + totalHours;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(submittedHours.Text);
        }
    }
}