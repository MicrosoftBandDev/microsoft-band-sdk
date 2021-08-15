// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.OneofAccessor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Compatibility;
using System;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  public sealed class OneofAccessor
  {
    private readonly Func<IMessage, int> caseDelegate;
    private readonly Action<IMessage> clearDelegate;
    private OneofDescriptor descriptor;

    internal OneofAccessor(
      PropertyInfo caseProperty,
      MethodInfo clearMethod,
      OneofDescriptor descriptor)
    {
      if (!caseProperty.CanRead)
        throw new ArgumentException("Cannot read from property");
      this.descriptor = descriptor;
      this.caseDelegate = ReflectionUtil.CreateFuncIMessageT<int>(caseProperty.GetGetMethod());
      this.descriptor = descriptor;
      this.clearDelegate = ReflectionUtil.CreateActionIMessage(clearMethod);
    }

    public OneofDescriptor Descriptor => this.descriptor;

    public void Clear(IMessage message) => this.clearDelegate(message);

    public FieldDescriptor GetCaseFieldDescriptor(IMessage message)
    {
      int number = this.caseDelegate(message);
      return number > 0 ? this.descriptor.ContainingType.FindFieldByNumber(number) : (FieldDescriptor) null;
    }
  }
}
