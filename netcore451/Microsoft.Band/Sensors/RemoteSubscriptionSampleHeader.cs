// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.RemoteSubscriptionSampleHeader
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Sensors
{
  internal class RemoteSubscriptionSampleHeader
  {
    private const int serializedByteCount = 4;

    private RemoteSubscriptionSampleHeader()
    {
    }

    public SubscriptionType SubscriptionType { get; private set; }

    public byte NumMissedSamples { get; private set; }

    public short SampleSize { get; private set; }

    internal static int GetSerializedByteCount() => 4;

    internal static RemoteSubscriptionSampleHeader DeserializeFromBand(
      ICargoReader reader)
    {
      return new RemoteSubscriptionSampleHeader()
      {
        SubscriptionType = (SubscriptionType) reader.ReadByte(),
        NumMissedSamples = reader.ReadByte(),
        SampleSize = reader.ReadInt16()
      };
    }
  }
}
