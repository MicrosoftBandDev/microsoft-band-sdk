// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandContactReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandContactReading : BandSensorReadingBase, IBandContactReading, IBandSensorReading
  {
    private const int serializedByteCount = 3;

    private BandContactReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public BandContactState State { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.contact == null)
        return;
      client.contact.ProcessSensorReading((IBandContactReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 3;

    internal static BandContactReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandContactReading bandContactReading = new BandContactReading(timestamp);
      bandContactReading.State = (BandContactState) reader.ReadByte();
      reader.ReadExactAndDiscard(2);
      return bandContactReading;
    }
  }
}
