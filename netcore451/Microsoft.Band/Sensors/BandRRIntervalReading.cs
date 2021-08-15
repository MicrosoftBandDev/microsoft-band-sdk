// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandRRIntervalReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandRRIntervalReading : 
    BandSensorReadingBase,
    IBandRRIntervalReading,
    IBandSensorReading
  {
    private const double RRIntervalConversionFactor = 0.016592;
    private const int serializedByteCount = 6;

    private BandRRIntervalReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public double Interval { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.rrInterval == null)
        return;
      client.rrInterval.ProcessSensorReading((IBandRRIntervalReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 6;

    internal static BandRRIntervalReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      reader.ReadExactAndDiscard(4);
      return new BandRRIntervalReading(timestamp)
      {
        Interval = (double) reader.ReadUInt16() * 0.016592
      };
    }
  }
}
