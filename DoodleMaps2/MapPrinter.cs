namespace Kse.Algorithms.Samples
{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {
        // maze двовимірний масив рядків, представляє карту лабіринта 
        // path список координат точок, через які проходять шлях
        // метод Print
        public void Print(string[,] maze, List<Point> path)
        {
            PrintTopLine();
            var start = path[0];
            var goal = path[^1];
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t"); // формує рядок який містить номер рядка
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    var current_point = new Point(column: column, row: row);
                    if (start.Equals(current_point))
                    {
                        Console.Write('B');
                    }
                    else if (goal.Equals(current_point))
                    {
                        Console.Write('A');
                    }
                    else if (path.Contains(current_point))
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(maze[column, row]);
                    }

                }

                Console.WriteLine("");
            }

            void PrintTopLine()
            {
                Console.Write(" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0 ? i / 10 : " ");
                }

                Console.Write("\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }

                Console.WriteLine("\n");
            }
        }
    }
}


