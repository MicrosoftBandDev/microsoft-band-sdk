// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandBarometerReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandBarometerReading : 
    BandSensorReadingBase,
    IBandBarometerReading,
    IBandSensorReading
  {
    private const double AirPressureConversionFactor = 4096.0;
    private const double TemperatureConversionFactor = 480.0;
    private const double TemperatureConversionOffset = 42.5;
    private const int serializedByteCount = 6;

    private BandBarometerReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public double AirPressure { get; private set; }

    public double Temperature { get; private set; }

    internal override void Dispatch(BandClient client)
    {
      if (client.barometer == null)
        return;
      client.barometer.ProcessSensorReading((IBandBarometerReading) this);
    }

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 6;

    internal static BandBarometerReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      return new BandBarometerReading(timestamp)
      {
        AirPressure = (double) reader.ReadInt32() / 4096.0,
        Temperature = (double) reader.ReadInt16() / 480.0 + 42.5
      };
    }
  }
}
