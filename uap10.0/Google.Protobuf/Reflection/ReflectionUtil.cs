// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.ReflectionUtil
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
  internal static class ReflectionUtil
  {
    internal static readonly System.Type[] EmptyTypes = new System.Type[0];

    internal static Func<IMessage, object> CreateFuncIMessageObject(MethodInfo method) => ((Expression<Func<IMessage, object>>) (p => (object) Expression.Call((Expression) Expression.Convert(p, method.DeclaringType), method))).Compile();

    internal static Func<IMessage, T> CreateFuncIMessageT<T>(MethodInfo method) => ((Expression<Func<IMessage, T>>) (p => (T) Expression.Call((Expression) Expression.Convert(p, method.DeclaringType), method))).Compile();

    internal static Action<IMessage, object> CreateActionIMessageObject(
      MethodInfo method)
    {
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (IMessage), "target");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (object), "arg");
      Expression instance = (Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType);
      Expression expression = (Expression) Expression.Convert((Expression) parameterExpression2, method.GetParameters()[0].ParameterType);
      return Expression.Lambda<Action<IMessage, object>>((Expression) Expression.Call(instance, method, expression), parameterExpression1, parameterExpression2).Compile();
    }

    internal static Action<IMessage> CreateActionIMessage(MethodInfo method) => ((Expression<Action<IMessage>>) (target => Expression.Call((Expression) Expression.Convert(target, method.DeclaringType), method))).Compile();
  }
}
