// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandTimerEventSubscriptions
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  public static class BandTimerEventSubscriptions
  {
    public static async Task SetTimerAsync(
      string timerId,
      BandTimerType timerType,
      TimeSpan interval,
      TimeSpan delay)
    {
      ValueSet requestMessage = new ValueSet();
      ((IDictionary<string, object>) requestMessage).Add(InternalBandControllerNames.RequestCommandParameterName, (object) InternalBandTimerEventSubscriptionNames.SetTimerCommandName);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestOneTimerIdParameterName, (object) timerId);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestIntervalParameterName, (object) interval);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestTimerTypeParameterName, (object) (int) timerType);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestDelayParameterName, (object) delay);
      BandControllerSubscriptions.CheckResponse(await BandControllerSubscriptions.SendRequestToBandControllerServiceAsync(requestMessage));
    }

    public static async Task StopTimersAsync(string timerIds)
    {
      ValueSet requestMessage = new ValueSet();
      ((IDictionary<string, object>) requestMessage).Add(InternalBandControllerNames.RequestCommandParameterName, (object) InternalBandTimerEventSubscriptionNames.StopTimersCommandName);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestManyTimerIdsParameterName, (object) timerIds);
      BandControllerSubscriptions.CheckResponse(await BandControllerSubscriptions.SendRequestToBandControllerServiceAsync(requestMessage));
    }

    public static async Task<bool> CheckTimerExists(string timerId)
    {
      ValueSet requestMessage = new ValueSet();
      ((IDictionary<string, object>) requestMessage).Add(InternalBandControllerNames.RequestCommandParameterName, (object) InternalBandTimerEventSubscriptionNames.DoesTimerExistsCommandName);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTimerEventSubscriptionNames.RequestOneTimerIdParameterName, (object) timerId);
      AppServiceResponse controllerServiceAsync = await BandControllerSubscriptions.SendRequestToBandControllerServiceAsync(requestMessage);
      BandControllerSubscriptions.CheckResponse(controllerServiceAsync);
      object obj;
      if (!((IDictionary<string, object>) controllerServiceAsync.Message).TryGetValue(InternalBandTimerEventSubscriptionNames.ResponseTimerExistsParameterName, out obj))
        throw new BandException(string.Format("Response missing '{0}'", (object) InternalBandTimerEventSubscriptionNames.ResponseTimerExistsParameterName));
      return (bool) obj;
    }
  }
}
