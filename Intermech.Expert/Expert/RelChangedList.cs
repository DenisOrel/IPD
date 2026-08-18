// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.RelChangedList
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Список измененных атрибутов связи (для показа в окне измененных атрибутов)
/// </summary>
[Serializable]
public class RelChangedList : List<AttrChange>
{
  private long _relID;
  private int _relType;
  private long _childVerId;

  /// <summary>
  /// Идентификатор связи, которой принадлежат изменяемые атрибуты
  /// </summary>
  public long RelId
  {
    get => this._relID;
    set => this._relID = value;
  }

  /// <summary>
  /// Тип связи - из него при показе получаем название связи, а также определяем, будет ли изменен объект
  /// </summary>
  public int RelType
  {
    get => this._relType;
    set => this._relType = value;
  }

  /// <summary>
  /// ИД версии объекта, на который ссылается связь. Если нет конкретизации версии,
  /// будет выбрана первая попавшаяся, но для показа какая-то версия должна быть
  /// </summary>
  public long ChildVerId
  {
    get => this._childVerId;
    set => this._childVerId = value;
  }

  /// <summary>Общий конструктор, если все параметры уже известны</summary>
  /// <param name="relId">ИД связи</param>
  /// <param name="relType">ИД типа связи</param>
  /// <param name="childVerId">ИД версии объекта, на которую ссылается связь</param>
  public RelChangedList(long relId, int relType, long childVerId)
  {
    this._relID = relId;
    this._relType = relType;
    this._childVerId = childVerId;
  }

  /// <summary>Конструктор из пользовательской сессии</summary>
  /// <param name="relId">ИД связи</param>
  /// <param name="ius">Пользовательская сессия</param>
  public RelChangedList(long relId, IUserSession ius)
  {
    this._relID = relId;
    IDBRelation relation = ius.GetRelation(this._relID, true);
    this._relType = relation.RelationType;
    IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
    if (attributeById != null && attributeById.Value != DBNull.Value)
    {
      long int64 = Convert.ToInt64(attributeById.Value);
      if (!ius.GetObjectInfo(int64).Empty)
        this._childVerId = int64;
    }
    if (this._childVerId != 0L)
      return;
    this._childVerId = ius.GetObjectByID(relation.PartID, true).ObjectID;
  }
}
