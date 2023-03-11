using System.ComponentModel.DataAnnotations.Schema;
using Kse.Algorithms.Samples;
// MapGenerator клас, а MapGeneratorOptions це об'єкт через який передається ширина і довжина 
var generator = new MapGenerator(new MapGeneratorOptions()
{
     Height = 16,
     Width = 24,
     Seed = 3, // якщо не задавати seed то карта буде кожен раз різна 
     Noise = 0.1f // стінки наші, самий плотний це 0 і до 1 можна обирати
});
// Generate використовується для створення двовимірного масиву 
// Print друкує мапу 
// кома вказує на те що массив є двовимірним string[,] всі елементи масиву мають тип строки
string[,] map = generator.Generate();
List<Point> track = new List<Point>();
var distances = new Dictionary<Point, int>();
var origins = new Dictionary<Point, Point>();
var start = new Point(column: 0, row: 2);
var goal = new Point(row: 8, column: 12);
distances[start] = 0;
// track.Add(start);
// track.Add(goal);

var my_result = GetPath(map, start, goal);

// new MapPrinter().Print(map, track);

List<Point> GetPath(string[,] map, Point start, Point goal)
{
     var path = BFS(start, goal);
     // List<Point> path = new List<Point>();
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
     while (queue.Count > 0)
     {
          var next = queue.Dequeue();
          var neighbours = GetNeighbours(row:next.Row, column:next.Column, maze:map);
          foreach (var neighbour in neighbours)
          {
               if (!visited.Contains(neighbour))
               {
                    origins[neighbour] = next; // next це current точка з якою ми працюємо
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
          //map[point.Column, point.Row] = distances[point].ToString();
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