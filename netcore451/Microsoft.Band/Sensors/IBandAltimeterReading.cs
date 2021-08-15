// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.IBandAltimeterReading
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Sensors
{
  public interface IBandAltimeterReading : IBandSensorReading
  {
    long TotalGain { get; }

    long TotalLoss { get; }

    long SteppingGain { get; }

    long SteppingLoss { get; }

    long StepsAscended { get; }

    long StepsDescended { get; }

    float Rate { get; }

    long FlightsAscended { get; }

    long FlightsDescended { get; }

    long FlightsAscendedToday { get; }

    long TotalGainToday { get; }
  }
}
