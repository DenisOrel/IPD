
// Type: Intermech.Client.Core.NamedImageList
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
public class NamedImageList : INamedImageList, IDisposable
{
  private ImageList _imageList;
  private Hashtable _namesId;
  private bool _dispose;

  public event EventHandler Changed;

  public NamedImageList()
  {
    this._namesId = new Hashtable();
    this._imageList = new ImageList();
    this._imageList.ColorDepth = ColorDepth.Depth24Bit;
    this._imageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this._dispose = true;
  }

  public NamedImageList(ImageList imagelist)
  {
    this._namesId = new Hashtable();
    this._imageList = imagelist;
    this._dispose = false;
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
      this._namesId.Add((object) name, (object) num1++);
    }
  }

  public int Add(Icon icon, string name)
  {
    return icon != null ? this.Add((Image) icon.ToBitmap(), name) : throw new ArgumentException(sc_2285.ssp_imclient_2286());
  }

  public int Add(Image image, string name)
  {
    if (image == null)
      throw new ArgumentException(sc_2285.ssp_imclient_2287());
    if (name == null || name.Length == 0)
      throw new ArgumentException(sc_2285.ssp_imclient_2288());
    if (this._namesId.ContainsKey((object) name))
    {
      int index = (int) this._namesId[(object) name];
      this._imageList.Images[index] = (Image) image.Clone();
      return index;
    }
    this._imageList.Images.Add((Image) image.Clone());
    int num = this._imageList.Images.Count - 1;
    this._namesId[(object) name] = (object) num;
    this.OnChanged();
    return num;
  }

  public int AddStrip(Image images, string[] names)
  {
    int length = names.Length;
    int num = images.Width / images.Height;
    if (length != num)
      throw new ArgumentException(sc_2285.ssp_imclient_2289());
    int count = this._imageList.Images.Count;
    this._imageList.Images.AddStrip(images);
    for (int index = 0; index < length; ++index)
      this._namesId[(object) names[index]] = (object) (count + index);
    this.OnChanged();
    return count;
  }

  public int ImageIndex(string name)
  {
    return this._namesId.ContainsKey((object) name) ? (int) this._namesId[(object) name] : -1;
  }

  public string ImageName(int imageIndex)
  {
    foreach (string key in (IEnumerable) this._namesId.Keys)
    {
      if ((int) this._namesId[(object) key] == imageIndex)
        return key;
    }
    return (string) null;
  }

  public ImageList ImageList => this._imageList;

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public ICollection Keys => this._namesId.Keys;

  public void Dispose()
  {
    if (this._dispose && this._imageList != null)
      this._imageList.Dispose();
    this._imageList = (ImageList) null;
  }
}
