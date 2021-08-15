// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Tiles.Pages.PageData
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Band.Tiles.Pages
{
  public sealed class PageData
  {
    private int pageLayoutIndex;
    private readonly IList<PageElementData> values;

    public PageData(Guid pageId, int pageLayoutIndex, params PageElementData[] values)
      : this(pageId, pageLayoutIndex, (IEnumerable<PageElementData>) values)
    {
    }

    public PageData(Guid pageId, int pageLayoutIndex, IEnumerable<PageElementData> values)
    {
      if (pageLayoutIndex < 0 || pageLayoutIndex >= 5)
        throw new ArgumentOutOfRangeException(nameof (pageLayoutIndex));
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this.PageId = pageId;
      this.PageLayoutIndex = pageLayoutIndex;
      this.values = (IList<PageElementData>) values.ToList<PageElementData>();
    }

    public Guid PageId { get; set; }

    public int PageLayoutIndex
    {
      get => this.pageLayoutIndex;
      set => this.pageLayoutIndex = value >= 0 && value < 5 ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public IList<PageElementData> Values => this.values;

    internal int GetSerializedLengthAndValidate(BandTypeConstants constants)
    {
      if (this.values.Count<PageElementData>() == 0)
        throw new InvalidOperationException(string.Format(BandResources.BandTilePageDataInvalidElementChildCount));
      int num = 0;
      foreach (PageElementData pageElementData in (IEnumerable<PageElementData>) this.values)
      {
        if (pageElementData == null)
          throw new InvalidOperationException(string.Format(BandResources.BandTilePageDataNullElementEncountered));
        pageElementData.Validate(constants);
        num += pageElementData.GetSerializedLength();
      }
      return num;
    }

    internal void SerializeToBand(ICargoWriter writer)
    {
      foreach (PageElementData pageElementData in (IEnumerable<PageElementData>) this.values)
        pageElementData.SerializeToBand(writer);
    }
  }
}
