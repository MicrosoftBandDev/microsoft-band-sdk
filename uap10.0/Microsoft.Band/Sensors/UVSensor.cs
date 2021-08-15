// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.UVSensor
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Band.Sensors
{
  internal sealed class UVSensor : BandSensorBase<IBandUVReading>
  {
    public UVSensor(BandClient bandClient)
      : base(bandClient, (IEnumerable<BandType>) new List<BandType>()
      {
        BandType.Cargo,
        BandType.Envoy
      }, new Dictionary<TimeSpan, SubscriptionType>()
      {
        {
          TimeSpan.FromMinutes(1.0),
          bandClient.BandTypeConstants.GetBandSpecificValue<SubscriptionType>(SubscriptionType.UV, SubscriptionType.UVWithDailyValues)
        }
      })
    {
    }
  }
}
