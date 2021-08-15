// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.CargoFileTime
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band
{
  internal static class CargoFileTime
  {
    internal static int GetSerializedByteCount() => 8;

    internal static DateTime DeserializeFromBandAsDateTime(ICargoReader reader)
    {
      long fileTime = reader.ReadInt64();
      return fileTime <= 2650467743999990000L ? DateTime.FromFileTimeUtc(fileTime) : DateTime.FromFileTimeUtc(2650467743999990000L);
    }

    internal static void SerializeToBandFromDateTime(ICargoWriter writer, DateTime timestamp)
    {
      if (timestamp >= BandConstants.MinDateTimeForFileTime)
        writer.WriteInt64(timestamp.ToFileTime());
      else
        writer.WriteInt64(BandConstants.MinDateTimeForFileTime.ToFileTime());
    }

    internal static void SerializeToBandFromDateTime(ICargoWriter writer, DateTime? timestamp)
    {
      if (timestamp.HasValue)
      {
        DateTime? nullable = timestamp;
        DateTime dateTimeForFileTime = BandConstants.MinDateTimeForFileTime;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() >= dateTimeForFileTime ? 1 : 0) : 0) != 0)
          writer.WriteInt64(timestamp.Value.ToFileTime());
        else
          writer.WriteInt64(BandConstants.MinDateTimeForFileTime.ToFileTime());
      }
      else
        writer.WriteInt64(0L);
    }

    internal static DateTimeOffset DeserializeFromBandAsDateTimeOffset(
      ICargoReader reader)
    {
      return (DateTimeOffset) CargoFileTime.DeserializeFromBandAsDateTime(reader);
    }

    internal static void SerializeToBandFromDateTimeOffset(
      ICargoWriter writer,
      DateTimeOffset timestamp)
    {
      if (timestamp >= BandConstants.MinDateTimeOffsetForFileTime)
        writer.WriteInt64(timestamp.ToFileTime());
      else
        writer.WriteInt64(BandConstants.MinDateTimeOffsetForFileTime.ToFileTime());
    }

    internal static void SerializeToBandFromDateTimeOffset(
      ICargoWriter writer,
      DateTimeOffset? timestamp)
    {
      if (timestamp.HasValue)
      {
        DateTimeOffset? nullable = timestamp;
        DateTimeOffset offsetForFileTime = BandConstants.MinDateTimeOffsetForFileTime;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() >= offsetForFileTime ? 1 : 0) : 0) != 0)
          writer.WriteInt64(timestamp.Value.ToFileTime());
        else
          writer.WriteInt64(BandConstants.MinDateTimeOffsetForFileTime.ToFileTime());
      }
      else
        writer.WriteInt64(0L);
    }
  }
}
