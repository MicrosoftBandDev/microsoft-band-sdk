// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorValidationException
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;

namespace Google.Protobuf.Reflection
{
  public sealed class DescriptorValidationException : Exception
  {
    private readonly string name;
    private readonly string description;

    public string ProblemSymbolName => this.name;

    public string Description => this.description;

    internal DescriptorValidationException(IDescriptor problemDescriptor, string description)
      : base(problemDescriptor.FullName + ": " + description)
    {
      this.name = problemDescriptor.FullName;
      this.description = description;
    }

    internal DescriptorValidationException(
      IDescriptor problemDescriptor,
      string description,
      Exception cause)
      : base(problemDescriptor.FullName + ": " + description, cause)
    {
      this.name = problemDescriptor.FullName;
      this.description = description;
    }
  }
}
