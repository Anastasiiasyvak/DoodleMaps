using Kse.Algorithms.Samples;
var generator = new MapGenerator(new MapGeneratorOptions()
{
     Height = 10,
     Width = 15,
     Seed = 3,  
     Noise = 0.1f,
     AddTraffic = true,
     TrafficSeed = 12345
});
// Generate використовується для створення двовимірного масиву 
// кома вказує на те що массив є двовимірним string[,] всі елементи масиву мають тип строки
string[,] map = generator.Generate();
var distances = new Dictionary<Point, int>(); // зберігає відстань від початкової точки до кожної відвіданої точки на карті
                                              // Ключами словника є точки, які представлені об'єктами класу Point, а значеннями - відстані від початкової точки до цих точок.
var origins = new Dictionary<Point, Point>(); // зберігає інформацію про те, з якої точки на карті була відвідана кожна точка. 
//Ключами словника також є об'єкти класу Point, а значеннями - об'єкти Point, які представляють точки, з яких була відвідана поточна точка.
var start = new Point(column: 0, row: 2);
var goal = new Point(row: 8, column: 2);
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
     var queue = new Queue<Point>(); // використовується для зберігання точок, що треба дослідити
     var path = new List<Point>(); // містить всі точки, що складають найкоротший шлях від стартової до цільової точки
     
     queue.Enqueue(start);
     Visit(start);
     bool stop = false;
     while (queue.Count > 0)
     {
          // змінна next це поточна точка яка буде використовуватися для того щоб знайти її сусідів 
          var next = queue.Dequeue();
          var neighbours = GetNeighbours(row:next.Row, column:next.Column, maze:map);
          foreach (var neighbour in neighbours)
          {
               if (!visited.Contains(neighbour))
               {
                    origins[neighbour] = next; 
                    distances[neighbour] = distances[next] + 1;
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
                    next_point = origins[next_point];
               }
               path.Add(start);
               break;
          }
     }

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
          // row поточне значення рядка
          var newX = row + offsetRow;
          var newY = column + offsetColumn;
          if (newX >= 0 && newY>= 0 && newX < maze.GetLength(1) && newY < maze.GetLength(0) && maze[newY, newX] != "█")
          {
               result.Add(new Point(newY, newX));
          }
     }
     
}
