// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.GeneratedCodeInfo
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

namespace Google.Protobuf.Reflection
{
  public sealed class GeneratedCodeInfo
  {
    private static readonly string[] EmptyNames = new string[0];
    private static readonly GeneratedCodeInfo[] EmptyCodeInfo = new GeneratedCodeInfo[0];

    public System.Type ClrType { get; private set; }

    public string[] PropertyNames { get; private set; }

    public string[] OneofNames { get; private set; }

    public GeneratedCodeInfo[] NestedTypes { get; private set; }

    public System.Type[] NestedEnums { get; private set; }

    public GeneratedCodeInfo(
      System.Type clrType,
      string[] propertyNames,
      string[] oneofNames,
      System.Type[] nestedEnums,
      GeneratedCodeInfo[] nestedTypes)
    {
      this.NestedTypes = nestedTypes ?? GeneratedCodeInfo.EmptyCodeInfo;
      this.NestedEnums = nestedEnums ?? ReflectionUtil.EmptyTypes;
      this.ClrType = clrType;
      this.PropertyNames = propertyNames ?? GeneratedCodeInfo.EmptyNames;
      this.OneofNames = oneofNames ?? GeneratedCodeInfo.EmptyNames;
    }

    public GeneratedCodeInfo(System.Type[] nestedEnums, GeneratedCodeInfo[] nestedTypes)
      : this((System.Type) null, (string[]) null, (string[]) null, nestedEnums, nestedTypes)
    {
    }
  }
}
