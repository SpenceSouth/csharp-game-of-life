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
using System.Diagnostics;

namespace WPF_Game_of_Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int marginX = 0;
        int marginY = 0;
        int shift = 0;
        Boolean stop = false;
        Boolean diagRules = true;
        NodeManager manager = new NodeManager();

        public MainWindow()
        {
            InitializeComponent();

            DebugDisplay.Text = "Conway's Game of Life\n\n" +
                                "Each cell has 8 neighbors\n" +
                                "(North, East, West, South, and Diagnol)\n\n" +
                                "The Rules: (In priority order)\n\n" +
                                "1. A live cell will die if it has more than three or less than two neighbors (north, south, east, west, and diagonally).\n" +
                                "2. A dead cell will spring to life if it has exactly three neighbors.\n" +
                                "3. A cell with two or three neighbors stays alive.\n" +
                                "4. An infected cell infects all alive neighbors.";



            //int x = (int)Plane.ActualWidth / 15;
            //int y = (int)Plane.ActualHeight / 15;
            int x = 50;
            int y = 50;
            manager.width = x;
            manager.height = y;
            Debug.WriteLine("x: " + x.ToString());
            Debug.WriteLine("y: " + y.ToString());
            Node[,] list = new Node[x, y];
            //Node[,] list = new Node[25, 25];




            //Create grid
            for (int i = 0; i < y; i++)
            {
                marginX = 0;
                for (int j = 0; j < x; j++)
                {
                    Node temp = new Node();
                    temp.x = j + 1;
                    temp.y = i + 1;
                    temp.Margin = new Thickness(marginX, marginY, 0, 0);
                    temp.Click += new RoutedEventHandler(Node_Click);
                    Plane.Children.Add(temp);
                    marginX += 10;
                    list[j, i] = temp;
                    //Debug.WriteLine("Create node[" + i + "," + j + "]");
                }
                marginY += 10;
            }

            manager.setList(list);
            manager.assignNeighbors((int)x, (int)y);
        }

        private void Debug_Button_Click(object sender, RoutedEventArgs e)
        {
            //double temp = Plane.ActualHeight;
            //string stemp = temp.ToString();
            //Debug_Button.Content = stemp;
            stop = true;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            manager.reset();
        }


        private void drawList(int k, int l, int x, int y, Node[,] list)


        {
            if ((k == x) || (l == y))
            {
                return;
            }
            //Debug.WriteLine("Drawing [" + k + "," + l + "]");

            for (int i = 0; i < x; i++)
            {
                Plane.Children.Add(list[i, l]);
            }

            drawList(k, (l + 1), x, y, list);

        }

        private void DisplayNodeInfo(object sender)
        {
            DebugDisplay.Text = "Node [" + ((Node)sender).x.ToString() + "," + ((Node)sender).y.ToString() + "]" + ((Node)sender).condition() + "\n";
            try
            {
                DebugDisplay.Text += "N [" + ((Node)sender).neighborN.x.ToString() + "," + ((Node)sender).neighborN.y.ToString() + "]" + ((Node)sender).neighborN.condition() + " \n";
            }
            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "E [" + ((Node)sender).neighborE.x.ToString() + "," + ((Node)sender).neighborE.y.ToString() + "]" + ((Node)sender).neighborE.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "S [" + ((Node)sender).neighborS.x.ToString() + "," + ((Node)sender).neighborS.y.ToString() + "]" + ((Node)sender).neighborS.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "W [" + ((Node)sender).neighborW.x.ToString() + "," + ((Node)sender).neighborW.y.ToString() + "]" + ((Node)sender).neighborW.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "NE [" + ((Node)sender).neighborNE.x.ToString() + "," + ((Node)sender).neighborNE.y.ToString() + "]" + ((Node)sender).neighborNE.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "NW [" + ((Node)sender).neighborNW.x.ToString() + "," + ((Node)sender).neighborNW.y.ToString() + "]" + ((Node)sender).neighborNW.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "SE [" + ((Node)sender).neighborSE.x.ToString() + "," + ((Node)sender).neighborSE.y.ToString() + "]" + ((Node)sender).neighborSE.condition() + " \n";
            }

            catch (Exception ex)
            {

            }

            try
            {
                DebugDisplay.Text += "SW [" + ((Node)sender).neighborSW.x.ToString() + "," + ((Node)sender).neighborSW.y.ToString() + "]" + ((Node)sender).neighborSW.condition() + " \n";
            }

            catch (Exception ex)
            {

            }
        }

        private void Node_Click(object sender, RoutedEventArgs e)
        {
            if (((Node)sender).dead)
            {
                ((Node)sender).setAlive();
            }
            else if (((Node)sender).alive)
            {
                ((Node)sender).setInfected();
            }
            else if (((Node)sender).infected)
            {
                ((Node)sender).setDead();
            }
            //DisplayNodeInfo(sender);
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            manager.randomize();
        }

        private async void Cycle_Click(object sender, RoutedEventArgs e)
        {
            //Run iteration

            int input = 1;
            input = int.Parse(Input_Box.Text);

            for (int i = 0; i < input; i++)
            { 
                if(stop)
                {
                    stop = false;
                    break;
                }
                manager.cycleNodesOnce();
                await Task.Delay(150);
            }
        }

        private void altRules_Checked(object sender, RoutedEventArgs e)
        {
            manager.altRules = true;
        }

        private void uncheckedClick(object sender, RoutedEventArgs e)
        {
            manager.altRules = false;
        }


    }
}
