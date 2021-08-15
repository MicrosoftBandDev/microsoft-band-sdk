// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.InternalBandTimerEventSubscriptionNames
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

namespace Microsoft.Band
{
  public static class InternalBandTimerEventSubscriptionNames
  {
    public static string CommandPrefix => "timer.event.";

    public static string SetTimerCommandName => "timer.event.set";

    public static string StopTimersCommandName => "timer.event.stop";

    public static string DoesTimerExistsCommandName => "timer.event.checkexists";

    public static string TimerTickCommandName => "timer.event.tick";

    public static string RequestOneTimerIdParameterName => "TimerId";

    public static string RequestManyTimerIdsParameterName => "NTimerIds";

    public static string RequestTimerTypeParameterName => "TimerType";

    public static string RequestIntervalParameterName => "Interval";

    public static string RequestDelayParameterName => "Delay";

    public static string ResponseTimerExistsParameterName => "TimerExists";
  }
}
