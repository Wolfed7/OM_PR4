﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4.GlobalSearchMethods;

public class GlobalSearch2 : IMinSearchMethodND
{
   public PointND MinPoint { get; private set; }
   public double MinValue { get; private set; }

   public IDictionary<int, Interval> Area { get; init; }
   public double Eps { get; init; }
   public int Trying { get; init; }

   public GlobalSearch2(IDictionary<int, Interval> area, double eps = 1e-3, int trying = 100)
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
      PointND newPoint;

      MinPoint = startPoint;
      MinValue = function.Compute(MinPoint);

      for (int i = 0; i < Trying; i++)
      {
         int index = 0;
         directedMethod.Search(function, MinPoint);

         double newValue = function.Compute(directedMethod.MinPoint);
         if (newValue < MinValue)
         {
            MinPoint = directedMethod.MinPoint;
            MinValue = newValue;
         }

         do
         {
            index++;
            newPoint = new PointND() { };
            for (int j = 0; j < startPoint.Dimention; j++)
               newPoint.Add(Area[j].LeftBoundary + new Random().NextDouble() * Area[j].Length);

         } while ((newValue = function.Compute(newPoint)) >= MinValue && index < Trying);

         if (newValue < MinValue)
         {
            MinPoint = newPoint;
            MinValue = newValue;
         }
      }
   }
}