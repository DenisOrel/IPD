
// Type: Intermech.PropertyEditors.ObjectPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ObjectPropertyClass
{
  /// <summary>
  /// Вариант реализации ObjectPropertyClass - обычный или как текущий пользователь или как-то еще
  /// </summary>
  protected ObjectPropertyClassVariant objectPropertyClassVariant;
  /// <summary>Ид. версии объекта</summary>
  protected long _objectID;
  /// <summary>Тип объекта</summary>
  protected int _objectTypeID;
  /// <summary>Заголовок объекта</summary>
  [NonSerialized]
  protected string _caption;
  /// <summary>флажок для обработки на null</summary>
  protected bool _nullObject;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  public ObjectPropertyClassVariant ObjectPropertyClassVariant => this.objectPropertyClassVariant;

  /// <summary>
  /// Флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  public bool ObjectVersionProcessed => this.objectVersionProcessed;

  public ObjectPropertyClass(ObjectPropertyClassVariant opcv, bool _objectVersionProcessed = true)
    : this(opcv == ObjectPropertyClassVariant.opcvCurrentUser ? 0L : 0L, opcv == ObjectPropertyClassVariant.opcvCurrentUser ? CoreConsts.CurrentUserCaption : (string) null, _objectVersionProcessed)
  {
    this.objectPropertyClassVariant = opcv;
    if (opcv != ObjectPropertyClassVariant.opcvCurrentUser)
      return;
    this._objectTypeID = MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
  }

  /// <summary>Конструктор</summary>
  /// <param name="aObjectID"></param>
  /// <param name="aCaption"></param>
  public ObjectPropertyClass(long aObjectID, string aCaption, bool _objectVersionProcessed = true)
    : this(aObjectID, aCaption, CoreConsts.NegativeIdDefaultMCaption, _objectVersionProcessed)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="aObjectID"></param>
  /// <param name="aCaption"></param>
  /// <param name="aNegativeIDCaption"></param>
  public ObjectPropertyClass(
    long aObjectID,
    string aCaption,
    string aNegativeIDCaption,
    bool _objectVersionProcessed = true)
  {
    this._objectID = aObjectID;
    this._caption = aObjectID != -1L ? aCaption : aNegativeIDCaption;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  /// <summary>Конструктор</summary>
  /// <param name="aObjectID"></param>
  public ObjectPropertyClass(long aObjectID, bool _objectVersionProcessed = true)
    : this(aObjectID, (string) null, _objectVersionProcessed)
  {
  }

  /// <summary>
  /// Ид. версии объекта / Ид. объекта (при !_objectVersionProcessed)
  /// </summary>
  public long ObjectID => this._objectID;

  public long ObjectTypeID
  {
    get
    {
      if (this._objectTypeID == 0)
      {
        try
        {
          IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
          QuickObjectInfo quickObjectInfo = this.objectVersionProcessed ? service.GetObjectInfo(this._objectID) : service.GetObjectInfoByID(this._objectID);
          if (!quickObjectInfo.Empty)
          {
            this._objectTypeID = quickObjectInfo.ObjectTypeID;
            this._caption = this._caption ?? quickObjectInfo.Caption;
          }
        }
        catch
        {
        }
      }
      return (long) this._objectTypeID;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool NullObject
  {
    get => this._nullObject;
    set => this._nullObject = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual string Caption
  {
    get
    {
      if (this._objectID == -1L || this._caption != null)
        return this._caption;
      if (this._objectID == 0L)
        return this._caption = string.Empty;
      using (new SessionKeeper())
      {
        try
        {
          IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
          QuickObjectInfo quickObjectInfo = this.objectVersionProcessed ? service.GetObjectInfo(this._objectID) : service.GetObjectInfoByID(this._objectID);
          if (!quickObjectInfo.Empty)
          {
            this._objectTypeID = quickObjectInfo.ObjectTypeID;
            return this._caption = quickObjectInfo.Caption;
          }
        }
        catch
        {
        }
        return this._caption = LocalizationHolder.rm.GetString("Client.Core_1019") + (this.objectVersionProcessed ? " versionID=" : " ID=") + this._objectID.ToString();
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.Caption;
}
