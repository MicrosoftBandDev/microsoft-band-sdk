// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.PulseSensor`1
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
  internal abstract class PulseSensor<T> : BandSensorBase<T> where T : IBandSensorReading
  {
    protected PulseSensor(
      BandClient bandClient,
      IEnumerable<BandType> supportedBandClasses,
      Dictionary<TimeSpan, SubscriptionType> supportedReportingSubscriptions)
      : base(bandClient, supportedBandClasses, supportedReportingSubscriptions)
    {
    }

    public override UserConsent GetCurrentUserConsent() => this.ClientHandle.applicationPlatformProvider.GetCurrentSensorConsent(typeof (HeartRateSensor));

    public override async Task<bool> RequestUserConsentAsync(CancellationToken token)
    {
      int num = await this.ClientHandle.applicationPlatformProvider.RequestSensorConsentAsync(typeof (HeartRateSensor), BandResources.HeartRateSensorConsentPrompt, token) ? 1 : 0;
      if (num == 0)
      {
        this.ClientHandle.SensorUnsubscribe(SubscriptionType.HeartRate);
        this.ClientHandle.SensorUnsubscribe(SubscriptionType.RRInterval);
      }
      return num != 0;
    }
  }
}
