// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.GetPageLayoutArgs
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles
{
  internal static class GetPageLayoutArgs
  {
    private const int serializedByteCount = 24;

    internal static int GetSerializedByteCount() => 24;

    internal static void SerializeToBand(ICargoWriter writer, Guid tileId, uint layoutIndex)
    {
      writer.WriteGuid(tileId);
      writer.WriteUInt32(layoutIndex);
      writer.WriteUInt32(768U);
    }
  }
}
