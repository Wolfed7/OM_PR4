using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

public interface IMinSearchMethodND
{
   // Номер переменной - интервал области определения.
   public IDictionary<int, Interval> Area { get; init; }

   // Точка, в которой достигается минимум функции.
   public PointND MinPoint { get; }

   // Минимум функции, найденный методом поиска.
   public double MinValue { get; }

   public int FCALCS { get; } // temp

   public void Search(IFunction function, PointND startPoint);
}
