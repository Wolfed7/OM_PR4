﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM_PR4;

// Класс N - мерной точки (является аргументом многомерной функций)
public class PointND : IEnumerable<double>, ICloneable
{
   private List<double> _variables;
   public int Dimention { get; private set; }

   public double this[int index]
   {
      get => _variables[index];
      set => _variables[index] = value;
   }

   public PointND(int dimension = 0)
   {
      _variables = new(dimension);
      for (int i = 0; i < _variables.Capacity; i++)
         _variables.Add(0);
      Dimention = dimension;
   }

   public void Fill(double value)
   {
      for (int i = 0; i < Dimention; i++)
         _variables[i] = value;
   }

   public void Clear()
   {
      for (int i = 0; i < Dimention; i++)
         _variables[i] = 0;
   }

   public double Norm()
   {
      double result = 0.0;

      for (int i = 0; i < Dimention; i++)
         result += _variables[i] * _variables[i];

      return Math.Sqrt(result);
   }

   public override string ToString()
   {
      string str = "";
      for (int i = 0; i < Dimention; i++)
      {
         str += _variables[i].ToString("f8");
         if (i != Dimention - 1) str += " ";
      }
      return str + "";
   }

   public static PointND operator -(PointND point1, PointND point2)
   {
      PointND result = new(point1.Dimention);

      for (int i = 0; i < result.Dimention; i++)
         result[i] = point1[i] - point2[i];

      return result;
   }

   public static PointND operator +(PointND point1, PointND point2)
   {
      PointND result = new(point1.Dimention);

      for (int i = 0; i < result.Dimention; i++)
         result[i] = point1[i] + point2[i];

      return result;
   }

   public static PointND operator *(double constant, PointND point)
   {
      PointND result = new(point.Dimention);

      for (int i = 0; i < result.Dimention; i++)
         result[i] = constant * point[i];

      return result;
   }

   public static PointND operator /(PointND point, double constant)
   {
      PointND result = new(point.Dimention);

      for (int i = 0; i < result.Dimention; i++)
         result[i] = point[i] / constant;

      return result;
   }

   public object Clone()
   {
      var point = new PointND(Dimention);
      point._variables = this._variables.ToList();

      return point;
   }

   public static PointND Parse(double[] array)
   {
      PointND point = new(array.Length);
      for (int i = 0; i < point.Dimention; i++)
      {
         point[i] = array[i];
      }
      return point;
   }

   IEnumerator IEnumerable.GetEnumerator() 
      => _variables.GetEnumerator();

   public void Add(double variable)
   {
      _variables.Add(variable);
      Dimention = _variables.Count;
   }

   public IEnumerator<double> GetEnumerator() 
      => _variables.GetEnumerator();
}