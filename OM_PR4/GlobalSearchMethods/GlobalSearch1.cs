using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4.GlobalSearchMethods;

public class GlobalSearch1 : IMinSearchMethodND
{
   public int FCALCS { get; private set; } // temp

   public PointND MinPoint { get; private set; }
   public double MinValue { get; private set; }

   public IDictionary<int, Interval> Area { get; init; }
   public double Eps { get; init; }
   public int Trying { get; init; }

   public GlobalSearch1(IDictionary<int, Interval> area, double eps = 1e-3, int trying = 100)
   {
      FCALCS = 0;


      MinPoint = new PointND();
      MinValue = 0;

      Area = area;
      Eps = eps;
      Trying = trying;
   }

   public void Search(IFunction function, PointND startPoint)
   {
      FCALCS = 0;
      IMinSearchMethodND directedMethod = new SimplexSearch(Eps, 1000);
      MinPoint = startPoint;
      MinValue = function.Compute(MinPoint);
      FCALCS++;

      for (int i = 0; i < Trying; i++)
      {
         var newPoint = new PointND() { };
         for (int j = 0; j < startPoint.Dimention; j++)
            newPoint.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         directedMethod.Search(function, newPoint);
         FCALCS += directedMethod.FCALCS;
         if (directedMethod.MinValue < MinValue)
         {
            i = 0;
            MinPoint = directedMethod.MinPoint;
            MinValue = directedMethod.MinValue;
         }
      }
   }
}