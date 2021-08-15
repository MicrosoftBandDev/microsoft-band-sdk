// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandGsrReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandGsrReading : BandSensorReadingBase, IBandGsrReading, IBandSensorReading
  {
    private const int serializedByteCount = 7;

    private BandGsrReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public int Resistance { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.gsr == null)
        return;
      client.gsr.ProcessSensorReading((IBandGsrReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 7;

    internal static BandGsrReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandGsrReading bandGsrReading = new BandGsrReading(timestamp);
      reader.ReadExactAndDiscard(1);
      bandGsrReading.Resistance = (int) reader.ReadUInt32();
      reader.ReadExactAndDiscard(2);
      return bandGsrReading;
    }
  }
}
