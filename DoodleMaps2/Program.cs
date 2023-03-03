using System.ComponentModel.DataAnnotations.Schema;
using Kse.Algorithms.Samples;
// MapGenerator клас, а MapGeneratorOptions це об'єкт через який передається ширина і довжина 
var generator = new MapGenerator(new MapGeneratorOptions()
{
     Height = 10,
     Width = 15,
     Seed = 3, // якщо не задавати seed то карта буде кожен раз різна 
     Noise = 0.1f // стінки наші, самий плотний це 0 і до 1 можна обирати
});
// Generate використовується для створення двовимірного масиву 
// Print друкує мапу 
// кома вказує на те що массив є двовимірним string[,] всі елементи масиву мають тип строки
string[,] map = generator.Generate();
List<Point> track = new List<Point>();

Point start = new Point(column: 0, row: 2);
Point goal = new Point(column: 14, row: 8);
track.Add(start);
track.Add(new Point(6, 6));
track.Add(goal);


new MapPrinter().Print(map, track);

// List<Point> GetShortestPath(string[,] map, Point start, Point goal)
// {
//      List<Point> result = new List<Point>();
//      return result;
//      // your code here
// }

