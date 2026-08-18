
// Type: Intermech.Client.Core.CompositionView.CompDBTypedObjectID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Composition's dbTyped object class</summary>
public class CompDBTypedObjectID : ICompDBTypedObjectID, IDBTypedObjectID, IDBObjectID
{
  /// <summary>
  /// 
  /// </summary>
  private int _objectType;
  /// <summary>
  /// 
  /// </summary>
  private long _objectId;
  /// <summary>
  /// 
  /// </summary>
  private long _owner;
  /// <summary>
  /// 
  /// </summary>
  private long _id;
  /// <summary>
  /// 
  /// </summary>
  private string _caption;
  /// <summary>
  /// 
  /// </summary>
  private long _version;
  /// <summary>
  /// 
  /// </summary>
  private long _baseVersion;
  /// <summary>
  /// 
  /// </summary>
  private string _siteID;
  /// <summary>
  /// 
  /// </summary>
  private object _infoObject;
  /// <summary>
  /// 
  /// </summary>
  private long _modificationID;

  /// <summary>Constructor</summary>
  /// <param name="value"></param>
  /// <param name="infoObject"></param>
  public CompDBTypedObjectID(IDBTypedObjectID value, object infoObject)
  {
    if (value == null)
    {
      this.Clear();
    }
    else
    {
      this._id = value.ID;
      this._objectType = value.ObjectType;
      this._objectId = value.ObjectID;
      this._caption = value.Caption;
      this._owner = value.Owner;
      this._version = value.Version;
      this._baseVersion = value.BaseVersion;
      this._siteID = value.SiteID;
      this._infoObject = infoObject;
      this._modificationID = value.ModificationID;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  long IDBObjectID.Value => this.ObjectID;

  /// <summary>
  /// 
  /// </summary>
  public int ObjectType
  {
    get => this._objectType;
    set => this._objectType = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public long ObjectID => this._objectId;

  /// <summary>
  /// 
  /// </summary>
  public long ID => this._id;

  /// <summary>
  /// 
  /// </summary>
  public long Owner => this._owner;

  /// <summary>
  /// 
  /// </summary>
  public string Caption => this._caption;

  /// <summary>
  /// 
  /// </summary>
  public long Version => this._version;

  /// <summary>
  /// 
  /// </summary>
  public long BaseVersion => this._baseVersion;

  /// <summary>Owner/navigator's view node</summary>
  public object InfoObject
  {
    get => this._infoObject;
    set => this._infoObject = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public string SiteID => this._siteID;

  /// <summary>
  /// 
  /// </summary>
  public long ModificationID => this._modificationID;

  /// <summary>Clear data</summary>
  public void Clear()
  {
    this._objectType = -1;
    this._objectId = 0L;
    this._owner = 0L;
    this._id = 0L;
    this._caption = string.Empty;
    this._version = 0L;
    this._baseVersion = 0L;
    this._infoObject = (object) null;
  }
}
