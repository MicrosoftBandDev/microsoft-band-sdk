// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.ContactSensor
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
  internal sealed class ContactSensor : 
    BandSensorBase<IBandContactReading>,
    IBandContactSensor,
    IBandSensor<IBandContactReading>
  {
    private IBandContactReading lastReading;

    public ContactSensor(BandClient bandClient)
      : base(bandClient, (IEnumerable<BandType>) new List<BandType>()
      {
        BandType.Cargo,
        BandType.Envoy
      }, new Dictionary<TimeSpan, SubscriptionType>()
      {
        {
          TimeSpan.Zero,
          SubscriptionType.DeviceContact
        }
      })
    {
    }

    public event EventHandler<BandSensorReadingEventArgs<IBandContactReading>> InternalReadingChanged;

    public Task<IBandContactReading> GetCurrentStateAsync() => Task.Run<IBandContactReading>((Func<IBandContactReading>) (() =>
    {
      object readingsLock = this.ReadingsLock;
      bool lockTaken = false;
      try
      {
        Monitor.Enter(readingsLock, ref lockTaken);
        if (this.ClientHandle.IsSensorSubscribed(this.supportedReportingSubscriptions[this.ReportingInterval]))
          return this.lastReading;
        TaskCompletionSource<IBandContactReading> contactTask = new TaskCompletionSource<IBandContactReading>();
        this.InternalReadingChanged += (EventHandler<BandSensorReadingEventArgs<IBandContactReading>>) ((sender, args) => contactTask.TrySetResult(args.SensorReading));
        this.ClientHandle.SensorSubscribe(SubscriptionType.DeviceContact);
        IBandContactReading result = contactTask.Task.Result;
        this.ClientHandle.SensorUnsubscribe(SubscriptionType.DeviceContact);
        return result;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(readingsLock);
      }
    }));

    public override void ProcessSensorReading(IBandContactReading reading)
    {
      this.lastReading = reading != null ? reading : throw new ArgumentNullException(nameof (reading));
      if (this.InternalReadingChanged != null)
        this.InternalReadingChanged((object) this, new BandSensorReadingEventArgs<IBandContactReading>(reading));
      base.ProcessSensorReading(reading);
    }
  }
}
