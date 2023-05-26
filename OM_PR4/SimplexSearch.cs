using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

public class SimplexSearch : IMinSearchMethodND
{
   private PointND[] _simplex;

   public int FunctionCalcs;        // Количество вычислений функции.
   public static double Alpha => 1; // Коэффициент отражения
   public static double Beta => 0.5;
   public static double Gamma => 2;
   public static double Distance => 1;

   public double Eps { get; init; }
   public int MaxIters { get; init; }
   public IDictionary<int, Interval> Area { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

   public PointND MinPoint { get; private set; }

   public double MinValue { get; private set; }

   public SimplexSearch(double eps, int maxIters)
   {
      _simplex = Array.Empty<PointND>();
      MinPoint = new PointND(0);
      MinValue = 0;

      Eps = eps;
      MaxIters = maxIters;
      FunctionCalcs = 0;
   }

   public void Search(IFunction function, PointND startPoint)
   {
      int PointDimension = startPoint.Dimention;
      int SimplexSize = PointDimension + 1;

      MinPoint = startPoint;
      _simplex = new PointND[SimplexSize];
      for (int i = 0; i < SimplexSize; i++)
         _simplex[i] = new PointND(PointDimension);

      double d1 = Distance * (Math.Sqrt(PointDimension + 1) + PointDimension - 1) / (PointDimension * Math.Sqrt(2));
      double d2 = Distance / ((PointDimension * Math.Sqrt(2)) * (Math.Sqrt(PointDimension + 1) - 1));

      // TODO сделать симплекс зависимым от стартовой точки. PS: вроде готово...?
      _simplex[PointDimension] = startPoint;
      for (int i = 0; i < PointDimension; i++)
         for (int j = 0; j < PointDimension; j++)
         {
            if (i == j)
            {
               _simplex[i][j] = d1 + _simplex[PointDimension][j];
               continue;
            }
            _simplex[i][j] = d2 + _simplex[PointDimension][j];
         }

      PointND xr = new(PointDimension);
      PointND xg = new(PointDimension);
      PointND xe = new(PointDimension);
      PointND xc = new(PointDimension); // Центр тяжести симплекса.

      for (int iters = 0; iters < MaxIters; iters++)
      {
         _simplex = _simplex.OrderBy(function.Compute).ToArray();
         FunctionCalcs += SimplexSize;
         xc.Fill(0);

         // Центр тяжести = сумма всех векторов (не скалярка), кроме xh 
         for (int i = 0; i < PointDimension; i++)
            for (int j = 0; j < PointDimension; j++)
               xc[i] += _simplex[j][i] / PointDimension;

         // Выйдем, если достигли заданной точности.
         FunctionCalcs += 2; // В идеале дважды вычисляем для проверки точности.
         if (IsAccuracyAchieved(_simplex, xc, function))
         {
            MinPoint = _simplex[0]; //xc
            MinValue = function.Compute(MinPoint);
            break;
         }

         // Отразим наибольшую точку относительно центра тяжести.
         xr = Reflection(_simplex, xc);

         double fr = function.Compute(xr); // Новое значение функции.
         double fl = function.Compute(_simplex[0]); // Худшее значение функции.
         double fg = function.Compute(_simplex[PointDimension - 1]);
         double fh = function.Compute(_simplex[PointDimension]);
         FunctionCalcs += 4;


         if (fl < fr && fr < fg)
         {
            _simplex[PointDimension] = (PointND)xr.Clone();
         }
         else if (fr < fl)
         {
            // Производим растяжение.
            xe = Expansion(xc, xr);

            // fe < fr
            if (function.Compute(xe) < fr)
               _simplex[PointDimension] = (PointND)xe.Clone();
            else
               _simplex[PointDimension] = (PointND)xr.Clone();
         }
         else if (fr < fh)
         {
            // Вход в эту часть кода означает, что поменяны
            // местами xr и наибольший элемент симплекса.
            // Проведём сжатие.
            xg = OutsideContraction(xc, xr);

            // fg < fr - сжимаем ещё сильнее
            if (function.Compute(xg) < fr)
               _simplex[PointDimension] = (PointND)xg.Clone();
            // Иначе глобально сжимаем симплекс к наименьшей точке.
            else
               Shrink(_simplex);
         }
         else
         {
            // Не меняем xr и наибольший элемент симплекса, делаем сжатие.
            xg = InsideContraction(_simplex, xc);

            FunctionCalcs += 1;
            if (function.Compute(xg) < fh)
               _simplex[PointDimension] = (PointND)xg.Clone();
            else
               Shrink(_simplex);
         }
      }

      MinPoint = _simplex[0];
   }

   private bool IsAccuracyAchieved(PointND[] Simplex, PointND xc, IFunction function)
   {
      double sum = 0;

      for (int i = 0; i < xc.Dimention + 1; i++)
      {
         sum += (function.Compute(Simplex[i]) - function.Compute(xc)) *
                (function.Compute(Simplex[i]) - function.Compute(xc));
      }

      return Math.Sqrt(sum / (xc.Dimention + 1)) < Eps;
   }

   private static PointND Reflection(PointND[] Simplex, PointND xc)
      => (1 + Alpha) * xc - Alpha * Simplex[xc.Dimention];

   private static PointND Expansion(PointND xc, PointND xr)
      => (1 - Gamma) * xc + Gamma * xr;

   private static PointND OutsideContraction(PointND xc, PointND xr)
      => Beta * xr + (1 - Beta) * xc;

   private static PointND InsideContraction(PointND[] Simplex, PointND xc)
        => Beta * Simplex[xc.Dimention] + (1 - Beta) * xc;

   private static void Shrink(PointND[] Simplex)
   {
      for (int i = 1; i <= Simplex[0].Dimention; i++)
         Simplex[i] = (PointND)(Simplex[0] + (Simplex[i] - Simplex[0]) / 2).Clone();
   }
   private static double Norm(PointND arg)
   {
      double result = 0;

      for (int i = 0; i < arg.Dimention; i++)
      {
         result += arg[i] * arg[i];
      }

      return Math.Sqrt(result);
   }
}
