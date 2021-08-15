// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FieldAccessorBase
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  internal abstract class FieldAccessorBase : IFieldAccessor
  {
    private readonly Func<IMessage, object> getValueDelegate;
    private readonly FieldDescriptor descriptor;

    internal FieldAccessorBase(PropertyInfo property, FieldDescriptor descriptor)
    {
      this.descriptor = descriptor;
      this.getValueDelegate = ReflectionUtil.CreateFuncIMessageObject(property.GetGetMethod());
    }

    public FieldDescriptor Descriptor => this.descriptor;

    public object GetValue(IMessage message) => this.getValueDelegate(message);

    public abstract void Clear(IMessage message);

    public abstract void SetValue(IMessage message, object value);
  }
}
