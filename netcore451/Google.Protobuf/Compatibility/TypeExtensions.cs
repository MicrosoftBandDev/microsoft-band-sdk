// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Compatibility.TypeExtensions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Reflection;

namespace Google.Protobuf.Compatibility
{
  internal static class TypeExtensions
  {
    internal static bool IsValueType(this Type target) => target.GetTypeInfo().IsValueType;

    internal static bool IsAssignableFrom(this Type target, Type c) => target.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());

    internal static PropertyInfo GetProperty(this Type target, string name)
    {
      TypeInfo typeInfo;
      for (; target != null; target = typeInfo.BaseType)
      {
        typeInfo = target.GetTypeInfo();
        PropertyInfo declaredProperty = typeInfo.GetDeclaredProperty(name);
        if (declaredProperty != null && (declaredProperty.CanRead && declaredProperty.GetMethod.IsPublic || declaredProperty.CanWrite && declaredProperty.SetMethod.IsPublic))
          return declaredProperty;
      }
      return (PropertyInfo) null;
    }

    internal static MethodInfo GetMethod(this Type target, string name)
    {
      TypeInfo typeInfo;
      for (; target != null; target = typeInfo.BaseType)
      {
        typeInfo = target.GetTypeInfo();
        MethodInfo declaredMethod = typeInfo.GetDeclaredMethod(name);
        if (declaredMethod != null && declaredMethod.IsPublic)
          return declaredMethod;
      }
      return (MethodInfo) null;
    }
  }
}
