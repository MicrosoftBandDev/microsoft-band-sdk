// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.IApplicationPlatformProvider
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using Microsoft.Band.Tiles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band
{
  internal interface IApplicationPlatformProvider
  {
    Task<Guid> GetApplicationIdAsync(CancellationToken token);

    Task<bool> GetAddTileConsentAsync(BandTile tile, CancellationToken token);

    UserConsent GetCurrentSensorConsent(Type sensorType);

    Task<bool> RequestSensorConsentAsync(
      Type sensorType,
      string prompt,
      CancellationToken token);
  }
}
