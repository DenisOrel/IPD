// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SectionSortSchema
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary> Схема сортировки записей в некотором разделе спецификации </summary>
[Serializable]
public class SectionSortSchema : IWriteReadXml, ICloneable
{
  private bool _vedomostiSection;
  private string sectionName = string.Empty;
  private Guid _sectionGuid = Guid.Empty;
  private AttributeSortSchema[] attributeSortSchemas = new AttributeSortSchema[0];

  public SectionSortSchema(IUserSession iUserSession, Guid sectionGuid, string sectionName)
  {
    this.sectionName = sectionName;
    this._sectionGuid = sectionGuid;
  }

  public SectionSortSchema(IUserSession iUserSession, Guid sectionGuid)
  {
    this.SetSectionGuid(iUserSession, sectionGuid);
  }

  public SectionSortSchema(string tripleName)
  {
    this.sectionName = tripleName;
    this._vedomostiSection = true;
  }

  public SectionSortSchema() => this._sectionGuid = Guid.NewGuid();

  public SectionSortSchema(params AttributeSortSchema[] attributeSortSchemas)
  {
    this._sectionGuid = Guid.NewGuid();
    this.attributeSortSchemas = attributeSortSchemas;
  }

  public bool VedomostiSection => this._vedomostiSection;

  /// <summary> Уникальный идентификатор группы настроек </summary>
  public Guid SectionGuid => this._sectionGuid;

  public void SetSectionGuid(IUserSession iUserSession, Guid value)
  {
    this._sectionGuid = value;
    this.sectionName = string.Empty;
    if (!(this._sectionGuid != Guid.Empty) || iUserSession == null)
      return;
    if (!SpecificationSectionInfo.Cached)
      SpecificationSectionInfo.CacheSpecSections(iUserSession);
    if (!(SpecificationSectionInfo.SectionDictionaryByGuid[(object) value] is SpecificationSectionInfo specificationSectionInfo))
      return;
    this.sectionName = specificationSectionInfo.Caption;
  }

  /// <summary> Наименование раздела </summary>
  public string SectionName
  {
    get => this.sectionName;
    set
    {
      this.sectionName = value;
      this._vedomostiSection = true;
      this._sectionGuid = Guid.Empty;
    }
  }

  /// <summary>Уровни сортировки</summary>
  public AttributeSortSchema[] AttributeSortSchemas
  {
    get => this.attributeSortSchemas;
    set => this.attributeSortSchemas = value;
  }

  public AttributeSortSchema[] CloneAttributeSortSchemas()
  {
    AttributeSortSchema[] attributeSortSchemaArray = new AttributeSortSchema[this.attributeSortSchemas.Length];
    for (int index = 0; index < this.attributeSortSchemas.Length; ++index)
      attributeSortSchemaArray[index] = this.attributeSortSchemas[index].Clone();
    return attributeSortSchemaArray;
  }

  public int Add(AttributeSortSchema attributeSortSchema)
  {
    AttributeSortSchema[] attributeSortSchemaArray = new AttributeSortSchema[this.attributeSortSchemas.Length + 1];
    this.attributeSortSchemas.CopyTo((Array) attributeSortSchemaArray, 0);
    attributeSortSchemaArray[attributeSortSchemaArray.Length - 1] = attributeSortSchema;
    this.AttributeSortSchemas = attributeSortSchemaArray;
    return attributeSortSchemaArray.Length - 1;
  }

  public void Insert(AttributeSortSchema attributeSortSchema, int index)
  {
    if (index < 0 || index > this.attributeSortSchemas.Length)
      throw new ArgumentOutOfRangeException(nameof (index));
    AttributeSortSchema[] destinationArray = new AttributeSortSchema[this.attributeSortSchemas.Length + 1];
    if (index > 0)
      Array.Copy((Array) this.attributeSortSchemas, (Array) destinationArray, index);
    destinationArray[index] = attributeSortSchema;
    if (index < destinationArray.Length - 1)
      Array.Copy((Array) this.attributeSortSchemas, index, (Array) destinationArray, index + 1, this.attributeSortSchemas.Length - index);
    this.AttributeSortSchemas = destinationArray;
  }

  public int IndexOf(AttributeSortSchema attributeSortSchema)
  {
    for (int index = 0; index < this.attributeSortSchemas.Length; ++index)
    {
      if (this.attributeSortSchemas[index] == attributeSortSchema)
        return index;
    }
    return -1;
  }

