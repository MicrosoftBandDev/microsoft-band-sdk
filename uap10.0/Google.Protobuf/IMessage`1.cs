// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.IMessage`1
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;

namespace Google.Protobuf
{
  public interface IMessage<T> : IMessage, IEquatable<T>, IDeepCloneable<T>
    where T : IMessage<T>
  {
    void MergeFrom(T message);
  }
}
