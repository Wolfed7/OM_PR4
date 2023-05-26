using OM_PR4;
using OM_PR4.GlobalSearchMethods;

var area = new Dictionary<int, Interval>()
{
   { 0, new Interval(-10, 10) },  // -10 <= x <= 10.
   { 1, new Interval(-10, 10) }   // -10 <= y <= 10.
};

var startPoint = new PointND() { 2.0, 1.0 };
var function = new VariantFunction();
double eps = 1e-7;
double probability = 0.8;
int attempts = 20;

//IMinSearchMethodND MinMethod = new SimpleRandomSearch(area, eps, probability);
//IMinSearchMethodND MinMethod = new GlobalSearch1(area, eps, attempts);
//IMinSearchMethodND MinMethod = new GlobalSearch2(area, eps, attempts);
IMinSearchMethodND MinMethod = new GlobalSearch3(area, eps, attempts);

MinMethod.Search(function, startPoint);
Console.WriteLine(MinMethod.MinPoint);
Console.WriteLine(MinMethod.MinValue);