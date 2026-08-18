// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OldAVSFields
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.IniFiles;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, описывающий дополнительные колонки старого AVS </summary>
internal class OldAVSFields
{
  private static HybridDictionary _oldFieldsCache = new HybridDictionary();
  private Dictionary<int, long> _sectionNumToSectionIdDictionary;
  private Dictionary<int, OldAVSField> _list = new Dictionary<int, OldAVSField>();
  private const string constOldSpecificationsFieldsSectionName = "S4PRJ_FIELDS";
  private SkipLinesSchema skipLinesSchema;
  private OldAVSFields defaultFields;

  public SkipLinesSchema SkipLinesSchema
  {
    get => this.skipLinesSchema;
    set => this.skipLinesSchema = value;
  }

  /// <summary> Создание списка полей старой спецификации или ведомости из  ini файла </summary>
  public OldAVSFields(
    InMemoryIniFile inMemoryIniFile,
    Dictionary<int, long> sectionNumToSectionIdDictionary)
  {
    this._sectionNumToSectionIdDictionary = sectionNumToSectionIdDictionary;
    this.LoadFromIni(inMemoryIniFile);
  }

  /// <summary> Создание списка полей старой спецификации или ведомости из  ini файла </summary>
  public OldAVSFields(InMemoryIniFile inMemoryIniFile) => this.LoadFromIni(inMemoryIniFile);

  private void LoadFromIni(InMemoryIniFile inMemoryIniFile)
  {
    if (inMemoryIniFile != null && inMemoryIniFile.SectionNames != null && inMemoryIniFile.ValueNames.ContainsKey("S4PRJ_FIELDS"))
    {
      System.Collections.Generic.List<string> valueName = inMemoryIniFile.ValueNames["S4PRJ_FIELDS"];
      int result = 0;
      string empty = string.Empty;
      foreach (string str1 in valueName)
      {
        if (int.TryParse(str1, out result) && result > 50 && result < 100)
        {
          string str2 = inMemoryIniFile.ReadString("S4PRJ_FIELDS", str1, string.Empty);
          if (str2.Trim() != string.Empty)
            this._list[result] = OldAVSField.GetFieldByCaption(str2.Trim());
        }
      }
    }
    if (inMemoryIniFile == null)
      return;
    this.SkipLinesSchema = new SkipLinesSchema((SkipLinesSchema) null, -1L, (SettingsLevel) null);
    this.SkipLinesSchema.BetweenDifferentDesignations = int.Parse(inMemoryIniFile.ReadString("OptDraw", "DesignDifferent", "0"));
    this.SkipLinesSchema.BetweenSameDesignations = int.Parse(inMemoryIniFile.ReadString("OptDraw", "DesignSimilar", "0"));
    this.SkipLinesSchema.BetweenArtVariants = int.Parse(inMemoryIniFile.ReadString("OptDraw", "BetweenDelta", "0"));
    this.SkipLinesSchema.BeforeSectionName = int.Parse(inMemoryIniFile.ReadString("OptDraw", "RazdelBefore", "0"));
    this.SkipLinesSchema.AfterSectionName = int.Parse(inMemoryIniFile.ReadString("OptDraw", "RazdelAfter", "0"));
    this.SkipLinesSchema.BeforeVariableData = int.Parse(inMemoryIniFile.ReadString("OptDraw", "VariablesBefore", "0"));
    this.SkipLinesSchema.AfterVariableData = int.Parse(inMemoryIniFile.ReadString("OptDraw", "VariablesAfter", "0"));
    this.SkipLinesSchema.BeforeVariantNumber = int.Parse(inMemoryIniFile.ReadString("OptDraw", "NumbVarBefore", "0"));
    this.SkipLinesSchema.AfterVariantNumber = int.Parse(inMemoryIniFile.ReadString("OptDraw", "NumbVarAfter", "0"));
    this.SkipLinesSchema.AfterNote = int.Parse(inMemoryIniFile.ReadString("OptDraw", "RemarkBefore", "0"));
    this.SkipLinesSchema.BeforeAdd1 = int.Parse(inMemoryIniFile.ReadString("OptDraw", "Dop1Before", "0"));
    this.SkipLinesSchema.AfterAdd1 = int.Parse(inMemoryIniFile.ReadString("OptDraw", "Dop1After", "0"));
    this.SkipLinesSchema.BeforeAdd2 = int.Parse(inMemoryIniFile.ReadString("OptDraw", "Dop2Before", "0"));
    int num = int.Parse(inMemoryIniFile.ReadString("COMPARE_DESIGN_N", "COMPARE_DESIGN_N", "0"));
    if (this.SkipLinesSchema.CompareDesignationSchema == null || this.SkipLinesSchema.CompareDesignationSchema.SubStrs == null || this.SkipLinesSchema.CompareDesignationSchema.SubStrs.Length == 0)
      return;
    CompareDesignationSubStr subStr = this.SkipLinesSchema.CompareDesignationSchema.SubStrs[0];
    subStr.FinishFindWhat = CompareDesignationSubStr.FindWhat.AnySymbolNumber;
    subStr.FinishNumber = num;
    subStr.StartFindWhat = CompareDesignationSubStr.FindWhat.StartEndString;
  }

