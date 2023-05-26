using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

public interface IFunction
{
   public double Compute(PointND point);
}

public class VariantFunction : IFunction
{
   double[] C = new double[] { 2, 1, 7, 2, 8, 4 };
   double[] a = new double[] { 5, 2, -9, 0, -3, -3 };
   double[] b = new double[] { 4, 0, -6, -3, 7, 3 };

   public double Compute(PointND point)
   {
      double sum = 0;
      for (int i = 0; i < 6; i++)
      {
         sum += C[i] / ( 1 + (point[0] - a[i]) * (point[0] - a[i])
            + (point[1] - b[i]) * (point[1] - b[i]) );
      }
      return -sum;
   }
}
