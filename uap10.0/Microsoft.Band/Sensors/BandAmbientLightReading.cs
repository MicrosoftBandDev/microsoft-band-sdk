// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandAmbientLightReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandAmbientLightReading : 
    BandSensorReadingBase,
    IBandAmbientLightReading,
    IBandSensorReading
  {
    private const int serializedByteCount = 2;

    private BandAmbientLightReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public int Brightness { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.als == null)
        return;
      client.als.ProcessSensorReading((IBandAmbientLightReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 2;

    internal static BandAmbientLightReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      return new BandAmbientLightReading(timestamp)
      {
        Brightness = (int) reader.ReadUInt16()
      };
    }
  }
}
