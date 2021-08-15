// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorBase
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

namespace Google.Protobuf.Reflection
{
  public abstract class DescriptorBase : IDescriptor
  {
    private readonly FileDescriptor file;
    private readonly string fullName;
    private readonly int index;

    internal DescriptorBase(FileDescriptor file, string fullName, int index)
    {
      this.file = file;
      this.fullName = fullName;
      this.index = index;
    }

    public int Index => this.index;

    public abstract string Name { get; }

    public string FullName => this.fullName;

    public FileDescriptor File => this.file;
  }
}
