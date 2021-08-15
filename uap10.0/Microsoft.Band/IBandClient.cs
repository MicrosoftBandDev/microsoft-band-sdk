// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.IBandClient
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band
{
  public interface IBandClient : IDisposable
  {
    IBandNotificationManager NotificationManager { get; }

    IBandPersonalizationManager PersonalizationManager { get; }

    IBandTileManager TileManager { get; }

    IBandSensorManager SensorManager { get; }

    Task<string> GetFirmwareVersionAsync();

    Task<string> GetFirmwareVersionAsync(CancellationToken token);

    Task<string> GetHardwareVersionAsync();

    Task<string> GetHardwareVersionAsync(CancellationToken token);
  }
}
