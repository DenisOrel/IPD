
// Type: Intermech.Client.Core.CategoryTypeIconService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Navigator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Summary description for CategoryTypeIconService.</summary>
public sealed class CategoryTypeIconService : ICategoryTypeIconService, IDisposable
{
  private int _updateCount;
  private int _imageListsUpdateCount;
  private ImageList _imageList16;
  private ImageList _imageList32;
  private Dictionary<CategoryTypeIconService.CatType, int> _categories;
  private Dictionary<int, byte[]> _iconData;
  private Dictionary<int, Icon> _icons;
  private Control _syncronizer;
  private object _syncRoot;
  private ImChunkedStream _tempStream;

  public event FindIconEventHandler _findIcon;

  public event FindIconEventHandler FindIcon
  {
    add => this._findIcon += value;
    remove => this._findIcon -= value;
  }

  private void DisableImageListEvents(ImageList imageList)
  {
    this.SetImageListInAddRangeMode(imageList, true);
  }

  private void EnableImageListEvents(ImageList imageList)
  {
    this.SetImageListInAddRangeMode(imageList, false);
  }

  private void SetImageListInAddRangeMode(ImageList imageList, bool inAddRange)
  {
    imageList.GetType().GetField(nameof (inAddRange), BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue((object) imageList, (object) inAddRange);
  }

  private void CallImageListChangeEvents(ImageList imageList)
  {
    imageList.GetType().GetMethod("OnChangeHandle", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke((object) imageList, new object[1]
    {
      (object) EventArgs.Empty
    });
  }

  public CategoryTypeIconService(Control control)
  {
    this._syncronizer = control;
    this._syncRoot = new object();
    this._tempStream = new ImChunkedStream();
    this._iconData = new Dictionary<int, byte[]>(1024 /*0x0400*/);
    this._categories = new Dictionary<CategoryTypeIconService.CatType, int>(1024 /*0x0400*/);
    this._icons = new Dictionary<int, Icon>(1024 /*0x0400*/);
    this._imageList16 = new ImageList();
    this._imageList16.ColorDepth = ColorDepth.Depth24Bit;
    this._imageList16.ImageSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this._imageList32 = new ImageList();
    this._imageList32.ColorDepth = ColorDepth.Depth24Bit;
    this._imageList32.ImageSize = new Size(32 /*0x20*/, 32 /*0x20*/);
    Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.UnknownType.ico");
    if (manifestResourceStream == null)
      return;
    try
    {
      ++Services.IconsCount;
      Icon icon1 = new Icon(manifestResourceStream);
      Icon icon2 = ImagesResizeHelper.ResizeIconTo32x16(icon1, this._imageList16.TransparentColor);
      if (icon1.Height != icon1.Width)
        this._imageList16.Images.Add((Image) icon1.ToBitmap());
      else
        this._imageList16.Images.Add(icon2);
      this._imageList32.Images.Add((Image) icon1.ToBitmap());
      this._iconData[0] = icon1.Height != 16 /*0x10*/ ? this.InternalGetIconData(icon2) : this.InternalGetIconData(icon1);
      icon1.Dispose();
      icon2.Dispose();
    }
    finally
    {
      manifestResourceStream.Close();
    }
  }

  private byte[] InternalGetIconData(Icon icon)
  {
    lock (this._syncRoot)
      return this.ConvertIconToBytes(icon);
  }

  private byte[] ConvertIconToBytes(Icon icon)
  {
    try
    {
      icon.Save((Stream) this._tempStream);
      this._tempStream.Flush();
      return this._tempStream.ToArray();
    }
    finally
    {
      this._tempStream.SetLength(0L);
    }
  }

  private int OnFindIcon(int category, int type, object data)
  {
    if (this._findIcon != null)
    {
      foreach (FindIconEventHandler invocation in this._findIcon.GetInvocationList())
      {
        Icon icon = invocation(category, type, data);
        if (icon != null)
          return this.AddIcon(icon, category, type, data);
      }
    }
    return -1;
  }

  private Icon BestIcon(Icon icon, Size size) => icon;

  public int AddIcon(Icon icon, int category) => this.AddIcon(icon, category, -1, (object) null);

  public int AddIcon(Icon icon, int category, int type)
  {
    return this.AddIcon(icon, category, type, (object) null);
  }

  public int AddIcon(Icon icon, int category, int type, object data)
  {
    if (!this._syncronizer.InvokeRequired)
      return this.AddIconInternal(icon, category, type, data);
    return (int) this._syncronizer.Invoke((Delegate) new CategoryTypeIconService.AddIconHandler(this.AddIconInternal), (object) icon, (object) category, (object) type, data);
  }

  private int AddIconInternal(Icon icon, int category, int type, object data)
  {
    lock (this._syncRoot)
    {
      CategoryTypeIconService.CatType key1 = new CategoryTypeIconService.CatType(category, type, data);
      if (this._categories.ContainsKey(key1))
      {
        int category1 = this._categories[key1];
        if (category1 >= 0)
        {
          this._icons.Remove(category1);
          this._categories.Remove(key1);
        }
        else
        {
          Bitmap bitmap = icon.ToBitmap();
          if (icon.Height == icon.Width)
            this._imageList16.Images[category1] = (Image) bitmap;
          this._imageList32.Images[category1] = (Image) bitmap;
          ++this._imageListsUpdateCount;
          return category1;
        }
      }
      if (icon == null)
        return 0;
      Bitmap bitmap1 = icon.ToBitmap();
      if (icon.Height != icon.Width)
      {
        this._imageList16.Images.Add((Image) bitmap1);
      }
      else
      {
        using (Icon icon1 = ImagesResizeHelper.ResizeIconTo32x16(icon, this._imageList16.TransparentColor))
        {
          this._imageList16.Images.Add((Image) icon1.ToBitmap());
          icon1.Dispose();
        }
      }
      this._imageList32.Images.Add((Image) bitmap1);
      int key2 = this._imageList16.Images.Count - 1;
      this._categories.Add(key1, key2);
      if (icon.Height == 16 /*0x10*/)
      {
        this._iconData[key2] = this.InternalGetIconData(icon);
        icon = (Icon) null;
      }
      else if (icon.Height == icon.Width)
      {
        this._iconData[key2] = this.InternalGetIconData(icon);
        icon = (Icon) null;
      }
      else
      {
        using (Icon icon2 = ImagesResizeHelper.ResizeIconTo32x16(icon, this._imageList16.TransparentColor))
        {
          this._iconData[key2] = this.InternalGetIconData(icon2);
          icon2.Dispose();
          icon = (Icon) null;
        }
      }
      ++this._imageListsUpdateCount;
      icon = (Icon) null;
      return key2;
    }
  }

  public int IndexOf(int category) => this.IndexOf(category, -1, (object) null);

  public int IndexOf(int category, int type) => this.IndexOf(category, type, (object) null);

  public int IndexOf(int category, int type, object data)
  {
    if (category == Intermech.Navigator.Consts.CategoryLifeCycleStepNode)
    {
      IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(type);
      if (lcStep != null)
      {
        type = lcStep.LevelID;
        category = 8;
      }
    }
    CategoryTypeIconService.CatType key = new CategoryTypeIconService.CatType(category, type, data);
    if (this._categories.ContainsKey(key))
      return this._categories[key];
    int icon = this.OnFindIcon(category, type, data);
    if (icon > 0)
      return icon;
    int num = type != -1 ? this.IndexOf(category, -1, data) : 0;
    this._categories.Add(key, num);
    return num;
  }

  public ImageList ImageList => this._imageList16;

  public ImageList BigImageList => this._imageList32;

  public Icon GetIcon(int category) => this.GetIcon(category, -1);

  public Icon GetIcon(int category, int type) => this.GetIcon(category, type, (object) null);

  public Icon GetIcon(int category, int type, object data)
  {
    return this.InternalGetIcon(this.IndexOf(category, type, data));
  }

  private Icon InternalGetIcon(int index)
  {
    lock (this._syncRoot)
    {
      if (this._icons.ContainsKey(index))
        return this._icons[index];
      Icon icon = this.ConvertBytesToIcon(this._iconData[index]);
      this._icons[index] = icon;
      return icon;
    }
  }

  private Icon ConvertBytesToIcon(byte[] iconBytes)
  {
    try
    {
      this._tempStream.Write(iconBytes, 0, iconBytes.Length);
      this._tempStream.Flush();
      this._tempStream.Position = 0L;
      return new Icon((Stream) this._tempStream);
    }
    finally
    {
      this._tempStream.SetLength(0L);
    }
  }

  public Icon GetIndexIcon(int index) => this.InternalGetIcon(index);

  public Icon GetIconEx(int category) => this.GetIconEx(category, -1);

  public Icon GetIconEx(int category, int type) => this.GetIconEx(category, type, (object) null);

  public Icon GetIconEx(int category, int type, object data)
  {
    int index = this.IndexOf(category, type, data);
    return index == 0 ? (Icon) null : this.InternalGetIcon(index);
  }

  public void BeginUpdate()
  {
    if (this._updateCount == 0)
    {
      this._imageListsUpdateCount = 0;
      this.DisableImageListEvents(this._imageList16);
      this.DisableImageListEvents(this._imageList32);
    }
    ++this._updateCount;
  }

  public void EndUpdate()
  {
    if (this._updateCount <= 0)
      return;
    --this._updateCount;
    if (this._updateCount != 0)
      return;
    this.EnableImageListEvents(this._imageList16);
    this.EnableImageListEvents(this._imageList32);
    if (this._imageListsUpdateCount == 0)
      return;
    this._imageListsUpdateCount = 0;
    this.CallImageListChangeEvents(this._imageList16);
    this.CallImageListChangeEvents(this._imageList32);
  }

  public void Dispose()
  {
    if (this._imageList16 != null)
      this._imageList16.Dispose();
    if (this._imageList32 != null)
      this._imageList32.Dispose();
    this._imageList16 = (ImageList) null;
    this._imageList32 = (ImageList) null;
    this._iconData.Clear();
    this._iconData = (Dictionary<int, byte[]>) null;
    this._categories.Clear();
    this._categories = (Dictionary<CategoryTypeIconService.CatType, int>) null;
  }

  internal class CatType
  {
    public int _cat;
    public int _type;
    public object _data;

    public CatType(int category)
      : this(category, 0, (object) null)
    {
    }

    public CatType(int category, int type)
      : this(category, type, (object) null)
    {
    }

    public CatType(int category, int type, object data)
    {
      this._cat = category;
      this._type = type;
      this._data = data;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is CategoryTypeIconService.CatType catType))
        return base.Equals(obj);
      return this._cat == catType._cat && this._type == catType._type && object.Equals(this._data, catType._data);
    }

    public override int GetHashCode()
    {
      int hashCode = this._cat.GetHashCode() << 20 ^ this._type.GetHashCode() << 12;
      if (this._data != null)
        hashCode ^= this._data.GetHashCode();
      return hashCode;
    }
  }

  public delegate int AddIconHandler(Icon icon, int category, int type, object data);
}
