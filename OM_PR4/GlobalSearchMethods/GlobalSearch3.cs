using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4.GlobalSearchMethods;

public class GlobalSearch3 : IMinSearchMethodND
{
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

   //public void Search(IFunction function, PointND startPoint)
   //{
   //   IMinSearchMethodND directedMethod = new SimplexSearch(Eps, 1000);

   //   double temp;
   //   double functionValueX2;

   //   Vector2D x1, x2;
   //   MinPoint = startPoint;
   //   newPoint =  = new(newX, newY);

   //   double functionValueX1 = function.Value(MinPoint);

   //   for (int i = 0; i < Trying; i++)
   //   {
   //      double step = 1.0;

   //      simplexMethod.Compute(function, initPoint);
   //      x1 = simplexMethod.Min!.Value;

   //      if ((temp = function.Value(simplexMethod.Min!.Value)) < functionValueX1)
   //      {
   //         _min = simplexMethod.Min;
   //         functionValueX1 = temp;
   //      }

   //      newX = new Random().NextDouble(rectangle.LeftBottom.X, rectangle.RightBottom.X);
   //      newY = new Random().NextDouble(rectangle.LeftBottom.Y, rectangle.LeftTop.Y);

   //      Vector2D randomVector = new(newX, newY);
   //      Vector2D direction = simplexMethod.Min!.Value + step * (simplexMethod.Min.Value - randomVector);
   //      double functionValueDirection = function.Value(direction);

   //      while (functionValueDirection >= function.Value(simplexMethod.Min.Value) && rectangle.Inside(direction))
   //      {
   //         direction = simplexMethod.Min!.Value + step * (simplexMethod.Min.Value - randomVector);
   //         functionValueDirection = function.Value(direction);
   //         step++;
   //      }

   //      simplexMethod.Compute(function, direction);
   //      functionValueX2 = function.Value(simplexMethod.Min.Value);

   //      x2 = simplexMethod.Min.Value;

   //      if (functionValueX2 < functionValueX1)
   //      {
   //         _min = simplexMethod.Min;
   //      }

   //      if (functionValueX2 < functionValueX1)
   //      {
   //         initPoint = x2;
   //      }
   //      else
   //      {
   //         initPoint = x1;
   //      }
   //   }
   }
}