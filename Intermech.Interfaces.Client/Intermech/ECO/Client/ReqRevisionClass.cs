// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ReqRevisionClass
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ECO.Client;

public class ReqRevisionClass
{
  /// <summary>
  /// Способ создания версии объекта - по умолчанию извещение не требуем
  /// </summary>
  private ReqRevision data;
  public int _lcId = -1;
  public int _objectType = -1;
  private InheritModes _overriden = InheritModes.Public;

  public ReqRevision Value => this.data;

  public ReqRevisionClass(int lcId, int objTypeID, bool readOnly)
  {
    this._lcId = lcId;
    this._objectType = objTypeID;
    this.LoadStep(lcId, readOnly);
  }

  public ReqRevisionClass(ReqRevision d) => this.data = d;

  public ReqRevisionClass(string s)
  {
    this.data = (ReqRevision) EnumTypeHelper.GetEnumValue(typeof (ReqRevision), s, (object) ReqRevision.NoRevision);
  }

  public override string ToString() => EnumTypeHelper.GetCaption((Enum) this.data);

  public void SetString(string s)
  {
    this.data = (ReqRevision) EnumTypeHelper.GetEnumValue(typeof (ReqRevision), s, (object) ReqRevision.NoRevision);
  }

  /// <summary>Сохранить изменения в атрибут объекта - контейнера</summary>
  /// <param name="stepID">Шаг жизненного цикла</param>
  /// <returns>True если все ок</returns>
  public bool SaveStep(int stepID)
  {
    this._lcId = stepID;
    bool flag = false;
    if (stepID >= 0 && !this._overriden.Equals((object) InheritModes.Inherited))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IDBAttribute idbAT = (session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForLCStep((object) session.SessionGUID, stepID, true).Attributes.AddAttribute(RevReqHelper.idAttrNewRevNeed, false);
        Dictionary<int, ReqRevision> ht = ReqRevisionClass.LoadAttrValues(idbAT);
        ht[this._objectType] = this.data;
        ReqRevisionClass.SaveAttrValues(idbAT, ht);
        RevReqHelper.SetRevReq(stepID, this._objectType, this.data);
      }
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// Загрузить все значения атрибута шага ЖЦ "способ изменения" в словарь
  /// </summary>
  /// <param name="idbAT">Обработчик атрибута</param>
  /// <returns>Словарь ReqRevision'ов для каждого типа объекта</returns>
  internal static Dictionary<int, ReqRevision> LoadAttrValues(IDBAttribute idbAT)
  {
    Dictionary<int, ReqRevision> dictionary = new Dictionary<int, ReqRevision>();
    foreach (object obj in idbAT.Values)
    {
      if (obj != DBNull.Value)
      {
        long num = (long) obj;
        int key = (int) (num >> 32 /*0x20*/);
        ReqRevision reqRevision = (ReqRevision) (num & (long) uint.MaxValue);
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, reqRevision);
      }
    }
    return dictionary;
  }

  internal static void SaveAttrValues(IDBAttribute idbAT, Dictionary<int, ReqRevision> ht)
  {
    object[] objArray = new object[ht.Count];
    int num1 = 0;
    foreach (int key in ht.Keys)
    {
      int num2 = (int) ht[key];
      objArray[num1++] = (object) (((long) key << 32 /*0x20*/) + (long) num2);
    }
    idbAT.Values = objArray;
  }

  /// <summary>Загрузить данные из контейнера</summary>
  /// <param name="step">Шаг жизненного цикла</param>
  /// <param name="isReadOnly">Только для чтения</param>
  private void LoadStep(int step, bool isReadOnly)
  {
    this._lcId = step;
    if (step <= 0)
      return;
    this.data = ReqRevisionClass.GetDefaultValue(this._objectType);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (this._objectType != -1)
      {
        IDBObjectType objectType = session.GetObjectType(this._objectType);
        this._overriden = isReadOnly ? InheritModes.Inherited : objectType.PublicLC;
      }
      else
        this._overriden = isReadOnly ? InheritModes.Inherited : InheritModes.Public;
      IDBObject containerForLcStep = (session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForLCStep((object) session.SessionGUID, step);
      if (containerForLcStep == null)
        return;
      IDBAttribute attributeById1 = containerForLcStep.GetAttributeByID(RevReqHelper.idAttrNewRevNeed);
      if (attributeById1 != null)
      {
        Dictionary<int, ReqRevision> dictionary = ReqRevisionClass.LoadAttrValues(attributeById1);
        if (!dictionary.ContainsKey(this._objectType))
          return;
        this.data = dictionary[this._objectType];
      }
      else
      {
        IDBAttribute attributeById2 = containerForLcStep.GetAttributeByID(RevReqHelper.idAttrRevNeed);
        if (attributeById1 == null)
          return;
        this.data = (ReqRevision) attributeById2.AsInteger;
      }
    }
  }

  /// <summary>
  /// Получение значение флага по умолчанию для типа объекта
  /// </summary>
  /// <param name="objectType">Ид. типа ообъекта</param>
  /// <returns></returns>
  internal static ReqRevision GetDefaultValue(int objectType)
  {
    List<int> objectTypeParentsId = objectType != -1 ? MetaDataHelper.GetObjectTypeParentsID(objectType) : (List<int>) null;
    return objectTypeParentsId == null || objectTypeParentsId.Count <= 0 ? ReqRevision.NoRevision : ReqRevision.Inherited;
  }
}
