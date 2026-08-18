
// Type: Intermech.Search.Statuses.Status
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.Statuses;

public sealed class Status
{
  public Status(ImageList imageList, int imageIndex, string hint)
  {
    if (imageList == null)
      throw new ArgumentNullException(nameof (imageList));
    if (imageIndex < 0 || imageIndex > imageList.Images.Count - 1)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(hint))
      throw new ArgumentException();
    this.ImageList = imageList;
    this.ImageIndex = imageIndex;
    this.Hint = hint;
  }

  public Status(Image image, string hint)
  {
    if (image == null)
      throw new ArgumentException(nameof (image));
    if (string.IsNullOrEmpty(hint))
      throw new ArgumentException();
    this.Image = image;
    this.Hint = hint;
  }

  public ImageList ImageList { get; private set; }

  public int ImageIndex { get; private set; }

  public Image Image { get; private set; }

  public string Hint { get; private set; }
}
