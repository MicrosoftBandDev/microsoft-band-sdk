// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.TimeExtensions
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;

namespace Google.Protobuf.WellKnownTypes
{
  public static class TimeExtensions
  {
    public static Timestamp ToTimestamp(this DateTime dateTime) => Timestamp.FromDateTime(dateTime);

    public static Timestamp ToTimestamp(this DateTimeOffset dateTimeOffset) => Timestamp.FromDateTimeOffset(dateTimeOffset);

    public static Duration ToDuration(this TimeSpan timeSpan) => Duration.FromTimeSpan(timeSpan);
  }
}
