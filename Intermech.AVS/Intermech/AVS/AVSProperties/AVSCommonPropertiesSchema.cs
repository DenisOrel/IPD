// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSProperties.AVSCommonPropertiesSchema
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS.AVSProperties;

/// <summary>Общие настройки конструкторских документов</summary>
public class AVSCommonPropertiesSchema : ICloneable, IWriteReadXml
{
  private static bool _additionalChapterSettingsInDbIsCreated;
  private long _ownerObjectID = -1;
  private readonly AVSCommonPropertiesSchema _parent;
  private readonly SettingsLevel _level;
  private bool _readOnly;
  private bool? showBCh;
  private bool? hideEqual;
  private bool? mergeVariableChapters;
  private string nameDivider;
  private bool? createChangesList;
  private bool? showAddComplect;
  private int? changesListCount;
  private List<Guid> imbaseCatalogs;
  private Guid? userAttributeForDocTypeName;
  private Guid? userAttributeForNameField;
  private bool? useUserAttributeForNameFieldForDocuments;
  private bool? autoGenerateTextLinkToMainDocumentInNameField;
  private AttributeForNamePosition? userAttributeForNamePosition;
  private LimitAndNominalValueMode? limitAndNominalValueModeForNote;
  private bool? displayPartOnNewPage;
  /// <summary>Заголовки разделов</summary>
  private readonly Dictionary<Guid, string> SectionsCaptions = new Dictionary<Guid, string>();
  /// <summary>Заголовки разделов экспортного документа</summary>
  private readonly Dictionary<Guid, string> SectionsExportCaptions = new Dictionary<Guid, string>();
  /// <summary>Части спецификации</summary>
  private List<AdditionalChapterSettings> additionalChapters;

