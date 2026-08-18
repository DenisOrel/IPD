// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SortSchema
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Expert;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary> Общая схема сортировки спецификации </summary>
[Serializable]
public class SortSchema : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  public const string DefaultSchema = "По умолчанию";
  public SectionSortSchema[] _sectionSortSchemas = new SectionSortSchema[0];
  private long _ownerObjectID;
  private SortSchema _parentLevel;
  private bool _readOnly;
  [NonSerialized]
  private SettingsLevel _level;
  private bool _changed;
  private List<Triple> _tripleList;
  private bool sortPartForPodborAfterBasePart;
  private bool sortDocumentsByType = true;

  public SortSchema(
    IUserSession iUserSession,
    SortSchema parentLevel,
    long ownerObjectID,
    SettingsLevel level)
    : this(iUserSession, parentLevel, ownerObjectID, level, (List<Triple>) null)
  {
  }

  public SortSchema(
    IUserSession iUserSession,
    SortSchema parentLevel,
    long ownerObjectID,
    SettingsLevel level,
    List<Triple> tripleList)
  {
    this._tripleList = tripleList;
    this._parentLevel = parentLevel;
    this._level = level;
    this.SetOwnerObjectID(iUserSession, ownerObjectID);
  }

  public SortSchema()
  {
  }

  /// <summary> Идентификатор объекта, в настройках которого должны храниться настройки </summary>
  public long OwnerObjectID => this._ownerObjectID;

  public void SetOwnerObjectID(IUserSession iUserSession, long value)
  {
    this._ownerObjectID = value;
    this.LoadParams(iUserSession);
  }

  /// <summary> Признак что используются собственные настройки (если false то настройки читавются из вышестоящего уровня настроек) </summary>
  public bool Changed
  {
    get => this._changed;
    set => this._changed = value;
  }

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public SortSchema ParentLevel => this._parentLevel;

  /// <summary> Дексриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly => this._readOnly;

  /// <summary> Параметры сортировки разделов </summary>
  public SectionSortSchema[] SectionSortSchemas
  {
    get => this._sectionSortSchemas;
    set => this._sectionSortSchemas = value;
  }

  /// <summary> Список разделов ведомостей </summary>
  public List<Triple> TripleList
  {
    get => this._tripleList;
    set => this._tripleList = value;
  }

  /// <summary>Размещать копмоненты для подбора рядом с основными компонентами</summary>
  public bool SortPartForPodborAfterBasePart
  {
    get => this.sortPartForPodborAfterBasePart;
    set => this.sortPartForPodborAfterBasePart = value;
  }

  /// <summary>Сортировать документы по типам</summary>
  public bool SortDocumentsByType
  {
    get => this.sortDocumentsByType;
    set => this.sortDocumentsByType = value;
  }

  public void LoadAttributeFieldNames(IUserSession iUserSession)
  {
    Dictionary<int, string> dictionary = new Dictionary<int, string>();
    if (this._sectionSortSchemas == null)
      return;
    foreach (SectionSortSchema sectionSortSchema in this._sectionSortSchemas)
    {
      if (sectionSortSchema != null && sectionSortSchema.AttributeSortSchemas != null)
      {
        foreach (AttributeSortSchema attributeSortSchema in sectionSortSchema.AttributeSortSchemas)
        {
          if (dictionary.ContainsKey(attributeSortSchema.AttributeID))
          {
            attributeSortSchema.FieldName = dictionary[attributeSortSchema.AttributeID];
          }
          else
          {
            IDBAttributeType attributeType = iUserSession.GetAttributeType(attributeSortSchema.AttributeID);
            if (attributeType != null && attributeType.FieldNames.Length != 0)
              attributeSortSchema.FieldName = attributeType.FieldNames[0];
          }
        }
      }
    }
  }

  /// <summary>Загрузка параметров из объекта с guid-ом = OwnerGuid</summary>
  public void LoadParams(IUserSession iUserSession)
  {
    if (this.OwnerObjectID == 0L)
      return;
    try
    {
      IDBObject dbObject = iUserSession.GetObjectByID(this.OwnerObjectID, false) ?? iUserSession.GetObject(this.OwnerObjectID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_SortSchema);
        if (attributeById != null)
        {
          bool flag = false;
          if (attributeById is IBlobReader blobReader)
          {
            BlobInformation blobInformation = blobReader.OpenBlob(0);
            long dataBlockSize = Math.Max(blobInformation.RealFileSize, blobInformation.PackedFileSize);
            if (dataBlockSize > 0L)
            {
              MemoryStream inStream = (MemoryStream) null;
              try
              {
                byte[] buffer = blobReader.ReadDataBlock((int) dataBlockSize);
                if (buffer.Length != 0)
                {
                  inStream = new MemoryStream(buffer);
                  if (inStream.Length > 0L)
                  {
                    inStream.Seek(0L, SeekOrigin.Begin);
                    inStream.Write(buffer, 0, buffer.Length);
                    inStream.Seek(0L, SeekOrigin.Begin);
                    if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
                    {
                      MemoryStream outStream = new MemoryStream();
                      ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
                      inStream = outStream;
                      inStream.Seek(0L, SeekOrigin.Begin);
                    }
                    if (inStream.Length != 0L)
                    {
                      try
                      {
                        WriteReadXmlHelper.LoadFromXmlDocument(iUserSession, (Stream) inStream, (IWriteReadXml) this, typeof (SortSchema).Name);
                      }
                      catch
                      {
                        this.LoadDefaultSchema(iUserSession);
                      }
                      if (!this.Changed)
                        this.LoadDefaultSchema(iUserSession);
                      else
                        flag = true;
                    }
                    else
                      this.LoadDefaultSchema(iUserSession);
                  }
                }
              }
              finally
              {
                inStream?.Close();
                blobReader.CloseBlob();
              }
            }
          }
          if (!flag)
            this.LoadDefaultSchema(iUserSession);
          this._readOnly = attributeById.ReadOnly && dbObject.ObjectID > 0L && dbObject.CheckoutBy != 0L;
        }
        else
        {
          this.LoadDefaultSchema(iUserSession);
          this._readOnly = false;
        }
        if (this._readOnly || dbObject.ObjectModifyMode != ObjectModifyModes.CantModify && dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          return;
        this._readOnly = this._tripleList == null;
      }
      else
      {
        this.LoadDefaultSchema(iUserSession);
        this._readOnly = this._tripleList == null;
      }
    }
    catch (Exception ex)
    {
      this._readOnly = this._tripleList == null;
      throw ex;
    }
  }

  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  public void SaveParams()
  {
    if (this.ReadOnly)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_SortSchema);
  }

  /// <summary> Получить схему сортировки раздела по его идентификатору. Может вернуть null !!! </summary>
  /// <param name="sectionGuid"> Guid раздела спецификации </param>
  /// <returns> Схема сортировки раздела. Может вернуть null !!! </returns>
  public SectionSortSchema GetSectionSchemaBySectionGuid(Guid sectionGuid)
  {
    foreach (SectionSortSchema sectionSortSchema in this._sectionSortSchemas)
    {
      if (sectionSortSchema.SectionGuid == sectionGuid)
        return sectionSortSchema;
    }
    return (SectionSortSchema) null;
  }

  /// <summary> Получить схему сортировки раздела по его идентификатору. Может вернуть null !!! </summary>
  /// <param name="tripleName"> Наименование раздела ведомости </param>
  /// <returns> Схема сортировки раздела. Может вернуть null !!! </returns>
  public SectionSortSchema GetSectionSchemaByTripleName(string tripleName)
  {
    foreach (SectionSortSchema sectionSortSchema in this._sectionSortSchemas)
    {
      if (sectionSortSchema.SectionName == tripleName)
        return sectionSortSchema;
    }
    return (SectionSortSchema) null;
  }

  /// <summary> Получить схему сортировки по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема сортировки </returns>
  public SortSchema GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level || this._level == null)
      return this;
    return this._parentLevel != null ? this._parentLevel.GetSchemaByLevel(level) : (SortSchema) null;
  }

  /// <summary> Загрузить схему по умолчанию </summary>
  public void LoadDefaultSchema(IUserSession iUserSession)
  {
    if (this._parentLevel != null)
    {
      SortSchema parentLevel = this._parentLevel;
      while (!parentLevel.Changed && parentLevel.ParentLevel != null)
        parentLevel = parentLevel.ParentLevel;
      this.CopyParamsFrom(parentLevel);
    }
    else
    {
      if (this._tripleList != null || this._level == null || !this._level.IsRoot)
        return;
      this.SortPartForPodborAfterBasePart = false;
      this.SortDocumentsByType = true;
      if (AVSDocumentsSettings.IsSpecificationDocType(this._level.DocumentType))
      {
        this._sectionSortSchemas = new SectionSortSchema[8];
        SectionSortSchema sectionSortSchema1 = new SectionSortSchema(iUserSession, new Guid("cad00256-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema1.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema1.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema1.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema1, 0);
        SectionSortSchema sectionSortSchema2 = new SectionSortSchema(iUserSession, new Guid("cad00257-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema2.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema2.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema2.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema2, 1);
        SectionSortSchema sectionSortSchema3 = new SectionSortSchema(iUserSession, new Guid("cad00258-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema3.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema3.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema3.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema3, 2);
        SectionSortSchema sectionSortSchema4 = new SectionSortSchema(iUserSession, new Guid("cad00259-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema4.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema4.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema4.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema4, 3);
        SectionSortSchema sectionSortSchema5 = new SectionSortSchema(iUserSession, new Guid("cad0025a-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema5.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema5.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema5.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema5.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema5, 4);
        SectionSortSchema sectionSortSchema6 = new SectionSortSchema(iUserSession, new Guid("cad0025b-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema6.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema6.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema6.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema6.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema6, 5);
        SectionSortSchema sectionSortSchema7 = new SectionSortSchema(iUserSession, new Guid("cad0025c-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema7.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema7.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema7.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema7.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema7, 6);
        SectionSortSchema sectionSortSchema8 = new SectionSortSchema(iUserSession, new Guid("cad0025d-306c-11d8-b4e9-00304f19f545"));
        sectionSortSchema8.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
        sectionSortSchema8.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema8.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema8.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema8, 7);
      }
      else
      {
        if (this._level.DocumentType != AVSDocumentType.ElementList)
          return;
        this._sectionSortSchemas = new SectionSortSchema[1];
        SectionSortSchema sectionSortSchema = new SectionSortSchema(iUserSession, AvsIDCache.ObjIdElementListSortChapterGuid);
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cae06280-554b-44fb-8ad3-70a3c9f7fc3c");
        if (attributeTypeId != -1)
          sectionSortSchema.AddAttributeSortSchema(iUserSession, attributeTypeId, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_PosDesignation, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        sectionSortSchema.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
        this._sectionSortSchemas.SetValue((object) sectionSortSchema, 0);
      }
    }
  }

  public List<AvsRowAttributeInfo> GetAllAttrInfo()
  {
    List<AvsRowAttributeInfo> allAttrInfo = new List<AvsRowAttributeInfo>();
    for (int index = 0; index < this._sectionSortSchemas.Length; ++index)
      allAttrInfo.AddRange((IEnumerable<AvsRowAttributeInfo>) this._sectionSortSchemas[index].GetAllAttrInfo());
    return allAttrInfo;
  }

  public void AddSectionScheme(
    IUserSession iUserSession,
    Guid sectionGuid,
    string sectionName,
    int index)
  {
    if (sectionGuid == Guid.Empty || string.IsNullOrWhiteSpace(sectionName) || index < 0)
      return;
    SectionSortSchema element = new SectionSortSchema(iUserSession, sectionGuid, sectionName);
    element.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_SortAVS, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToEnd);
    element.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Podbor, FieldSource.Relation, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
    element.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Designation, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Text, SortOrder.Ascending, EmptyOrder.ToBegin);
    element.AddAttributeSortSchema(iUserSession, AvsIDCache.Attr_Name, FieldSource.Object, SubstringStartFinishType.FinishStart, SubstringStartFinishType.FinishStart, CompareType.Number, SortOrder.Ascending, EmptyOrder.ToBegin);
    if (index >= this._sectionSortSchemas.Length)
      this._sectionSortSchemas = ((IEnumerable<SectionSortSchema>) this._sectionSortSchemas).ToList<SectionSortSchema>().Append<SectionSortSchema>(element).ToArray<SectionSortSchema>();
    else if (index == 0)
      this._sectionSortSchemas = new List<SectionSortSchema>()
      {
        element
      }.Concat<SectionSortSchema>((IEnumerable<SectionSortSchema>) this._sectionSortSchemas).ToArray<SectionSortSchema>();
    else
      this._sectionSortSchemas = ((IEnumerable<SectionSortSchema>) this._sectionSortSchemas).Take<SectionSortSchema>(index).Concat<SectionSortSchema>((IEnumerable<SectionSortSchema>) new List<SectionSortSchema>()
      {
        element
      }).Concat<SectionSortSchema>(((IEnumerable<SectionSortSchema>) this._sectionSortSchemas).Skip<SectionSortSchema>(index)).ToArray<SectionSortSchema>();
  }

  public void RemoveSectionScheme(Guid sectionGuid)
  {
    this._sectionSortSchemas = ((IEnumerable<SectionSortSchema>) this._sectionSortSchemas).Where<SectionSortSchema>((Func<SectionSortSchema, bool>) (s => s.SectionGuid != sectionGuid)).ToArray<SectionSortSchema>();
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public virtual SortSchema Clone()
  {
    SortSchema instance = (SortSchema) Activator.CreateInstance(this.GetType());
    instance.CopyParamsFrom(this);
    return instance;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(SortSchema copy)
  {
    this._sectionSortSchemas = new SectionSortSchema[copy.SectionSortSchemas.Length];
    for (int index = 0; index < copy.SectionSortSchemas.Length; ++index)
      this._sectionSortSchemas[index] = copy.SectionSortSchemas[index].Clone();
    this.SortPartForPodborAfterBasePart = copy.SortPartForPodborAfterBasePart;
    this.SortDocumentsByType = copy.SortDocumentsByType;
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if ("SectionsSortParams" == readArgs.Reader.LocalName)
    {
      this._sectionSortSchemas = (SectionSortSchema[]) WriteReadXmlHelper.ReadArrayFromXml(typeof (SectionSortSchema), readArgs);
      this._changed = true;
      return true;
    }
    if ("SortPartForPodborAfterBasePart" == readArgs.Reader.LocalName)
    {
      this.SortPartForPodborAfterBasePart = readArgs.Reader.Value == "1";
      this._changed = true;
      return true;
    }
    if (!("SortDocumentsByType" == readArgs.Reader.LocalName))
      return false;
    this.SortDocumentsByType = readArgs.Reader.Value == "1";
    this._changed = true;
    return true;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    if (this.Changed || this.ParentLevel == null)
    {
      xw.WriteAttributeString("SortPartForPodborAfterBasePart", this.SortPartForPodborAfterBasePart ? "1" : "0");
      xw.WriteAttributeString("SortDocumentsByType", this.SortDocumentsByType ? "1" : "0");
      WriteReadXmlHelper.WriteArrayToXml("SectionsSortParams", (IList) this._sectionSortSchemas, typeof (SectionSortSchema).Name, xw, objectRefId);
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.IUserSession == null)
      throw new Exception("XmlReadArgs.IUserSession must be init");
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (!this._changed)
      this.LoadDefaultSchema(readArgs.IUserSession);
    Array array = ArrayEditHelper.DeleteValues((Array) this._sectionSortSchemas, new ArrayEditHelper.ValidateItemDelegate(this.ValidateSection));
    if (array.Length == this._sectionSortSchemas.Length)
      return;
    this._sectionSortSchemas = (SectionSortSchema[]) array;
  }

  /// <summary> Процедура валидаци настроек сортировки записей в разделе </summary>
  public bool ValidateSection(object arrayItem)
  {
    return arrayItem is SectionSortSchema sectionSortSchema && sectionSortSchema.SectionGuid != Guid.Empty;
  }

  public void SaveToXmlDocument(string fileName)
  {
    WriteReadXmlHelper.WriteXmlDocument(fileName, (IWriteReadXml) this, typeof (SortSchema).Name);
  }

  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, typeof (SortSchema).Name);
  }
}
