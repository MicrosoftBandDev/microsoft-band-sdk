// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.DisposableList`1
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Band
{
  internal class DisposableList<T> : List<T>, IDisposable where T : IDisposable
  {
    public DisposableList()
    {
    }

    public DisposableList(int capacity)
      : base(capacity)
    {
    }

    public DisposableList(IEnumerable<T> collection)
      : base(collection)
    {
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    public void Dispose()
    {
      foreach (T obj in (List<T>) this)
      {
        IDisposable disposable = (IDisposable) obj;
        try
        {
          disposable?.Dispose();
        }
        catch
        {
        }
      }
      this.Clear();
    }
  }
}
