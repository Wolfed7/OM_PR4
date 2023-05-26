using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

public class Interval
{
   public double LeftBoundary { get; }
   public double RightBoundary { get; }
   public double Center => (LeftBoundary + RightBoundary) / 2;
   public double Length => Math.Abs(RightBoundary - LeftBoundary);

   public Interval(double leftBoundary = 0, double rightBoundary = 1)
   {
      if (leftBoundary <= rightBoundary)
      {
         LeftBoundary = leftBoundary;
         RightBoundary = rightBoundary;
      }
      else
      {
         throw new ArgumentException("Неверно задан интервал.");
      }
   }

   public static Interval Parse(string str)
   {
      var data = str.Split();
      return new Interval(double.Parse(data[0]), double.Parse(data[1]));
   }

   public bool Contains(double point)
    => (point >= LeftBoundary && point <= RightBoundary);

   public override string ToString()
   {
      return $"[{LeftBoundary}; {RightBoundary}]";
   }
}