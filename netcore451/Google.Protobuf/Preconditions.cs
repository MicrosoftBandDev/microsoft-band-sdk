// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Preconditions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;

namespace Google.Protobuf
{
  public static class Preconditions
  {
    public static T CheckNotNull<T>(T value, string name) where T : class => (object) value != null ? value : throw new ArgumentNullException(name);

    internal static T CheckNotNullUnconstrained<T>(T value, string name) => (object) value != null ? value : throw new ArgumentNullException(name);
  }
}
