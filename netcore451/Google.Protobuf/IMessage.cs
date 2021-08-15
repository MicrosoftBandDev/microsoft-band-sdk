// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.IMessage
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;

namespace Google.Protobuf
{
  public interface IMessage
  {
    void MergeFrom(CodedInputStream input);

    void WriteTo(CodedOutputStream output);

    int CalculateSize();

    MessageDescriptor Descriptor { get; }
  }
}
