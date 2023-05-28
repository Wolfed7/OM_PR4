using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

public class SimpleRandomSearch : IMinSearchMethodND
{
   public int FCALCS { get; private set; } // temp

   private double _probability;
   public PointND MinPoint { get; private set; }
   public double MinValue { get; private set; }

   public double Eps { get; init; }
   public IDictionary<int, Interval> Area { get; init; }

   public SimpleRandomSearch(IDictionary<int, Interval> area, double eps = 1e-1, double probability = 0.5)
   {
      MinPoint = new PointND(0);
      MinValue = 0;

      Area = area;
      Eps = eps;
      _probability = probability;
   }

   public void Search(IFunction function, PointND startPoint)
   {
      FCALCS = 0;
      double minValue = function.Compute(startPoint);

      int numberOfExperiments = 1;
      double epsVicinityVolume = 1;
      for (int i = 0; i < startPoint.Dimention; i++)
         epsVicinityVolume *= Eps;

      double areaVolume = 1;
      for (int i = 0; i < startPoint.Dimention; i++)
         areaVolume *= Area[i].Length;

      double probabilityEps = epsVicinityVolume / areaVolume;

      while (numberOfExperiments < Math.Log(1.0 - _probability) / Math.Log(1.0 - probabilityEps))
         numberOfExperiments++;

      for (int i = 0; i < numberOfExperiments; i++)
      {
         var newPoint = new PointND() { };
         for (int j = 0; j < startPoint.Dimention; j++)
            newPoint.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         double newValue = function.Compute(newPoint);
         if (newValue < minValue)
         {
            minValue = newValue;
            startPoint = newPoint;
         }
      }

      MinPoint = startPoint;
      MinValue = minValue;

      Console.Write($"{numberOfExperiments}   ");
   }
}
