
// Type: Intermech.Client.Core.ImagesCacheIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

internal class ImagesCacheIndex
{
  public int Category;
  public int Type;

  public ImagesCacheIndex(int category, int type)
  {
    this.Category = category;
    this.Type = type;
  }

  public override bool Equals(object obj)
  {
    return obj is ImagesCacheIndex imagesCacheIndex && imagesCacheIndex.Category == this.Category && imagesCacheIndex.Type == this.Type;
  }

  public override int GetHashCode() => this.Category.GetHashCode() ^ this.Type.GetHashCode();
}
