// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.TextRegionManager
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal class TextRegionManager
{
  private ArrayList m_regions = new ArrayList();

  public void Add(TextRegion region)
  {
    TextRegion[] regions = region != null ? this.Intersect(region) : throw new ArgumentNullException(nameof (region));
    this.m_regions.Add((object) this.Union(regions, region));
    this.Remove(regions);
  }

  public void Clear() => this.m_regions.Clear();

  public float GetCoordinate(float y)
  {
    float coordinate = y;
    TextRegion[] textRegionArray = this.Intersect(new TextRegion(y, 1f));
    if (textRegionArray != null && textRegionArray.Length == 1)
      return textRegionArray[0].Y;
    if (textRegionArray != null && textRegionArray.Length > 1)
    {
      TextRegion textRegion1 = textRegionArray[0];
      TextRegion textRegion2 = textRegionArray[1];
      return (double) textRegion1.Y < (double) textRegion2.Y ? textRegion1.Y : textRegion2.Y;
    }
    if (textRegionArray.Length == 0)
      coordinate = 0.0f;
    return coordinate;
  }

  public float GetTopCoordinate(float y)
  {
    float topCoordinate = y;
    TextRegion[] textRegionArray = this.Intersect(new TextRegion(y, 1f));
    if (textRegionArray != null && textRegionArray.Length == 1)
      return textRegionArray[0].Y;
    if (textRegionArray == null || textRegionArray.Length <= 1)
      return topCoordinate;
    TextRegion textRegion1 = textRegionArray[0];
    TextRegion textRegion2 = textRegionArray[1];
    return (double) textRegion1.Y < (double) textRegion2.Y ? textRegion1.Y : textRegion2.Y;
  }

  private TextRegion[] Intersect(TextRegion region)
  {
    if (region == null)
      throw new ArgumentNullException(nameof (region));
    ArrayList arrayList = new ArrayList();
    int index = 0;
    for (int count = this.m_regions.Count; index < count; ++index)
    {
      TextRegion region1 = (TextRegion) this.m_regions[index];
      if (region.IntersectsWith(region1))
        arrayList.Add((object) region1);
    }
    return (TextRegion[]) arrayList.ToArray(typeof (TextRegion));
  }

  private void Remove(TextRegion region)
  {
    if (region == null)
      throw new ArgumentNullException(nameof (region));
    this.m_regions.Remove((object) region);
  }

  private void Remove(TextRegion[] regions)
  {
    if (regions == null)
      throw new ArgumentNullException(nameof (regions));
    int index = 0;
    for (int length = regions.Length; index < length; ++index)
      this.m_regions.Remove((object) regions[index]);
  }

  private TextRegion Union(TextRegion[] regions, TextRegion region)
  {
    if (regions == null)
      throw new ArgumentNullException(nameof (regions));
    if (region == null)
      throw new ArgumentNullException(nameof (region));
    int index = 0;
    for (int length = regions.Length; index < length; ++index)
    {
      TextRegion region1 = regions[index];
      if (region.IntersectsWith(region1))
        region = TextRegion.Union(region, region1);
    }
    return region;
  }

  internal int Count => this.m_regions.Count;
}
