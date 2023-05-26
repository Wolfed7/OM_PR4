using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4.GlobalSearchMethods;

public class GlobalSearch1 : IMinSearchMethodND
{
   public PointND MinPoint { get; private set; }
   public double MinValue { get; private set; }

   public IDictionary<int, Interval> Area { get; init; }
   public double Eps { get; init; }
   public int Trying { get; init; }

   public GlobalSearch1(IDictionary<int, Interval> area, double eps = 1e-3, int trying = 100)
   {
      MinPoint = new PointND();
      MinValue = 0;

      Area = area;
      Eps = eps;
      Trying = trying;
   }

   public void Search(IFunction function, PointND startPoint)
   {
      IMinSearchMethodND directedMethod = new SimplexSearch(Eps, 1000);
      MinPoint = startPoint;

      for (int i = 0; i < Trying; i++)
      {
         var newPoint = new PointND() { };
         for (int j = 0; j < startPoint.Dimention; j++)
            newPoint.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         directedMethod.Search(function, newPoint);

         if (function.Compute(directedMethod.MinPoint) < function.Compute(MinPoint))
            MinPoint = directedMethod.MinPoint;
      }

      MinValue = function.Compute(MinPoint);
   }
}