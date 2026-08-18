
// Type: Intermech.Client.Core.DoubleNamedImageList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Summary description for NamedImagesList.</summary>
public class DoubleNamedImageList : INamedImageList, IDisposable
{
  private ImageList _imageList;
  private Hashtable _names;

  public DoubleNamedImageList()
  {
    this._names = new Hashtable();
    this._imageList = new ImageList();
    this._imageList.ColorDepth = ColorDepth.Depth24Bit;
    this._imageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
  }

  public DoubleNamedImageList(ImageList imagelist)
  {
    this._names = new Hashtable();
    this._imageList = imagelist;
  }

  internal Icon BestIcon(Icon icon, Size size) => new Icon(icon, size);

  internal void AddNames(string[] names)
  {
    int num1 = 0;
    int num2 = this._imageList.Images.Count - 1;
    foreach (string name in names)
    {
      if (num1 > num2)
        break;
      this._names.Add((object) name, (object) num1++);
    }
  }

  public int Add(Icon icon, string name)
  {
    return icon != null ? this.Add((Image) icon.ToBitmap(), name) : throw new ArgumentException(sc_2271.ssp_imclient_2272());
  }

  public int Add(Image image, string name)
  {
    if (image == null)
      throw new ArgumentException(sc_2271.ssp_imclient_2273());
    if (name == null || name.Length == 0)
      throw new ArgumentException(sc_2271.ssp_imclient_2274());
    if (this._names.ContainsKey((object) name))
    {
      int name1 = (int) this._names[(object) name];
      this._imageList.Images[name1] = image;
      return name1;
    }
    this._imageList.Images.Add(image);
    int num = this._imageList.Images.Count - 1;
    this._names[(object) name] = (object) num;
    return num;
  }

  public int Add(Image image16, Image image32, string name) => this.Add(image16, name);

  public int AddStrip(Image images, string[] names) => 0;

  public int AddStrip(Image images16, Image images32, string[] names)
  {
    return this.AddStrip(images16, names);
  }

  public int ImageIndex(string name)
  {
    return this._names.ContainsKey((object) name) ? (int) this._names[(object) name] : -1;
  }

  public string ImageName(int imageIndex)
  {
    foreach (string key in (IEnumerable) this._names.Keys)
    {
      if ((int) this._names[(object) key] == imageIndex)
        return key;
    }
    return (string) null;
  }

  public ImageList ImageList => this._imageList;

  public ImageList BigImageList => this._imageList;

  public Color TransparentColor
  {
    get => this._imageList.TransparentColor;
    set => this._imageList.TransparentColor = value;
  }

  public void Dispose()
  {
    if (this._imageList != null)
      this._imageList.Dispose();
    this._imageList = (ImageList) null;
  }
}
