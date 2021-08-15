// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandHeartRateReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandHeartRateReading : 
    BandSensorReadingBase,
    IBandHeartRateReading,
    IBandSensorReading
  {
    private const int HeartRateLockedMinimumQualityValue = 6;
    private const int serializedByteCount = 2;

    private BandHeartRateReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public int HeartRate { get; private set; }

    public HeartRateQuality Quality { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.heartRate == null)
        return;
      client.heartRate.ProcessSensorReading((IBandHeartRateReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 2;

    internal static BandHeartRateReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      return new BandHeartRateReading(timestamp)
      {
        HeartRate = (int) reader.ReadByte(),
        Quality = reader.ReadByte() >= (byte) 6 ? HeartRateQuality.Locked : HeartRateQuality.Acquiring
      };
    }
  }
}
