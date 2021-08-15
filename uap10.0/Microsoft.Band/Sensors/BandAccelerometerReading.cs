// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandAccelerometerReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal class BandAccelerometerReading : 
    BandSensorReadingBase,
    IBandAccelerometerReading,
    IBandSensorReading
  {
    private const double ConversionFactor = 0.000244140625;
    protected const int serializedByteCount = 6;

    protected BandAccelerometerReading(DateTimeOffset timestamp)
      : base(timestamp)
    {
    }

    public double AccelerationX { get; private set; }

    public double AccelerationY { get; private set; }

    public double AccelerationZ { get; private set; }

    internal override void Dispatch(BandClient client) => client.accelerometer.ProcessSensorReading((IBandAccelerometerReading) this);

    internal static int GetSerializedByteCount(RemoteSubscriptionSampleHeader header) => 6;

    internal static BandAccelerometerReading DeserializeFromBand(
      ICargoReader reader,
      RemoteSubscriptionSampleHeader header,
      DateTimeOffset timestamp)
    {
      BandAccelerometerReading accelerometerReading = new BandAccelerometerReading(timestamp);
      accelerometerReading.DeserializeFromBand(reader);
      return accelerometerReading;
    }

    protected void DeserializeFromBand(ICargoReader reader)
    {
      this.AccelerationX = (double) reader.ReadInt16() * 0.000244140625;
      this.AccelerationY = (double) reader.ReadInt16() * 0.000244140625;
      this.AccelerationZ = (double) reader.ReadInt16() * 0.000244140625;
    }
  }
}
