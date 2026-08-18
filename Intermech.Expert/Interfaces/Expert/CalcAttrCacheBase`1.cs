// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.CalcAttrCacheBase`1
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Базовый кеш для "расчитываемых" атрибутов</summary>
/// <typeparam name="T"></typeparam>
public class CalcAttrCacheBase<T> : Dictionary<CalcAttrPair, T>
{
  /// <summary>Спец запись для поиска по содержимому кеша</summary>
  protected CalcAttrPair _searchRec = new CalcAttrPair(-1L, -1);

  /// <summary>Конструктор</summary>
  public CalcAttrCacheBase()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="capacity"></param>
  public CalcAttrCacheBase(int capacity)
    : base(capacity)
  {
  }

  /// <summary>Поиск записи в кеше</summary>
  /// <param name="objId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="attrTypeId"></param>
  public virtual bool ContainsAttr(long objId, int objTypeId, int attrTypeId)
  {
    lock (this._searchRec)
    {
      this._searchRec._objID = objId;
      this._searchRec._objTypeID = objTypeId;
      this._searchRec._attrTypeID = attrTypeId;
      return this.ContainsKey(this._searchRec);
    }
  }

  /// <summary>Добавление атрибута в кеш (с проверкой наличия)</summary>
  /// <param name="objId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="attrTypeId"></param>
  /// <param name="value"></param>
  public virtual void AddAttr(long objId, int objTypeId, int attrTypeId, T value)
  {
    this.AddAttr(objId, objTypeId, attrTypeId, value, false);
  }

  /// <summary>
  /// Добавление /обновление атрибута в кеш (с проверкой наличия)
  /// </summary>
  /// <param name="objId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="attrTypeId"></param>
  /// <param name="value"></param>
  /// <param name="updateIfExist"></param>
  public virtual void AddAttr(
    long objId,
    int objTypeId,
    int attrTypeId,
    T value,
    bool updateIfExist)
  {
    bool flag = this.ContainsAttr(objId, objTypeId, attrTypeId);
    if (flag && !updateIfExist)
      return;
    if (!flag)
    {
      this.Add(new CalcAttrPair(objId, objTypeId, attrTypeId), value);
    }
    else
    {
      lock (this._searchRec)
      {
        this._searchRec._objID = objId;
        this._searchRec._objTypeID = objTypeId;
        this._searchRec._attrTypeID = attrTypeId;
        this[this._searchRec] = value;
      }
    }
  }

  /// <summary>Поиск записи в кеше</summary>
  /// <param name="objId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="attrTypeId"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public virtual bool TryGetValue(long objId, int objTypeId, int attrTypeId, out T value)
  {
    lock (this._searchRec)
    {
      this._searchRec._objID = objId;
      this._searchRec._objTypeID = objTypeId;
      this._searchRec._attrTypeID = attrTypeId;
      return this.TryGetValue(this._searchRec, out value);
    }
  }

  /// <summary>Удаление атрибута из кеша</summary>
  /// <param name="objId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="attrTypeId"></param>
  public virtual bool Remove(long objId, int objTypeId, int attrTypeId)
  {
    lock (this._searchRec)
    {
      this._searchRec._objID = objId;
      this._searchRec._objTypeID = objTypeId;
      this._searchRec._attrTypeID = attrTypeId;
      return this.Remove(this._searchRec);
    }
  }
}
