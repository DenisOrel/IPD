// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ObjectTypeHolder
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Описатель класса типа объекта</summary>
[Serializable]
public class ObjectTypeHolder : ISerializable, ICloneable
{
  private Guid _objectTypeGuid = Guid.Empty;
  private string _objectTypeName = LocalizationHolder.rm.GetString("Expert_12");

  /// <summary>Версия класса</summary>
  private ObjectTypeHolder()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeGuid">Guid типа объекта</param>
  /// <param name="session">юзерская сессия</param>
  public ObjectTypeHolder(Guid objectTypeGuid, IUserSession session)
  {
    if (objectTypeGuid.Equals(Guid.Empty))
      return;
    IDBObjectType objectType = session.GetObjectType(objectTypeGuid);
    this._objectTypeGuid = objectTypeGuid;
    this._objectTypeName = objectType.ObjectTypeName;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeID">идентификатор типа объекта</param>
  /// <param name="session">юзерская сессия</param>
  public ObjectTypeHolder(int objectTypeID, IUserSession session)
  {
    if (objectTypeID.Equals(-1))
      return;
    IDBObjectType objectType = session.GetObjectType(objectTypeID);
    this._objectTypeGuid = (objectType as IDBGuid).GUID;
    this._objectTypeName = objectType.ObjectTypeName;
  }

  /// <summary>Конструктор без сессии</summary>
  /// <param name="objectTypeGuid">идентификатор типа объекта</param>
  /// <param name="objectTypeName">наименование типа объекта</param>
  public ObjectTypeHolder(Guid objectTypeGuid, string objectTypeName)
  {
    if (objectTypeGuid.Equals(Guid.Empty))
      return;
    this._objectTypeGuid = objectTypeGuid;
    this._objectTypeName = objectTypeName;
  }

  /// <summary>Guid типа объекта</summary>
  public Guid Guid => this._objectTypeGuid;

  /// <summary>Наименование типа объекта</summary>
  public string Name => this._objectTypeName;

  /// <summary>Объект в строку</summary>
  /// <returns></returns>
  public override string ToString() => this._objectTypeName;

  /// <summary>Проверка на равенство</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    return obj.GetType().Equals(typeof (ObjectTypeHolder)) ? this._objectTypeGuid.Equals((obj as ObjectTypeHolder).Guid) : base.Equals(obj);
  }

  /// <summary>Получения хэш кода</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected ObjectTypeHolder(SerializationInfo info, StreamingContext context)
  {
    Dictionary<string, Type> paramsType = SerializationInfoHelper.GetParamsType(info);
    Type type = (Type) null;
    ref Type local = ref type;
    if (paramsType.TryGetValue("TypeG", out local))
    {
      this._objectTypeGuid = new Guid(info.GetString("TypeG"));
      this._objectTypeName = info.GetString("TypeN");
    }
    else
    {
      this._objectTypeGuid = new Guid(info.GetString("TypeGuid"));
      this._objectTypeName = info.GetString("TypeName");
    }
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("TypeG", (object) this._objectTypeGuid.ToString());
    info.AddValue("TypeN", (object) this._objectTypeName);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new ObjectTypeHolder()
    {
      _objectTypeGuid = this._objectTypeGuid,
      _objectTypeName = this._objectTypeName
    };
  }
}
