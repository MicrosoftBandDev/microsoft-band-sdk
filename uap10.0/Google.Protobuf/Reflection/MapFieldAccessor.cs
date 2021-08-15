// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.MapFieldAccessor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  internal sealed class MapFieldAccessor : FieldAccessorBase
  {
    internal MapFieldAccessor(PropertyInfo property, FieldDescriptor descriptor)
      : base(property, descriptor)
    {
    }

    public override void Clear(IMessage message) => ((IDictionary) this.GetValue(message)).Clear();

    public override void SetValue(IMessage message, object value) => throw new InvalidOperationException("SetValue is not implemented for map fields");
  }
}
