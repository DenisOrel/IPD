// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.ImageRegionManager
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    internal class ImageRegionManager
    {
      private ArrayList m_regions = new ArrayList();

      public void Add(ImageRegion region)
      {
        ImageRegion[] regions = region != null ? this.Intersect(region) : throw new ArgumentNullException(nameof (region));
        this.m_regions.Add((object) this.Union(regions, region));
        this.Remove(regions);
      }

      public void Clear() => this.m_regions.Clear();

      public float GetCoordinate(float y)
      {
        float coordinate = y;
        ImageRegion[] imageRegionArray = this.Intersect(new ImageRegion(y, 1f));
        if (imageRegionArray != null && imageRegionArray.Length == 1)
          return imageRegionArray[0].Y;
        if (imageRegionArray != null && imageRegionArray.Length > 1)
        {
          ImageRegion imageRegion1 = imageRegionArray[0];
          ImageRegion imageRegion2 = imageRegionArray[1];
          return (double) imageRegion1.Y < (double) imageRegion2.Y ? imageRegion1.Y : imageRegion2.Y;
        }
        if (imageRegionArray == null)
          coordinate = 0.0f;
        return coordinate;
      }

      public float GetTopCoordinate(float y)
      {
        float topCoordinate = y;
        ImageRegion[] imageRegionArray = this.Intersect(new ImageRegion(y, 1f));
        if (imageRegionArray != null && imageRegionArray.Length == 1)
          return imageRegionArray[0].Y;
        if (imageRegionArray == null || imageRegionArray.Length <= 1)
          return topCoordinate;
        ImageRegion imageRegion1 = imageRegionArray[0];
        ImageRegion imageRegion2 = imageRegionArray[1];
        return (double) imageRegion1.Y < (double) imageRegion2.Y ? imageRegion1.Y : imageRegion2.Y;
      }

      private ImageRegion[] Intersect(ImageRegion region)
      {
        if (region == null)
          throw new ArgumentNullException(nameof (region));
        ArrayList arrayList = new ArrayList();
        int index = 0;
        for (int count = this.m_regions.Count; index < count; ++index)
        {
          ImageRegion region1 = (ImageRegion) this.m_regions[index];
          if (region.IntersectsWith(region1))
            arrayList.Add((object) region1);
        }
        return (ImageRegion[]) arrayList.ToArray(typeof (ImageRegion));
      }

      private void Remove(ImageRegion region)
      {
        if (region == null)
          throw new ArgumentNullException(nameof (region));
        this.m_regions.Remove((object) region);
      }

      private void Remove(ImageRegion[] regions)
      {
        if (regions == null)
          throw new ArgumentNullException(nameof (regions));
        int index = 0;
        for (int length = regions.Length; index < length; ++index)
          this.m_regions.Remove((object) regions[index]);
      }

      private ImageRegion Union(ImageRegion[] regions, ImageRegion region)
      {
        if (regions == null)
          throw new ArgumentNullException(nameof (regions));
        if (region == null)
          throw new ArgumentNullException(nameof (region));
        int index = 0;
        for (int length = regions.Length; index < length; ++index)
        {
          ImageRegion region1 = regions[index];
          if (region.IntersectsWith(region1))
            region = ImageRegion.Union(region, region1);
        }
        return region;
      }
    }
}
