// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.EnumerableExtensions
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Band
{
  internal static class EnumerableExtensions
  {
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, T additional)
    {
      foreach (T obj in first)
        yield return obj;
      yield return additional;
    }

    public static IEnumerable<T> Concat<T>(T first, T second)
    {
      yield return first;
      yield return second;
    }

    public static IEnumerable<T> Concat<T>(T first, IEnumerable<T> additional)
    {
      yield return first;
      foreach (T obj in additional)
        yield return obj;
    }

    public static IEnumerable<T> TakeAddDefaults<T>(
      this IEnumerable<T> items,
      int count,
      T defaultValue = null)
    {
      foreach (T obj in items.Take<T>(count))
      {
        --count;
        yield return obj;
      }
      while (count-- > 0)
        yield return defaultValue;
    }

    public static IEnumerable<T> AsEnumerable<T>(this T item)
    {
      yield return item;
    }
  }
}
