// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandUVReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandUVReading : BandSensorReadingBase, IBandUVReading, IBandSensorReading
  {
    private const int cargoSerializedByteCount = 6;
    private const int envoySerializedByteCount = 5;

    private BandUVReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public UVIndexLevel IndexLevel { get; private set; }

    public long ExposureToday { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.uv == null)
        return;
      client.uv.ProcessSensorReading((IBandUVReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header)
    {
      switch (header.SubscriptionType)
      {
        case SubscriptionType.UV:
          return 6;
        case SubscriptionType.UVWithDailyValues:
          return 5;
        default:
          throw new InvalidOperationException();
      }
    }

    internal static BandUVReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandUVReading bandUvReading = new BandUVReading(timestamp);
      if (header.SubscriptionType == SubscriptionType.UV)
      {
        reader.ReadExactAndDiscard(2);
        bandUvReading.IndexLevel = (UVIndexLevel) reader.ReadUInt16();
        reader.ReadExactAndDiscard(2);
      }
      else
      {
        bandUvReading.IndexLevel = (UVIndexLevel) reader.ReadByte();
        bandUvReading.ExposureToday = (long) reader.ReadUInt32();
      }
      return bandUvReading;
    }
  }
}
