// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Notifications.VibrationTypeExtensions
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Notifications
{
  internal static class VibrationTypeExtensions
  {
    public static BandVibrationType ToBandVibrationType(
      this VibrationType vibrationType)
    {
      switch (vibrationType)
      {
        case VibrationType.NotificationOneTone:
          return BandVibrationType.ToastTextMessage;
        case VibrationType.NotificationTwoTone:
          return BandVibrationType.AlertIncomingCall;
        case VibrationType.NotificationAlarm:
          return BandVibrationType.AlertAlarm;
        case VibrationType.NotificationTimer:
          return BandVibrationType.AlertTimer;
        case VibrationType.OneToneHigh:
          return BandVibrationType.ExerciseGuidedWorkoutTimer;
        case VibrationType.TwoToneHigh:
          return BandVibrationType.ExerciseGuidedWorkoutCircuitComplete;
        case VibrationType.ThreeToneHigh:
          return BandVibrationType.ExerciseGuidedWorkoutComplete;
        case VibrationType.RampUp:
          return BandVibrationType.SystemStartUp;
        case VibrationType.RampDown:
          return BandVibrationType.SystemShutDown;
        default:
          throw new ArgumentException("Unknown VibrationType value.");
      }
    }
  }
}
