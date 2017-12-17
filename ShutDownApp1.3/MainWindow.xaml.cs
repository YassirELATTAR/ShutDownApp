using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Timers;

namespace ShutDownApp1._1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class ExceptionOfWrongNumber : Exception
        {
            public ExceptionOfWrongNumber(string msg)
                : base(msg)
            {
            }
        }

        DispatcherTimer timer = new DispatcherTimer();

        public static int timecount = 0, hour, minute, second;
        public static bool status = true;

        public MainWindow()
        {

            InitializeComponent();
            okay.IsEnabled = false;
            reset.Focus();
            TextBox_Hour.IsEnabled = TextBox_Minute.IsEnabled = TextBox_Second.IsEnabled = false;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            if (timecount > 0)
            {
                timecount = timecount - 1;
                hour = timecount / 3600;
                minute = (timecount - (hour * 3600)) / 60;
                second = timecount - (hour * 3600) - (minute * 60);
                if (hour < 10)
                    TextBox_Hour.Text = string.Format("0{0}", hour);
                else
                    TextBox_Hour.Text = string.Format("{0}", hour);
                if (minute < 10)
                    TextBox_Minute.Text = string.Format("0{0}", minute);
                else
                    TextBox_Minute.Text = string.Format("{0}", minute);

                if (second < 10)
                    TextBox_Second.Text = string.Format("0{0}", second);
                else
                    TextBox_Second.Text = string.Format("{0}", second);

            }
            else
            {

                timer.Stop();
                this.Close();
                Process.Start("shutdown", "/s /t 0");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            okay.IsEnabled = true;
            TextBox_Hour.IsEnabled = TextBox_Minute.IsEnabled = TextBox_Second.IsEnabled = true;
            reset.Content = "RE-SET";
            reset.IsEnabled = false;
            TextBox_Hour.Focus();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                hour = Int32.Parse(TextBox_Hour.Text);
                minute = Int32.Parse(TextBox_Minute.Text);
                second = Int32.Parse(TextBox_Second.Text);
                if (hour < 0 || minute > 59 || minute < 0 || second < 0 || second > 59)
                {
                    throw new ExceptionOfWrongNumber("There is a wrong value");
                }
                else
                {

                    timecount = hour * 3600 + minute * 60 + second;
                    timer.Start();

                }

                TextBox_Hour.IsEnabled = TextBox_Minute.IsEnabled = TextBox_Second.IsEnabled = false;
                okay.IsEnabled = false;
                reset.IsEnabled = true;
                sometitle.Content = "Time Left to ShutDown:";
                reset.Focus();

            }
            catch
            {
                MessageBox.Show("There is a fucking error in the number you entered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
