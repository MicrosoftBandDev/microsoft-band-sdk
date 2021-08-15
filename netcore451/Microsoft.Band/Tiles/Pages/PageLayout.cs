// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PageLayout
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class PageLayout
  {
    internal const int SerializedByteCount = 16;

    public PageLayout(PagePanel root = null) => this.Root = root;

    public PagePanel Root { get; set; }

    internal IEnumerable<PageElement> Elements
    {
      get
      {
        if (this.Root != null)
        {
          Queue<PageElement> queue = new Queue<PageElement>();
          queue.Enqueue((PageElement) this.Root);
          while (queue.Count > 0)
          {
            PageElement element = queue.Dequeue();
            yield return element;
            if (element is PagePanel pagePanel4)
            {
              foreach (PageElement element1 in (IEnumerable<PageElement>) pagePanel4.Elements)
                queue.Enqueue(element1);
            }
            element = (PageElement) null;
          }
          queue = (Queue<PageElement>) null;
        }
      }
    }

    internal int GetSerializedByteCountAndValidate()
    {
      if (this.Root == null)
        throw new InvalidOperationException(BandResources.BandTilePageTemplateNullElementEncountered);
      return 16 + this.Root.GetSerializedByteCountAndValidate(new HashSet<short>(), new HashSet<PageElement>());
    }

    private int GetElementCountAndGenerateMissingIDs()
    {
      HashSet<short> existingIDs = new HashSet<short>();
      short nextId = 1;
      int num = 0 + 1 + this.Root.GetElementCountAndIDs(existingIDs);
      this.Root.GenerateMissingIDs(existingIDs, ref nextId);
      return num;
    }

    internal void SerializeToBand(ICargoWriter writer)
    {
      ushort generateMissingIds = (ushort) this.GetElementCountAndGenerateMissingIDs();
      writer.WriteUInt16((ushort) 1);
      writer.WriteUInt16((ushort) 0);
      writer.WriteUInt16((ushort) 0);
      writer.WriteUInt16((ushort) 1);
      writer.WriteUInt16((ushort) 0);
      writer.WriteUInt16(this.Root != null ? (ushort) 1 : (ushort) 0);
      writer.WriteUInt16(generateMissingIds);
      writer.WriteUInt16(PageElement.GetCheckNumber(PageElementType.PageHeader));
      if (this.Root == null)
        return;
      this.Root.SerializeToBand(writer);
    }

    internal static PageLayout DeserializeFromBand(ICargoReader reader)
    {
      if (reader.ReadUInt16() == (ushort) 0)
        return (PageLayout) null;
      int num1 = (int) reader.ReadUInt16();
      int num2 = (int) reader.ReadUInt16();
      PageElement pageElement = (PageElement) null;
      int num3 = reader.ReadUInt16() == (ushort) 1 ? (int) reader.ReadUInt16() : throw new SerializationException(BandResources.BandTilePageTemplateUnexpectedElementType);
      int num4 = (int) reader.ReadUInt16();
      if (num4 > 1)
        throw new SerializationException(BandResources.BandTilePageTemplateInvalidElementChildCount);
      int num5 = (int) reader.ReadUInt16();
      if ((int) reader.ReadUInt16() != (int) PageElement.GetCheckNumber(PageElementType.PageHeader))
        throw new SerializationException(BandResources.BandTilePageTemplateInvalidCheckDigit);
      if (num4 == 1)
      {
        pageElement = PageElement.DeserializeFromBand(reader);
        if (!(pageElement is PagePanel))
          throw new SerializationException(BandResources.BandTilePageTemplateUnexpectedElementType);
      }
      return new PageLayout(pageElement as PagePanel);
    }
  }
}
