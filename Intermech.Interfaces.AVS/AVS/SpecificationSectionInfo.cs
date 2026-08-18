// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SpecificationSectionInfo
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Информация о разделе спецификации</summary>
[Serializable]
public class SpecificationSectionInfo : IComparable
{
  /// <summary>Словарь всех разделов спецификации</summary>
  public static List<SpecificationSectionInfo> Sections = new List<SpecificationSectionInfo>(20);
  public static HybridDictionary SectionDictionaryByID = new HybridDictionary(10);
  public static HybridDictionary SectionDictionaryByGuid = new HybridDictionary(10);
  public static Dictionary<string, SpecificationSectionInfo> SectionDictionaryByRazdelSP = new Dictionary<string, SpecificationSectionInfo>(10);
  private static Dictionary<long, List<SpecificationSectionInfo>> TemplateSections = new Dictionary<long, List<SpecificationSectionInfo>>();
  public static bool Cached = false;
  public static Guid DocumentSectionGuid = new Guid("cad00256-306c-11d8-b4e9-00304f19f545");
  public static Guid ComplexSectionGuid = new Guid("cad00257-306c-11d8-b4e9-00304f19f545");
  public static Guid AssemblySectionGuid = new Guid("cad00258-306c-11d8-b4e9-00304f19f545");
  public static Guid DetailSectionGuid = new Guid("cad00259-306c-11d8-b4e9-00304f19f545");
  public static Guid StandartDetailSectionGuid = new Guid("cad0025a-306c-11d8-b4e9-00304f19f545");
  public static Guid OtherDetailSectionGuid = new Guid("cad0025b-306c-11d8-b4e9-00304f19f545");
  public static Guid MaterialSectionGuid = new Guid("cad0025c-306c-11d8-b4e9-00304f19f545");
  public static Guid ComplectSectionGuid = new Guid("cad0025d-306c-11d8-b4e9-00304f19f545");
  public static Guid ComplectUnitsSectionGuid = new Guid("cad00271-306c-11d8-b4e9-00304f19f545");
  public Guid SectionGuid = Guid.Empty;
  public long SectionID = -1;
  public int SectionType = -1;
  public string Caption;
  public long SortIndex;
  public int[] PartTypes;
  public long[] ImBaseCatalogs;
  public string RazdelSP;

  public static void CacheSpecSections(IUserSession session)
  {
    SpecificationSectionInfo.UpdateCacheSpecSections(session, (IList<long>) null);
  }

  public static List<SpecificationSectionInfo> GetAllowableSpecSections(long templateId)
  {
    List<SpecificationSectionInfo> specificationSectionInfoList = new List<SpecificationSectionInfo>();
    return SpecificationSectionInfo.TemplateSections.ContainsKey(templateId) ? SpecificationSectionInfo.TemplateSections[templateId] : (List<SpecificationSectionInfo>) null;
  }

  /// <summary>Получить заголовок раздела по умолчанию</summary>
  /// <param name="docType">Тип документа</param>
  /// <returns>заголовок</returns>
  public string GetDefaultCaption(AVSDocumentType docType)
  {
    string defaultCaption = (string) null;
    if (docType == AVSDocumentType.AutoIndustrySpecification)
    {
      switch (this.SectionGuid.ToString())
      {
        case "cad00256-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "0." + this.Caption;
          break;
        case "cad00257-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "1." + this.Caption;
          break;
        case "cad00258-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "2." + this.Caption;
          break;
        case "cad00259-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "4." + this.Caption;
          break;
        case "cad0025a-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "5." + this.Caption;
          break;
        case "cad0025b-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "6." + this.Caption;
          break;
        case "cad0025c-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "7." + this.Caption;
          break;
        case "cad0025d-306c-11d8-b4e9-00304f19f545":
          defaultCaption = "8." + this.Caption;
          break;
      }
    }
    return defaultCaption;
  }

