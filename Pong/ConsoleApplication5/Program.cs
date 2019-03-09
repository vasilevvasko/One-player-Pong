using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            int planeX = 30;
            int planeY = 15;
            int pongX = 2;
            int pongY = 3;
            int padY = 4;
            bool isDone = false;
            bool isNorthwise = true;
            bool isEastwise = true;
            char[,] plane = new char[planeX, planeY];
            fillPlane(plane, planeX, planeY, pongX, pongY, padY);
            printPlane(plane, planeX, planeY);

            while (isDone != true)
            {
      
                while (!Console.KeyAvailable)
                {
                    System.Threading.Thread.Sleep(50);
                    movePad(ref padY, ref plane, planeX, planeY);
                    movePong(isNorthwise, isEastwise, ref pongX, ref pongY, ref plane);
                    if (pongY == 0)
                    {
                        isDone = true;
                        Console.Clear();
                        Console.WriteLine("YOU LOSE");
                        break;

                    }
                    checkHit(ref isNorthwise, ref isEastwise, plane, pongX, pongY);
                    checkBump(ref isNorthwise, ref isEastwise, plane, pongX, pongY);


                    Console.Clear();
                    printPlane(plane, planeX, planeY);
                   
                }
                var ch = Console.ReadKey(false).Key;
                if (ch == ConsoleKey.UpArrow) { padY--; }
                if (ch == ConsoleKey.DownArrow) { padY++; }

            }
            
            //Console.ReadLine();
        }

        public static void fillPlane(char[,] plane, int planeX, int planeY, int pongX, int pongY, int padY)
        {
            for (int i = 0; i < planeY; i++)
            {
                for (int j = 0; j < planeX; j++)
                {
                    if (i == 0 || j == planeX - 1 || i == planeY - 1)
                    {
                        plane[j, i] = '@';
                    }
                }
            }
            plane[0, padY] = '/';
            plane[0, padY + 1] = '/';
            plane[0, padY - 1] = '/';
            plane[pongY, pongX] = 'x';
        }
        public static void printPlane(char[,] plane, int planeX, int planeY)
        {
            for (int i = 0; i < planeY; i++)
            {
                for (int j = 0; j < planeX; j++)
                {
                    Console.Write(plane[j, i]);
                }
                Console.Write("\n");
            }

        }
        public static void movePong(bool isNW, bool isEW, ref int pngX, ref int pngY, ref char[,] plane)
        {
            plane[pngY, pngX] = ' ';
            if (isNW == true)
            {
                pngY++;
            }
            else
            {
                pngY--;
            }
            if (isEW == true)
            {
                pngX++;
            }
            else
            {
                pngX--;
            }
            plane[pngY, pngX] = 'x';
        }
        public static void checkBump(ref bool isNW, ref bool isEW, char[,] plane, int pngX, int pngY)
        {
            if (plane[pngY + 1, pngX] == '@' && plane[pngY + 1, pngX + 1] == '@' && plane[pngY + 1, pngX - 1] == '@')
            {
                isNW = false; //check down
            }
            else if (plane[pngY - 1, pngX] == '@' && plane[pngY - 1, pngX + 1] == '@' && plane[pngY - 1, pngX - 1] == '@')
            {
                isNW = true;
            }
            if (plane[pngY, pngX + 1] == '@' && plane[pngY + 1, pngX + 1] == '@' && plane[pngY - 1, pngX + 1] == '@')
            {
                isEW = false;
            }
            else if (plane[pngY, pngX - 1] == '@' && plane[pngY + 1, pngX - 1] == '@' && plane[pngY - 1, pngX - 1] == '@')
            {
                isEW = true;
            }
        }
        public static void checkHit(ref bool isNW, ref bool isEW, char[,] plane, int pngX, int pngY)
        {
            if (isNW == false && plane[pngY - 1, pngX] == '/' /*|| plane[pngY - 1, pngX + 1] == '/' || plane[pngY - 1, pngX - 1] == '/'*/)
            {
                isNW = true;
            }
            else if (isEW == false && plane[pngY, pngX - 1] == '/' /*|| plane[pngY + 1, pngX - 1] == '/' || plane[pngY - 1, pngX - 1] == '/'*/)
            {
                isEW = true;
            }
        }
        public static void movePad(ref int padY, ref char[,] plane, int planeX, int planeY)
        {
            for (int i = 0; i < planeY; i++)
            {
                for (int j = 0; j < planeX; j++)
                {
                    plane[0, i] = ' ';
                }
            }
            int temp = padY;
          
            if (temp < planeY - (planeY - 1)) { temp = 1; }
            if (temp > planeY - 2) { temp = planeY - 2; }

            padY = temp;
            plane[0, temp] = '/';
            plane[0, temp+1] = '/';
            plane[0, temp-1] = '/';
           
        }
    }
}
