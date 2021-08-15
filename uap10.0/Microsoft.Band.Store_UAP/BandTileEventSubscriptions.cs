// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandTileEventSubscriptions
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  public static class BandTileEventSubscriptions
  {
    private static int nextRequestId;

    public static async Task SubscribeToBackgroundTileEventsAsync(
      this IBandClient bandClient,
      Guid tileId)
    {
      if (!bandClient.TileManager.TileInstalledAndOwned(tileId, CancellationToken.None))
        throw new Exception("Not owned");
      BandControllerSubscriptions.CheckResponse(await BandTileEventSubscriptions.SendTileCommand(InternalBandTileEventSubscriptionNames.SubscribeCommandName, tileId));
    }

    public static async Task UnsubscribeFromBackgroundTileEventsAsync(
      this IBandClient bandClient,
      Guid tileId)
    {
      BandControllerSubscriptions.CheckResponse(await BandTileEventSubscriptions.SendTileCommand(InternalBandTileEventSubscriptionNames.UnsubscribeCommandName, tileId));
    }

    public static async Task<bool> IsSubscribedToBackgroundTileEventsAsync(
      this IBandClient bandClient,
      Guid tileId)
    {
      AppServiceResponse response = await BandTileEventSubscriptions.SendTileCommand(InternalBandTileEventSubscriptionNames.IsSubscribedCommandName, tileId);
      BandControllerSubscriptions.CheckResponse(response);
      return (((IDictionary<string, object>) response.Message)[InternalBandTileEventSubscriptionNames.ResponseIsSubscribedParameterName] as bool? ?? throw new BandException("Response missing 'Subscribed'")).Value;
    }

    private static async Task<AppServiceResponse> SendTileCommand(
      string command,
      Guid tileId)
    {
      ValueSet requestMessage = new ValueSet();
      ((IDictionary<string, object>) requestMessage).Add(InternalBandControllerNames.RequestCommandParameterName, (object) command);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTileEventSubscriptionNames.RequestTileIdParameterName, (object) tileId);
      ((IDictionary<string, object>) requestMessage).Add(InternalBandTileEventSubscriptionNames.SequenceNumberParameterName, (object) BandTileEventSubscriptions.nextRequestId++);
      AppServiceResponse controllerServiceAsync = await BandControllerSubscriptions.SendRequestToBandControllerServiceAsync(requestMessage);
      BandControllerSubscriptions.CheckResponse(controllerServiceAsync);
      return controllerServiceAsync;
    }
  }
}
