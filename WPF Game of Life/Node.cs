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
using System.Windows.Threading;

namespace WPF_Game_of_Life
{
    class Node : Button
    {
        //Primitives
        public Boolean alive = true;
        public Boolean dead = false;
        public Boolean infected = false;
        public int y;
        public int x;
        public Node neighborN = null;
        public Node neighborE = null;
        public Node neighborW = null;
        public Node neighborS = null;
        public Node neighborNW = null;
        public Node neighborNE = null;
        public Node neighborSE = null;
        public Node neighborSW = null;
        int aliveNeighbors;
        int deadNeighbors;
        int infectedNeighbors;
        int nextCase = 0;
        public int shift = 0;

        public Node()
        {
            setDead();
            this.Height = 12;
            this.Width = 12;
            this.Margin = new Thickness(0, -800, 0, 0);
            this.BorderThickness = new Thickness(1);
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }

        public void setDead()
        {
            alive = false;
            dead = true;
            infected = false;

            //Change color
            //this.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            this.Background = new SolidColorBrush(Color.FromRgb(0,0,0));

        }

        public void setAlive()
        {
            alive = true;
            dead = false;
            infected = false;

            //Change color
            this.Background = new SolidColorBrush(Color.FromRgb(0, 100, 0));
            //Debug.WriteLine("setAlive Called");
        }

        public void setInfected()
        {
            alive = false;
            dead = false;
            infected = true;

            //Change color
            this.Background = new SolidColorBrush(Color.FromRgb(178, 34, 34));
            //Debug.WriteLine("setInfected Called");
        }

        //MULTITHREADING: Set to async.  Have to hold function to wait to draw async updates
        public void play(Boolean rules)
        {
                countNeighbors();

                //Need to add die conditions for infected cells.
                if (infected == true)
                {
                    nextCase = 0;
                }

                else
                {
                    if(rules == false)
                    {
                        if (aliveNeighbors > 3)
                        {
                            nextCase = 0; //setDead();
                        }

                        else if (aliveNeighbors < 2)
                        {
                            nextCase = 0; //setDead();
                        }

                        else if (infectedNeighbors > 0)
                        {
                            if (alive == true) //Only infects alive neighbors
                                nextCase = 1; //set infectedNeighbors
                        }

                        else if (aliveNeighbors == 3)
                        {
                            nextCase = 2; //setAlive();
                        }
                        else if (aliveNeighbors == 2 && this.alive)
                            nextCase = 2;

                    } // End if

                    else
                    {
                        if (aliveNeighbors > 2)
                        {
                            nextCase = 0; //setDead();
                        }

                        else if (aliveNeighbors < 2)
                        {
                            nextCase = 0; //setDead();
                        }

                        else if (infectedNeighbors > 0)
                        {
                            if (alive == true) //Only infects alive neighbors
                                nextCase = 1; //set infectedNeighbors
                        }

                        else if (aliveNeighbors == 2)
                        {
                            nextCase = 2; //setAlive();
                        }
                        else if (aliveNeighbors == 2 && this.alive)
                            nextCase = 2;

                    }
                }
        }

        void countNeighbors()
        {
            //await Task.Run(() =>
            //{
                int a = 0;
                int d = 0;
                int i = 0;

                if (!(neighborN == null))
                {
                    if (neighborN.alive)
                        a++;
                    if (neighborN.dead)
                        d++;
                    if (neighborN.infected)
                        i++;
                }
                if (!(neighborE == null))
                {
                    if (neighborE.alive)
                        a++;
                    if (neighborE.dead)
                        d++;
                    if (neighborE.infected)
                        i++;
                }
                if (!(neighborW == null))
                {
                    if (neighborW.alive)
                        a++;
                    if (neighborW.dead)
                        d++;
                    if (neighborW.infected)
                        i++;
                }
                if (!(neighborS == null))
                {
                    if (neighborS.alive)
                        a++;
                    if (neighborS.dead)
                        d++;
                    if (neighborS.infected)
                        i++;
                }
                if (!(neighborNE == null))
                {
                    if (neighborNE.alive)
                        a++;
                    if (neighborNE.dead)
                        d++;
                    if (neighborNE.infected)
                        i++;
                }
                if (!(neighborNW == null))
                {
                    if (neighborNW.alive)
                        a++;
                    if (neighborNW.dead)
                        d++;
                    if (neighborNW.infected)
                        i++;
                }
                if (!(neighborSE == null))
                {
                    if (neighborSE.alive)
                        a++;
                    if (neighborSE.dead)
                        d++;
                    if (neighborSE.infected)
                        i++;
                }
                if (!(neighborSW == null))
                {
                    if (neighborSW.alive)
                        a++;
                    if (neighborSW.dead)
                        d++;
                    if (neighborSW.infected)
                        i++;
                }

                aliveNeighbors = a;
                deadNeighbors = d;
                infectedNeighbors = i;

            //});

            


        }

        public string condition()
        {
            if (this.dead)
                return " is dead ";
            else if (this.alive)
                return " is alive ";
            else
                return " is infected ";
        }


        //On async task this will break setDead() setAlive() setInfected().  Not on same thread.
        public void updateNodes()
        {

            if (nextCase == 0)
                setDead();

            if (nextCase == 1)
                setInfected();

            if (nextCase == 2)
                setAlive();
        }

    }
}


//Create a Node class that controls buttons and uses a Node Manager to manage the colletive nodes
//Nodes need to have 3 states: Alive, Dead, Infected
//Nodes need to be able to identify their neighbor cells; including diagnolly
