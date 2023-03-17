using Kse.Algorithms.Samples;
var generator = new MapGenerator(new MapGeneratorOptions()
{
     Height = 16,
     Width = 24,
     Seed = 3, 
     Noise = 0.1f, 
     AddTraffic = true,
     TrafficSeed = 1234
});
string[,] map = generator.Generate();
var distances = new Dictionary<Point, int>();
var origins = new Dictionary<Point, Point>();
var start = new Point(column: 0, row: 2);
var goal = new Point(row: 4, column: 13);
distances[start] = 0;

var my_result = GetPath(map, start, goal);


List<Point> GetPath(string[,] map, Point start, Point goal)
{
     var path = BFS(start, goal);
     new MapPrinter().Print(map, path);
     return path;
}

List<Point> BFS(Point start, Point goal)
{
     var visited = new List<Point>();
     var queue = new Queue<Point>();
     List<Point> path = new List<Point>();
     queue.Enqueue(start);
     Visit(start);
     bool stop = false;
     var totalTime = 0; // лічильник сумарного часу подорожі
     var counter = 0; 
     while (queue.Count > 0)
     {
          var next = queue.Dequeue();
          var neighbours = GetNeighbours(row:next.Row, column:next.Column, maze:map);
          foreach (var neighbour in neighbours)
          {
               if (!visited.Contains(neighbour))
               {
                    origins[neighbour] = next; // next це current точка з якою ми працюємо
                    distances[neighbour] = distances[next] + GetDistance(next, neighbour);
                    distances[neighbour] = distances[next] + GetLength(next, neighbour);
                    Visit(neighbour);
                    queue.Enqueue(neighbour);
                    if (neighbour.Equals(goal))
                    {
                         stop = true;
                    }
               }
          }
          if (stop)
          {
               path.Add(goal);
               var next_point = origins[goal];
               while (!next_point.Equals(start))
               {
                    path.Add(next_point);
                    totalTime += GetDistance(next_point, origins[next_point]); // додаємо час до лічильника
                    counter += GetLength(next_point, origins[next_point]);
                    next_point = origins[next_point];
               }
               path.Add(start);
               totalTime += GetDistance(start, next); // додаємо час від початку до першої точки маршруту
               break;
          }
     }
     Console.WriteLine($"Total time: {totalTime} minutes"); // виводимо сумарний час
     Console.WriteLine($"Total lengths: {counter} m");
     Console.WriteLine($"Visited points: {visited.Count}");
     return path;
     
     void Visit(Point point)
     {
          visited.Add(point);
     }
}


List<Point> GetNeighbours(int row, int column, string[,] maze)
{
     var result = new List<Point>();
     TryAddWithOffset(1, 0);
     TryAddWithOffset(-1, 0);
     TryAddWithOffset(0, 1);
     TryAddWithOffset(0, -1);
     return result;

     void TryAddWithOffset(int offsetRow, int offsetColumn)
     {
          var newX = row + offsetRow;
          var newY = column + offsetColumn;
          if (newX >= 0 && newY>= 0 && newX < maze.GetLength(1) && newY < maze.GetLength(0) && maze[newY, newX] != "█")
          {
               result.Add(new Point(newY, newX));
          }
     }
}

int GetDistance(Point next, Point neighbour)
{
     int traffic = 0;
     if (int.TryParse(map[neighbour.Row, next.Column], out int result))
     {
          traffic = result;
     }
     int speed = 60 - (traffic - 1) * 6;
     return 1 * 60 / speed;
}

int GetLength(Point next, Point neighbour)
{
     int traffic = 0;
     if (int.TryParse(map[neighbour.Row, next.Column], out int result))
     {
          traffic = result;
     }
     int speed = 60 - (traffic - 1) * 6;
     return speed * (1 * 60 / speed);

}