// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.GyroscopeSensor
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Sensors
{
  internal sealed class GyroscopeSensor : BandSensorBase<IBandGyroscopeReading>
  {
    public GyroscopeSensor(BandClient bandClient)
      : base(bandClient, (IEnumerable<BandType>) new List<BandType>()
      {
        BandType.Cargo,
        BandType.Envoy
      }, new Dictionary<TimeSpan, SubscriptionType>()
      {
        {
          TimeSpan.FromMilliseconds(16.0),
          SubscriptionType.AccelerometerGyroscope16MS
        },
        {
          TimeSpan.FromMilliseconds(32.0),
          SubscriptionType.AccelerometerGyroscope32MS
        },
        {
          TimeSpan.FromMilliseconds(128.0),
          SubscriptionType.AccelerometerGyroscope128MS
        }
      })
    {
    }
  }
}
