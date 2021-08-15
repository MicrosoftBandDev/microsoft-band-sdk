﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PagePanel
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Band.Tiles.Pages
{
  public abstract class PagePanel : PageElement
  {
    protected PagePanel(params PageElement[] elements)
      : this(CommonElementColors.Black, (IEnumerable<PageElement>) elements)
    {
    }

    protected PagePanel(IEnumerable<PageElement> elements)
      : this(CommonElementColors.Black, elements)
    {
    }

    protected PagePanel(BandColor color, IEnumerable<PageElement> elements)
      : base(color)
    {
      this.Elements = elements != null ? (IList<PageElement>) elements.ToList<PageElement>() : throw new ArgumentNullException(nameof (elements));
    }

    public IList<PageElement> Elements { get; private set; }

    public override PageElement FindElement(short elementIdToFind)
    {
      PageElement element1 = base.FindElement(elementIdToFind);
      if (element1 == null)
      {
        foreach (PageElement element2 in (IEnumerable<PageElement>) this.Elements)
        {
          element1 = element2.FindElement(elementIdToFind);
          if (element1 != null)
            break;
        }
      }
      return element1;
    }

    protected override ushort ChildCount => (ushort) this.Elements.Count;

    internal override int GetSerializedByteCountAndValidate(
      HashSet<short> existingIDs,
      HashSet<PageElement> existingElements)
    {
      base.GetSerializedByteCountAndValidate(existingIDs, existingElements);
      int num = 36 + this.SerializedCustomByteCount;
      foreach (PageElement element in (IEnumerable<PageElement>) this.Elements)
      {
        if (element == null)
          throw new InvalidOperationException(BandResources.BandTilePageTemplateNullElementEncountered);
        num += element.GetSerializedByteCountAndValidate(existingIDs, existingElements);
      }
      return num;
    }

    internal override int GetElementCountAndIDs(HashSet<short> existingIDs)
    {
      int elementCountAndIds = base.GetElementCountAndIDs(existingIDs);
      foreach (PageElement element in (IEnumerable<PageElement>) this.Elements)
      {
        ++elementCountAndIds;
        elementCountAndIds += element.GetElementCountAndIDs(existingIDs);
      }
      return elementCountAndIds;
    }

    internal override void GenerateMissingIDs(HashSet<short> existingIDs, ref short nextId)
    {
      base.GenerateMissingIDs(existingIDs, ref nextId);
      foreach (PageElement element in (IEnumerable<PageElement>) this.Elements)
        element.GenerateMissingIDs(existingIDs, ref nextId);
    }

    internal override void SerializeElementsToBand(ICargoWriter writer)
    {
      foreach (PageElement element in (IEnumerable<PageElement>) this.Elements)
        element.SerializeToBand(writer);
    }

    internal override void DeserializeElementsFromBand(ICargoReader reader, int childCount)
    {
      for (int index = 0; index < childCount; ++index)
        this.Elements.Add(PageElement.DeserializeFromBand(reader));
    }
  }
}