  public AVSCommonPropertiesSchema(
    AVSCommonPropertiesSchema parent,
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
  public AVSCommonPropertiesSchema Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  /// <summary> Группировать исполнения в надписи 'Различия исполнений' </summary>
  public bool MergeVariableChapters
  {
    get
    {
      if (this.mergeVariableChapters.HasValue)
        return this.mergeVariableChapters.Value;
      return this._parent != null && this._parent.MergeVariableChapters;
    }
    set
    {
      if (this._parent != null && value == this._parent.MergeVariableChapters)
        this.mergeVariableChapters = new bool?();
      else
        this.mergeVariableChapters = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool MergeVariableChaptersChanged
  {
    get => this._parent != null && this.mergeVariableChapters.HasValue;
  }

  public bool ImbaseCatalogsChanged => this._parent != null && this.imbaseCatalogs != null;

  /// <summary> Список каталогов ImBase для не спецификаций </summary>
  public List<Guid> ImbaseCatalogs
  {
    get
    {
      if (this.imbaseCatalogs != null)
        return this.imbaseCatalogs;
      if (this._parent != null)
        return this._parent.ImbaseCatalogs;
      this.imbaseCatalogs = new List<Guid>();
      this.imbaseCatalogs.Add(new Guid("{cad008d9-306c-11d8-b4e9-00304f19f545}"));
      this.imbaseCatalogs.Add(new Guid("{cad008e6-306c-11d8-b4e9-00304f19f545}"));
      return this.imbaseCatalogs;
    }
    set
    {
      if (this._parent != null && value == this._parent.ImbaseCatalogs)
        this.imbaseCatalogs = (List<Guid>) null;
      else
        this.imbaseCatalogs = value;
    }
  }

  /// <summary> Прятать одинаковые номера позиций у записей идущих подряд </summary>
  public bool HideEqualNumber
  {
    get
    {
      if (this.hideEqual.HasValue)
        return this.hideEqual.Value;
      return this._parent != null && this._parent.HideEqualNumber;
    }
    set
    {
      if (this._parent != null && value == this._parent.HideEqualNumber)
        this.hideEqual = new bool?();
      else
        this.hideEqual = new bool?(value);
    }
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool HideEqualNumberChanged => this._parent != null && this.hideEqual.HasValue;

  /// <summary> Символ разделения наименования и условного наименования </summary>
  public string NameDivider
  {
    get
    {
      if (this.nameDivider != null)
        return this.nameDivider;
      return this._parent == null ? " " : this._parent.NameDivider;
    }
    set
    {
      if (this._parent != null && value == this._parent.NameDivider)
        this.nameDivider = (string) null;
      else
        this.nameDivider = value;
    }
  }

  public bool NameDividerChanged => this._parent != null && this.nameDivider != null;

  /// <summary> Отображать БЧ в графе формат </summary>
  public bool ShowBCh
  {
    get
    {
      if (this.showBCh.HasValue)
        return this.showBCh.Value;
      return this._parent == null || this._parent.ShowBCh;
    }
    set
    {
      if (this._parent != null && value == this._parent.ShowBCh)
        this.showBCh = new bool?();
      else
        this.showBCh = new bool?(value);
    }
  }

  public bool ShowBChChanged => this._parent != null && this.showBCh.HasValue;

  /// <summary>Отображать примечание по комплектам поставляемым отдельно</summary>
  public bool ShowAdditionalComplects
  {
    get
    {
      if (this.showAddComplect.HasValue)
        return this.showAddComplect.Value;
      return this._parent == null || this._parent.ShowAdditionalComplects;
    }
    set
    {
      if (this._parent != null && value == this._parent.ShowAdditionalComplects)
        this.showAddComplect = new bool?();
      else
        this.showAddComplect = new bool?(value);
    }
  }

  public bool ShowAdditionalComplectsChanged
  {
    get => this._parent != null && this.showAddComplect.HasValue;
  }

  /// <summary>Список дополнительных частей</summary>
  public List<AdditionalChapterSettings> AdditionalChapters
  {
    get
    {
      if (this._parent != null)
        return this._parent.AdditionalChapters;
      if (this.additionalChapters == null)
        this.additionalChapters = new List<AdditionalChapterSettings>();
      return this.additionalChapters;
    }
    set
    {
      if (this._parent != null)
        this._parent.AdditionalChapters = value;
      else
        this.additionalChapters = value;
    }
  }

  /// <summary>Создавать лист регистрации изменений</summary>
  public bool CreateChangesList
  {
    get
    {
      if (this.createChangesList.HasValue)
        return this.createChangesList.Value;
      return this._parent != null && this._parent.CreateChangesList;
    }
    set
    {
      if (this._parent != null && value == this._parent.CreateChangesList)
        this.createChangesList = new bool?();
      else
        this.createChangesList = new bool?(value);
    }
  }

  /// <summary>После скольки листов вставлять лист изменений</summary>
  public int ChangesListCount
  {
    get
    {
      if (this.changesListCount.HasValue)
        return this.changesListCount.Value;
      return this._parent == null ? 0 : this._parent.ChangesListCount;
    }
    set
    {
      if (this._parent != null && value == this._parent.ChangesListCount)
        this.changesListCount = new int?();
      else
        this.changesListCount = new int?(value);
    }
  }

  /// <summary>Установить заголовок раздела СП</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <param name="caption">заголовок, если передать null или string.Empty заголовок сбросится</param>
  public void SetSectionCaption(Guid id, string caption)
  {
    if (caption == null || caption == string.Empty)
    {
      if (!this.SectionsCaptions.ContainsKey(id))
        return;
      this.SectionsCaptions.Remove(id);
    }
    else
      this.SectionsCaptions[id] = caption;
  }

  /// <summary>Получить заголовок раздела СП</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <returns></returns>
  public string GetSectionCaption(Guid id)
  {
    if (this.SectionsCaptions.ContainsKey(id))
      return this.SectionsCaptions[id];
    return this.Parent != null ? this.Parent.GetSectionCaption(id) : (string) null;
  }

  /// <summary>Установить заголовок раздела экспортной СП</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <param name="caption">заголовок, если передать null или string.Empty заголовок сбросится</param>
  public void SetSectionExportCaption(Guid id, string caption)
  {
    if (caption == null || caption == string.Empty)
    {
      if (!this.SectionsExportCaptions.ContainsKey(id))
        return;
      this.SectionsExportCaptions.Remove(id);
    }
    else
      this.SectionsExportCaptions[id] = caption;
  }

  /// <summary>Получить заголовок раздела экспортной СП</summary>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <returns></returns>
  public string GetSectionExportCaption(Guid id)
  {
    if (this.SectionsExportCaptions.ContainsKey(id))
      return this.SectionsExportCaptions[id];
    return this.Parent != null ? this.Parent.GetSectionExportCaption(id) : (string) null;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool CreateChangesListChanged => this._parent != null && this.createChangesList.HasValue;

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool ChangesListCountChanged => this._parent != null && this.changesListCount.HasValue;

  /// <summary>
  /// Пользовательский атрибут объекта для замены наименования типа в графе Наименования
  /// </summary>
  public Guid UserAttributeForDocTypeName
  {
    get
    {
      if (this.userAttributeForDocTypeName.HasValue)
        return this.userAttributeForDocTypeName.Value;
      return this._parent != null ? this._parent.UserAttributeForDocTypeName : Guid.Empty;
    }
    set => this.userAttributeForDocTypeName = new Guid?(value);
  }

  public bool UserAttributeForDocTypeNameChanged
  {
    get => this._parent != null && this.userAttributeForDocTypeName.HasValue;
  }

  /// <summary>
  /// Пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования
  /// </summary>
  public Guid UserAttributeForNameField
  {
    get
    {
      if (this.userAttributeForNameField.HasValue)
        return this.userAttributeForNameField.Value;
      return this._parent != null ? this._parent.UserAttributeForNameField : AvsIDCache.AttrNameForAVS_Guid;
    }
    set => this.userAttributeForNameField = new Guid?(value);
  }

  public bool UserAttributeForNameFieldChanged
  {
    get => this._parent != null && this.userAttributeForNameField.HasValue;
  }

  /// <summary>
  /// Использовать пользовательский атрибут объекта для замены настроенного атрибута наименования в графе Наименования для документов
  /// </summary>
  public bool UseUserAttributeForNameFieldForDocuments
  {
    get
    {
      if (this.useUserAttributeForNameFieldForDocuments.HasValue)
        return this.useUserAttributeForNameFieldForDocuments.Value;
      return this._parent != null && this._parent.UseUserAttributeForNameFieldForDocuments;
    }
    set => this.useUserAttributeForNameFieldForDocuments = new bool?(value);
  }

  public bool UseUserAttributeForNameFieldForDocumentsChanged
  {
    get => this._parent != null && this.useUserAttributeForNameFieldForDocuments.HasValue;
  }

  /// <summary>
  /// Автоматически добавлять "Смотри"
  /// Вставлять в графу "Наименование" текст с обозначением главного конструкторского документа, когда оно значительно отличается от обозначения изделия. Например: "(см. 123.456.000)"
  /// </summary>
  public bool AutoGenerateTextLinkToMainDocumentInNameField
  {
    get
    {
      if (this.autoGenerateTextLinkToMainDocumentInNameField.HasValue)
        return this.autoGenerateTextLinkToMainDocumentInNameField.Value;
      return this._parent == null || this._parent.AutoGenerateTextLinkToMainDocumentInNameField;
    }
    set => this.autoGenerateTextLinkToMainDocumentInNameField = new bool?(value);
  }

  public bool AutoGenerateTextLinkToMainDocumentInNameFieldChanged
  {
    get => this._parent != null && this.autoGenerateTextLinkToMainDocumentInNameField.HasValue;
  }

  /// <summary>Режим вывода Предельных значений и Значений номиналов в примечание</summary>
  public LimitAndNominalValueMode LimitAndNominalValueModeForNote
  {
    get
    {
      if (this.limitAndNominalValueModeForNote.HasValue)
        return this.limitAndNominalValueModeForNote.Value;
      return this._parent != null ? this._parent.LimitAndNominalValueModeForNote : LimitAndNominalValueMode.List;
    }
    set => this.limitAndNominalValueModeForNote = new LimitAndNominalValueMode?(value);
  }

  public bool LimitAndNominalValueModeForNoteChanged
  {
    get => this._parent != null && this.limitAndNominalValueModeForNote.HasValue;
  }

  /// <summary>Режим вывода аттрибута заменителя наименования</summary>
  public AttributeForNamePosition UserAttributeForNamePosition
  {
    get
    {
      if (this.userAttributeForNamePosition.HasValue)
        return this.userAttributeForNamePosition.Value;
      return this._parent != null ? this._parent.UserAttributeForNamePosition : AttributeForNamePosition.Instead;
    }
    set => this.userAttributeForNamePosition = new AttributeForNamePosition?(value);
  }

  public bool UserAttributeForNamePositionChanged
  {
    get => this._parent != null && this.userAttributeForNamePosition.HasValue;
  }

  /// <summary> Выводить части спецификации с новой страницы</summary>
  public bool DisplayPartOnNewPage
  {
    get
    {
      if (this.displayPartOnNewPage.HasValue)
        return this.displayPartOnNewPage.Value;
      return this._parent != null && this._parent.DisplayPartOnNewPage;
    }
    set
    {
      if (this._parent != null && value == this._parent.DisplayPartOnNewPage)
        this.displayPartOnNewPage = new bool?();
      else
        this.displayPartOnNewPage = new bool?(value);
    }
  }

  public bool DisplayPartOnNewPageChanged
  {
    get => this._parent != null && this.displayPartOnNewPage.HasValue;
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public AVSCommonPropertiesSchema Clone()
  {
    AVSCommonPropertiesSchema propertiesSchema = new AVSCommonPropertiesSchema(this._parent, this._ownerObjectID, this._level);
    propertiesSchema.CopyParamsFrom(this);
    return propertiesSchema;
  }

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(AVSCommonPropertiesSchema copy)
  {
    this.createChangesList = copy.createChangesList;
    this.changesListCount = copy.changesListCount;
    this.hideEqual = copy.hideEqual;
    this.showBCh = copy.showBCh;
    this.showAddComplect = copy.showAddComplect;
    this.nameDivider = copy.nameDivider;
    this.mergeVariableChapters = copy.mergeVariableChapters;
    this.ImbaseCatalogs = new List<Guid>((IEnumerable<Guid>) copy.ImbaseCatalogs.ToArray());
    this.userAttributeForDocTypeName = copy.userAttributeForDocTypeName;
    this.userAttributeForNameField = copy.userAttributeForNameField;
    this.useUserAttributeForNameFieldForDocuments = copy.useUserAttributeForNameFieldForDocuments;
    this.limitAndNominalValueModeForNote = copy.limitAndNominalValueModeForNote;
    this.userAttributeForNamePosition = copy.userAttributeForNamePosition;
    this.displayPartOnNewPage = copy.displayPartOnNewPage;
    this.autoGenerateTextLinkToMainDocumentInNameField = copy.autoGenerateTextLinkToMainDocumentInNameField;
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    string str = readArgs.Reader.Value;
    switch (readArgs.Reader.LocalName)
    {
      case "AdditionalChapters":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.additionalChapters = new List<AdditionalChapterSettings>();
        WriteReadXmlHelper.ReadListFromXml((IList) this.additionalChapters, typeof (AdditionalChapterSettings), readArgs);
        return true;
      case "AutoGenerateTextLinkToMainDocumentInNameField":
        this.autoGenerateTextLinkToMainDocumentInNameField = new bool?(readArgs.Reader.Value == "1");
        return true;
      case "ChangesListCount":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.ChangesListCount = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "CreateChangesList":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.CreateChangesList = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "DisplayPartOnNewPage":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DisplayPartOnNewPage = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "HideEqualNumber":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.HideEqualNumber = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "ImbaseCatalogsCount":
        string s1 = readArgs.Reader.Value;
        int num1 = 0;
        ref int local1 = ref num1;
        int.TryParse(s1, out local1);
        if (num1 > 0)
          this.imbaseCatalogs = new List<Guid>();
        for (int index = 0; index < num1; ++index)
        {
          string attribute = readArgs.Reader.GetAttribute("ImbaseCatalogsValue" + index.ToString());
          if (attribute != null && GuidHelper.IsGuid(attribute))
            this.imbaseCatalogs.Add(new Guid(attribute));
        }
        return true;
      case "LimitAndNominalValueModeForNote":
        if (!string.IsNullOrEmpty(readArgs.Reader.Value))
          this.limitAndNominalValueModeForNote = new LimitAndNominalValueMode?((LimitAndNominalValueMode) Enum.Parse(typeof (LimitAndNominalValueMode), readArgs.Reader.Value));
        return true;
      case "MergeVariableChapters":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.MergeVariableChapters = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "NameDivider":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.NameDivider = readArgs.Reader.Value;
        return true;
      case "SectExpCaptionsCount":
        string s2 = readArgs.Reader.Value;
        int num2 = 0;
        ref int local2 = ref num2;
        int.TryParse(s2, out local2);
        for (int index = 0; index < num2; ++index)
        {
          string attribute1 = readArgs.Reader.GetAttribute("SECKey" + index.ToString());
          string attribute2 = readArgs.Reader.GetAttribute("SECValue" + index.ToString());
          if (attribute1 != null && attribute2 != null && GuidHelper.IsGuid(attribute1))
          {
            Guid key = new Guid(attribute1);
            if (!this.SectionsExportCaptions.ContainsKey(key))
              this.SectionsExportCaptions.Add(key, attribute2);
            else
              this.SectionsExportCaptions[key] = attribute2;
          }
        }
        return true;
      case "SectionsCaptionsCount":
        string s3 = readArgs.Reader.Value;
        int num3 = 0;
        ref int local3 = ref num3;
        int.TryParse(s3, out local3);
        for (int index = 0; index < num3; ++index)
        {
          string attribute3 = readArgs.Reader.GetAttribute("SectionsCaptionsKey" + index.ToString());
          string attribute4 = readArgs.Reader.GetAttribute("SectionsCaptionsValue" + index.ToString());
          if (attribute3 != null && attribute4 != null && GuidHelper.IsGuid(attribute3))
          {
            Guid key = new Guid(attribute3);
            if (!this.SectionsCaptions.ContainsKey(key))
              this.SectionsCaptions.Add(key, attribute4);
            else
              this.SectionsCaptions[key] = attribute4;
          }
        }
        return true;
      case "ShowAdditionalComplects":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.ShowAdditionalComplects = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "ShowBCh":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.ShowBCh = Convert.ToBoolean(readArgs.Reader.Value);
        return true;
      case "UseUserAttributeForNameFieldForDocuments":
        this.useUserAttributeForNameFieldForDocuments = new bool?(readArgs.Reader.Value == "1");
        return true;
      case "UserAttributeForDocTypeName":
        this.userAttributeForDocTypeName = new Guid?(new Guid(readArgs.Reader.Value));
        return true;
      case "UserAttributeForNameField":
        this.userAttributeForNameField = new Guid?(new Guid(readArgs.Reader.Value));
        return true;
      case "UserAttributeForNamePosition":
        if (!string.IsNullOrEmpty(readArgs.Reader.Value))
          this.userAttributeForNamePosition = new AttributeForNamePosition?((AttributeForNamePosition) Enum.Parse(typeof (AttributeForNamePosition), readArgs.Reader.Value));
        return true;
      default:
        return false;
    }
  }

  /// <summary>Загрузить части СП</summary>
  public void LoadAdditionalChaptersCache()
  {
    List<AdditionalChapterSettings> additionalChapters = this.AdditionalChapters;
    this.AdditionalChapters = new List<AdditionalChapterSettings>();
    DataTable dataTable = AVSCommonPropertiesSchema.LoadChapterObjects((IList<long>) null);
    if (dataTable.Rows.Count == 0 && additionalChapters.Count > 0 && !AVSCommonPropertiesSchema._additionalChapterSettingsInDbIsCreated)
    {
      this.AdditionalChapters = additionalChapters;
    }
    else
    {
      foreach (DataRow chapterDataRow in (IEnumerable<DataRow>) dataTable.Rows.OfType<DataRow>().OrderBy<DataRow, long>((System.Func<DataRow, long>) (dr => Convert.ToInt64(dr[-2.ToString()]))))
        AVSCommonPropertiesSchema.UpdateChapterSettings(chapterDataRow, this.AdditionalChapters);
    }
    AVSCommonPropertiesSchema._additionalChapterSettingsInDbIsCreated = true;
  }

  /// <summary>Загрузить части СП</summary>
  public static List<AdditionalChapterSettings> LoadAdditionalChaptersSettingsFromDB()
  {
    List<AdditionalChapterSettings> additionalChapterSettingsList = new List<AdditionalChapterSettings>();
    foreach (DataRow row in (InternalDataCollectionBase) AVSCommonPropertiesSchema.LoadChapterObjects((IList<long>) null).Rows)
      AVSCommonPropertiesSchema.UpdateChapterSettings(row, additionalChapterSettingsList);
    return additionalChapterSettingsList.OrderBy<AdditionalChapterSettings, long>((System.Func<AdditionalChapterSettings, long>) (ac => ac.ChapterID)).ToList<AdditionalChapterSettings>();
  }

  private void SynchronizeChapterSettingsWithDbObjects(
    List<AdditionalChapterSettings> oldAdditionalChapters)
  {
    if (oldAdditionalChapters == null)
      throw new ArgumentNullException(nameof (oldAdditionalChapters));
    AVSCommonPropertiesSchema.SaveChapterSettingsToDbObjects(oldAdditionalChapters);
    this.AdditionalChapters = oldAdditionalChapters;
  }

  public static void SaveChapterSettingsToDbObjects(
    List<AdditionalChapterSettings> additionalChapters)
  {
    if (additionalChapters == null)
      throw new ArgumentNullException(nameof (additionalChapters));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationChapter);
      foreach (AdditionalChapterSettings additionalChapter in additionalChapters)
        AVSCommonPropertiesSchema.SaveChapterSettingsToDbObject(sessionKeeper.Session, additionalChapter, objectCollection);
    }
  }

  internal static void SaveChapterSettingsToDbObject(
    IUserSession session,
    AdditionalChapterSettings chapterSettings,
    IDBObjectCollection objectCollection)
  {
    IDBObject dbObject = session.GetObject(chapterSettings.ChapterGuid, false) ?? objectCollection.Create();
    dbObject.SetAttributesValues(new AttributeValues[3]
    {
      new AttributeValues(-12, (object) chapterSettings.ChapterGuid),
      new AttributeValues(AvsIDCache.Attr_Name, (object) chapterSettings.Caption),
      new AttributeValues(AvsIDCache.Attr_SortIndex, (object) chapterSettings.SortIndex)
    });
    if (dbObject.IsCreationMode)
      dbObject.CommitCreation(true, true);
    chapterSettings.ChapterID = dbObject.ObjectID;
  }

  /// <summary>Обновить кэш частей СП</summary>
  /// <param name="additionalChaptersID">Идентификаторы объектов разделов. Если null, то обновить все разделы</param>
  public void UpdateAdditionalChaptersCache(IList<long> additionalChaptersID)
  {
    int num1 = additionalChaptersID == null ? 1 : (additionalChaptersID.Count == 0 ? 1 : 0);
    if (num1 != 0)
      this.AdditionalChapters = new List<AdditionalChapterSettings>();
    List<long> longList = new List<long>();
    if (num1 == 0)
      longList.AddRange((IEnumerable<long>) additionalChaptersID);
    foreach (DataRow row in (InternalDataCollectionBase) AVSCommonPropertiesSchema.LoadChapterObjects(additionalChaptersID).Rows)
    {
      long num2 = AVSCommonPropertiesSchema.UpdateChapterSettings(row, this.AdditionalChapters);
      longList.Remove(num2);
    }
    foreach (long additionalChapterId in longList)
    {
      AdditionalChapterSettings additionalChapterSettings = this.FindAdditionalChapterSettings(additionalChapterId);
      if (additionalChapterSettings != null)
        this.AdditionalChapters.Remove(additionalChapterSettings);
    }
  }

  internal void LoadNewAdditionalChapters(DBObjectsEventArgs newObjectsEventArgs)
  {
    List<long> additionalChaptersID = new List<long>();
    for (int index = 0; index < newObjectsEventArgs.ItemsCount; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(newObjectsEventArgs.ObjectTypeIDs[index], AvsIDCache.ObjType_SpecificationChapter))
        additionalChaptersID.Add(newObjectsEventArgs.ObjectIDs[index]);
    }
    if (additionalChaptersID.Count <= 0)
      return;
    this.UpdateAdditionalChaptersCache((IList<long>) additionalChaptersID);
  }

  internal void UpdateAdditionalChaptersCache(DBObjectsExtendedEventArgs changedObjectEventArgs)
  {
    if (!MetaDataHelper.IsObjectTypeChildOf(changedObjectEventArgs.ObjectType, AvsIDCache.ObjType_SpecificationChapter) || changedObjectEventArgs.ObjectIDs.Count == 0)
      return;
    AdditionalChapterSettings additionalChapterSettings = this.FindAdditionalChapterSettings(changedObjectEventArgs.ObjectIDs[0]);
    if (additionalChapterSettings == null)
      return;
    int index = ((IEnumerable<AttributeValues>) changedObjectEventArgs.AttributeValuesArray).IndexOfFirst<AttributeValues>((Predicate<AttributeValues>) (x => x.AttributeID == AvsIDCache.Attr_Name));
    if (index == -1)
      return;
    additionalChapterSettings.Caption = changedObjectEventArgs.AttributeValuesArray[index].Values[0].ToString();
  }

  private static long UpdateChapterSettings(
    DataRow chapterDataRow,
    List<AdditionalChapterSettings> additionalChapters)
  {
    long int64_1 = Convert.ToInt64(chapterDataRow[-2.ToString()]);
    Guid guid = new Guid(Convert.ToString(chapterDataRow[-12.ToString()]));
    string caption = Convert.ToString(chapterDataRow[-50.ToString()]);
    long int64_2 = AvsIDCache.ConvertDbValueToInt64(chapterDataRow[AvsIDCache.Attr_SortIndex.ToString()], 0L);
    AdditionalChapterSettings additionalChapterSettings = AVSCommonPropertiesSchema.FindAdditionalChapterSettings(guid, additionalChapters);
    if (additionalChapterSettings != null)
    {
      additionalChapterSettings.Caption = caption;
      additionalChapterSettings.ChapterID = int64_1;
    }
    else
      additionalChapters.Add(new AdditionalChapterSettings(guid, int64_1, caption, int64_2));
    return int64_1;
  }

  private static DataTable LoadChapterObjects(IList<long> additionalChaptersID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams paramSet = new DBRecordSetParams(AVSCommonPropertiesSchema.CreateFilterByObjectsID(additionalChaptersID), AVSCommonPropertiesSchema.CreateColumnDescriptors());
      return sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationChapter).Select(paramSet);
    }
  }

  public void RemoveAdditionalChapterObject_NotificationHandler(DBObjectsEventArgs eventArgs)
  {
    for (int index = 0; index < eventArgs.ItemsCount; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(eventArgs.ObjectTypeIDs[index], AvsIDCache.ObjType_SpecificationChapter))
      {
        AdditionalChapterSettings additionalChapterSettings = this.FindAdditionalChapterSettings(eventArgs.ObjectIDs[index]);
        if (additionalChapterSettings != null)
          this.AdditionalChapters.Remove(additionalChapterSettings);
      }
    }
  }

