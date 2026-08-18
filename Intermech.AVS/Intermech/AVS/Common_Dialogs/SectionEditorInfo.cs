// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.SectionEditorInfo
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

internal class SectionEditorInfo : IComparable
{
  private const long MaxSortOrderValue = 1000000;
  public bool Changed;
  public bool Deleted;
  public bool New;
  public Guid SectionGuid = Guid.Empty;
  public long SectionID = -1;
  public int SectionType = -1;
  public string Caption;
  private long sortIndex;
  public SectionItemList PartTypes;
  public SectionItemList ImBaseCatalogs;
  public long RazdelSP = -1;

  public override string ToString() => this.Caption;

  public List<AttributeValues> GetAttrubuteValues()
  {
    List<AttributeValues> attrubuteValues = new List<AttributeValues>();
    attrubuteValues.Add(new AttributeValues(AvsIDCache.Attr_Name, (object) this.Caption));
    attrubuteValues.Add(new AttributeValues(AvsIDCache.Attr_SectionNum, (object) this.RazdelSP));
    attrubuteValues.Add(new AttributeValues(AvsIDCache.Attr_SortIndex, (object) this.SortIndex));
    List<object> objectList1 = new List<object>();
    foreach (SectionItem partType in (List<SectionItem>) this.PartTypes)
    {
      if ((Guid) partType.Value != Guid.Empty)
        objectList1.Add((object) (Guid) partType.Value);
    }
    if (objectList1.Count == 0)
      objectList1.Add((object) DBNull.Value);
    List<object> objectList2 = new List<object>();
    foreach (SectionItem imBaseCatalog in (List<SectionItem>) this.ImBaseCatalogs)
    {
      if ((long) imBaseCatalog.Value != -1L)
        objectList2.Add((object) (long) imBaseCatalog.Value);
    }
    if (objectList2.Count == 0)
      objectList2.Add((object) DBNull.Value);
    attrubuteValues.Add(new AttributeValues(AvsIDCache.Attr_RefToImBaseDirectory, (object) objectList2.ToArray()));
    attrubuteValues.Add(new AttributeValues(AvsIDCache.Attr_PossibleTypes, (object) objectList1.ToArray()));
    return attrubuteValues;
  }