  /// <summary>Получить заголовок раздела экспортной СП по умолчанию</summary>
  /// <param name="docType">Тип документа</param>
  /// <returns>заголовок</returns>
  public string GetDefaultExportCaption()
  {
    string defaultExportCaption = (string) null;
    switch (this.SectionGuid.ToString())
    {
      case "cad00256-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Documentation";
        break;
      case "cad00257-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Complexes";
        break;
      case "cad00258-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Assembly units";
        break;
      case "cad00259-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Parts";
        break;
      case "cad0025a-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Standard items";
        break;
      case "cad0025b-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Other items";
        break;
      case "cad0025c-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Materials";
        break;
      case "cad0025d-306c-11d8-b4e9-00304f19f545":
        defaultExportCaption = "Sets";
        break;
    }
    return defaultExportCaption;
  }

  /// <summary>Получить список допустимых разделов для шаблона</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <returns></returns>
  private static List<SpecificationSectionInfo> GetSpecSectionsFromDB(
    IUserSession session,
    long templateId,
    AVSDocumentType? docType)
  {
    List<SpecificationSectionInfo> specSectionsFromDb = new List<SpecificationSectionInfo>();
    List<long> longList = new List<long>();
    IDBObject dbObject = session.GetObject(templateId, false);
    if (dbObject != null)
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_AllowableSections);
      if (attributeById != null)
      {
        for (int index = 0; index < attributeById.ValuesCount; ++index)
        {
          if (!(attributeById.Values[index] is DBNull))
          {
            long int64 = Convert.ToInt64(attributeById.Values[index]);
            if (int64 != 0L && !longList.Contains(int64))
              longList.Add(int64);
          }
        }
      }
    }
    if (longList.Count == 0)
    {
      specSectionsFromDb.AddRange((IEnumerable<SpecificationSectionInfo>) SpecificationSectionInfo.Sections);
    }
    else
    {
      for (int index = 0; index < longList.Count; ++index)
      {
        foreach (SpecificationSectionInfo section in SpecificationSectionInfo.Sections)
        {
          if (section.SectionID == longList[index])
          {
            specSectionsFromDb.Add(section);
            break;
          }
        }
      }
    }
    return specSectionsFromDb;
  }

  public static int[] GetDefaultPartTypes(IUserSession session, Guid SectionGuid)
  {
    List<int> intList = new List<int>();
    if (SectionGuid == new Guid("cad00256-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_ConstructorDocument);
    if (SectionGuid == new Guid("cad00257-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_Complex);
    if (SectionGuid == new Guid("cad00258-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_AssemblyUnit);
    if (SectionGuid == new Guid("cad00259-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_Detail);
    if (SectionGuid == new Guid("cad0025a-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_StandartProduct);
    if (SectionGuid == SpecificationSectionInfo.MaterialSectionGuid)
      intList.Add(AvsIDCache.ObjType_Materials);
    if (SectionGuid == new Guid("cad0025b-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_OtherProduct);
    if (SectionGuid == new Guid("cad0025d-306c-11d8-b4e9-00304f19f545"))
      intList.Add(AvsIDCache.ObjType_Complect);
    if (SectionGuid == new Guid("cad00271-306c-11d8-b4e9-00304f19f545"))
    {
      intList.Add(AvsIDCache.ObjType_AssemblyUnit);
      intList.Add(AvsIDCache.ObjType_Detail);
    }
    return intList.ToArray();
  }

  public static int GetDefaultPartType(long sectionID)
  {
    SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(sectionID);
    return sectionById != null ? SpecificationSectionInfo.GetDefaultPartType(sectionById.SectionGuid) : -1;
  }

  /// <summary>Метод только для внутреннего использования в процессе отладки</summary>
  /// <param name="sectionGuid"></param>
  /// <returns></returns>
  public static int GetDefaultPartType(Guid sectionGuid)
  {
    if (sectionGuid == new Guid("cad00256-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_ConstructorDocument;
    if (sectionGuid == new Guid("cad00257-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_Complex;
    if (sectionGuid == new Guid("cad00258-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_AssemblyUnit;
    if (sectionGuid == new Guid("cad00259-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_Detail;
    if (sectionGuid == new Guid("cad0025a-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_StandartProduct;
    if (sectionGuid == SpecificationSectionInfo.MaterialSectionGuid)
      return AvsIDCache.ObjType_Materials;
    if (sectionGuid == new Guid("cad0025b-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_OtherProduct;
    if (sectionGuid == new Guid("cad0025d-306c-11d8-b4e9-00304f19f545"))
      return AvsIDCache.ObjType_Complect;
    return sectionGuid == new Guid("cad00271-306c-11d8-b4e9-00304f19f545") ? AvsIDCache.ObjType_AssemblyUnit : -1;
  }

  public static long[] GetDefaultImbaseCatalogs(IUserSession session, Guid SectionGuid)
  {
    List<long> longList = new List<long>();
    if (SectionGuid == new Guid("cad00258-306c-11d8-b4e9-00304f19f545"))
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cad008ea-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo.Empty)
        longList.Add(objectInfo.ObjectID);
    }
    else if (SectionGuid == new Guid("cad00259-306c-11d8-b4e9-00304f19f545") || SectionGuid == new Guid("cad0025a-306c-11d8-b4e9-00304f19f545") || SectionGuid == new Guid("cad0025b-306c-11d8-b4e9-00304f19f545"))
    {
      QuickObjectInfo objectInfo1 = session.GetObjectInfo(new Guid("cad008d9-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo1.Empty)
        longList.Add(objectInfo1.ObjectID);
      QuickObjectInfo objectInfo2 = session.GetObjectInfo(new Guid("cad008e6-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo2.Empty)
        longList.Add(objectInfo2.ObjectID);
    }
    else if (SectionGuid == SpecificationSectionInfo.MaterialSectionGuid)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cad008db-306c-11d8-b4e9-00304f19f545"));
      if (!objectInfo.Empty)
        longList.Add(objectInfo.ObjectID);
    }
    return longList.ToArray();
  }

  /// <summary>Кэширование разделов для шаблона</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <param name="docType">Тип конструкторского документа</param>
  /// <returns></returns>
  public static List<SpecificationSectionInfo> CacheSpecSections(
    IUserSession session,
    long templateId,
    AVSDocumentType? docType)
  {
    List<SpecificationSectionInfo> specSectionsFromDb = SpecificationSectionInfo.GetSpecSectionsFromDB(session, templateId, docType);
    specSectionsFromDb.Sort();
    SpecificationSectionInfo.TemplateSections[templateId] = specSectionsFromDb;
    return specSectionsFromDb;
  }

  /// <summary>Обновить кэш разделов СП</summary>
  /// <param name="session">Сессия</param>
  /// <param name="sectionsID">Идентификаторы объектов разделов. Если null, то обновить все разделы</param>
  public static void UpdateCacheSpecSections(IUserSession session, IList<long> sectionsID)
  {
    if (sectionsID == null)
    {
      SpecificationSectionInfo.Sections.Clear();
      SpecificationSectionInfo.SectionDictionaryByID.Clear();
      SpecificationSectionInfo.SectionDictionaryByGuid.Clear();
      SpecificationSectionInfo.SectionDictionaryByRazdelSP.Clear();
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection);
    ConditionStructure[] conditions = (ConditionStructure[]) null;
    if (sectionsID != null && sectionsID.Count > 0)
    {
      conditions = new ConditionStructure[sectionsID.Count];
      for (int index = 0; index < sectionsID.Count; ++index)
      {
        int groupID = index != 0 ? (index != sectionsID.Count - 1 ? 0 : -1) : 1;
        conditions[index] = new ConditionStructure(-2, RelationalOperators.Equal, (object) sectionsID[index], LogicalOperators.OR, groupID, true);
      }
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) AvsIDCache.Attr_SortIndex, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0),
      new ColumnDescriptor((object) AvsIDCache.Attr_SectionNum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    bool flag = false;
    long num1 = -1;
    Guid empty = Guid.Empty;
    while (!flag)
    {
      DataTable dataTable = objectCollection.Select(paramSet);
      int num2;
      for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
      {
        DataRow row1 = dataTable.Rows[index1];
        num2 = -2;
        string columnName1 = num2.ToString();
        num1 = Convert.ToInt64(row1[columnName1]);
        DataRow row2 = dataTable.Rows[index1];
        num2 = -7;
        string columnName2 = num2.ToString();
        int int32 = Convert.ToInt32(row2[columnName2]);
        ref Guid local = ref empty;
        DataRow row3 = dataTable.Rows[index1];
        num2 = -12;
        string columnName3 = num2.ToString();
        string g = Convert.ToString(row3[columnName3]);
        local = new Guid(g);
        DataRow row4 = dataTable.Rows[index1];
        num2 = -50;
        string columnName4 = num2.ToString();
        string caption = Convert.ToString(row4[columnName4]);
        DataRow row5 = dataTable.Rows[index1];
        num2 = AvsIDCache.Attr_SectionNum;
        string columnName5 = num2.ToString().ToString();
        object obj1 = row5[columnName5];
        string str1;
        switch (obj1)
        {
          case null:
          case DBNull _:
            str1 = (string) null;
            break;
          default:
            str1 = Convert.ToString(obj1);
            break;
        }
        DataRow row6 = dataTable.Rows[index1];
        num2 = AvsIDCache.Attr_SortIndex;
        string columnName6 = num2.ToString();
        object obj2 = row6[columnName6];
        long int64 = obj2 == null || obj2 == DBNull.Value ? 0L : Convert.ToInt64(obj2);
        IDBObject dbObject = session.GetObject(num1);
        IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_PossibleTypes);
        List<int> intList = new List<int>(attributeById.ValuesCount);
        int index2 = 0;
        for (int valuesCount = attributeById.ValuesCount; index2 < valuesCount; ++index2)
        {
          try
          {
            string Guid = attributeById.Values[index2].ToString();
            if (Guid != null)
            {
              if (Guid != "")
              {
                int objectTypeId = MetaDataHelper.GetObjectTypeID(Guid);
                if (objectTypeId != -1)
                  intList.Add(objectTypeId);
              }
            }
          }
          catch (Exception ex)
          {
          }
        }
        int[] partTypes = intList.ToArray();
        if (partTypes.Length == 0 || partTypes.Length == 1 && partTypes[0] == -1)
          partTypes = SpecificationSectionInfo.GetDefaultPartTypes(session, empty);
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(AvsIDCache.AttrRefToImBaseDirectory);
        long[] imBaseCatalogs;
        if (attributeByGuid != null)
        {
          imBaseCatalogs = new long[attributeByGuid.ValuesCount];
          int index3 = 0;
          for (int valuesCount = attributeByGuid.ValuesCount; index3 < valuesCount; ++index3)
          {
            try
            {
              string str2 = attributeByGuid.Values[index3].ToString();
              imBaseCatalogs[index3] = str2 == null || !(str2 != "") ? -1L : Convert.ToInt64(str2);
            }
            catch (Exception ex)
            {
            }
          }
        }
        else
          imBaseCatalogs = new long[0];
        if (imBaseCatalogs.Length == 0)
          imBaseCatalogs = SpecificationSectionInfo.GetDefaultImbaseCatalogs(session, empty);
        specificationSectionInfo = (SpecificationSectionInfo) null;
        if (sectionsID != null && sectionsID.Count > 0)
        {
          if (!(SpecificationSectionInfo.SectionDictionaryByID[(object) num1] is SpecificationSectionInfo specificationSectionInfo))
            specificationSectionInfo = SpecificationSectionInfo.SectionDictionaryByGuid[(object) empty] as SpecificationSectionInfo;
          if (specificationSectionInfo != null)
          {
            if (specificationSectionInfo.SectionID != num1)
            {
              SpecificationSectionInfo.SectionDictionaryByID.Remove((object) specificationSectionInfo.SectionID);
              SpecificationSectionInfo.SectionDictionaryByID[(object) num1] = (object) specificationSectionInfo;
            }
            if (specificationSectionInfo.SectionGuid != empty)
            {
              SpecificationSectionInfo.SectionDictionaryByGuid.Remove((object) specificationSectionInfo.SectionGuid);
              SpecificationSectionInfo.SectionDictionaryByGuid[(object) empty] = (object) specificationSectionInfo;
            }
            if (specificationSectionInfo.RazdelSP != str1)
            {
              if (specificationSectionInfo.RazdelSP != null && specificationSectionInfo.RazdelSP != "")
                SpecificationSectionInfo.SectionDictionaryByRazdelSP.Remove(specificationSectionInfo.RazdelSP);
              if (str1 != null && str1 != "")
                SpecificationSectionInfo.SectionDictionaryByRazdelSP[str1] = specificationSectionInfo;
            }
            specificationSectionInfo.SetSectionInfo(empty, num1, int32, caption, int64, str1, partTypes, imBaseCatalogs);
          }
        }
        if (specificationSectionInfo == null)
        {
          SpecificationSectionInfo specificationSectionInfo = new SpecificationSectionInfo(empty, num1, int32, caption, int64, str1, partTypes, imBaseCatalogs);
          SpecificationSectionInfo.Sections.Add(specificationSectionInfo);
          SpecificationSectionInfo.SectionDictionaryByID[(object) specificationSectionInfo.SectionID] = (object) specificationSectionInfo;
          SpecificationSectionInfo.SectionDictionaryByGuid[(object) specificationSectionInfo.SectionGuid] = (object) specificationSectionInfo;
          if (str1 != null && str1 != "")
            SpecificationSectionInfo.SectionDictionaryByRazdelSP[str1] = specificationSectionInfo;
        }
      }
      if (dataTable.Rows.Count > 0)
      {
        paramSet.LastKeyValue = num1;
        ref DBRecordSetParams local = ref paramSet;
        DataRow row = dataTable.Rows[dataTable.Rows.Count - 1];
        num2 = AvsIDCache.Attr_SortIndex;
        string columnName = num2.ToString();
        object obj = row[columnName];
        local.LastOrderValue = obj;
        flag = Convert.ToBoolean(dataTable.ExtendedProperties[(object) "Eof"]);
      }
      else
        flag = true;
      dataTable.Dispose();
    }
    SpecificationSectionInfo.Sections.Sort();
    SpecificationSectionInfo.Cached = true;
  }

  private static int GetSortIndexScaleForStandartSection(Guid sectionGuid)
  {
    if (sectionGuid == SpecificationSectionInfo.DocumentSectionGuid)
      return 10;
    if (sectionGuid == SpecificationSectionInfo.ComplexSectionGuid)
      return 20;
    if (sectionGuid == SpecificationSectionInfo.AssemblySectionGuid)
      return 30;
    if (sectionGuid == SpecificationSectionInfo.DetailSectionGuid)
      return 40;
    if (sectionGuid == SpecificationSectionInfo.StandartDetailSectionGuid)
      return 50;
    if (sectionGuid == SpecificationSectionInfo.OtherDetailSectionGuid)
      return 60;
    if (sectionGuid == SpecificationSectionInfo.MaterialSectionGuid)
      return 70;
    if (sectionGuid == SpecificationSectionInfo.ComplectSectionGuid)
      return 80 /*0x50*/;
    return sectionGuid == SpecificationSectionInfo.ComplectUnitsSectionGuid ? 90 : -1;
  }

  /// <summary>Получить множитель для индекса сортировки раздела</summary>
  /// <param name="sectionGuid">Guid раздела</param>
  /// <returns></returns>
  public static int GetSortIndexScale(Guid sectionGuid)
  {
    int forStandartSection = SpecificationSectionInfo.GetSortIndexScaleForStandartSection(sectionGuid);
    if (forStandartSection != -1)
      return forStandartSection;
    for (int index1 = 0; index1 < SpecificationSectionInfo.Sections.Count; ++index1)
    {
      if (SpecificationSectionInfo.Sections[index1].SectionGuid == sectionGuid)
      {
        int index2;
        for (index2 = index1 - 1; forStandartSection == -1 && index2 >= 0; --index2)
          forStandartSection = SpecificationSectionInfo.GetSortIndexScaleForStandartSection(SpecificationSectionInfo.Sections[index2].SectionGuid);
        return forStandartSection == -1 ? index1 : forStandartSection + index1 - index2;
      }
    }
    return 1;
  }

  /// <summary>Найти раздел по идентификтору</summary>
  /// <param name="id">Идентификатор</param>
  /// <returns></returns>
  public static SpecificationSectionInfo FindSectionById(long id)
  {
    for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
    {
      SpecificationSectionInfo section = SpecificationSectionInfo.Sections[index];
      if (section.SectionID == id)
        return section;
    }
    return (SpecificationSectionInfo) null;
  }

  /// <summary> Найти раздел по заголовку </summary>
  /// <param name="caption">Заголовок</param>
  /// <returns></returns>
  public static SpecificationSectionInfo FindSectionByCaption(string caption)
  {
    if (caption != "")
    {
      for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
      {
        SpecificationSectionInfo section = SpecificationSectionInfo.Sections[index];
        if (section.Caption == caption)
          return section;
      }
    }
    return (SpecificationSectionInfo) null;
  }

  /// <summary>Найти раздел по GUID</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public static SpecificationSectionInfo FindSectionById(Guid id)
  {
    for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
    {
      SpecificationSectionInfo section = SpecificationSectionInfo.Sections[index];
      if (section.SectionGuid == id)
        return section;
    }
    return (SpecificationSectionInfo) null;
  }

  /// <summary> Найти раздел по номеру (string)</summary>
  /// <param name="razdelSp"></param>
  /// <returns></returns>
  public static SpecificationSectionInfo FindSectionByRazdelSp(string razdelSp)
  {
    if (razdelSp != "")
    {
      for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
      {
        SpecificationSectionInfo section = SpecificationSectionInfo.Sections[index];
        if (section.RazdelSP == razdelSp)
          return section;
      }
    }
    return (SpecificationSectionInfo) null;
  }

  /// <summary> Найти раздел по номеру (long) </summary>
  /// <param name="razdelSp"></param>
  /// <returns></returns>
  public static SpecificationSectionInfo FindSectionByRazdelSpLong(long razdelSp)
  {
    string str = Convert.ToString(razdelSp);
    for (int index = 0; index < SpecificationSectionInfo.Sections.Count; ++index)
    {
      SpecificationSectionInfo section = SpecificationSectionInfo.Sections[index];
      if (section.RazdelSP == str)
        return section;
    }
    return (SpecificationSectionInfo) null;
  }

  public SpecificationSectionInfo(
    Guid sectionGuid,
    long sectionID,
    int sectionType,
    string caption,
    long sortIndex,
    string razdelSP,
    int[] partTypes,
    long[] imBaseCatalogs)
  {
    this.SetSectionInfo(sectionGuid, sectionID, sectionType, caption, sortIndex, razdelSP, partTypes, imBaseCatalogs);
  }

  public void SetSectionInfo(
    Guid sectionGuid,
    long sectionID,
    int sectionType,
    string caption,
    long sortIndex,
    string razdelSP,
    int[] partTypes,
    long[] imBaseCatalogs)
  {
    this.SectionGuid = sectionGuid;
    this.SectionID = sectionID;
    this.SectionType = sectionType;
    this.Caption = caption;
    this.SortIndex = sortIndex;
    this.PartTypes = partTypes;
    this.ImBaseCatalogs = imBaseCatalogs;
    this.RazdelSP = razdelSP;
  }

  public static bool IsAllowableTypeInSection(int objType, long sectionID)
  {
    if (SpecificationSectionInfo.SectionDictionaryByID.Contains((object) sectionID) && SpecificationSectionInfo.SectionDictionaryByID[(object) sectionID] is SpecificationSectionInfo specificationSectionInfo && specificationSectionInfo.PartTypes != null)
    {
      foreach (int partType in specificationSectionInfo.PartTypes)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objType, partType))
          return true;
      }
    }
    return false;
  }

  public override string ToString() => this.Caption;

  public int CompareTo(object obj)
  {
    return obj != null ? this.SortIndex.CompareTo(((SpecificationSectionInfo) obj).SortIndex) : throw new ArgumentNullException(nameof (obj));
  }
}
