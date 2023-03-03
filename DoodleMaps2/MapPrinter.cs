namespace Kse.Algorithms.Samples
{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {
        // maze представляє карту лабіринта 
        // track список координат точок, через які проходять шлях
        public void Print(string[,] maze, List<Point> track)  
        {
            var start = track[0]; // координати стартової точки
            var goal = track[^1];  // повертає останній символ із списку track
            PrintTopLine(); // метод який відображає відступи для кожного рядка, зокрема- координати верхньої лінії
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t"); // формує рядок який містить номер рядка
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    if (start.Equals(new Point(column, row)))
                    {
                        Console.Write('A');
                    }
                    else if (goal.Equals(new Point(column, row)))
                    {
                        Console.Write('B');
                    }
                    // else if (track.Contains(new Point(column, row)))
                    // {
                    //     Console.Write('.');
                    // }
                    else if (maze[column, row] != "█")
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write(maze[column, row]);
                    }
                }

                Console.WriteLine();
            }

            void PrintTopLine()
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0? i / 10 : " ");
                }
    
                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }
    
                Console.WriteLine("\n");
            }
        }
    }
}
