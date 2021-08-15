// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.BarcodeData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Runtime.Serialization;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class BarcodeData : PageElementData
  {
    private static readonly byte[] validPdf417CharMapEnvoy = new byte[128]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 0
    };
    private static readonly byte[] validCode39CharMap = new byte[59]
    {
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1
    };
    private string barcodeData;

    public BarcodeData(BarcodeType barcodeType, short elementId, string barcode)
      : base(BarcodeData.BarcodeTypeToPageElementType(barcodeType), elementId)
    {
      if (barcode == null)
        throw new ArgumentNullException(nameof (barcode));
      this.BarcodeType = barcodeType;
      this.SetBarcode(barcode, nameof (barcode));
    }

    public static int MaximumDataLength => 39;

    private static PageElementType BarcodeTypeToPageElementType(
      BarcodeType barcodeType)
    {
      if (barcodeType == BarcodeType.Pdf417)
        return PageElementType.BarcodePdf417;
      if (barcodeType == BarcodeType.Code39)
        return PageElementType.BarcodeCode39;
      throw new InvalidOperationException();
    }

    private static void ValidatePDF417Length(string value, string paramName)
    {
      if (value.Length > BarcodeData.MaximumDataLength)
        throw new ArgumentOutOfRangeException(paramName, string.Format(BandResources.BarcodeDataTooLong, (object) BarcodeData.MaximumDataLength));
    }

    private static void ValidateCode39Length(string value, string paramName)
    {
      if (value.Length > BarcodeData.MaximumDataLength)
        throw new ArgumentOutOfRangeException(paramName, string.Format(BandResources.BarcodeDataTooLong, (object) BarcodeData.MaximumDataLength));
    }

    public BarcodeType BarcodeType { get; private set; }

    public string Barcode
    {
      get => this.barcodeData;
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.SetBarcode(value, "Value");
      }
    }

    private void SetBarcode(string value, string paramName)
    {
      if (value.Length == 0)
        throw new ArgumentOutOfRangeException(paramName, BandResources.BarcodeDataEmpty);
      switch (this.BarcodeType)
      {
        case BarcodeType.Pdf417:
          BarcodeData.ValidatePDF417Length(value, paramName);
          break;
        case BarcodeType.Code39:
          BarcodeData.ValidateCode39Length(value, paramName);
          break;
        default:
          throw new InvalidOperationException();
      }
      this.barcodeData = value;
    }

    internal override void Validate(BandTypeConstants constants)
    {
      switch (this.BarcodeType)
      {
        case BarcodeType.Pdf417:
          switch (constants.BandType)
          {
            case BandType.Cargo:
              BarcodeData.ValidatePdf417ForBandV1(this.Barcode);
              return;
            case BandType.Envoy:
              BarcodeData.ValidatePdf417ForBandV2(this.Barcode);
              return;
            default:
              throw new InvalidOperationException();
          }
        case BarcodeType.Code39:
          BarcodeData.ValidateCode39(this.Barcode);
          break;
        default:
          throw new InvalidOperationException();
      }
    }

    internal override int GetSerializedLength() => base.GetSerializedLength() + 2 + this.Barcode.Length * 2;

    private static void ValidatePdf417ForBandV1(string value)
    {
      foreach (char c in value)
      {
        if (!char.IsDigit(c))
          throw new SerializationException(BandResources.InvalidBarcodePdf417Data);
      }
    }

    private static void ValidatePdf417ForBandV2(string value)
    {
      foreach (char ch in value)
      {
        if ((int) ch >= BarcodeData.validPdf417CharMapEnvoy.Length || BarcodeData.validPdf417CharMapEnvoy[(int) ch] == (byte) 0)
          throw new SerializationException(BandResources.InvalidBarcodePdf417Data);
      }
    }

    private static void ValidateCode39(string value)
    {
      foreach (int num in value)
      {
        int index = num - 32;
        if (index < 0 || index >= BarcodeData.validCode39CharMap.Length || BarcodeData.validCode39CharMap[index] == (byte) 0)
          throw new SerializationException(BandResources.InvalidBarcodeCode39Data);
      }
    }

    internal override void SerializeToBand(ICargoWriter writer)
    {
      base.SerializeToBand(writer);
      writer.WriteUInt16((ushort) this.Barcode.Length);
      writer.WriteString(this.Barcode);
    }
  }
}