  public void Remove(AttributeSortSchema attributeSortSchema)
  {
    this.Remove(this.IndexOf(attributeSortSchema));
  }

  public void Remove(int index)
  {
    if (index < 0 || index >= this.attributeSortSchemas.Length)
      return;
    AttributeSortSchema[] attributeSortSchemaArray = new AttributeSortSchema[this.attributeSortSchemas.Length - 1];
    for (int index1 = 0; index1 < index; ++index1)
      attributeSortSchemaArray[index1] = this.attributeSortSchemas[index1];
    for (int index2 = index; index2 < attributeSortSchemaArray.Length; ++index2)
      attributeSortSchemaArray[index2] = this.attributeSortSchemas[index2 + 1];
    this.AttributeSortSchemas = attributeSortSchemaArray;
  }

  public List<AvsRowAttributeInfo> GetAllAttrInfo()
  {
    List<AvsRowAttributeInfo> allAttrInfo = new List<AvsRowAttributeInfo>(this.attributeSortSchemas.Length);
    for (int index = 0; index < this.attributeSortSchemas.Length; ++index)
      allAttrInfo.Add(this.attributeSortSchemas[index].GetAttrInfo());
    return allAttrInfo;
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    SubstringStartFinishType substringEndType,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, 1, "-", substringEndType, 1, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="endPosition"> Номер буквы, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    SubstringStartFinishType substringEndType,
    int endPosition,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, 1, "-", substringEndType, endPosition, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FromNFoundSubstring или FromEndFoundNSubstring) </param>
  /// <param name="endPosition"> Номер символа, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="endSubstring"> Символ, на котором надо заканчивать обрезать строку </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    SubstringStartFinishType substringEndType,
    int endPosition,
    string endSubstring,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FromNFoundSubstring && substringEndType != SubstringStartFinishType.FromEndFoundNSubstring)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, 1, "-", substringEndType, endPosition, endSubstring, compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="startPosition"> Номер буквы, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    SubstringStartFinishType substringEndType,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, startPosition, "-", substringEndType, 1, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="startPosition"> Номер буквы, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="endPosition"> Номер буквы, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    SubstringStartFinishType substringEndType,
    int endPosition,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, startPosition, "-", substringEndType, endPosition, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="startPosition"> Номер буквы, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FromNFoundSubstring или FromEndFoundNSubstring) </param>
  /// <param name="endPosition"> Номер буквы, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="endSubstring"> Символ, на котором надо заканчивать обрезать строку </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    SubstringStartFinishType substringEndType,
    int endPosition,
    string endSubstring,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FromNFoundSubstring && substringEndType != SubstringStartFinishType.FromEndFoundNSubstring)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, startPosition, "-", substringEndType, endPosition, endSubstring, compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FromNFoundSubstring или FromEndFoundNSubstring) </param>
  /// <param name="startPosition"> Номер символа, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="startSubstring"> Символ, на котором надо начинать обрезать строку </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FinishStart) </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    string startSubstring,
    SubstringStartFinishType substringEndType,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FromNFoundSubstring && substringStartType != SubstringStartFinishType.FromEndFoundNSubstring)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FinishStart)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, startPosition, startSubstring, substringEndType, 1, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку (SubstringStartFinishType.FromNFoundSubstring или FromEndFoundNSubstring) </param>
  /// <param name="startPosition"> Номер символа, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="startSubstring"> Символ, на котором надо начинать обрезать строку </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку (SubstringStartFinishType.FromNPosition) </param>
  /// <param name="endPosition"> Номер буквы, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    string startSubstring,
    SubstringStartFinishType substringEndType,
    int endPosition,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType != SubstringStartFinishType.FromNFoundSubstring && substringStartType != SubstringStartFinishType.FromEndFoundNSubstring)
      throw new Exception("wrong substringStartType");
    if (substringEndType != SubstringStartFinishType.FromNPosition)
      throw new Exception("wrong substringStartType");
    return this.AddAttributeSortSchema(iUserSession, attributeID, attrSrc, substringStartType, startPosition, startSubstring, substringEndType, endPosition, "-", compareType, sortOrder, emptyOrder);
  }

  /// <summary> Добавить в схему сортировки новое правило </summary>
  /// <param name="iUserSession"></param>
  /// <param name="attributeID"> Идентификатор атрибута </param>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="substringStartType"> С чего начинать обрезать строку </param>
  /// <param name="startPosition"> Номер буквы / символа, с котого должно начаться вырезание подстроки для сортировки </param>
  /// <param name="startSubstring"> Символ, на котором надо начинать обрезать строку </param>
  /// <param name="substringEndType"> На чем заканчивать обрезать строку </param>
  /// <param name="endPosition"> Номер буквы / символа, на котором должно начаться вырезание подстроки для сортировки </param>
  /// <param name="endSubstring"> Символ, на котором надо заканчивать обрезать строку </param>
  /// <param name="compareType"> Сравнивать как число или как строки </param>
  /// <param name="sortOrder"> Направление сортировки (по возрастанию или по убыванию) </param>
  /// <param name="emptyOrder"> Куда помещать пустые подстроки </param>
  /// <returns> Созданое правило сортировки </returns>
  public AttributeSortSchema AddAttributeSortSchema(
    IUserSession iUserSession,
    int attributeID,
    FieldSource attrSrc,
    SubstringStartFinishType substringStartType,
    int startPosition,
    string startSubstring,
    SubstringStartFinishType substringEndType,
    int endPosition,
    string endSubstring,
    CompareType compareType,
    SortOrder sortOrder,
    EmptyOrder emptyOrder)
  {
    if (substringStartType == SubstringStartFinishType.Unknow)
      throw new Exception("wrong substringStartType");
    if (substringEndType == SubstringStartFinishType.Unknow)
      throw new Exception("wrong substringStartType");
    AttributeSortSchema attributeSortSchema = new AttributeSortSchema();
    attributeSortSchema.SetAttributeID(iUserSession, attributeID);
    attributeSortSchema.AttrSrc = attrSrc;
    attributeSortSchema.SubstringStartType = substringStartType;
    attributeSortSchema.StartPosition = startPosition;
    attributeSortSchema.StartSubstring = startSubstring;
    attributeSortSchema.SubstringEndType = substringEndType;
    attributeSortSchema.EndPosition = endPosition;
    attributeSortSchema.EndSubstring = endSubstring;
    attributeSortSchema.CompareType = compareType;
    attributeSortSchema.SortOrder = sortOrder;
    attributeSortSchema.EmptyOrder = emptyOrder;
    this.Add(attributeSortSchema);
    return attributeSortSchema;
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("AttributeSortSchemas" == readArgs.Reader.LocalName)
    {
      this.attributeSortSchemas = (AttributeSortSchema[]) WriteReadXmlHelper.ReadArrayFromXml(typeof (AttributeSortSchema), readArgs);
      return true;
    }
    if ("sectionGuid" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      Guid result;
      if (Guid.TryParse(readArgs.Reader.Value, out result))
        this.SetSectionGuid(readArgs.IUserSession, result);
      return true;
    }
    if ("VedomostiSectionName" == readArgs.Reader.LocalName)
    {
      if (!readArgs.Reader.HasValue)
        readArgs.Reader.Read();
      this.sectionName = readArgs.Reader.Value;
      this._vedomostiSection = true;
    }
    return false;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    if (!this._vedomostiSection)
      xw.WriteAttributeString("sectionGuid", this._sectionGuid.ToString());
    else
      xw.WriteAttributeString("VedomostiSectionName", this.SectionName);
    WriteReadXmlHelper.WriteArrayToXml("AttributeSortSchemas", (IList) this.attributeSortSchemas, "AttributeSortSchema", xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    Array attributeSortSchemas = (Array) this.attributeSortSchemas;
    if (attributeSortSchemas.Length == this.attributeSortSchemas.Length)
      return;
    this.attributeSortSchemas = (AttributeSortSchema[]) attributeSortSchemas;
  }

  /// <summary> Заполнение по-умолчанию для ведомости </summary>
  public void LoadDefaultVedomostiSchema(IUserSession iUserSession)
  {
    if (this.SectionName == "По умолчанию")
      this.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
    else
      this.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
  }

  /// <summary> Процедура валидаци настроек сортировки атрибутов </summary>
  public bool ValidateAttribute(object arrayItem)
  {
    return arrayItem != null && arrayItem is AttributeSortSchema && ((AttributeSortSchema) arrayItem).AttributeID != 0;
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public virtual SectionSortSchema Clone()
  {
    SectionSortSchema instance = (SectionSortSchema) Activator.CreateInstance(this.GetType());
    instance.attributeSortSchemas = this.CloneAttributeSortSchemas();
    instance._sectionGuid = this.SectionGuid;
    instance.sectionName = this.sectionName;
    instance._vedomostiSection = this._vedomostiSection;
    return instance;
  }
}
