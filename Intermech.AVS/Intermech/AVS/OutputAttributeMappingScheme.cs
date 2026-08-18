// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OutputAttributeMappingScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSProperties;
using Intermech.AVS.Output;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace Intermech.AVS;

/// <summary>Схема назначения атрибутов для вывода в спецификации</summary>
public class OutputAttributeMappingScheme : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  private static CellOutputMapping[] defaultSpecificationCells;
  private static CellOutputMapping[] defaultElementListCells;
  private static Dictionary<AvsRowAttributeInfo, string> noteFieldOutputAttributes;
  public const string RootNodeSectionGuid = "00000000-0000-0000-0000-000000000000";
  public const string AllObjTypesGuid = "00000000-0000-0000-0000-000000000000";
  private long _ownerObjectID = -1;
  private readonly OutputAttributeMappingScheme _parent;
  private readonly SettingsLevel _level;
  private bool _readOnly;
  protected XDocument xmlScheme;

  public OutputAttributeMappingScheme(
    OutputAttributeMappingScheme parent,
    long ownerObjectID,
    SettingsLevel level)
  {
    this._parent = parent;
    this._level = level;
    this._ownerObjectID = ownerObjectID;
    this.LoadParams();
  }

  /// <summary> Идентификатор объекта, в атрибутах которого хранится схема </summary>
  public long OwnerObjectID
  {
    get => this._ownerObjectID;
    set
    {
      this._ownerObjectID = value;
      this.LoadParams();
    }
  }

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public OutputAttributeMappingScheme Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  internal List<CellOutputMapping> CellMaping { get; set; } = new List<CellOutputMapping>();

  /// <summary>
  /// Вернуть перечисление всех объектов маппинга с учетом наследования и переопределения
  /// </summary>
  internal IEnumerable<CellOutputMapping> GetOverallMappingList()
  {
    if (this.Parent == null)
      return (IEnumerable<CellOutputMapping>) this.CellMaping;
    List<CellOutputMapping> parentLevelMapping = this.Parent.GetOverallMappingList().ToList<CellOutputMapping>();
    return parentLevelMapping.SelectMany<CellOutputMapping, CellOutputMapping, CellOutputMapping>((Func<CellOutputMapping, IEnumerable<CellOutputMapping>>) (upper => this.CellMaping.Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (i => i.CellId == upper.CellId && i.SectionGuid == upper.SectionGuid && i.ObjTypeGuid == upper.ObjTypeGuid)).DefaultIfEmpty<CellOutputMapping>()), (Func<CellOutputMapping, CellOutputMapping, CellOutputMapping>) ((upper, curr) => curr ?? upper)).Union<CellOutputMapping>(this.CellMaping.SelectMany<CellOutputMapping, CellOutputMapping, CellOutputMapping>((Func<CellOutputMapping, IEnumerable<CellOutputMapping>>) (curr => parentLevelMapping.Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (i => i.CellId == curr.CellId && i.SectionGuid == curr.SectionGuid && i.ObjTypeGuid == curr.ObjTypeGuid)).DefaultIfEmpty<CellOutputMapping>()), (Func<CellOutputMapping, CellOutputMapping, CellOutputMapping>) ((curr, upper) => upper == null ? curr : (CellOutputMapping) null)).Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (i => i != null)));
  }

  /// <summary>
  /// Поиск настройки вывода для конкретной ячейки, с учетом раздела и типа объектов,
  /// принимая во внимание наследование и переопределение.
  /// </summary>
  internal CellOutputMapping GetCellMapping(
    string sectionGuid,
    string cellId,
    string objTypeGuid,
    bool currentOrParent = false)
  {
    if (string.IsNullOrWhiteSpace(cellId))
      throw new ArgumentException(nameof (cellId));
    if (sectionGuid == null)
      return (CellOutputMapping) null;
    objTypeGuid = objTypeGuid ?? "00000000-0000-0000-0000-000000000000";
    CellOutputMapping cellMapping1 = this.CellMaping.FirstOrDefault<CellOutputMapping>((Func<CellOutputMapping, bool>) (c => c.SectionGuid == sectionGuid && c.CellId == cellId && c.ObjTypeGuid == objTypeGuid));
    if (cellMapping1 != null)
    {
      if (cellMapping1.IsHidden && objTypeGuid != "00000000-0000-0000-0000-000000000000")
        cellMapping1 = this.CellMaping.FirstOrDefault<CellOutputMapping>((Func<CellOutputMapping, bool>) (c => c.SectionGuid == sectionGuid && c.CellId == cellId && c.ObjTypeGuid == "00000000-0000-0000-0000-000000000000"));
      return cellMapping1;
    }
    CellOutputMapping cellMapping2 = this.Parent?.GetCellMapping(sectionGuid, cellId, objTypeGuid, true);
    if (cellMapping2 != null | currentOrParent)
      return cellMapping2;
    if (objTypeGuid != "00000000-0000-0000-0000-000000000000")
    {
      cellMapping2 = this.GetCellMapping(sectionGuid, cellId, "00000000-0000-0000-0000-000000000000", true);
      if (cellMapping2 != null)
        return cellMapping2;
    }
    if (sectionGuid != "00000000-0000-0000-0000-000000000000")
    {
      cellMapping2 = this.GetCellMapping("00000000-0000-0000-0000-000000000000", cellId, objTypeGuid, true);
      if (cellMapping2 != null)
        return cellMapping2;
    }
    string str1 = objTypeGuid;
    Guid empty = Guid.Empty;
    string str2 = empty.ToString();
    if (str1 != str2)
    {
      empty = Guid.Empty;
      string sectionGuid1 = empty.ToString();
      string cellId1 = cellId;
      empty = Guid.Empty;
      string objTypeGuid1 = empty.ToString();
      cellMapping2 = this.GetCellMapping(sectionGuid1, cellId1, objTypeGuid1, true);
      if (cellMapping2 != null)
        return cellMapping2;
    }
    if (cellMapping2 == null)
      return (CellOutputMapping) null;
    cellMapping2.ObjTypeGuid = objTypeGuid;
    return cellMapping2;
  }

  internal string[] GetObjectTypesForSection(string sectionGuid)
  {
    List<string> list1 = this.CellMaping.Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (c => c.SectionGuid == sectionGuid && c.IsHidden)).Select<CellOutputMapping, string>((Func<CellOutputMapping, string>) (r => r.ObjTypeGuid)).ToList<string>();
    List<string> list2 = this.CellMaping.Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (c => c.SectionGuid == sectionGuid)).Select<CellOutputMapping, string>((Func<CellOutputMapping, string>) (r => r.ObjTypeGuid)).ToList<string>();
    if (this._parent != null)
      list2.AddRange((IEnumerable<string>) this._parent.GetObjectTypesForSection(sectionGuid));
    if (list2.Any<string>())
    {
      string[] array = list2.Except<string>((IEnumerable<string>) list1).Distinct<string>().ToArray<string>();
      if (array.Length != 0)
        return array;
    }
    return new string[1]{ Guid.Empty.ToString() };
  }

  public bool IsNew { get; private set; } = true;

  internal static AvsRowAttributeInfo[] DefaultAttributeInfos
  {
    get
    {
      return new AvsRowAttributeInfo[9]
      {
        new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00255-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Format, AVSRow.DocAttr_Format, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad0027a-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Zone, AVSRow.DocAttr_Zone, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00270-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Position, AVSRow.DocAttr_Position, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Designation, AVSRow.DocAttr_Designation, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Object, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Name, AvsIDCache.DocAttr_Name, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Relation, AvsIDCache.Attr_Count_Guid, AvsIDCache.Attr_Count, AVSRow.DocAttr_Count, ColumnContents.Text),
        new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_Note, AVSRow.DocAttr_Note, ColumnContents.Text),
        AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, new Guid("cad01478-306c-11d8-b4e9-00304f19f545")),
        AvsRowAttributeInfo.CreateByGuid(FieldSource.Relation, AvsIDCache.AttrNotePE_Guid)
      };
    }
  }

  internal static CellOutputMapping[] DefaultSpecificationCells
  {
    get
    {
      if (OutputAttributeMappingScheme.defaultSpecificationCells == null)
        OutputAttributeMappingScheme.defaultSpecificationCells = new CellOutputMapping[7]
        {
          new CellOutputMapping(AVSRow.DocAttr_Format, (OutputMappingBase) new AttributeMapping(new AttributeInfo(false, AvsIDCache.Attr_Format))),
          new CellOutputMapping(AVSRow.DocAttr_Zone, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_Zone))),
          new CellOutputMapping(AVSRow.DocAttr_Position, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_Position))),
          new CellOutputMapping(AVSRow.DocAttr_Designation, (OutputMappingBase) new AttributeMapping(new AttributeInfo(false, AvsIDCache.Attr_Designation))),
          new CellOutputMapping(AVSRow.DocAttr_Name, (OutputMappingBase) new AttributeMapping(new AttributeInfo(false, AvsIDCache.Attr_Name))),
          new CellOutputMapping(AVSRow.DocAttr_Count, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_Count))),
          new CellOutputMapping(AVSRow.DocAttr_Note, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_Note)))
        };
      return OutputAttributeMappingScheme.defaultSpecificationCells;
    }
  }

  internal static CellOutputMapping[] DefaultElementListCells
  {
    get
    {
      if (OutputAttributeMappingScheme.defaultElementListCells == null)
        OutputAttributeMappingScheme.defaultElementListCells = new CellOutputMapping[4]
        {
          new CellOutputMapping(AVSRow.DocAttr_PosDesignation, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_PosDesignation))),
          new CellOutputMapping(AVSRow.DocAttr_Name, (OutputMappingBase) new AttributeMapping(new AttributeInfo(false, AvsIDCache.Attr_Name))),
          new CellOutputMapping(AVSRow.DocAttr_Count, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_Count))),
          new CellOutputMapping(AVSRow.DocAttr_Note, (OutputMappingBase) new AttributeMapping(new AttributeInfo(true, AvsIDCache.Attr_NotePE)))
        };
      return OutputAttributeMappingScheme.defaultElementListCells;
    }
  }

  internal static List<SpecificationSectionInfo> SectionInfos
  {
    get
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      return SpecificationSectionInfo.Sections;
    }
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public OutputAttributeMappingScheme Clone()
  {
    OutputAttributeMappingScheme attributeMappingScheme = new OutputAttributeMappingScheme(this._parent, this._ownerObjectID, this._level);
    attributeMappingScheme.CopyParamsFrom(this);
    return attributeMappingScheme;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(OutputAttributeMappingScheme copy)
  {
    this.xmlScheme = copy.xmlScheme;
    this.ReadSchemeData();
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (!readArgs.Reader.LocalName.Equals("OutputMapping"))
      return false;
    this.ReadSchemeData(readArgs);
    return true;
  }

  /// <summary>
  /// Загрузить данные в схему (если нужно, сначала загрузить xml)
  /// </summary>
  private void ReadSchemeData(XmlReadArgs readArgs = null)
  {
    if (readArgs?.Reader != null)
      this.xmlScheme = XDocument.Parse(readArgs.Reader.ReadOuterXml());
    CellOutputMapping[] cellMappings;
    if (this.xmlScheme == null || !OutputAttributeMappingScheme.TryParseXml((XContainer) this.xmlScheme, out cellMappings))
      return;
    this.CellMaping.Clear();
    this.CellMaping.AddRange((IEnumerable<CellOutputMapping>) cellMappings);
  }

  /// <summary>
  /// Прочитать данные схемы вывода атрибутов с разделителями из XML контейнера
  /// </summary>
  /// <param name="xElement">контейнер</param>
  /// <param name="cellMappings">массив данных схемы вывода</param>
  /// <returns></returns>
  protected static bool TryParseXml(XContainer xElement, out CellOutputMapping[] cellMappings)
  {
    try
    {
      cellMappings = xElement.Descendants((XName) "CellOutput").Select<XElement, CellOutputMapping>(new Func<XElement, CellOutputMapping>(OutputAttributeMappingScheme.ParseCellOutputMapping)).ToArray<CellOutputMapping>();
      return true;
    }
    catch
    {
      cellMappings = new CellOutputMapping[0];
      throw;
    }
  }

  /// <summary>
  /// Разбирает XML элемент и создает экземпляр модели данных схемы вывода
  /// </summary>
  private static CellOutputMapping ParseCellOutputMapping(XElement el)
  {
    string str1 = el.Attribute((XName) "Sid").Value;
    string str2 = el.Attribute((XName) "Oid")?.Value ?? Guid.Empty.ToString();
    string str3 = el.Attribute((XName) "Cid").Value;
    CellOutputMapping cellOutputMapping = new CellOutputMapping()
    {
      SectionGuid = str1,
      ObjTypeGuid = str2,
      CellId = str3
    };
    foreach (XElement descendant in el.Descendants((XName) "Mapping"))
    {
      string input = descendant.Attribute((XName) "AttrGuid")?.Value;
      if (input != null)
      {
        int attributeId = Convert.ToInt32(descendant.Attribute((XName) "ID")?.Value ?? "-1");
        FieldSource result1;
        if (!Enum.TryParse<FieldSource>(descendant.Attribute((XName) "AttrType")?.Value ?? string.Empty, out result1))
          throw new Exception("Не удалось прочитать тип источника данных атрибута.");
        Guid result2;
        if (!Guid.TryParse(input, out result2))
          throw new Exception("Не удалось прочитать GUID атрибута.");
        string attributeName = "";
        AvsRowAttributeInfo virtualAttributInfo = AvsIDCache.GetVirtualAttributInfo(result2);
        if (virtualAttributInfo != null)
        {
          attributeId = virtualAttributInfo.AttributeId;
          attributeName = virtualAttributInfo.Name;
        }
        AttributeInfo attrInfo = new AttributeInfo(result1, result2, attributeId, attributeName);
        cellOutputMapping.Add((OutputMappingBase) new AttributeMapping(attrInfo));
      }
      else
      {
        string delimiter = descendant.Attribute((XName) "ID")?.Value ?? string.Empty;
        if (delimiter == string.Empty)
        {
          cellOutputMapping.Items.Clear();
          cellOutputMapping.Add((OutputMappingBase) DelimiterMapping.EmptyStub);
          break;
        }
        cellOutputMapping.Add((OutputMappingBase) DelimiterMapping.Create(delimiter));
      }
    }
    return cellOutputMapping;
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public virtual void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      XElement xelement = this.xmlScheme.Descendants((XName) "OutputMapping").FirstOrDefault<XElement>();
      if (xelement == null)
        return;
      xw.WriteRaw(xelement.ToString());
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs) => this.ReadSchemeData(readArgs);

  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, "OutputMapping");
  }

  public virtual void LoadParams()
  {
    this.LoadParamsFromOwnerObjectAttribute();
    if (!this.IsNew || !this.OwnerObjectID.IsDefinedId())
      return;
    this.InitDefaultOutputMapping(this.OwnerObjectID);
  }

  /// <summary> Загрузка параметров из объекта с guid-ом = OwnerGuid </summary>
  public void LoadParamsFromOwnerObjectAttribute()
  {
    if (this.OwnerObjectID.IsUndefinedId())
      return;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_OutputMappingSchema);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
          {
            this.IsNew = false;
            WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, "OutputMapping");
          }
          this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
        }
        else
          this._readOnly = AvsIDCache.Attr_OutputMappingSchema == -1;
        if (this._readOnly || objectActual.ObjectModifyMode != ObjectModifyModes.CantModify && objectActual.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          return;
        this._readOnly = true;
      }
    }
    finally
    {
      aDestStream.Close();
    }
  }

  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  public virtual void SaveParams()
  {
    if (this.ReadOnly)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_OutputMappingSchema);
  }

  /// <summary> Получить схему вывода атрибутов по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема вывода атрибутов </returns>
  public OutputAttributeMappingScheme GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent?.GetSchemaByLevel(level);
  }

  internal void SetCellMapping(
    string sectionGuid,
    string objTypeGuid,
    string cellId,
    CellOutputMapping newCellMaping)
  {
    this.CellMaping.RemoveAll((Predicate<CellOutputMapping>) (e => e.SectionGuid.Equals(sectionGuid, StringComparison.CurrentCulture) && e.ObjTypeGuid.Equals(objTypeGuid, StringComparison.CurrentCulture) && e.CellId.Equals(cellId, StringComparison.CurrentCulture)));
    if (newCellMaping == null)
      return;
    newCellMaping.CellId = cellId;
    newCellMaping.SectionGuid = sectionGuid;
    newCellMaping.ObjTypeGuid = objTypeGuid;
    this.CellMaping.Add(newCellMaping);
  }

  internal void SetCellMapping(CellOutputMapping newCellMaping)
  {
    if (newCellMaping == null)
      return;
    this.SetCellMapping(newCellMaping.SectionGuid, newCellMaping.ObjTypeGuid, newCellMaping.CellId, newCellMaping);
  }

  internal void SetCellMapping(CellOutputMapping[] newCellMaping)
  {
    if (newCellMaping == null)
      return;
    this.CellMaping.Clear();
    this.CellMaping.AddRange((IEnumerable<CellOutputMapping>) newCellMaping);
  }

  internal void UpdateXml()
  {
    this.xmlScheme = new XDocument(new object[1]
    {
      (object) new XElement((XName) "OutputMapping", (object) this.CellMaping.Select<CellOutputMapping, XElement>((Func<CellOutputMapping, XElement>) (ocm => ocm.ToXML())))
    });
  }

  public void InitDefaultOutputMapping(long templateObjectId)
  {
    ImDocument document = DocumentEditorPlugin.LoadDocumentFromDBObject(templateObjectId, createIfNotFound: true);
    TableData avsDocRow = AVSDocument.FindAvsDocRow((ImDocumentData) document);
    InheritanceSettingsLevel inheritanceLevel;
    AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(document.DBObjectGuid, out inheritanceLevel);
    bool isFormB = false;
    bool flag1 = false;
    bool isElementList = false;
    bool flag2 = false;
    if (settingsForTemplate != null)
    {
      isFormB = ((IEnumerable<AVSDocumentForm>) AVSDocumentsSettings.GetAllowableDocumentForm(settingsForTemplate.AVSDocType)).Any<AVSDocumentForm>((Func<AVSDocumentForm, bool>) (f => AVSDocument.IsDocumentFormB(f)));
      flag1 = AVSDocumentsSettings.IsSpecificationDocType(settingsForTemplate.AVSDocType);
      isElementList = AVSDocumentsSettings.IsElementListDocType(settingsForTemplate.AVSDocType);
      flag2 = inheritanceLevel == InheritanceSettingsLevel.CommonTemplate;
    }
    bool insertTextLinkToMainDocument = false;
    NoteFieldSettings oldNoteFieldSettings = (NoteFieldSettings) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (flag2)
        oldNoteFieldSettings = OutputAttributeMappingScheme.GetOldNoteFieldSettings(sessionKeeper.Session, settingsForTemplate);
      if (flag1)
        insertTextLinkToMainDocument = ((AVSCommonPropertiesSchema) settingsForTemplate.SettingsInheritanceStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, document.DBObjectID, document.DBObjectType, -1L, AvsIDCache.Attr_ConstructorDocumentProperties, typeof (AVSCommonPropertiesSchema))).AutoGenerateTextLinkToMainDocumentInNameField;
    }
    List<CellOutputMapping> list = (List<CellOutputMapping>) null;
    if (avsDocRow != null)
      list = OutputAttributeMappingScheme.ConvertDocRowCellSettingsToCellMapping(avsDocRow, isFormB, isElementList);
    if (list.IsEmpty<CellOutputMapping>())
      list = !flag1 ? (!isElementList ? (avsDocRow == null ? new List<CellOutputMapping>() : OutputAttributeMappingScheme.CreateDefaultCellMappingsForDorRow(avsDocRow, isFormB)) : new List<CellOutputMapping>((IEnumerable<CellOutputMapping>) OutputAttributeMappingScheme.DefaultElementListCells)) : new List<CellOutputMapping>((IEnumerable<CellOutputMapping>) OutputAttributeMappingScheme.DefaultSpecificationCells);
    foreach (CellOutputMapping cellOutputMapping in list)
    {
      bool flag3 = false;
      cellOutputMapping.SectionGuid = "00000000-0000-0000-0000-000000000000";
      cellOutputMapping.ObjTypeGuid = "00000000-0000-0000-0000-000000000000";
      if (cellOutputMapping.CellId == AVSRow.DocAttr_Note)
      {
        if (flag2)
        {
          if (oldNoteFieldSettings != null && !oldNoteFieldSettings.Items.IsEmpty<RemarkAttribute>())
            OutputAttributeMappingScheme.ConvertOldNoteFieldSettingsToCellMapping(oldNoteFieldSettings, cellOutputMapping, false);
          else if (flag1)
            OutputAttributeMappingScheme.CreateDefaultSpecificationNoteMapping(cellOutputMapping);
          else if (isElementList)
            OutputAttributeMappingScheme.CreateDefaultElementListNoteMapping(cellOutputMapping);
        }
        else
        {
          cellOutputMapping.Items.Clear();
          flag3 = true;
        }
      }
      else if (flag1 && cellOutputMapping.CellId == AVSRow.DocAttr_Name)
        OutputAttributeMappingScheme.CreateDefaultSpecificationNameMapping(cellOutputMapping, insertTextLinkToMainDocument);
      CellOutputMapping cellMapping = this.GetCellMapping("00000000-0000-0000-0000-000000000000", cellOutputMapping.CellId, "00000000-0000-0000-0000-000000000000", true);
      if (!flag3 && ((cellOutputMapping.IsEmpty ? 1 : 0) != (cellMapping != null ? (cellMapping.IsEmpty ? 1 : 0) : 1) || !cellMapping.Items.SequenceEqual<OutputMappingBase>((IEnumerable<OutputMappingBase>) cellOutputMapping.Items)))
        this.SetCellMapping("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", cellOutputMapping.CellId, cellOutputMapping);
    }
    if (!flag2 || oldNoteFieldSettings == null || !oldNoteFieldSettings.Items.Any<RemarkAttribute>((Func<RemarkAttribute, bool>) (n => n.WithoutDrawing)))
      return;
    CellOutputMapping cellOutputMapping1 = new CellOutputMapping()
    {
      CellId = AVSRow.DocAttr_Note,
      SectionGuid = "00000000-0000-0000-0000-000000000000",
      ObjTypeGuid = "cad00861-306c-11d8-b4e9-00304f19f545"
    };
    OutputAttributeMappingScheme.ConvertOldNoteFieldSettingsToCellMapping(oldNoteFieldSettings, cellOutputMapping1, true);
    this.SetCellMapping("00000000-0000-0000-0000-000000000000", "cad00861-306c-11d8-b4e9-00304f19f545", cellOutputMapping1.CellId, cellOutputMapping1);
  }

  private static List<CellOutputMapping> ConvertDocRowCellSettingsToCellMapping(
    TableData docRowTemplate,
    bool isFormB,
    bool isElementList)
  {
    if (docRowTemplate == null)
      throw new ArgumentNullException(nameof (docRowTemplate));
    List<CellOutputMapping> cellMapping = new List<CellOutputMapping>();
    bool flag = false;
    int cellIndex = -1;
    foreach (TextData cell in (IEnumerable<TextData>) new TextCellEnumerator(docRowTemplate))
    {
      ++cellIndex;
      AvsRowAttributeInfo attrInfoFromCell = AVSDocument.GetAttrInfoFromCell(cell, cellIndex, isFormB);
      if (attrInfoFromCell != null)
      {
        string cellId = cell.Id;
        AvsRowAttributeInfo attrInfo = AVSRow.ConvertOldCellDocAttrInfo(attrInfoFromCell, cell, isElementList);
        if (!attrInfo.IsDocField || !attrInfo.Name.Equals(cellId, StringComparison.OrdinalIgnoreCase))
        {
          if (AVSRow.IsCountFormBCell(isFormB, cell))
          {
            if (!flag)
            {
              flag = true;
              if (isFormB && cellId.IndexOf(AVSRow.DocAttr_Count, StringComparison.InvariantCultureIgnoreCase) == 0)
                cellId = AVSRow.DocAttr_Count;
            }
            else
              continue;
          }
          cellMapping.Add(new CellOutputMapping(cellId, (OutputMappingBase) new AttributeMapping((AttributeInfo) attrInfo)));
        }
      }
    }
    return cellMapping;
  }

  private static List<CellOutputMapping> CreateDefaultCellMappingsForDorRow(
    TableData docRowTemplate,
    bool isFormB)
  {
    if (docRowTemplate == null)
      throw new ArgumentNullException(nameof (docRowTemplate));
    List<CellOutputMapping> mappingsForDorRow = new List<CellOutputMapping>();
    bool flag = false;
    foreach (TextData textData in (IEnumerable<TextData>) new TextCellEnumerator(docRowTemplate))
    {
      TextData cell = textData;
      if (AVSRow.IsCountFormBCell(isFormB, cell))
      {
        if (!flag)
          flag = true;
        else
          continue;
      }
      CellOutputMapping cellOutputMapping = new CellOutputMapping()
      {
        CellId = cell.Id
      };
      AvsRowAttributeInfo attrInfo = ((IEnumerable<AvsRowAttributeInfo>) OutputAttributeMappingScheme.DefaultAttributeInfos).FirstOrDefault<AvsRowAttributeInfo>((Func<AvsRowAttributeInfo, bool>) (a => a.Name == cell.Id));
      if (attrInfo != null)
        cellOutputMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) attrInfo));
      mappingsForDorRow.Add(cellOutputMapping);
    }
    return mappingsForDorRow;
  }

  private static void CreateDefaultSpecificationNameMapping(
    CellOutputMapping newMapping,
    bool insertTextLinkToMainDocument)
  {
    newMapping.Items.Clear();
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.Attr_NameForSpecification));
    if (insertTextLinkToMainDocument)
    {
      newMapping.Add((OutputMappingBase) new DelimiterMapping("\r\n"));
      newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.Attr_LookMainDocTextLink));
    }
    newMapping.Add((OutputMappingBase) new DelimiterMapping("\r\n"));
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.Attr_DraftForPartTextLink));
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.Attr_AdditionalNameNote));
  }

  public void ResetToDefaults()
  {
    this.CellMaping.Clear();
    this.InitDefaultOutputMapping(this.OwnerObjectID);
  }

  /// <summary>Возвращает старую настройку графы Примечание</summary>
  internal static NoteFieldSettings GetOldNoteFieldSettings(
    IUserSession session,
    AVSDocumentTypeSettings docTypeSettings)
  {
    long template = AVSDocumentsSettings.Instance.GetTemplate(docTypeSettings.AVSDocType, new AVSDocumentForm?(), out Guid _, session, true);
    NoteFieldSettings noteFieldSettings = new NoteFieldSettings();
    noteFieldSettings.LoadFromDBObjectAttribute(template, AvsIDCache.Attr_NoteFieldSettings, session);
    return noteFieldSettings;
  }

  /// <summary>
  /// Заполняет newCellMapping для примечания значениями из старой настройки
  /// </summary>
  internal static void ConvertOldNoteFieldSettingsToCellMapping(
    NoteFieldSettings oldNoteFieldSettings,
    CellOutputMapping newCellMapping,
    bool onlyForPartWithoutDrawing)
  {
    newCellMapping.Items.Clear();
    if ((oldNoteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != NoteFieldOptions.None)
      newCellMapping.Add(new AttributeMapping((AttributeInfo) AvsIDCache.CountMeasureAttrInfo), DelimiterMapping.DelimiterSpace);
    if (!oldNoteFieldSettings.Items.Any<RemarkAttribute>((Func<RemarkAttribute, bool>) (a => a.ID == AvsIDCache.Attr_Format && a.AttrSource == AttributeSourceTypes.Object)))
      newCellMapping.Add(new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Format), DelimiterMapping.DelimiterSpace);
    if (!oldNoteFieldSettings.Items.Any<RemarkAttribute>((Func<RemarkAttribute, bool>) (a => a.ID == AvsIDCache.Attr_Zone && a.AttrSource == AttributeSourceTypes.Relation)))
      newCellMapping.Add(new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Zone), DelimiterMapping.DelimiterSpace);
    foreach (RemarkAttribute remarkAttribute in oldNoteFieldSettings.Items.Where<RemarkAttribute>((Func<RemarkAttribute, bool>) (a => !a.WithoutDrawing | onlyForPartWithoutDrawing)))
      newCellMapping.Add(new AttributeMapping((AttributeInfo) remarkAttribute.CreateRowAttrInfo()), new DelimiterMapping(remarkAttribute.Separator));
    for (int index = newCellMapping.Items.Count - 1; index >= 0 && !(newCellMapping.Items[index] is AttributeMapping); --index)
      newCellMapping.Items.RemoveAt(index);
  }

  private static void CreateDefaultSpecificationNoteMapping(CellOutputMapping newMapping)
  {
    newMapping.Items.Clear();
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.CountMeasureAttrInfo));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Format));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Zone));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Note));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.DopZamenTextAttrInfo));
  }

  private static void CreateDefaultElementListNoteMapping(CellOutputMapping newMapping)
  {
    newMapping.Items.Clear();
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.CountMeasureAttrInfo));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.StdField_Note));
    newMapping.Add((OutputMappingBase) DelimiterMapping.DelimiterSpace);
    newMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) AvsIDCache.DopZamenTextAttrInfo));
  }

  /// <summary>
  /// Создает или загружает объект "Схема вывода атрибутов" применимо к шаблону документа
  /// </summary>
  /// <returns>Объект "Схема вывода атрибутов"</returns>
  public static OutputAttributeMappingScheme CreateOrLoad(
    long holderObjectId,
    ref SettingsStructure settingsStructure)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(holderObjectId);
      int objectTypeId = objectInfo.ObjectTypeID;
      settingsStructure = settingsStructure ?? SettingsSchemeBase.GetSettingsStructure(objectInfo, objectTypeId);
      OutputAttributeMappingScheme settingsLevelFromObject = (OutputAttributeMappingScheme) settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, holderObjectId, objectTypeId, holderObjectId, AvsIDCache.Attr_OutputMappingSchema, typeof (OutputAttributeMappingScheme));
      if (settingsLevelFromObject.IsNew)
        settingsLevelFromObject.UpdateXml();
      return settingsLevelFromObject;
    }
  }

  /// <summary>
  /// Процедура начальной инициализации схемы вывода граф документа в шаблоне
  /// </summary>
  public static void InitializeTemplateData(long templateId)
  {
    SettingsStructure settingsStructure = (SettingsStructure) null;
    OutputAttributeMappingScheme orLoad = OutputAttributeMappingScheme.CreateOrLoad(templateId, ref settingsStructure);
    if (orLoad.CellMaping.Count != 0)
      return;
    orLoad.UpdateXml();
    orLoad.SaveParams();
  }

  /// <summary>
  /// Проинициализировать все шаблоны документов AVS начальной схемой вывода в графы документа
  /// </summary>
  /// <param name="session">Сессия</param>
  public static void InitializeAllTemplates(IUserSession session)
  {
    foreach (AVSDocumentTypeSettings avsDocumentType in AVSDocumentsSettings.GetAvsDocumentTypes(session))
    {
      AVSDocumentForm[] allowableDocumentForm = AVSDocumentsSettings.GetAllowableDocumentForm(avsDocumentType.AVSDocType);
      if (allowableDocumentForm != null)
      {
        foreach (AVSDocumentForm avsDocumentForm in allowableDocumentForm)
        {
          long template = AVSDocumentsSettings.Instance.GetTemplate(avsDocumentType.TypeGuid, new AVSDocumentForm?(avsDocumentForm), out Guid _, session, true);
          if (template.IsDefinedId())
            OutputAttributeMappingScheme.InitializeTemplateData(template);
        }
      }
    }
  }

  internal string GetPreviewStringForCellId(CellOutputMapping cellMapping)
  {
    return cellMapping == null ? string.Empty : string.Concat(cellMapping.Items.Select<OutputMappingBase, string>((Func<OutputMappingBase, string>) (cm => cm.ToString())));
  }

  /// <summary>
  /// Признак того, что данные настройки вывода были определены для текущего уровня
  /// </summary>
  internal bool IsDefinedOnCurrentLevel(CellOutputMapping mapping)
  {
    return this._parent != null && mapping != null && this.CellMaping.Any<CellOutputMapping>((Func<CellOutputMapping, bool>) (cm => cm.CellId == mapping.CellId && cm.ObjTypeGuid == mapping.ObjTypeGuid && cm.SectionGuid == mapping.SectionGuid));
  }
}
