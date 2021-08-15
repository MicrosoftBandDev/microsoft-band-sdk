// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandPedometerReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandPedometerReading : 
    BandSensorReadingBase,
    IBandPedometerReading,
    IBandSensorReading
  {
    private const int cargoSerializedByteCount = 13;
    private const int envoySerializedByteCount = 8;

    private BandPedometerReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public long TotalSteps { get; private set; }

    public long StepsToday { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.pedometer == null)
        return;
      client.pedometer.ProcessSensorReading((IBandPedometerReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header)
    {
      switch (header.SubscriptionType)
      {
        case SubscriptionType.Pedometer:
          return 13;
        case SubscriptionType.PedometerWithDailyValues:
          return 8;
        default:
          throw new InvalidOperationException();
      }
    }

    internal static BandPedometerReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandPedometerReading pedometerReading = new BandPedometerReading(timestamp);
      if (header.SubscriptionType == SubscriptionType.Pedometer)
      {
        pedometerReading.TotalSteps = (long) reader.ReadUInt32();
        reader.ReadExactAndDiscard(9);
      }
      else if (header.SubscriptionType == SubscriptionType.PedometerWithDailyValues)
      {
        pedometerReading.TotalSteps = (long) reader.ReadUInt32();
        pedometerReading.StepsToday = (long) reader.ReadUInt32();
      }
      return pedometerReading;
    }
  }
}
