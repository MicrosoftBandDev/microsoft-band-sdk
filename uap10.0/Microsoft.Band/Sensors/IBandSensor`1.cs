// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.IBandSensor`1
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Sensors
{
  public interface IBandSensor<T> where T : IBandSensorReading
  {
    bool IsSupported { get; }

    IEnumerable<TimeSpan> SupportedReportingIntervals { get; }

    TimeSpan ReportingInterval { get; set; }

    event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

    UserConsent GetCurrentUserConsent();

    Task<bool> RequestUserConsentAsync();

    Task<bool> RequestUserConsentAsync(CancellationToken token);

    Task<bool> StartReadingsAsync();

    Task<bool> StartReadingsAsync(CancellationToken token);

    Task StopReadingsAsync();

    Task StopReadingsAsync(CancellationToken token);
  }
}