  /// <summary>Получить список разделов спецификации для указанного шаблона</summary>
  /// <param name="session">Сессия</param>
  /// <param name="templateId"></param>
  /// <returns></returns>
  public static List<SectionEditorInfo> GetAllowableSpecSections(
    IUserSession session,
    long templateId,
    List<SectionEditorInfo> allSections)
  {
    List<SectionEditorInfo> sectionEditorInfoList = new List<SectionEditorInfo>();
    IDBObject dbObject = session.GetObject(templateId, false);
    if (dbObject != null)
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_AllowableSections);
      if (attributeById != null)
      {
        List<long> longList = new List<long>();
        for (int index = 0; index < attributeById.ValuesCount; ++index)
        {
          if (!(attributeById.Values[index] is DBNull))
          {
            long int64 = Convert.ToInt64(attributeById.Values[index]);
            if (int64 != 0L)
              longList.Add(int64);
          }
        }
        for (int index = 0; index < longList.Count; ++index)
        {
          foreach (SectionEditorInfo allSection in allSections)
          {
            if (allSection.SectionID == longList[index])
            {
              sectionEditorInfoList.Add(allSection);
              break;
            }
          }
        }
      }
    }
    return sectionEditorInfoList.Count <= 0 ? allSections : sectionEditorInfoList;
  }

  /// <summary>Получить список разделов спецификации</summary>
  /// <param name="session">Сессия</param>
  public static List<SectionEditorInfo> GetAllowableSpecSections(IUserSession session)
  {
    List<SectionEditorInfo> allowableSpecSections = new List<SectionEditorInfo>();
    IDBObjectCollection objectCollection = session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[6]
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
        string g1 = Convert.ToString(row3[columnName3]);
        local = new Guid(g1);
        DataRow row4 = dataTable.Rows[index1];
        num2 = -50;
        string columnName4 = num2.ToString();
        string caption = Convert.ToString(row4[columnName4]);
        DataRow row5 = dataTable.Rows[index1];
        num2 = AvsIDCache.Attr_SectionNum;
        string columnName5 = num2.ToString();
        object obj1 = row5[columnName5];
        long razdelSP;
        switch (obj1)
        {
          case null:
          case DBNull _:
            razdelSP = -1L;
            break;
          default:
            razdelSP = Convert.ToInt64(obj1);
            break;
        }
        DataRow row6 = dataTable.Rows[index1];
        num2 = AvsIDCache.Attr_SortIndex;
        string columnName6 = num2.ToString();
        object obj2 = row6[columnName6];
        long int64 = obj2 == null || obj2 == DBNull.Value ? 0L : Convert.ToInt64(obj2);
        IDBObject dbObject = session.GetObject(num1);
        IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_PossibleTypes);
        SectionItemList sectionItemList1 = new SectionItemList(AvsIDCache.Attr_PossibleTypes);
        int index2 = 0;
        for (int valuesCount = attributeById.ValuesCount; index2 < valuesCount; ++index2)
        {
          try
          {
            string g2 = attributeById.Values[index2].ToString();
            if (g2 != null && g2 != "")
            {
              Guid objTypeGuid = new Guid(g2);
              if (MetaDataHelper.GetObjectTypeID(objTypeGuid) != -1)
                sectionItemList1.Add(new SectionItem((object) objTypeGuid, sectionItemList1));
            }
            else
              sectionItemList1.Add(new SectionItem((object) Guid.Empty, sectionItemList1));
          }
          catch (Exception ex)
          {
          }
        }
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(AvsIDCache.AttrRefToImBaseDirectory);
        SectionItemList sectionItemList2;
        if (attributeByGuid != null)
        {
          sectionItemList2 = new SectionItemList(AvsIDCache.Attr_RefToImBaseDirectory);
          int index3 = 0;
          for (int valuesCount = attributeByGuid.ValuesCount; index3 < valuesCount; ++index3)
          {
            try
            {
              object obj3 = attributeByGuid.Values[index3];
              string str = attributeByGuid.Values[index3].ToString();
              if (str != null && str != "")
                sectionItemList2.Add(new SectionItem((object) Convert.ToInt64(str), sectionItemList2));
              else
                sectionItemList2.Add(new SectionItem((object) -1L, sectionItemList2));
            }
            catch (Exception ex)
            {
            }
          }
        }
        else
          sectionItemList2 = new SectionItemList(AvsIDCache.Attr_RefToImBaseDirectory);
        SectionEditorInfo sectionEditorInfo = new SectionEditorInfo(empty, num1, int32, caption, int64, razdelSP, sectionItemList1, sectionItemList2);
        allowableSpecSections.Add(sectionEditorInfo);
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
    allowableSpecSections.Sort();
    return allowableSpecSections;
  }

  public long SortIndex
  {
    get
    {
      if (this.sortIndex > 1000000L)
        return 1000000;
      return this.sortIndex >= 0L ? this.sortIndex : 0L;
    }
    set => this.sortIndex = value < 0L ? 0L : (value > 1000000L ? 1000000L : value);
  }

  public SectionEditorInfo(
    Guid sectionGuid,
    long sectionID,
    int sectionType,
    string caption,
    long sortIndex,
    long razdelSP,
    SectionItemList partTypes,
    SectionItemList imBaseCatalogs)
  {
    this.SetSectionInfo(sectionGuid, sectionID, sectionType, caption, sortIndex, razdelSP, partTypes, imBaseCatalogs);
  }

  public SectionEditorInfo()
  {
    this.Caption = "Пустой раздел";
    this.PartTypes = new SectionItemList(AvsIDCache.Attr_PossibleTypes);
    this.ImBaseCatalogs = new SectionItemList(AvsIDCache.Attr_ImbaseKey);
  }

  public void SetSectionInfo(
    Guid sectionGuid,
    long sectionID,
    int sectionType,
    string caption,
    long sortIndex,
    long razdelSP,
    SectionItemList partTypes,
    SectionItemList imBaseCatalogs)
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

  public int CompareTo(object obj)
  {
    int num = obj != null ? this.SortIndex.CompareTo(((SectionEditorInfo) obj).SortIndex) : throw new ArgumentNullException(nameof (obj));
    if (num == 0)
      num = this.Caption.CompareTo(((SectionEditorInfo) obj).Caption);
    return num;
  }
}
