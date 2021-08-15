// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandDistanceReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal sealed class BandDistanceReading : 
    BandSensorReadingBase,
    IBandDistanceReading,
    IBandSensorReading
  {
    private const int cargoSerializedByteCount = 22;
    private const int envoySerializedByteCount = 17;

    private BandDistanceReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public long TotalDistance { get; private set; }

    public long DistanceToday { get; private set; }

    public double Speed { get; private set; }

    public double Pace { get; private set; }

    public MotionType CurrentMotion { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.distance == null)
        return;
      client.distance.ProcessSensorReading((IBandDistanceReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header)
    {
      switch (header.SubscriptionType)
      {
        case SubscriptionType.Distance:
          return 22;
        case SubscriptionType.DistanceWithDailyValues:
          return 17;
        default:
          throw new InvalidOperationException();
      }
    }

    internal static BandDistanceReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandDistanceReading bandDistanceReading = new BandDistanceReading(timestamp);
      if (header.SubscriptionType == SubscriptionType.Distance)
      {
        bandDistanceReading.TotalDistance = (long) reader.ReadUInt32();
        reader.ReadExactAndDiscard(8);
        bandDistanceReading.Speed = (double) reader.ReadUInt32();
        bandDistanceReading.Pace = (double) reader.ReadUInt32();
        bandDistanceReading.CurrentMotion = MotionType.Idle;
        reader.ReadExactAndDiscard(2);
      }
      else if (header.SubscriptionType == SubscriptionType.DistanceWithDailyValues)
      {
        bandDistanceReading.TotalDistance = (long) reader.ReadUInt32();
        bandDistanceReading.Speed = (double) reader.ReadUInt32();
        bandDistanceReading.Pace = (double) reader.ReadUInt32();
        bandDistanceReading.CurrentMotion = (MotionType) reader.ReadByte();
        bandDistanceReading.DistanceToday = (long) reader.ReadUInt32();
      }
      return bandDistanceReading;
    }
  }
}