  private static ColumnDescriptor[] CreateColumnDescriptors()
  {
    return new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) AvsIDCache.Attr_PartNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SortIndex, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0)
    };
  }

  private static ConditionStructure[] CreateFilterByObjectsID(IList<long> objectsID)
  {
    ConditionStructure[] filterByObjectsId = (ConditionStructure[]) null;
    if (objectsID != null && objectsID.Count > 0)
    {
      filterByObjectsId = new ConditionStructure[objectsID.Count];
      for (int index = 0; index < objectsID.Count; ++index)
      {
        int groupID = 0;
        if (index == 0)
          groupID = 1;
        else if (index == objectsID.Count - 1)
          groupID = -1;
        filterByObjectsId[index] = new ConditionStructure(-2, RelationalOperators.Equal, (object) objectsID[index], LogicalOperators.OR, groupID, true);
      }
    }
    return filterByObjectsId;
  }

  internal Guid GetAdditionalChapterGuid(long additionalChapterId)
  {
    if (additionalChapterId.IsUndefinedId())
      throw new ArgumentException("Неопределённое значение идентификатора additionalChapterId");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(additionalChapterId).VersionGuid;
  }

  internal AdditionalChapterSettings FindAdditionalChapterSettings(long additionalChapterId)
  {
    if (additionalChapterId.IsUndefinedId())
      throw new ArgumentException("Неопределённое значение идентификатора additionalChapterId");
    foreach (AdditionalChapterSettings additionalChapter in this.AdditionalChapters)
    {
      if (additionalChapter.ChapterID == additionalChapterId)
        return additionalChapter;
    }
    return (AdditionalChapterSettings) null;
  }

  internal static AdditionalChapterSettings FindAdditionalChapterSettings(
    Guid additionalChapterGuid,
    List<AdditionalChapterSettings> additionalChapters)
  {
    if (additionalChapterGuid == Guid.Empty)
      throw new ArgumentException("Неопределённое значение идентификатора additionalChapterGuid");
    foreach (AdditionalChapterSettings additionalChapter in additionalChapters)
    {
      if (additionalChapter.ChapterGuid == additionalChapterGuid)
        return additionalChapter;
    }
    return (AdditionalChapterSettings) null;
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      bool flag;
      if (this._parent == null || this.createChangesList.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.CreateChangesList;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("CreateChangesList", str);
      }
      if (this._parent == null || this.hideEqual.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.HideEqualNumber;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("HideEqualNumber", str);
      }
      if (this._parent == null || this.showBCh.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.ShowBCh;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("ShowBCh", str);
      }
      if (this._parent == null || this.showAddComplect.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.ShowAdditionalComplects;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("ShowAdditionalComplects", str);
      }
      if (this._parent == null || this.mergeVariableChapters.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.MergeVariableChapters;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("MergeVariableChapters", str);
      }
      int num1;
      if (this._parent == null || this.changesListCount.HasValue)
      {
        XmlWriter xmlWriter = xw;
        num1 = this.ChangesListCount;
        string str = num1.ToString();
        xmlWriter.WriteAttributeString("ChangesListCount", str);
      }
      if (this._parent == null || this.nameDivider != null)
        xw.WriteAttributeString("NameDivider", this.NameDivider.ToString());
      if (this._parent == null || this.displayPartOnNewPage.HasValue)
      {
        XmlWriter xmlWriter = xw;
        flag = this.DisplayPartOnNewPage;
        string str = flag.ToString();
        xmlWriter.WriteAttributeString("DisplayPartOnNewPage", str);
      }
      XmlWriter xmlWriter1 = xw;
      num1 = this.ImbaseCatalogs.Count;
      string str1 = num1.ToString();
      xmlWriter1.WriteAttributeString("ImbaseCatalogsCount", str1);
      int num2 = 0;
      foreach (Guid imbaseCatalog in this.ImbaseCatalogs)
      {
        xw.WriteAttributeString("ImbaseCatalogsValue" + num2.ToString(), imbaseCatalog.ToString());
        ++num2;
      }
      xw.WriteAttributeString("SectionsCaptionsCount", this.SectionsCaptions.Count.ToString());
      int num3 = 0;
      foreach (KeyValuePair<Guid, string> sectionsCaption in this.SectionsCaptions)
      {
        xw.WriteAttributeString("SectionsCaptionsKey" + num3.ToString(), sectionsCaption.Key.ToString());
        xw.WriteAttributeString("SectionsCaptionsValue" + num3.ToString(), sectionsCaption.Value.ToString());
        ++num3;
      }
      xw.WriteAttributeString("SectExpCaptionsCount", this.SectionsExportCaptions.Count.ToString());
      int num4 = 0;
      foreach (KeyValuePair<Guid, string> sectionsExportCaption in this.SectionsExportCaptions)
      {
        xw.WriteAttributeString("SECKey" + num4.ToString(), sectionsExportCaption.Key.ToString());
        xw.WriteAttributeString("SECValue" + num4.ToString(), sectionsExportCaption.Value.ToString());
        ++num4;
      }
      Guid guid;
      if (this._parent == null || this.userAttributeForDocTypeName.HasValue)
      {
        XmlWriter xmlWriter2 = xw;
        guid = this.UserAttributeForDocTypeName;
        string str2 = guid.ToString();
        xmlWriter2.WriteAttributeString("UserAttributeForDocTypeName", str2);
      }
      if (this._parent == null || this.userAttributeForNameField.HasValue)
      {
        XmlWriter xmlWriter3 = xw;
        guid = this.UserAttributeForNameField;
        string str3 = guid.ToString();
        xmlWriter3.WriteAttributeString("UserAttributeForNameField", str3);
      }
      if (this._parent == null || this.useUserAttributeForNameFieldForDocuments.HasValue)
        xw.WriteAttributeString("UseUserAttributeForNameFieldForDocuments", this.UseUserAttributeForNameFieldForDocuments ? "1" : "0");
      if (this._parent == null || this.limitAndNominalValueModeForNote.HasValue)
        xw.WriteAttributeString("LimitAndNominalValueModeForNote", this.LimitAndNominalValueModeForNote.ToString());
      if (this._parent == null || this.userAttributeForNamePosition.HasValue)
        xw.WriteAttributeString("UserAttributeForNamePosition", this.UserAttributeForNamePosition.ToString());
      if (this._parent != null && !this.autoGenerateTextLinkToMainDocumentInNameField.HasValue)
        return;
      xw.WriteAttributeString("AutoGenerateTextLinkToMainDocumentInNameField", this.AutoGenerateTextLinkToMainDocumentInNameField ? "1" : "0");
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  public void SaveToXmlDocument(Stream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument(stream, (IWriteReadXml) this, nameof (AVSCommonPropertiesSchema));
  }

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultParams()
  {
    if (this._parent == null)
    {
      this.createChangesList = new bool?(false);
      this.changesListCount = new int?(0);
      this.hideEqual = new bool?(true);
      this.showBCh = new bool?(true);
      this.showAddComplect = new bool?(true);
      this.mergeVariableChapters = new bool?(false);
      this.imbaseCatalogs = new List<Guid>();
      this.imbaseCatalogs.Add(new Guid("{cad008d9-306c-11d8-b4e9-00304f19f545}"));
      this.imbaseCatalogs.Add(new Guid("{cad008e6-306c-11d8-b4e9-00304f19f545}"));
      this.userAttributeForDocTypeName = new Guid?(Guid.Empty);
      this.userAttributeForNameField = new Guid?(AvsIDCache.AttrNameForAVS_Guid);
      this.useUserAttributeForNameFieldForDocuments = new bool?(false);
      this.nameDivider = " ";
      this.limitAndNominalValueModeForNote = new LimitAndNominalValueMode?();
      this.userAttributeForNamePosition = new AttributeForNamePosition?(AttributeForNamePosition.Instead);
      this.displayPartOnNewPage = new bool?(false);
      this.autoGenerateTextLinkToMainDocumentInNameField = new bool?(false);
    }
    else
    {
      this.imbaseCatalogs = (List<Guid>) null;
      this.mergeVariableChapters = new bool?();
      this.createChangesList = new bool?();
      this.changesListCount = new int?();
      this.hideEqual = new bool?();
      this.showBCh = new bool?();
      this.showAddComplect = new bool?();
      this.userAttributeForDocTypeName = new Guid?();
      this.userAttributeForNameField = new Guid?();
      this.useUserAttributeForNameFieldForDocuments = new bool?();
      this.limitAndNominalValueModeForNote = new LimitAndNominalValueMode?();
      this.nameDivider = (string) null;
      this.userAttributeForNamePosition = new AttributeForNamePosition?();
      this.displayPartOnNewPage = new bool?();
      this.autoGenerateTextLinkToMainDocumentInNameField = new bool?();
    }
  }

  /// <summary>Загрузка параметров из объекта с guid-ом = OwnerGuid</summary>
  public void LoadParams()
  {
    if (this.OwnerObjectID.IsUndefinedId())
      return;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_ConstructorDocumentProperties);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, nameof (AVSCommonPropertiesSchema));
          this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
        }
        else
          this._readOnly = AvsIDCache.Attr_ConstructorDocumentProperties == -1;
        if (!this._readOnly)
        {
          if (objectActual.ObjectModifyMode == ObjectModifyModes.CantModify || objectActual.ObjectModifyMode == ObjectModifyModes.CreateVersion)
            this._readOnly = true;
          if (objectActual.ObjectID > 0L)
          {
            if (objectActual.CheckoutBy != 0L)
              this._readOnly = true;
          }
        }
      }
    }
    finally
    {
      aDestStream.Close();
    }
    if (this._parent != null)
      return;
    this.LoadAdditionalChaptersCache();
  }

  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  public void SaveParams()
  {
    if (this.OwnerObjectID.IsUndefinedId() || this.ReadOnly)
      return;
    long aElementID = this.OwnerObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
      if (dbObject.GetAttributeByID(AvsIDCache.Attr_ConstructorDocumentProperties) == null)
      {
        bool flag = false;
        if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          dbObject = dbObject.CheckOut();
          flag = true;
        }
        if (dbObject != null && (dbObject.CheckoutBy == sessionKeeper.Session.UserID || dbObject.ObjectModifyMode != ObjectModifyModes.Checkout))
        {
          dbObject.Attributes.AddAttribute(AvsIDCache.Attr_ConstructorDocumentProperties, false);
          if (flag)
            dbObject.CheckIn();
        }
      }
      aElementID = dbObject.ObjectID;
    }
    MemoryStream aSourceStream = new MemoryStream();
    try
    {
      this.SaveToXmlDocument((Stream) aSourceStream);
      aSourceStream.Position = 0L;
      BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(aElementID, AttributableElements.Object, AvsIDCache.Attr_ConstructorDocumentProperties, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      aSourceStream.Position = 0L;
    }
    finally
    {
      aSourceStream.Close();
    }
  }

  /// <summary> Получить схему сортировки по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема сортировки </returns>
  public AVSCommonPropertiesSchema GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent != null ? this._parent.GetSchemaByLevel(level) : (AVSCommonPropertiesSchema) null;
  }
}
