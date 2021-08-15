// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Compatibility.PropertyInfoExtensions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.Reflection;

namespace Google.Protobuf.Compatibility
{
  internal static class PropertyInfoExtensions
  {
    internal static MethodInfo GetGetMethod(this PropertyInfo target)
    {
      MethodInfo getMethod = target.GetMethod;
      return getMethod == null || !getMethod.IsPublic ? (MethodInfo) null : getMethod;
    }

    internal static MethodInfo GetSetMethod(this PropertyInfo target)
    {
      MethodInfo setMethod = target.SetMethod;
      return setMethod == null || !setMethod.IsPublic ? (MethodInfo) null : setMethod;
    }
  }
}
