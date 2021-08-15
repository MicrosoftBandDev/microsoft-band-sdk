// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.TextData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band.Tiles.Pages
{
  public abstract class TextData : PageElementData
  {
    private string text;

    internal TextData(PageElementType typeId, short elementId, string text)
      : base(typeId, elementId)
    {
      this.text = text != null ? text : throw new ArgumentNullException(nameof (text));
    }

    public string Text
    {
      get => this.text;
      set => this.text = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    private string SerializationText
    {
      get
      {
        if (string.IsNullOrEmpty(this.text))
          return " ";
        string str = this.text.TruncateTrimDanglingHighSurrogate(160);
        return string.IsNullOrEmpty(str) ? " " : str;
      }
    }

    internal override int GetSerializedLength() => base.GetSerializedLength() + 2 + this.SerializationText.Length * 2;

    internal override void SerializeToBand(ICargoWriter writer)
    {
      base.SerializeToBand(writer);
      writer.WriteUInt16((ushort) this.SerializationText.Length);
      writer.WriteString(this.SerializationText);
    }
  }
}
