// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Sensors.IBandSensorManager
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

namespace Microsoft.Band.Sensors
{
  public interface IBandSensorManager
  {
    IBandSensor<IBandAccelerometerReading> Accelerometer { get; }

    IBandSensor<IBandGyroscopeReading> Gyroscope { get; }

    IBandSensor<IBandDistanceReading> Distance { get; }

    IBandSensor<IBandHeartRateReading> HeartRate { get; }

    IBandContactSensor Contact { get; }

    IBandSensor<IBandSkinTemperatureReading> SkinTemperature { get; }

    IBandSensor<IBandUVReading> UV { get; }

    IBandSensor<IBandPedometerReading> Pedometer { get; }

    IBandSensor<IBandCaloriesReading> Calories { get; }

    IBandSensor<IBandGsrReading> Gsr { get; }

    IBandSensor<IBandRRIntervalReading> RRInterval { get; }

    IBandSensor<IBandAmbientLightReading> AmbientLight { get; }

    IBandSensor<IBandBarometerReading> Barometer { get; }

    IBandSensor<IBandAltimeterReading> Altimeter { get; }
  }
}
