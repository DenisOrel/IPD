// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.ImageProcessing.ImageContainer
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System.Drawing;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.ImageProcessing;

public class ImageContainer
{
  private Image _image;

  public Image Data
  {
    get => this._image;
    set
    {
      this._image?.Dispose();
      this._image = value;
    }
  }
}
