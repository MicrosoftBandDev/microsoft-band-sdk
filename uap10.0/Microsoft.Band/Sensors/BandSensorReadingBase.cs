// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandSensorReadingBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Sensors
{
  internal abstract class BandSensorReadingBase : IBandSensorReading
  {
    protected BandSensorReadingBase(DateTimeOffset timestamp) => this.Timestamp = timestamp;

    public DateTimeOffset Timestamp { get; private set; }

    internal abstract void Dispatch(BandClient client);
  }
}
