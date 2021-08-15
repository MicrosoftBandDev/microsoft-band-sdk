// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.IBandNotificationManager
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Notifications
{
  public interface IBandNotificationManager
  {
    Task ShowDialogAsync(Guid tileId, string title, string body);

    Task ShowDialogAsync(Guid tileId, string title, string body, CancellationToken token);

    Task SendMessageAsync(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags = MessageFlags.None);

    Task SendMessageAsync(
      Guid tileId,
      string title,
      string body,
      DateTimeOffset timestamp,
      MessageFlags flags,
      CancellationToken token);

    Task VibrateAsync(VibrationType vibrationType);

    Task VibrateAsync(VibrationType vibrationType, CancellationToken token);
  }
}
