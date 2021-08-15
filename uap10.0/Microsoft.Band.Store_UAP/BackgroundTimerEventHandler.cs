// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BackgroundTimerEventHandler
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  public static class BackgroundTimerEventHandler
  {
    public static async Task<bool> TryHandle(
      ValueSet request,
      Func<BandBackgroundTimerEventArgs<IBandBackgroundTimerEvent>, Task> handler)
    {
      object obj;
      if (!((IDictionary<string, object>) request).TryGetValue(InternalBandControllerNames.RequestCommandParameterName, out obj) || !InternalBandTimerEventSubscriptionNames.TimerTickCommandName.Equals(obj))
        return false;
      await handler(new BandBackgroundTimerEventArgs<IBandBackgroundTimerEvent>((IBandBackgroundTimerEvent) BackgroundTimerEvent.FromValueSet(request)));
      return true;
    }
  }
}
