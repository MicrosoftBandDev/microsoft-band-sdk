// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PageElement
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Band.Tiles.Pages
{
  public abstract class PageElement
  {
    private ElementColorSource colorSource;
    private BandColor color;
    private short? elementId;
    internal const int SerializedByteCount = 36;

    internal PageElement()
      : this(CommonElementColors.Black)
    {
    }

    internal PageElement(BandColor color)
    {
      this.Visible = true;
      this.ColorSource = ElementColorSource.Custom;
      this.Color = color;
      this.HorizontalAlignment = HorizontalAlignment.Left;
      this.VerticalAlignment = VerticalAlignment.Top;
    }

    public PageRect Rect { get; set; }

    public virtual short? ElementId
    {
      get => this.elementId;
      set
      {
        if (value.HasValue)
        {
          short? nullable1 = value;
          int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num = 0;
          if ((nullable2.GetValueOrDefault() == num ? (nullable2.HasValue ? 1 : 0) : 0) != 0)
            throw new ArgumentOutOfRangeException(nameof (value));
        }
        this.elementId = value;
      }
    }

    public virtual PageElement FindElement(short elementIdToFind)
    {
      if (this.elementId.HasValue)
      {
        short? elementId = this.elementId;
        int? nullable = elementId.HasValue ? new int?((int) elementId.GetValueOrDefault()) : new int?();
        int num = (int) elementIdToFind;
        if ((nullable.GetValueOrDefault() == num ? (nullable.HasValue ? 1 : 0) : 0) != 0)
          return this;
      }
      return (PageElement) null;
    }

    public Margins Margins { get; set; }

    protected ElementColorSource ColorSource
    {
      get => this.colorSource;
      set => this.colorSource = value;
    }

    protected BandColor Color
    {
      get => this.color;
      set
      {
        this.color = value;
        this.colorSource = ElementColorSource.Custom;
      }
    }

    public HorizontalAlignment HorizontalAlignment { get; set; }

    public VerticalAlignment VerticalAlignment { get; set; }

    [Obsolete("Layouts are static. Making element as not visible is not useful. We will be removing this property", false)]
    public bool Visible { get; set; }

    internal virtual PageElementType TypeId => PageElementType.InvalidType;

    protected virtual int SerializedCustomByteCount => 0;

    internal static ushort GetCheckNumber(PageElementType typeId) => (ushort) (ushort.MaxValue - typeId);

    internal virtual int GetSerializedByteCountAndValidate(
      HashSet<short> existingIDs,
      HashSet<PageElement> existingElements)
    {
      if (!existingElements.Add(this))
        throw new SerializationException(BandResources.BandTilePageTemplateDuplicateElementOrId);
      short? elementId = this.ElementId;
      if (elementId.HasValue)
      {
        HashSet<short> shortSet = existingIDs;
        elementId = this.ElementId;
        int num = (int) elementId.Value;
        if (!shortSet.Add((short) num))
          throw new SerializationException(BandResources.BandTilePageTemplateDuplicateElementOrId);
      }
      return 36 + this.SerializedCustomByteCount;
    }

    internal virtual int GetElementCountAndIDs(HashSet<short> existingIDs)
    {
      if (this.ElementId.HasValue && !existingIDs.Add(this.ElementId.Value))
        throw new SerializationException(BandResources.BandTilePageTemplateDuplicateElementOrId);
      return (int) this.ChildCount;
    }

    internal virtual void GenerateMissingIDs(HashSet<short> existingIDs, ref short nextId)
    {
      if (this.ElementId.HasValue)
      {
        short? elementId = this.ElementId;
        int? nullable = elementId.HasValue ? new int?((int) elementId.GetValueOrDefault()) : new int?();
        int num = -1;
        if ((nullable.GetValueOrDefault() == num ? (nullable.HasValue ? 1 : 0) : 0) == 0)
          return;
      }
      while (existingIDs.Contains(nextId))
        ++nextId;
      this.ElementId = new short?(nextId++);
    }

    protected virtual ushort ChildCount => 0;

    protected virtual uint StyleMask() => this.HorizontalAlignmentToStyleMask() | this.VerticalAlignmentToStyleMask();

    protected virtual uint AttributesToCustomStyleMask() => 0;

    private uint StatusMask() => !this.Visible ? 65536U : 0U;

    private uint HorizontalAlignmentToStyleMask()
    {
      switch (this.HorizontalAlignment)
      {
        case HorizontalAlignment.Right:
          return 32;
        case HorizontalAlignment.Center:
          return 128;
        default:
          return 64;
      }
    }

    private uint VerticalAlignmentToStyleMask()
    {
      switch (this.VerticalAlignment)
      {
        case VerticalAlignment.Bottom:
          return 1024;
        case VerticalAlignment.Center:
          return 512;
        default:
          return 256;
      }
    }

    internal virtual void SerializeToBand(ICargoWriter writer)
    {
      writer.WriteUInt16((ushort) this.TypeId);
      writer.WriteUInt16((ushort) ((int) this.ElementId ?? 0));
      writer.WriteUInt16(this.ChildCount);
      this.Rect.SerializeToBand(writer);
      this.Margins.SerializeToBand(writer);
      writer.WriteUInt32(this.StyleMask() | this.AttributesToCustomStyleMask());
      writer.WriteUInt32(this.StatusMask());
      if (this.ColorSource == ElementColorSource.Custom)
        writer.WriteUInt32(this.Color.ToRgb((byte) 1));
      else
        writer.WriteUInt32((uint) this.ColorSource);
      this.SerializeCustomFieldsToBand(writer);
      writer.WriteUInt16(PageElement.GetCheckNumber(this.TypeId));
      this.SerializeElementsToBand(writer);
    }

    internal virtual void SerializeCustomFieldsToBand(ICargoWriter writer)
    {
    }

    internal virtual void SerializeElementsToBand(ICargoWriter writer)
    {
    }

    internal static PageElement DeserializeFromBand(ICargoReader reader)
    {
      PageElement elementFromTypeId = PageElement.CreateElementFromTypeId((PageElementType) reader.ReadUInt16());
      elementFromTypeId.ElementId = new short?(reader.ReadInt16());
      ushort num1 = reader.ReadUInt16();
      if (num1 > (ushort) 0 && !(elementFromTypeId is PagePanel))
        throw new SerializationException(BandResources.BandTilePageTemplateInvalidElementChildCount);
      elementFromTypeId.Rect = PageRect.DeserializeFromBand(reader);
      elementFromTypeId.Margins = Margins.DeserializeFromBand(reader);
      uint mask = reader.ReadUInt32();
      elementFromTypeId.HorizontalAlignment = PageElement.StyleMaskToHorizontalAlignment((PageElement.PageElementStyleMask) mask);
      elementFromTypeId.VerticalAlignment = PageElement.StyleMaskToVerticalAlignment((PageElement.PageElementStyleMask) mask);
      elementFromTypeId.CustomStyleMaskToAttributes(mask);
      uint num2 = reader.ReadUInt32();
      elementFromTypeId.Visible = PageElement.StatusMaskToVisibility((PageElement.PageElementStatusMask) num2);
      uint rgb = reader.ReadUInt32();
      switch ((rgb & 4278190080U) >> 24)
      {
        case 2:
        case 3:
          uint num3 = rgb & 16777215U;
          if (num3 > 5U)
            num3 = 0U;
          elementFromTypeId.ColorSource = (ElementColorSource) num3;
          break;
        default:
          elementFromTypeId.ColorSource = ElementColorSource.Custom;
          elementFromTypeId.Color = new BandColor(rgb);
          break;
      }
      elementFromTypeId.DeserializeCustomFieldsFromBand(reader);
      if ((int) reader.ReadUInt16() != (int) PageElement.GetCheckNumber(elementFromTypeId.TypeId))
        throw new SerializationException(BandResources.BandTilePageTemplateInvalidCheckDigit);
      if (num1 > (ushort) 0)
        elementFromTypeId.DeserializeElementsFromBand(reader, (int) num1);
      return elementFromTypeId;
    }

    protected virtual void CustomStyleMaskToAttributes(uint mask)
    {
    }

    internal virtual void DeserializeCustomFieldsFromBand(ICargoReader reader)
    {
    }

    internal virtual void DeserializeElementsFromBand(ICargoReader reader, int childCount) => throw new InvalidOperationException();

    private static PageElement CreateElementFromTypeId(PageElementType elementType)
    {
      if ((uint) elementType <= 3001U)
      {
        if ((uint) elementType <= 1001U)
        {
          if (elementType == PageElementType.PageHeader)
            throw new SerializationException(BandResources.BandTilePageTemplateUnexpectedElementType);
          if (elementType == PageElementType.FlowList)
            return (PageElement) new FlowPanel(new PageElement[0]);
        }
        else
        {
          if (elementType == PageElementType.ScrollFlowList)
            return (PageElement) new ScrollFlowPanel(new PageElement[0]);
          if (elementType == PageElementType.FilledQuad)
            return (PageElement) new FilledPanel(new PageElement[0]);
          if (elementType == PageElementType.Text)
            return (PageElement) new TextBlock();
        }
      }
      else if ((uint) elementType <= 3101U)
      {
        if (elementType == PageElementType.WrappableText)
          return (PageElement) new WrappedTextBlock();
        if (elementType == PageElementType.Icon)
          return (PageElement) new Icon();
      }
      else
      {
        switch (elementType)
        {
          case PageElementType.BarcodeCode39:
            return (PageElement) new Barcode(BarcodeType.Code39);
          case PageElementType.BarcodePdf417:
            return (PageElement) new Barcode(BarcodeType.Pdf417);
          case PageElementType.InteractiveButtonWithBorder:
            return (PageElement) new FilledButton();
          case PageElementType.InteractiveButtonWithText:
            return (PageElement) new TextButton();
          case PageElementType.InteractiveButtonWithIcon:
            return (PageElement) new IconButton();
        }
      }
      throw new SerializationException(BandResources.BandTilePageTemplateUnknownOrInvalidElementType);
    }

    private static HorizontalAlignment StyleMaskToHorizontalAlignment(
      PageElement.PageElementStyleMask mask)
    {
      if (mask.HasFlag((Enum) PageElement.PageElementStyleMask.HRight))
        return HorizontalAlignment.Right;
      return mask.HasFlag((Enum) PageElement.PageElementStyleMask.HCenter) ? HorizontalAlignment.Center : HorizontalAlignment.Left;
    }

    private static VerticalAlignment StyleMaskToVerticalAlignment(
      PageElement.PageElementStyleMask mask)
    {
      if (mask.HasFlag((Enum) PageElement.PageElementStyleMask.VBottom))
        return VerticalAlignment.Bottom;
      return mask.HasFlag((Enum) PageElement.PageElementStyleMask.VCenter) ? VerticalAlignment.Center : VerticalAlignment.Top;
    }

    private static bool StatusMaskToVisibility(PageElement.PageElementStatusMask mask) => !mask.HasFlag((Enum) PageElement.PageElementStatusMask.Hidden);

    [Flags]
    private enum PageElementStatusMask : uint
    {
      None = 0,
      Hidden = 65536, // 0x00010000
    }

    [Flags]
    internal enum PageElementStyleMask : uint
    {
      None = 0,
      HRight = 32, // 0x00000020
      HLeft = 64, // 0x00000040
      HCenter = 128, // 0x00000080
      VTop = 256, // 0x00000100
      VCenter = 512, // 0x00000200
      VBottom = 1024, // 0x00000400
      Transparent = 1073741824, // 0x40000000
    }
  }
}
