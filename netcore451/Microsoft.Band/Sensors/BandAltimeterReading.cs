// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandAltimeterReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandAltimeterReading : 
    BandSensorReadingBase,
    IBandAltimeterReading,
    IBandSensorReading
  {
    private const int cargoSerializedByteCount = 50;
    private const int envoySerializedByteCount = 42;

    private BandAltimeterReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public long TotalGain { get; private set; }

    public long TotalLoss { get; private set; }

    public long SteppingGain { get; private set; }

    public long SteppingLoss { get; private set; }

    public long StepsAscended { get; private set; }

    public long StepsDescended { get; private set; }

    public float Rate { get; private set; }

    public long FlightsAscended { get; private set; }

    public long FlightsDescended { get; private set; }

    public long FlightsAscendedToday { get; private set; }

    public long TotalGainToday { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.altimeter == null)
        return;
      client.altimeter.ProcessSensorReading((IBandAltimeterReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header)
    {
      switch (header.SubscriptionType)
      {
        case SubscriptionType.Elevation:
          return 50;
        case SubscriptionType.ElevationWithDailyValues:
          return 42;
        default:
          throw new InvalidOperationException();
      }
    }

    internal static BandAltimeterReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandAltimeterReading altimeterReading = new BandAltimeterReading(timestamp);
      if (header.SubscriptionType == SubscriptionType.ElevationWithDailyValues)
      {
        altimeterReading.TotalGain = (long) reader.ReadUInt32();
        altimeterReading.TotalLoss = (long) reader.ReadUInt32();
        altimeterReading.SteppingGain = (long) reader.ReadUInt32();
        altimeterReading.SteppingLoss = (long) reader.ReadUInt32();
        altimeterReading.StepsAscended = (long) reader.ReadUInt32();
        altimeterReading.StepsDescended = (long) reader.ReadUInt32();
        altimeterReading.Rate = (float) reader.ReadInt16();
        altimeterReading.FlightsAscended = (long) reader.ReadUInt32();
        altimeterReading.FlightsDescended = (long) reader.ReadUInt32();
        altimeterReading.FlightsAscendedToday = (long) reader.ReadUInt32();
        altimeterReading.TotalGainToday = (long) reader.ReadUInt32();
      }
      else
      {
        reader.ReadExactAndDiscard(8);
        altimeterReading.TotalGain = (long) reader.ReadUInt32();
        altimeterReading.TotalLoss = (long) reader.ReadUInt32();
        altimeterReading.SteppingGain = (long) reader.ReadUInt32();
        altimeterReading.SteppingLoss = (long) reader.ReadUInt32();
        reader.ReadExactAndDiscard(8);
        altimeterReading.StepsAscended = (long) reader.ReadUInt32();
        altimeterReading.StepsDescended = (long) reader.ReadUInt32();
        altimeterReading.Rate = (float) reader.ReadInt16();
        altimeterReading.FlightsAscended = (long) reader.ReadUInt32();
        altimeterReading.FlightsDescended = (long) reader.ReadUInt32();
      }
      return altimeterReading;
    }
  }
}
