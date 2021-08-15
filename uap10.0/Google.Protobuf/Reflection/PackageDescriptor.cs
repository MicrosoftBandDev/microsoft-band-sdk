// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.PackageDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

namespace Google.Protobuf.Reflection
{
  internal sealed class PackageDescriptor : IDescriptor
  {
    private readonly string name;
    private readonly string fullName;
    private readonly FileDescriptor file;

    internal PackageDescriptor(string name, string fullName, FileDescriptor file)
    {
      this.file = file;
      this.fullName = fullName;
      this.name = name;
    }

    public string Name => this.name;

    public string FullName => this.fullName;

    public FileDescriptor File => this.file;
  }
}
