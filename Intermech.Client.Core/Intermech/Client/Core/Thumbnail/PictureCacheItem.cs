
// Type: Intermech.Client.Core.Thumbnail.PictureCacheItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>
/// 
/// </summary>
internal class PictureCacheItem
{
  internal Guid _objectGuid;
  internal long _objectId;
  internal long _blobId;
  internal object _picture;
  internal string _pictureFileName;
  internal string _cacheFileName = string.Empty;
  internal DateTime _date;
  internal int _used;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectGuid"></param>
  /// <param name="objectId"></param>
  /// <param name="picture"></param>
  /// <param name="pictureFileName"></param>
  /// <param name="date"></param>
  public PictureCacheItem(
    Guid objectGuid,
    long objectId,
    object picture,
    string pictureFileName,
    DateTime date)
  {
    this._objectGuid = objectGuid;
    this._objectId = objectId;
    this._picture = picture;
    this._pictureFileName = pictureFileName;
    this._date = date;
    this._used = 1;
  }
}
