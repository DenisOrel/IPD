
// Type: Intermech.Client.Core.Thumbnail.ThumbnailItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>
/// 
/// </summary>
public class ThumbnailItem
{
  private int _typeId;
  private long _pictureObjectId;
  private long _objectId;
  private INodeID _nodeID;
  private object _image;
  private object _tag;
  private string _name;

  /// <summary>
  /// 
  /// </summary>
  public object Image
  {
    get => this._image;
    set => this._image = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public string Name => this._name;

  /// <summary>
  /// 
  /// </summary>
  public INodeID NodeID => this._nodeID;

  /// <summary>
  /// 
  /// </summary>
  public long ObjectId => this._objectId;

  /// <summary>
  /// 
  /// </summary>
  public long PictureObjectId
  {
    get => this._pictureObjectId;
    set => this._pictureObjectId = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public int TypeId => this._typeId;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="caption"></param>
  /// <param name="objectId"></param>
  /// <param name="typeId"></param>
  public ThumbnailItem(INodeID nodeID, string caption, long objectId, int typeId)
  {
    this._nodeID = nodeID;
    this._name = !string.IsNullOrEmpty(caption) ? caption : $"[{objectId}]";
    this._typeId = typeId;
    this._pictureObjectId = objectId;
    this._objectId = objectId;
    this._image = (object) null;
  }

  public void SetValues(string name, long objectId, int objectTypeId)
  {
    this._name = name;
    this._objectId = objectId;
    this._typeId = objectTypeId;
    this.CleanCache();
  }

  /// <summary>
  /// 
  /// </summary>
  public void CleanCache()
  {
    this._image = (object) null;
    this._pictureObjectId = this._objectId;
  }

  public void Clear()
  {
    this._objectId = -1L;
    this._typeId = -1;
    this.CleanCache();
  }
}
