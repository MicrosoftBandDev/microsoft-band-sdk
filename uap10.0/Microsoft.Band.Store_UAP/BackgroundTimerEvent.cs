// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BackgroundTimerEvent
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System.Collections.Generic;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  internal class BackgroundTimerEvent : IBandBackgroundTimerEvent
  {
    public string TimerId { get; set; }

    public bool AfterServiceRestart { get; set; }

    public static BackgroundTimerEvent FromValueSet(ValueSet set)
    {
      BackgroundTimerEvent backgroundTimerEvent = new BackgroundTimerEvent();
      object obj;
      if (((IDictionary<string, object>) set).TryGetValue("TimerId", out obj))
        backgroundTimerEvent.TimerId = (string) obj;
      if (((IDictionary<string, object>) set).TryGetValue("AfterServiceRestart", out obj))
        backgroundTimerEvent.AfterServiceRestart = (bool) obj;
      return backgroundTimerEvent;
    }

    public void ToValueSet(ValueSet set)
    {
      ((IDictionary<string, object>) set)["TimerId"] = (object) this.TimerId;
      ((IDictionary<string, object>) set)["AfterServiceRestart"] = (object) this.AfterServiceRestart;
    }
  }
}
