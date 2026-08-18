// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ThumbnailItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.Views;

internal struct ThumbnailItem(long imgID, int typeID)
{
  private long _imageID = imgID;
  private int _typeID = typeID;
  private object _image = (object) null;

  public object Image
  {
    get => this._image;
    set => this._image = value;
  }

  public long ImageID => this._imageID;

  public int TypeID => this._typeID;
}
