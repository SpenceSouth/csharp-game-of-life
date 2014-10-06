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
    class NodeManager
    {
        Node[,] nodelist;
        int marginX = 0;
        int marginY = 0;
        public int width;
        public int height;
        int counter = 0;
        int max;
        public Boolean altRules = false;




        public NodeManager()
        {
            
        }

        public void setList(Node[,] list)
        {
            nodelist = list;
        }

        public void randomize()
        {

            Random rand = new Random();
            max = height * width;
            int third = max / 5;

            for (int i = 0; i < third; i++)
            {
                int randomHeight = rand.Next(0, height);
                int randomWidth = rand.Next(0, width);

                nodelist[randomWidth, randomHeight].setAlive();
            }


        }

        public void assignNeighbors(int width, int height)
        {
            int tempWidth = 0;
            int tempHeight = 0;

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    if (!(h == 0)) //If "i" is not 0.  So that we are aren't trying to access array[-1]
                    {

                        nodelist[w, h].neighborN = nodelist[w, (h - 1)];

                        if (w != (width - 1))
                        {
                            nodelist[w, h].neighborNE = nodelist[(w + 1), (h - 1)];
                        }
                        if (w != 0)
                        {
                            nodelist[w, h].neighborNW = nodelist[(w - 1), (h - 1)];
                        }

                    }

                    if (h != (height - 1)) //Will not try to access array[height+1] (Out of bounds)
                    {
                        //Debug.WriteLine("Trying to assign node[" + i + "," + j + "] south neighbor  [" + (i+1) + "," + j + "]");
                        nodelist[w, h].neighborS = nodelist[w, (h + 1)];
                        //Debug.WriteLine("Assigned node[" + i + "," + j + "]  to south neighbor " + nodelist[i,j].neighborS);
                        //Debug.WriteLine("TempWidth: " + tempWidth);
                        if (w < width - 1)
                        {
                            nodelist[w, h].neighborSE = nodelist[(w + 1), (h + 1)];
                        }
                        if (w != 0)
                        {
                            nodelist[w, h].neighborSW = nodelist[(w - 1), (h + 1)];
                        }

                    }

                    if (w != (width - 1))
                    {
                        nodelist[w, h].neighborE = nodelist[(w + 1), h];

                    }

                    if (w != 0)
                    {
                        nodelist[w, h].neighborW = nodelist[(w - 1), h];
                    }

                    tempWidth++;
                }

                tempHeight++;
            }
        }

        public void cycleNodesOnce()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    nodelist[j, i].play(altRules);
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    nodelist[j, i].updateNodes();
                }
            }

        }

        public string countNodes()
        {
            return nodelist.Length.ToString();
        }

        public void reset()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    nodelist[j, i].setDead();
                }
            }
        }
    }
}
