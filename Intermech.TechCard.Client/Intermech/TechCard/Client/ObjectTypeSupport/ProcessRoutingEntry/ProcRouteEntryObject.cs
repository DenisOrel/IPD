// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.ProcRouteEntryObject
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>
/// Класс для работы с атрибутами привязок объекта Входимость маршрута обработки
/// </summary>
public class ProcRouteEntryObject
{
  /// <summary>
  /// 
  /// </summary>
  private long _objectId;
  private long _memberOfProductionReportObject;
  private long _memberOfProductionReportVersion;
  private Guid _memberOfExitAssembly;
  private IEnumerable<Guid> _memberOfAssemblyCopy = (IEnumerable<Guid>) new Guid[0];
  private long _memberOfOrderVersion;
  private long _memberOfOrderObject;
  private IEnumerable<long> _memberOfAssemblyVersion;
  private IEnumerable<long> _memberOfAssemblyObject;

  /// <summary>Событие будет дёргаться при необходимости</summary>
  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  /// <summary>
  /// Идентификатор версии объекта "Входимость маршрута обработки"
  /// </summary>
  /// <param name="objectId"></param>
  public ProcRouteEntryObject(long objectId)
  {
    this._objectId = objectId != 0L ? objectId : throw new ArgumentException($"Parameter {nameof (objectId)} cannot be {0L}");
  }

  /// <summary>
  /// 
  /// </summary>
  public void ClearData()
  {
    this.MemberOfProductionReportObject = 0L;
    this.MemberOfProductionReportVersion = 0L;
    this.MemberOfExitAssembly = Guid.Empty;
    this.MemberOfAssemblyCopy = (IEnumerable<Guid>) new Guid[0];
    this.MemberOfOrderVersion = 0L;
    this.MemberOfOrderObject = 0L;
    this._memberOfAssemblyVersion = (IEnumerable<long>) new long[0];
    this._memberOfAssemblyObject = (IEnumerable<long>) new long[0];
  }

