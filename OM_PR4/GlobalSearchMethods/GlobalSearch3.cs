using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4.GlobalSearchMethods;

public class GlobalSearch3 : IMinSearchMethodND
{
   public int FCALCS { get; private set; } // temp
   public PointND MinPoint { get; private set; }
   public double MinValue { get; private set; }

   public IDictionary<int, Interval> Area { get; init; }
   public double Eps { get; init; }
   public int Trying { get; init; }

   public GlobalSearch3(IDictionary<int, Interval> area, double eps = 1e-3, int trying = 100)
   {
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


      PointND x1, x2;
      MinPoint = startPoint;

      double functionValueX1;
      double functionValueX2;

      directedMethod.Search(function, MinPoint);
      x1 = directedMethod.MinPoint;
      functionValueX1 = directedMethod.MinValue;
      FCALCS += directedMethod.FCALCS;

      for (int i = 0; i < Trying; i++)
      {
         var newPoint = new PointND() { };
         for (int j = 0; j < startPoint.Dimention; j++)
            newPoint.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         var randomVector = new PointND() { };
         for (int j = 0; j < startPoint.Dimention; j++)
            randomVector.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         double step = 1.0;
         PointND direction = directedMethod.MinPoint + step * (directedMethod.MinPoint - randomVector);
         double functionValueDirection = function.Compute(direction);
         FCALCS++;

         double prevValue = directedMethod.MinValue;
         while (functionValueDirection >= prevValue && IsInside(Area, direction))
         {
            step *= 2;
            prevValue = functionValueDirection;
            direction = directedMethod.MinPoint + step * (directedMethod.MinPoint - randomVector);
            functionValueDirection = function.Compute(direction);
            FCALCS++;
         }

         directedMethod.Search(function, direction);
         FCALCS += directedMethod.FCALCS;
         x2 = directedMethod.MinPoint;
         functionValueX2 = directedMethod.MinValue;

         if (functionValueX2 < functionValueX1)
         {
            x1 = x2;
            functionValueX1 = functionValueX2;
         }
      }

      MinPoint = x1;
      MinValue = functionValueX1;

      bool IsInside(IDictionary<int, Interval> area, PointND point)
      {
         for (int i = 0; i < point.Dimention; i++)
            if (!(area[i].LeftBoundary <= point[i] && point[i] <= area[i].RightBoundary))
               return false;
         return true;
      }
   }
}