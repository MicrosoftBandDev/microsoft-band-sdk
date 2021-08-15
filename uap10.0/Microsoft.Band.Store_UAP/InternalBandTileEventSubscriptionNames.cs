// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.InternalBandTileEventSubscriptionNames
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

namespace Microsoft.Band
{
  public static class InternalBandTileEventSubscriptionNames
  {
    public static string CommandPrefix => "tile.event.";

    public static string SubscribeCommandName => "tile.event.subscribe";

    public static string UnsubscribeCommandName => "tile.event.unsubscribe";

    public static string IsSubscribedCommandName => "tile.event.issubscribed";

    public static string RequestTileIdParameterName => "TileId";

    public static string ResponseIsSubscribedParameterName => "Subscribed";

    public static string SequenceNumberParameterName => "Sequence#";
  }
}
