/*Jordan Ross
 * May 24, 2019
 * Quadratics Calculator
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace u5Quadratic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double descriminant = 0;
        double XPrevious = 0;
        double YPrevious = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            int Xindex = txtStatement.Text.IndexOf("x");
            string Input = txtStatement.Text;
            double Avalue = 0;
            string FirstValue = Input.Substring(0, Xindex);
            int previousPosiOfX = Xindex;
            Xindex = txtStatement.Text.IndexOf("x", Xindex + 1);
            double Bvalue = 0;
            string SecondValue = Input.Substring(previousPosiOfX + 5, Xindex - previousPosiOfX - 5);
            int indexOfEqual = txtStatement.Text.IndexOf("=");
            double Cvalue = 0;
            string ThirdValue = Input.Substring(Xindex + 4, indexOfEqual - Xindex - 5);
            double.TryParse(FirstValue, out Avalue);
            double.TryParse(SecondValue, out Bvalue);
            double.TryParse(ThirdValue, out Cvalue);

            if (Input.Substring(0, 1) == "-")
                {
                    Avalue = -Avalue;
                }
            if (Input.Substring(previousPosiOfX + 3, Xindex - previousPosiOfX - 5) == "-")
                {
                    Bvalue = -Bvalue;
                }
            if (Input.Substring(Xindex + 2, indexOfEqual - Xindex - 6) == "-")
                {
                    Cvalue = -1 * Cvalue;
                }

            descriminant = Math.Sqrt((Bvalue * Bvalue) - (4 * Avalue * Cvalue));
            double Root1 = (-Bvalue + descriminant) / (2 * Avalue);
            lblOutput.Content = "First Root " + Root1.ToString() + "        ";
            double Root2 = (-Bvalue - descriminant) / (2 * Avalue);
            lblOutput.Content += "Second Root " + Root2.ToString();
            txtStatement.Visibility = Visibility.Hidden;
            Calculate.Visibility = Visibility.Hidden;
            lbltitle.Visibility = Visibility.Hidden;
            lblLegend.Visibility = Visibility.Visible;

            for (int i = 0; i <= 40; i++)
                {
                    myCanvas.Children.Add(CreateLine(50, 50 + i * 10, 450, 50 + i * 10, 1));
                    myCanvas.Children.Add(CreateLine(50 + i * 10, 50, 50 + i * 10, 450, 1));
                }

            Ellipse Root1dot = new Ellipse();//points of roots
            Canvas.SetTop(Root1dot, 246);
            Root1dot.Width = 8;
            Root1dot.Height = 8;
            Canvas.SetLeft(Root1dot, 246 + 10 * Root1 / 2);
            Root1dot.Fill = Brushes.Black;
            myCanvas.Children.Add(Root1dot);
            Ellipse Root2dot = new Ellipse();
            Canvas.SetTop(Root2dot, 246);
            Root2dot.Width = 8;
            Root2dot.Height = 8;
            Canvas.SetLeft(Root2dot, 246 + 10 * Root2 / 2);
            Root2dot.Fill = Brushes.Black;
            myCanvas.Children.Add(Root2dot);
            double numberOfSteps = 0;

            if (Root1 > 0 && Root2 > 0)
                {
                    numberOfSteps = (Root2 + Root1) / 2;
                }
            else if (Root1 < 0 || Root2 < 0)
                {
                    numberOfSteps = (Root1 - Root2) / 2;

                    if (numberOfSteps < 0)
                        {
                            numberOfSteps = -numberOfSteps;
                        }
                }
            else
                {
                    numberOfSteps = (Root2 + Root2) / 2;
                    numberOfSteps = -numberOfSteps;
                }

            double FirstStep = Avalue * ((numberOfSteps * numberOfSteps) - (numberOfSteps - 1) * (numberOfSteps - 1));

            for (double i = -50; i <= 50; i++)
                {
                    myCanvas.Children.Add(CreateLine(i * 5 + 250, -((Avalue * (i * i) + Bvalue * i + Cvalue) * 5 - 250), (i + 1) * 5 + 250, -((Avalue * ((i + 1) * (i + 1)) + Bvalue * (i + 1) + Cvalue) * 5 - 250), 2));
                }

        }

        private Line CreateLine(double x1, double y1, double x2, double y2, double thickness)
        {
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.StrokeThickness = thickness;

            if (y1 == 250)
                {
                    myLine.StrokeThickness = 2;
                }
            if (x1 == 250)
                {
                    myLine.StrokeThickness = 2;
                }

            XPrevious = x2;
            YPrevious = y2;
            return myLine;
        }
    }
}
