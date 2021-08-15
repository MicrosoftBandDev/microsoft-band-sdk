// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandSkinTemperatureReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandSkinTemperatureReading : 
    BandSensorReadingBase,
    IBandSkinTemperatureReading,
    IBandSensorReading
  {
    private const int ConversionFactor = 100;
    private const int serializedByteCount = 10;

    private BandSkinTemperatureReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public double Temperature { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.skinTemperature == null)
        return;
      client.skinTemperature.ProcessSensorReading((IBandSkinTemperatureReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 10;

    internal static BandSkinTemperatureReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandSkinTemperatureReading temperatureReading = new BandSkinTemperatureReading(timestamp);
      temperatureReading.Temperature = (double) reader.ReadInt16() / 100.0;
      reader.ReadExactAndDiscard(8);
      return temperatureReading;
    }
  }
}