  /// <summary> Получить список полей старых спецификаций </summary>
  public static OldAVSFields GetColumnsForSpecifications(string fileType)
  {
    return OldAVSFields.GetColumnsForSpecifications(fileType, (Dictionary<int, long>) null);
  }

  internal OldAVSFields DefaultFields
  {
    get => this.defaultFields;
    set => this.defaultFields = value;
  }

  /// <summary> Получить получить список полей старых спецификаций </summary>
  public static OldAVSFields GetColumnsForSpecifications(
    string fileType,
    Dictionary<int, long> sectionNumToSectionIdDictionary)
  {
    if (OldAVSFields.OldFieldsCache.Contains((object) fileType))
      return (OldAVSFields) OldAVSFields.OldFieldsCache[(object) fileType];
    OldAVSFields forSpecifications = (OldAVSFields) null;
    OldFormatIniFileDescriptor iniFileByExtention = OldFormatIniFiles.OldSpecificationSettings.GetIniFileByExtention(fileType);
    if (iniFileByExtention != null)
    {
      InMemoryIniFile configFile = iniFileByExtention.GetConfigFile();
      if (configFile != null)
        forSpecifications = new OldAVSFields(configFile, sectionNumToSectionIdDictionary);
    }
    OldAVSFields.OldFieldsCache[(object) fileType] = (object) forSpecifications;
    return forSpecifications;
  }

  public static OldAVSFields Load(string file)
  {
    OldAVSFields oldAvsFields = (OldAVSFields) null;
    InMemoryIniFile configFile = OldFormatIniFileDescriptor.GetConfigFile(file);
    if (configFile != null)
      oldAvsFields = new OldAVSFields(configFile);
    return oldAvsFields;
  }

  /// <summary> Словарь, где ключом является тип старого документа AVS, а ключём - список его колонок </summary>
  protected static HybridDictionary OldFieldsCache => OldAVSFields._oldFieldsCache;

  /// <summary> Словарь, где ключом является номер поля в AVS, значением - OldAVSField </summary>
  public Dictionary<int, OldAVSField> List => this._list;

  /// <summary> Словарь, где ключом выступает номер раздела спецификации, значением - идентификатор раздела </summary>
  public Dictionary<int, long> SectionNumToSectionIdDictionary
  {
    get
    {
      if (this.DefaultFields != null)
        return this.DefaultFields.SectionNumToSectionIdDictionary;
      if (this._sectionNumToSectionIdDictionary == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable dataTable = sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
            new ColumnDescriptor((object) AvsIDCache.Attr_SectionNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
          }));
          if (dataTable != null)
          {
            this._sectionNumToSectionIdDictionary = new Dictionary<int, long>(dataTable.Rows.Count);
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (!(row[1] is DBNull) && !(row[0] is DBNull))
                this._sectionNumToSectionIdDictionary[Convert.ToInt32(row[1])] = Convert.ToInt64(row[0]);
            }
          }
        }
      }
      return this._sectionNumToSectionIdDictionary;
    }
  }
}
