// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoVersion
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Text;

namespace Microsoft.Band
{
  internal class CargoVersion
  {
    private const int maxAppNameCharCount = 5;
    private const int serializedByteCount = 19;

    public string AppName { get; private set; }

    public byte PCBId { get; private set; }

    public ushort VersionMajor { get; private set; }

    public ushort VersionMinor { get; private set; }

    public uint Revision { get; private set; }

    public uint BuildNumber { get; private set; }

    public byte DebugBuild { get; private set; }

    internal static int GetSerializedByteCount() => 19;

    internal static CargoVersion DeserializeFromBand(ICargoReader reader)
    {
      CargoVersion cargoVersion = new CargoVersion();
      using (PooledBuffer buffer = BufferServer.GetBuffer(5))
      {
        reader.ReadExact(buffer.Buffer, 0, buffer.Length);
        int count = 0;
        while (count < buffer.Length && buffer.Buffer[count] != (byte) 0)
          ++count;
        cargoVersion.AppName = Encoding.UTF8.GetString(buffer.Buffer, 0, count);
        cargoVersion.PCBId = reader.ReadByte();
        cargoVersion.VersionMajor = reader.ReadUInt16();
        cargoVersion.VersionMinor = reader.ReadUInt16();
        cargoVersion.Revision = reader.ReadUInt32();
        cargoVersion.BuildNumber = reader.ReadUInt32();
        cargoVersion.DebugBuild = reader.ReadByte();
      }
      return cargoVersion;
    }
  }
}
