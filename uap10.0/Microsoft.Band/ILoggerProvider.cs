// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.ILoggerProvider
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Band
{
  internal interface ILoggerProvider
  {
    void Log(ProviderLogLevel level, string message, params object[] args);

    void LogException(ProviderLogLevel level, Exception e);

    void LogWebException(ProviderLogLevel level, WebException e);

    void LogException(ProviderLogLevel level, Exception e, string message, params object[] args);

    void PerfStart(string eventName);

    void PerfEnd(string eventName);

    void TelemetryEvent(
      string eventName,
      IDictionary<string, string> properties,
      IDictionary<string, double> metrics);
  }
}
