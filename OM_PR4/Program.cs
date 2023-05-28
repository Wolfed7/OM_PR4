using OM_PR4;
using OM_PR4.GlobalSearchMethods;
using System.Globalization;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var area = new Dictionary<int, Interval>()
{
   { 0, new Interval(-10, 10) },  // -10 <= x <= 10.
   { 1, new Interval(-10, 10) }   // -10 <= y <= 10.
};

var startPoint = new PointND() { 2.0, 1.0 };
var function = new VariantFunction();
double eps = 1;
double probability = 0.8;
int attempts = 5;

IMinSearchMethodND MinMethod = new SimpleRandomSearch(area, eps, probability);
//IMinSearchMethodND MinMethod = new GlobalSearch1(area, eps, attempts);
//IMinSearchMethodND MinMethod = new GlobalSearch2(area, eps, attempts);
//IMinSearchMethodND MinMethod = new GlobalSearch3(area, eps, attempts);

//MinMethod.Search(function, startPoint);
//Console.Write(MinMethod.MinPoint);
//Console.WriteLine($"   {MinMethod.MinValue}");


////Исследование метода простого случайного поиска.
//double[] epss = new double[] { 0.1, 0.05, 0.025, 0.0125, 0.00625 };
//double[] probs = new double[] { 0.8, 0.9, 0.95, 0.99, 0.999 };

//foreach (var epsi in epss)
//{
//   foreach (var prob in probs)
//   {
//      IMinSearchMethodND MinMethodSRS = new SimpleRandomSearch(area, epsi, prob);
//      Console.Write($"{epsi}   ");
//      Console.Write($"{prob}   ");
//      MinMethodSRS.Search(function, startPoint);
//      Console.Write($"{MinMethodSRS.MinPoint}   ");
//      Console.WriteLine($"{-MinMethodSRS.MinValue}");
//   }
//}


// Исследование метода глобального поиска (алгоритм 1).
double[] epss = new double[] { 1e-3 };
int[] ms = new int[] { 2, 4, 8, 16, 32 };

foreach (var epsi in epss)
{
   foreach (var m in ms)
   {
      IMinSearchMethodND MinMethodSRS = new GlobalSearch1(area, epsi, m);
      Console.Write($"{epsi}   ");
      MinMethodSRS.Search(function, startPoint);
      Console.Write($"{m}   ");
      Console.Write($"{MinMethodSRS.FCALCS}   ");
      Console.Write($"{MinMethodSRS.MinPoint}   ");
      Console.WriteLine($"{-MinMethodSRS.MinValue}");

      MinMethodSRS = new GlobalSearch2(area, epsi, m);
      Console.Write($"{epsi}   ");
      MinMethodSRS.Search(function, startPoint);
      Console.Write($"{m}   ");
      Console.Write($"{MinMethodSRS.FCALCS}   ");
      Console.Write($"{MinMethodSRS.MinPoint}   ");
      Console.WriteLine($"{-MinMethodSRS.MinValue}");

      MinMethodSRS = new GlobalSearch3(area, epsi, m);
      Console.Write($"{epsi}   ");
      MinMethodSRS.Search(function, startPoint);
      Console.Write($"{m}   ");
      Console.Write($"{MinMethodSRS.FCALCS}   ");
      Console.Write($"{MinMethodSRS.MinPoint}   ");
      Console.WriteLine($"{-MinMethodSRS.MinValue}");
   }
}

//foreach (var epsi in epss)
//{
//   foreach (var m in ms)
//   {
//      IMinSearchMethodND MinMethodSRS = new GlobalSearch2(area, epsi, m);
//      Console.Write($"{epsi}   ");
//      MinMethodSRS.Search(function, startPoint);
//      Console.Write($"{m}   ");
//      Console.Write($"{MinMethodSRS.FCALCS}   ");
//      Console.Write($"{MinMethodSRS.MinPoint}   ");
//      Console.WriteLine($"{-MinMethodSRS.MinValue}");
//   }
//}

//foreach (var epsi in epss)
//{
//   foreach (var m in ms)
//   {
//      IMinSearchMethodND MinMethodSRS = new GlobalSearch3(area, epsi, m);
//      Console.Write($"{epsi}   ");
//      MinMethodSRS.Search(function, startPoint);
//      Console.Write($"{m}   ");
//      Console.Write($"{MinMethodSRS.FCALCS}   ");
//      Console.Write($"{MinMethodSRS.MinPoint}   ");
//      Console.WriteLine($"{-MinMethodSRS.MinValue}");
//   }
//}

