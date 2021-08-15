// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandBackgroundTimerEventArgs`1
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;

namespace Microsoft.Band
{
  public class BandBackgroundTimerEventArgs<T> : EventArgs where T : IBandBackgroundTimerEvent
  {
    private readonly T timerEvent;

    public BandBackgroundTimerEventArgs(T timerEvent) => this.timerEvent = timerEvent;

    public T TimerEvent => this.timerEvent;
  }
}
