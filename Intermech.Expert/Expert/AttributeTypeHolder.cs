// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.AttributeTypeHolder
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert;

/// <summary>Описатель класса тип атрибута</summary>
[Serializable]
public class AttributeTypeHolder : ISerializable, ICloneable
{
  private Guid _attributeTypeGuid = Guid.Empty;
  private string _attributeTypeName = string.Empty;
  private int _masterAttributeID;
  private int _sourceAttributeID;
  private FieldTypes _fieldTypes;
  private List<string> strValues;
  private bool strValuesUpdated;
  private MeasureDescriptor _savedMeasure;
  /// <summary>Версия класса</summary>
  public static int Version = 100;

  private AttributeTypeHolder()
  {
  }

  /// <summary>Конструктор по Guid</summary>
  /// <param name="attributeTypeGuid">Guid типа атрибута</param>
  /// <param name="session">юзерская сессия</param>
  public AttributeTypeHolder(Guid attributeTypeGuid, IUserSession session)
  {
    this._Init(session.GetAttributeType(attributeTypeGuid));
  }

  /// <summary>Конструктор по typeID</summary>
  /// <param name="attributeTypeID">идентификатор типа атрибута</param>
  /// <param name="session">юзерская сессия</param>
  public AttributeTypeHolder(int attributeTypeID, IUserSession session)
  {
    this._Init(session.GetAttributeType(attributeTypeID));
  }

  /// <summary>Конструктор по IDBAttributeType</summary>
  /// <param name="attributeType">Интерфейс типа атрибута</param>
  public AttributeTypeHolder(IDBAttributeType attributeType) => this._Init(attributeType);

  private void _Init(IDBAttributeType attributeType)
  {
    this._attributeTypeGuid = (attributeType as IDBGuid).GUID;
    this._attributeTypeName = attributeType.Name;
    this._masterAttributeID = attributeType.MasterAttributeID;
    this._sourceAttributeID = attributeType.SourceAttributeID;
    this._fieldTypes = attributeType.PropertiesStructure.FieldType;
    this.LoadStrValues(attributeType);
  }

  /// <summary>Конструктор без юзерской сессии</summary>
  /// <param name="attributeTypeGuid">идентификатор типа атрибута</param>
  /// <param name="attributeTypeName">наименование тип атрибута</param>
  /// <param name="masterAttributeID">идентификтор мастер-атрибута</param>
  /// <param name="sourceAttributeID">идентификатор атрибута-источника</param>
  /// <param name="attributeFieldType">тип атрибута</param>
  public AttributeTypeHolder(
    Guid attributeTypeGuid,
    string attributeTypeName,
    int masterAttributeID,
    int sourceAttributeID,
    FieldTypes attributeFieldType)
  {
    this._attributeTypeGuid = attributeTypeGuid;
    this._attributeTypeName = attributeTypeName;
    this._masterAttributeID = masterAttributeID;
    this._sourceAttributeID = sourceAttributeID;
    this._fieldTypes = attributeFieldType;
  }

  private void LoadStrValues(IDBAttributeType idbAT)
  {
    DataTable possibleValues = idbAT.GetPossibleValues();
    if (possibleValues == null || possibleValues.Rows.Count <= 0)
      return;
    this.strValues = new List<string>();
    foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
    {
      string str = "";
      if (possibleValues.Columns.IndexOf("F_STRING_VALUE") >= 0)
        str = Convert.ToString(row["F_STRING_VALUE"]);
      if (str == "")
        str = Convert.ToString(row["F_DESCRIPTION"]);
      this.strValues.Add(str);
    }
  }

  public void UpdateStrValues(IUserSession ius)
  {
    if (this.strValuesUpdated)
      return;
    this.strValuesUpdated = true;
    IDBAttributeType attributeType = ius.GetAttributeType(this._attributeTypeGuid);
    if (attributeType == null)
      return;
    this.LoadStrValues(attributeType);
  }

  /// <summary>Guid типа атрибута</summary>
  public Guid Guid => this._attributeTypeGuid;

  /// <summary>Наименование типа атрибута</summary>
  public string Name => this._attributeTypeName;

  /// <summary>Тип атрибута</summary>
  public FieldTypes FieldTypes
  {
    get => this._fieldTypes;
    set => this._fieldTypes = value;
  }

  /// <summary>Идентификатор мастер атрибута</summary>
  public int MasterAttributeID => this._masterAttributeID;

  /// <summary>Идентификатор значения</summary>
  public int SourceAttributeID => this._sourceAttributeID;

  /// <summary>Получение строки</summary>
  /// <returns></returns>
  public override string ToString() => this._attributeTypeName;

  /// <summary>Проверка на равенство</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    return obj.GetType().Equals(typeof (AttributeTypeHolder)) ? this._attributeTypeGuid.Equals((obj as AttributeTypeHolder).Guid) : base.Equals(obj);
  }

  /// <summary>Получение хэш кода</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  public List<string> StrValues => this.strValues;

  public MeasureDescriptor SavedMeasure
  {
    get => this._savedMeasure;
    set => this._savedMeasure = value;
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected AttributeTypeHolder(SerializationInfo info, StreamingContext context)
  {
    Dictionary<string, Type> paramsType = SerializationInfoHelper.GetParamsType(info);
    Type type = (Type) null;
    if (paramsType.TryGetValue("TypeG", out type))
    {
      this._attributeTypeGuid = new Guid(info.GetString("TypeG"));
      this._attributeTypeName = info.GetString("TypeN");
      this._fieldTypes = (FieldTypes) info.GetInt64("F_Types");
      this._masterAttributeID = info.GetInt32("MA_ID");
      this._sourceAttributeID = info.GetInt32("SA_ID");
    }
    else
    {
      this._attributeTypeGuid = new Guid(info.GetString("TypeGuid"));
      this._attributeTypeName = info.GetString("TypeName");
      this._fieldTypes = (FieldTypes) EnumTypeHelper.GetEnumValue(typeof (FieldTypes), info.GetString(nameof (FieldTypes)), (object) FieldTypes.ftUnknown);
      this._masterAttributeID = info.GetInt32(nameof (MasterAttributeID));
      this._sourceAttributeID = info.GetInt32(nameof (SourceAttributeID));
    }
    if (!paramsType.TryGetValue("strValue0", out type))
      return;
    if (this.strValues == null)
      this.strValues = new List<string>();
    else
      this.strValues.Clear();
    int num = 0;
    while (true)
    {
      string str = "strValue" + Convert.ToString(num);
      if (paramsType.TryGetValue(str, out type))
      {
        this.strValues.Add(info.GetString(str));
        ++num;
      }
      else
        break;
    }
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("TypeG", (object) this._attributeTypeGuid.ToString());
    info.AddValue("TypeN", (object) this._attributeTypeName);
    info.AddValue("F_Types", (long) this._fieldTypes);
    info.AddValue("MA_ID", this._masterAttributeID);
    info.AddValue("SA_ID", this._sourceAttributeID);
    if (this.strValues == null || this.strValues.Count <= 0)
      return;
    for (int index = 0; index < this.strValues.Count; ++index)
      info.AddValue("strValue" + Convert.ToString(index), (object) this.strValues[index]);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new AttributeTypeHolder()
    {
      _attributeTypeGuid = this._attributeTypeGuid,
      _attributeTypeName = this._attributeTypeName,
      _fieldTypes = this._fieldTypes,
      _masterAttributeID = this._masterAttributeID,
      _sourceAttributeID = this._sourceAttributeID
    };
  }
}