  /// <summary>Загрузить данные из объекта</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public bool LoadData([NotNull] IUserSession session)
  {
    this.ClearData();
    AttributeValues[] attributesValues = session.GetObjectAttributesValues(this._objectId, new int[8]
    {
      TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID,
      TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrID,
      TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID,
      TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID,
      TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID,
      TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID,
      TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID,
      TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID
    }, GetAttributeValuesModes.None, true);
    AttributeValues[] array = attributesValues != null ? ((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((Func<AttributeValues, bool>) (item => item != null)).ToArray<AttributeValues>() : (AttributeValues[]) null;
    if (array == null)
      return false;
    AttributeValues attributeValues1 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID));
    if (attributeValues1 != null && attributeValues1.Value != DBNull.Value)
      this.MemberOfProductionReportObject = attributeValues1.AsInteger;
    AttributeValues attributeValues2 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrID));
    if (attributeValues2 != null && attributeValues2.Value != DBNull.Value)
      this.MemberOfProductionReportVersion = attributeValues2.AsInteger;
    AttributeValues attributeValues3 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID));
    if (attributeValues3 != null && attributeValues3.Value != DBNull.Value)
    {
      string asString = attributeValues3.AsString;
      if (GuidHelper.IsGuid(asString))
        this.MemberOfExitAssembly = new Guid(asString);
    }
    AttributeValues attributeValues4 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID));
    if (attributeValues4?.Values != null)
    {
      List<Guid> guidList = new List<Guid>();
      foreach (object obj in attributeValues4.Values)
      {
        string str = Convert.ToString(obj);
        if (GuidHelper.IsGuid(str))
          guidList.Add(new Guid(str));
      }
      this.MemberOfAssemblyCopy = (IEnumerable<Guid>) guidList;
    }
    AttributeValues attributeValues5 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID));
    if (attributeValues5 != null && attributeValues5.Value != DBNull.Value)
      this.MemberOfOrderVersion = attributeValues5.AsInteger;
    AttributeValues attributeValues6 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID));
    if (attributeValues6 != null && attributeValues6.Value != DBNull.Value)
      this.MemberOfOrderObject = attributeValues6.AsInteger;
    AttributeValues attributeValues7 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID));
    if (attributeValues7?.Values != null)
    {
      List<long> longList = new List<long>();
      foreach (object obj in attributeValues7.Values)
      {
        long result;
        if (long.TryParse(obj?.ToString(), out result))
          longList.Add(result);
      }
      this.MemberOfAssemblyVersion = (IEnumerable<long>) longList;
    }
    AttributeValues attributeValues8 = ((IEnumerable<AttributeValues>) array).FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (item => item.AttributeID == TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID));
    if (attributeValues8?.Values != null)
    {
      List<long> longList = new List<long>();
      foreach (object obj in attributeValues8.Values)
      {
        long result;
        if (long.TryParse(obj?.ToString(), out result))
          longList.Add(result);
      }
      this.MemberOfAssemblyObject = (IEnumerable<long>) longList;
    }
    return true;
  }

  /// <summary>Сохранить данные в объект</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns></returns>
  public bool SaveData([NotNull] IUserSession session)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID, this.MemberOfProductionReportObject != 0L ? (object) this.MemberOfProductionReportObject : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrID, this.MemberOfProductionReportVersion != 0L ? (object) this.MemberOfProductionReportVersion : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, this.MemberOfExitAssembly != Guid.Empty ? (object) this.MemberOfExitAssembly : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID, this.MemberOfAssemblyCopy.Any<Guid>() ? (object) this.MemberOfAssemblyCopy.ConvertAll<Guid, object>((Converter<Guid, object>) (item => (object) item)).ToArray<object>() : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID, this.MemberOfOrderVersion != 0L ? (object) this.MemberOfOrderVersion : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID, this.MemberOfOrderObject != 0L ? (object) this.MemberOfOrderObject : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID, this.MemberOfAssemblyVersion.Any<long>() ? (object) this.MemberOfAssemblyVersion.ConvertAll<long, object>((Converter<long, object>) (item => (object) item)).ToArray<object>() : (object) DBNull.Value));
    attributeValuesList.Add(new AttributeValues(TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID, this._memberOfAssemblyObject.Any<long>() ? (object) this._memberOfAssemblyObject.ConvertAll<long, object>((Converter<long, object>) (item => (object) item)).ToArray<object>() : (object) DBNull.Value));
    if (attributeValuesList.Count == 0)
      return false;
    session.SetObjectAttributesValues(this._objectId, true, attributeValuesList.ToArray());
    return true;
  }

  /// <summary>Изменить статус привязки для сборки</summary>
  /// <param name="assembly"></param>
  /// <param name="addAssembly"></param>
  public void SetModifyStateAssembly(
    ObjInfoIDItem assembly,
    bool bindingToVersion,
    bool addAssembly)
  {
    long num = bindingToVersion ? Math.Abs(assembly.ObjectID) : assembly.ID;
    List<long> longList = new List<long>(bindingToVersion ? this.MemberOfAssemblyVersion : this.MemberOfAssemblyObject);
    if (addAssembly)
    {
      if (addAssembly)
        longList.Add(num);
    }
    else
      longList.Remove(num);
    if (bindingToVersion)
      this.MemberOfAssemblyVersion = (IEnumerable<long>) longList;
    else
      this.MemberOfAssemblyObject = (IEnumerable<long>) longList;
  }

  /// <summary>
  /// Идентификатор версии объекта "Входимость маршрута обработки"
  /// </summary>
  public long ObjectId
  {
    get => this._objectId;
    set => this._objectId = value;
  }

  /// <summary>Входимость - объект производственной ведомости</summary>
  public long MemberOfProductionReportObject
  {
    get => this._memberOfProductionReportObject;
    set
    {
      if (this._memberOfProductionReportObject == value)
        return;
      this._memberOfProductionReportObject = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - версию производственной ведомости</summary>
  public long MemberOfProductionReportVersion
  {
    get => this._memberOfProductionReportVersion;
    set
    {
      if (this._memberOfProductionReportVersion == value)
        return;
      this._memberOfProductionReportVersion = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - выходная сборка</summary>
  public Guid MemberOfExitAssembly
  {
    get => this._memberOfExitAssembly;
    set
    {
      if (this._memberOfExitAssembly == value)
        return;
      this._memberOfExitAssembly = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - ПК сборки</summary>
  public IEnumerable<Guid> MemberOfAssemblyCopy
  {
    get => this._memberOfAssemblyCopy;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (MemberOfAssemblyCopy));
      if (this._memberOfAssemblyCopy.SequenceEqual<Guid>(value))
        return;
      this._memberOfAssemblyCopy = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - Версия заказа</summary>
  public long MemberOfOrderVersion
  {
    get => this._memberOfOrderVersion;
    set
    {
      if (this._memberOfOrderVersion == value)
        return;
      this._memberOfOrderVersion = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - заказ</summary>
  public long MemberOfOrderObject
  {
    get => this._memberOfOrderObject;
    set
    {
      if (this._memberOfOrderObject == value)
        return;
      this._memberOfOrderObject = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - Версия сборки</summary>
  public IEnumerable<long> MemberOfAssemblyVersion
  {
    get => this._memberOfAssemblyVersion;
    set
    {
      if (value == null)
        throw new ArgumentNullException("_memberOfAssemblyVersion");
      if (this._memberOfAssemblyVersion.SequenceEqual<long>(value))
        return;
      this._memberOfAssemblyVersion = value;
      this.OnChanged();
    }
  }

  /// <summary>Входимость - Сборка</summary>
  public IEnumerable<long> MemberOfAssemblyObject
  {
    get => this._memberOfAssemblyObject;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (MemberOfAssemblyObject));
      if (this._memberOfAssemblyObject.SequenceEqual<long>(value))
        return;
      this._memberOfAssemblyObject = value;
      this.OnChanged();
    }
  }

  /// <summary>Привязка к версиям объектов</summary>
  public bool MemberBindingToVersions
  {
    get => this.MemberOfAssemblyVersion.Any<long>() || this.MemberOfOrderVersion != 0L;
  }

  /// <summary>Событие изменения данных</summary>
  public event EventHandler Changed;
}
