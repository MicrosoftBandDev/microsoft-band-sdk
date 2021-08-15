// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.SingleFieldAccessor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  internal sealed class SingleFieldAccessor : FieldAccessorBase
  {
    private readonly Action<IMessage, object> setValueDelegate;
    private readonly Action<IMessage> clearDelegate;

    internal SingleFieldAccessor(PropertyInfo property, FieldDescriptor descriptor)
      : base(property, descriptor)
    {
      this.setValueDelegate = property.CanWrite ? ReflectionUtil.CreateActionIMessageObject(property.GetSetMethod()) : throw new ArgumentException("Not all required properties/methods available");
      System.Type propertyType = property.PropertyType;
      object defaultValue = typeof (IMessage).IsAssignableFrom(propertyType) ? (object) null : (propertyType == typeof (string) ? (object) "" : (propertyType == typeof (ByteString) ? (object) ByteString.Empty : Activator.CreateInstance(propertyType)));
      this.clearDelegate = (Action<IMessage>) (message => this.SetValue(message, defaultValue));
    }

    public override void Clear(IMessage message) => this.clearDelegate(message);

    public override void SetValue(IMessage message, object value) => this.setValueDelegate(message, value);
  }
}
