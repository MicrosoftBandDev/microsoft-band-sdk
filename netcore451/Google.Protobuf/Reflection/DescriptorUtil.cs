// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorUtil
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Protobuf.Reflection
{
  internal static class DescriptorUtil
  {
    internal static IList<TOutput> ConvertAndMakeReadOnly<TInput, TOutput>(
      IList<TInput> input,
      DescriptorUtil.IndexedConverter<TInput, TOutput> converter)
    {
      TOutput[] outputArray = new TOutput[input.Count];
      for (int index = 0; index < outputArray.Length; ++index)
        outputArray[index] = converter(input[index], index);
      return (IList<TOutput>) new ReadOnlyCollection<TOutput>((IList<TOutput>) outputArray);
    }

    internal delegate TOutput IndexedConverter<TInput, TOutput>(TInput element, int index);
  }
}
