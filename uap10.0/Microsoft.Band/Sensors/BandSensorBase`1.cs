// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.BandSensorBase`1
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
  internal abstract class BandSensorBase<T> : IBandSensor<T> where T : IBandSensorReading
  {
    private object readingsLock;
    private BandClient clientHandle;
    private bool isSupported;
    protected Dictionary<TimeSpan, SubscriptionType> supportedReportingSubscriptions;
    private TimeSpan reportingInterval;

    protected BandSensorBase(
      BandClient bandClient,
      IEnumerable<BandType> supportedBandClasses,
      Dictionary<TimeSpan, SubscriptionType> supportedReportingSubscriptions)
    {
      if (bandClient == null)
        throw new ArgumentNullException(nameof (bandClient));
      if (supportedBandClasses == null)
        throw new ArgumentNullException(nameof (supportedBandClasses));
      if (supportedReportingSubscriptions == null)
        throw new ArgumentNullException(nameof (supportedReportingSubscriptions));
      this.clientHandle = bandClient;
      this.isSupported = supportedBandClasses.Contains<BandType>(bandClient.BandTypeConstants.BandType);
      this.supportedReportingSubscriptions = supportedReportingSubscriptions;
      this.reportingInterval = this.supportedReportingSubscriptions.Keys.FirstOrDefault<TimeSpan>();
      this.readingsLock = new object();
    }

    protected object ReadingsLock => this.readingsLock;

    protected BandClient ClientHandle => this.clientHandle;

    public bool IsSupported => this.isSupported;

    public IEnumerable<TimeSpan> SupportedReportingIntervals => (IEnumerable<TimeSpan>) this.supportedReportingSubscriptions.Keys;

    public TimeSpan ReportingInterval
    {
      get => this.reportingInterval;
      set
      {
        if (!this.supportedReportingSubscriptions.Keys.Contains<TimeSpan>(value))
          throw new ArgumentOutOfRangeException(BandResources.UnsupportedSensorInterval);
        this.reportingInterval = value;
      }
    }

    public event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

    public virtual UserConsent GetCurrentUserConsent() => UserConsent.Granted;

    public Task<bool> RequestUserConsentAsync() => this.RequestUserConsentAsync(CancellationToken.None);

    public virtual Task<bool> RequestUserConsentAsync(CancellationToken token) => Task.FromResult<bool>(true);

    public Task<bool> StartReadingsAsync() => this.StartReadingsAsync(CancellationToken.None);

    public virtual async Task<bool> StartReadingsAsync(CancellationToken token)
    {
      if (!this.isSupported)
        throw new InvalidOperationException(BandResources.UnsupportedSensor);
      switch (this.GetCurrentUserConsent())
      {
        case UserConsent.NotSpecified:
          throw new InvalidOperationException(BandResources.SensorUserConsentNotQueried);
        case UserConsent.Declined:
          return false;
        default:
          SubscriptionType type = this.supportedReportingSubscriptions[this.reportingInterval];
          await Task.Run((Action) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            object readingsLock = this.\u003C\u003E4__this.readingsLock;
            bool lockTaken = false;
            try
            {
              Monitor.Enter(readingsLock, ref lockTaken);
              // ISSUE: reference to a compiler-generated field
              this.\u003C\u003E4__this.clientHandle.SensorSubscribe(type);
            }
            finally
            {
              if (lockTaken)
                Monitor.Exit(readingsLock);
            }
          }), token);
          return true;
      }
    }

    public Task StopReadingsAsync() => this.StopReadingsAsync(CancellationToken.None);

    public virtual Task StopReadingsAsync(CancellationToken token)
    {
      if (!this.isSupported)
        throw new InvalidOperationException(BandResources.UnsupportedSensor);
      SubscriptionType type = this.supportedReportingSubscriptions[this.reportingInterval];
      return Task.Run((Action) (() =>
      {
        object readingsLock = this.readingsLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(readingsLock, ref lockTaken);
          this.clientHandle.SensorUnsubscribe(type);
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(readingsLock);
        }
      }), token);
    }

    public virtual void ProcessSensorReading(T reading)
    {
      if ((object) reading == null)
        throw new ArgumentNullException(nameof (reading));
      EventHandler<BandSensorReadingEventArgs<T>> readingChanged = this.ReadingChanged;
      if (readingChanged == null)
        return;
      if (!this.clientHandle.IsSensorSubscribed(this.supportedReportingSubscriptions[this.reportingInterval]))
        return;
      try
      {
        readingChanged((object) this, new BandSensorReadingEventArgs<T>(reading));
      }
      catch (Exception ex)
      {
        this.clientHandle.loggerProvider.LogException(ProviderLogLevel.Error, ex);
        Environment.FailFast("BandSensorBase.ReadingChanged event handler threw an exception that was not handled by the application.", ex);
      }
    }
  }
}
