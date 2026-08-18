// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.SubstitutesService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pdm.Substitutes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

public class SubstitutesService : LongLifeObject, ISubstitutesService
{
  private static IRelationsComparerService RelationsCompareSvc;

  protected virtual void ExtractRelationPackage(
    DataTable composition,
    List<int> attributes,
    List<string> attributesGUIDs,
    List<long> relations,
    ref RelationAttributesPackage relAttrs)
  {
    if (relAttrs == null)
      relAttrs = new RelationAttributesPackage(attributes);
    if (composition == null || attributes.Count != attributesGUIDs.Count)
      return;
    int columnIndex = SubstituteObjects.AttrsIndex[-20];
    foreach (DataRow row in (InternalDataCollectionBase) composition.Rows)
    {
      long result;
      if (long.TryParse(row[columnIndex].ToString(), out result) && (relations == null || relations.Contains(result)))
      {
        for (int index = 0; index < attributes.Count; ++index)
          relAttrs[result, attributes[index]] = row[SubstituteObjects.AttrsIndex[attributes[index]]];
      }
    }
  }

  private static List<int> GetChildObjectTypes(
    IUserSession session,
    int parentObjTypeID,
    int relTypeID)
  {
    List<int> childObjectTypes = new List<int>();
    bool flag1 = false;
    bool flag2 = false;
    CompositionsAutosortRule autosortRule = session.GetCustomService(typeof (ICompositionsAutomaticSortingService)) is ICompositionsAutomaticSortingService customService ? customService.GetAutosortRule((object) session.SessionGUID, false) : (CompositionsAutosortRule) null;
    if (autosortRule != null)
    {
      int index1 = autosortRule.IndexOfParentObjectType(parentObjTypeID, true);
      if (index1 >= 0)
      {
        ChildRelationType childRelationType = autosortRule.ParentObjectTypes[index1][relTypeID];
        if (childRelationType != null)
        {
          for (int index2 = 0; index2 < childRelationType.ChildObjectTypes.Count; ++index2)
          {
            List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(childRelationType.ChildObjectTypes[index2].ObjectTypeID);
            if (!flag1)
            {
              for (int index3 = 0; index3 < childrenIdRecursive.Count; ++index3)
              {
                IMSObjectType objectType = MetaDataHelper.GetObjectType(childrenIdRecursive[index3]);
                flag1 = objectType != null && objectType.IsLocalType;
                if (flag1)
                  break;
              }
            }
            for (int index4 = 0; index4 < childrenIdRecursive.Count; ++index4)
            {
              if (childObjectTypes.IndexOf(childrenIdRecursive[index4]) < 0)
                childObjectTypes.Add(childrenIdRecursive[index4]);
            }
          }
        }
        if (!flag1)
          childObjectTypes.Clear();
        flag2 = childObjectTypes.Count > 0;
      }
      if (!flag2)
      {
        DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(relTypeID, -1, parentObjTypeID);
        int num1 = -1;
        if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
        {
          List<int> intList1 = new List<int>(applicabilitiesList.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
          {
            int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
            if (!intList1.Contains(int32))
              intList1.Add(int32);
          }
          if (intList1.Count == 1)
          {
            num1 = intList1[0];
          }
          else
          {
            List<List<int>> intListList = new List<List<int>>(intList1.Count);
            for (int index5 = 0; index5 < intList1.Count; ++index5)
            {
              List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(intList1[index5]);
              List<int> intList2 = new List<int>(parentsIdReverse.Count);
              for (int index6 = parentsIdReverse.Count - 1; index6 >= 0 && !MetaDataHelper.IsLocalObjectType(parentsIdReverse[index6]); --index6)
                intList2.Insert(0, parentsIdReverse[index6]);
              intListList.Add(intList2);
            }
            int index7 = 0;
            bool flag3 = false;
            while (true)
            {
              int num2 = -1;
              for (int index8 = 0; index8 < intListList.Count; ++index8)
              {
                if (intListList[index8].Count <= index7)
                {
                  flag3 = true;
                  break;
                }
                if (!flag3)
                {
                  num2 = intListList[0][index7];
                  if (num2 != intListList[index8][index7])
                  {
                    flag3 = true;
                    break;
                  }
                  if (flag3)
                    break;
                }
                else
                  break;
              }
              if (!flag3)
              {
                if (num2 != -1)
                  num1 = num2;
                ++index7;
              }
              else
                break;
            }
          }
        }
        applicabilitiesList?.Dispose();
        childObjectTypes.Add(num1);
      }
    }
    return childObjectTypes;
  }

  public virtual DataTable LoadComposition(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> columns)
  {
    DataTable dataTable = (DataTable) null;
    if (projID == 0L || relationType == 0 || columns == null || columns.Count <= 0)
      return dataTable;
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    ColumnDescriptor[] array = columns.ToArray();
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.Equal, (object) projID, LogicalOperators.NONE, 0, true)
    }, array);
    IDBRelationCollection relationCollection = sessionById.GetRelationCollection(relationType, filtrationOwnerID);
    if (sessionById.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService && !string.IsNullOrEmpty(filtrationOwnerID))
    {
      FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) sessionById.SessionGUID, filtrationOwnerID);
      if (filtrationSettings != null && filtrationSettings.Tags != null)
        paramSet.Tags = filtrationSettings.Tags;
    }
    paramSet.Tags = paramSet.Tags != null ? paramSet.Tags : new HybridDictionary(0, true);
    if (paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] == null)
      paramSet.Tags[(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true;
    if (paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] == null)
      paramSet.Tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
    paramSet.Tags[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) contexts;
    if (paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] == null)
      paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true;
    try
    {
      if (relationCollection != null)
      {
        QuickObjectInfo objectInfo = sessionById.GetObjectInfo(projID);
        relationCollection.ChildObjectTypes = (IList<int>) SubstitutesService.GetChildObjectTypes(sessionById, objectInfo.ObjectTypeID, relationType);
        dataTable = relationCollection.Select(paramSet);
      }
    }
    catch
    {
    }
    return dataTable;
  }

  public virtual DataTable LoadComposition(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> advColumns,
    out SubstituteObjects substitutes,
    out Dictionary<long, DataRow> relationsIndex)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    List<ColumnDescriptor> substitutesColumns = SubstituteObjects.SubstitutesColumns;
    Dictionary<ColumnDescriptor, int> dictionary = new Dictionary<ColumnDescriptor, int>();
    if (advColumns != null && advColumns.Count > 0)
    {
      for (int index = 0; index < advColumns.Count; ++index)
      {
        ColumnDescriptor advColumn = advColumns[index];
        int num = substitutesColumns.IndexOf(advColumn);
        if (num == -1)
        {
          substitutesColumns.Add(advColumn);
          num = substitutesColumns.Count - 1;
        }
        dictionary.Add(advColumn, num);
      }
    }
    DataTable dataTable = this.LoadComposition(sessionID, filtrationOwnerID, contexts, projID, relationType, substitutesColumns);
    int columnIndex1 = SubstituteObjects.AttrsIndex[-20];
    int columnIndex2 = SubstituteObjects.AttrsIndex[-2];
    int columnIndex3 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteGroupNo];
    int columnIndex4 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteInGroup];
    int columnIndex5 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteGroupName];
    substitutes = new SubstituteObjects(sessionById);
    relationsIndex = new Dictionary<long, DataRow>();
    if (dataTable == null || dataTable.Rows.Count == 0)
      return dataTable;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long result1 = 0;
      long result2 = 0;
      string empty = string.Empty;
      long result3;
      long result4;
      if (long.TryParse(row[columnIndex1].ToString(), out result3) && long.TryParse(row[columnIndex2].ToString(), out result4))
      {
        if (!long.TryParse(row[columnIndex3].ToString(), out result1))
          result1 = 0L;
        if (!long.TryParse(row[columnIndex4].ToString(), out result2))
          result2 = 0L;
        if (row[columnIndex5] != DBNull.Value)
          empty = row[columnIndex5].ToString();
        substitutes.AddRelation(result1, result2, result3, result4);
        substitutes.SetRelationAttributes(result3, row);
        if (!string.IsNullOrEmpty(empty))
          substitutes.SetSubstGroupName(result1, empty);
        relationsIndex.Add(result3, row);
      }
    }
    return dataTable;
  }

  public virtual SubstituteObjects LoadSubstitutes(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType)
  {
    SubstituteObjects substitutes;
    this.LoadComposition(sessionID, filtrationOwnerID, contexts, projID, relationType, (List<ColumnDescriptor>) null, out substitutes, out Dictionary<long, DataRow> _)?.Dispose();
    return substitutes;
  }

  public virtual SubstituteObjects LoadSubstitutes(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    List<ColumnDescriptor> attributes,
    out RelationAttributesPackage relAttributes)
  {
    List<int> attributes1 = new List<int>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    List<ColumnDescriptor> substitutesColumns = SubstituteObjects.SubstitutesColumns;
    if (attributes != null)
    {
      for (int index = 0; index < attributes.Count; ++index)
      {
        attributes1.Add(((UserSession) sessionById).EventLogHelper.GetAttributeID((object) attributes[index]));
        if (substitutesColumns.IndexOf(attributes[index]) < 0)
          substitutesColumns.Add(attributes[index]);
      }
    }
    relAttributes = new RelationAttributesPackage(attributes1);
    SubstituteObjects substitutes;
    Dictionary<long, DataRow> relationsIndex;
    DataTable dataTable = this.LoadComposition(sessionID, filtrationOwnerID, contexts, projID, relationType, attributes, out substitutes, out relationsIndex);
    if (attributes != null && substitutes.Groups.Count > 0)
    {
      List<long> relations = new List<long>();
      substitutes.GatherRelations(ref relations);
      SubstituteObjects.InitStaticFields(sessionById);
      int[] numArray = new int[attributes.Count];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = substitutesColumns.IndexOf(attributes[index]);
      for (int index1 = 0; index1 < relations.Count; ++index1)
      {
        long num = relations[index1];
        DataRow dataRow = relationsIndex[num];
        for (int index2 = 0; index2 < numArray.Length; ++index2)
        {
          object obj1 = dataRow[numArray[index2]];
          object obj2 = obj1 != DBNull.Value ? obj1 : (object) null;
          relAttributes[num, attributes1[index2]] = obj2;
        }
      }
    }
    dataTable?.Dispose();
    return substitutes;
  }

  private void FinallyMarkF_PART_IDs(
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows,
    long F_PART_ID,
    ArticleRelationState state)
  {
    foreach (KeyValuePair<long, Dictionary<long, List<DataRowHolder>>> partsRow in partsRows)
    {
      if (partsRow.Value.ContainsKey(F_PART_ID))
      {
        List<DataRowHolder> dataRowHolderList = partsRow.Value[F_PART_ID];
        for (int index = 0; index < dataRowHolderList.Count; ++index)
        {
          dataRowHolderList[index].Tag = (object) state;
          dataRowHolderList[index].Parsed = true;
        }
      }
    }
  }

  private bool ExistsF_PART_ID(
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows,
    long F_PART_ID)
  {
    bool flag = partsRows.Count > 0;
    foreach (KeyValuePair<long, Dictionary<long, List<DataRowHolder>>> partsRow in partsRows)
    {
      flag &= partsRow.Value.ContainsKey(F_PART_ID);
      if (!flag)
        return flag;
    }
    return flag;
  }

  private void FindVariableF_PART_IDs(
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows)
  {
    foreach (KeyValuePair<long, Dictionary<long, List<DataRowHolder>>> partsRow in partsRows)
    {
      long key1 = partsRow.Key;
      long num = 0;
      foreach (KeyValuePair<long, List<DataRowHolder>> keyValuePair in partsRow.Value)
      {
        long key2 = keyValuePair.Key;
        if (key2 != num)
        {
          if (!this.ExistsF_PART_ID(partsRows, key2))
            this.FinallyMarkF_PART_IDs(partsRows, key2, ArticleRelationState.VariablePart);
          num = key2;
        }
      }
    }
  }

  private void CompareLinkWithLinks(
    IUserSession session,
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows,
    long articleID,
    long F_PART_ID,
    DataRowHolder partRow,
    AVSSpecificationForm spcForm)
  {
    if (partRow.Parsed)
      return;
    long int64Value1 = DataSetProcessor.GetInt64Value(partRow.Row, SubstituteObjects.CompareArtRelationsAttrsIndex[-20], 0L);
    List<long> longList = new List<long>((IEnumerable<long>) partsRows.Keys);
    longList.Remove(articleID);
    List<DataRowHolder> dataRowHolderList1 = new List<DataRowHolder>(longList.Count + 1);
    dataRowHolderList1.Add(partRow);
    for (int index1 = 0; index1 < longList.Count; ++index1)
    {
      long key = longList[index1];
      Dictionary<long, List<DataRowHolder>> partsRow = partsRows[key];
      List<DataRowHolder> dataRowHolderList2 = partsRow.ContainsKey(F_PART_ID) ? partsRow[F_PART_ID] : (List<DataRowHolder>) null;
      if (dataRowHolderList2 != null && dataRowHolderList2.Count != 0)
      {
        List<int> attrIDs = SubstituteObjects.AttrsToCompareArtRelations;
        if (spcForm == AVSSpecificationForm.B)
          attrIDs = SubstituteObjects.AttrsToCompareArtRelationsFormB;
        for (int index2 = 0; index2 < dataRowHolderList2.Count; ++index2)
        {
          DataRowHolder dataRowHolder = dataRowHolderList2[index2];
          if (!dataRowHolder.Parsed)
          {
            long int64Value2 = DataSetProcessor.GetInt64Value(dataRowHolder.Row, SubstituteObjects.CompareArtRelationsAttrsIndex[-20], 0L);
            if (SubstitutesService.RelationsCompareSvc.EqualsTo(session, attrIDs, int64Value1, int64Value2, partRow.Row, dataRowHolder.Row, false))
            {
              dataRowHolderList1.Add(dataRowHolder);
              break;
            }
          }
        }
      }
    }
    if (dataRowHolderList1.Count == longList.Count + 1)
    {
      for (int index = 0; index < dataRowHolderList1.Count; ++index)
      {
        dataRowHolderList1[index].Parsed = true;
        dataRowHolderList1[index].Tag = (object) ArticleRelationState.CommonPart;
      }
    }
    else
    {
      partRow.Parsed = true;
      partRow.Tag = (object) ArticleRelationState.VariablePart;
    }
  }

  private void ProcessAllRelations(
    IUserSession session,
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows,
    AVSSpecificationForm spcForm)
  {
    List<long> longList1 = new List<long>((IEnumerable<long>) partsRows.Keys);
    for (int index1 = 0; index1 < longList1.Count; ++index1)
    {
      long num = longList1[index1];
      List<long> longList2 = new List<long>((IEnumerable<long>) partsRows[num].Keys);
      Dictionary<long, List<DataRowHolder>> partsRow = partsRows[num];
      for (int index2 = 0; index2 < longList2.Count; ++index2)
      {
        List<DataRowHolder> dataRowHolderList = partsRow[longList2[index2]];
        for (int index3 = 0; index3 < dataRowHolderList.Count; ++index3)
        {
          if (!dataRowHolderList[index3].Parsed)
            this.CompareLinkWithLinks(session, partsRows, num, longList2[index2], dataRowHolderList[index3], spcForm);
        }
      }
    }
  }

  public virtual ArticlesPartsPackage FindCommonAndVariableParts(
    Guid sessionID,
    string filtrationOwnerID,
    long articleID,
    int relationType,
    AVSSpecificationForm spcForm)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    ArticlesPartsPackage andVariableParts = new ArticlesPartsPackage();
    IArticleService service1 = ServerServices.GetService(typeof (IArticleService)) as IArticleService;
    ICompositionLoadService service2 = ServerServices.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
    SubstitutesService.RelationsCompareSvc = SubstitutesService.RelationsCompareSvc == null ? sessionById.GetCustomService(typeof (IRelationsComparerService)) as IRelationsComparerService : SubstitutesService.RelationsCompareSvc;
    if (service1 == null || service2 == null || SubstitutesService.RelationsCompareSvc == null)
      return andVariableParts;
    SubstituteObjects.InitStaticFields(sessionById);
    int columnIndex1 = SubstituteObjects.CompareArtRelationsAttrsIndex[-21];
    int columnIndex2 = SubstituteObjects.CompareArtRelationsAttrsIndex[-22];
    int columnIndex3 = SubstituteObjects.CompareArtRelationsAttrsIndex[-20];
    List<long> listInstances = service1.GetListInstances(articleID, (object) sessionById);
    if (listInstances == null || listInstances.Count == 0)
      return andVariableParts;
    listInstances.Remove(articleID);
    listInstances.Insert(0, articleID);
    DataTable dataTable = service2.LoadComplexCompositions((object) sessionID, (IEnumerable<long>) listInstances, relationType, (IEnumerable<ColumnDescriptor>) SubstituteObjects.CompareArtRelationsColumns, filtrationOwnerID);
    if (dataTable == null)
      return (ArticlesPartsPackage) null;
    Dictionary<long, List<DataRowHolder>> dictionary = new Dictionary<long, List<DataRowHolder>>();
    Dictionary<long, Dictionary<long, List<DataRowHolder>>> partsRows = new Dictionary<long, Dictionary<long, List<DataRowHolder>>>();
    long key1 = 0;
    List<DataRowHolder> dataRowHolderList1 = (List<DataRowHolder>) null;
    long key2 = 0;
    List<DataRowHolder> dataRowHolderList2 = (List<DataRowHolder>) null;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      DataRow row = dataTable.Rows[index];
      long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex1, 0L);
      long int64Value2 = DataSetProcessor.GetInt64Value(row, columnIndex2, 0L);
      if (int64Value1 != 0L && int64Value2 != 0L)
      {
        if (key1 != int64Value1)
        {
          dataRowHolderList1 = new List<DataRowHolder>();
          key1 = int64Value1;
          dictionary[key1] = dataRowHolderList1;
          key2 = 0L;
          dataRowHolderList2 = (List<DataRowHolder>) null;
          partsRows[key1] = new Dictionary<long, List<DataRowHolder>>();
        }
        if (key2 != int64Value2)
        {
          dataRowHolderList2 = new List<DataRowHolder>();
          key2 = int64Value2;
          partsRows[key1][key2] = dataRowHolderList2;
        }
        DataRowHolder dataRowHolder = new DataRowHolder(row, (object) null, false);
        dataRowHolderList1.Add(dataRowHolder);
        dataRowHolderList2.Add(dataRowHolder);
      }
    }
    this.FindVariableF_PART_IDs(partsRows);
    this.ProcessAllRelations(sessionById, partsRows, spcForm);
    foreach (KeyValuePair<long, List<DataRowHolder>> keyValuePair in dictionary)
    {
      long key3 = keyValuePair.Key;
      List<long> commonPart = new List<long>();
      List<long> variablePart = new List<long>();
      List<DataRowHolder> dataRowHolderList3 = keyValuePair.Value;
      for (int index = 0; index < dataRowHolderList3.Count; ++index)
      {
        long int64Value = DataSetProcessor.GetInt64Value(dataRowHolderList3[index].Row, columnIndex3, 0L);
        ArticleRelationState tag = (ArticleRelationState) dataRowHolderList3[index].Tag;
        if (spcForm == AVSSpecificationForm.A)
        {
          if (tag == ArticleRelationState.CommonPart)
          {
            commonPart.Add(int64Value);
          }
          else
          {
            if (tag != ArticleRelationState.VariablePart)
              throw new ApplicationException(LocalizationHolder.rm.GetString("Pdm.Server_49"));
            variablePart.Add(int64Value);
          }
        }
        else
          variablePart.Add(int64Value);
      }
      andVariableParts.AddArticle(key3, commonPart, variablePart);
    }
    return andVariableParts;
  }

  public virtual bool FindCommonArticles(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationTypeID,
    List<ColumnDescriptor> advColumns,
    SubstituteObjects clientGroups,
    bool adminMode,
    AVSSpecificationForm spcForm,
    out Dictionary<long, RelationAttributesPackage> newGroups)
  {
    bool commonArticles = false;
    List<int> attrIDs = SubstituteObjects.AttrsToCompareArtRelations;
    if (spcForm == AVSSpecificationForm.B)
      attrIDs = SubstituteObjects.AttrsToCompareArtRelationsFormB;
    newGroups = new Dictionary<long, RelationAttributesPackage>();
    if (!(ServerServices.GetService(typeof (IArticleService)) is IArticleService service))
      return commonArticles;
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    List<long> listInstances = service.GetListInstances(projID, (object) sessionById);
    if (listInstances == null || listInstances.Count == 1 && listInstances[0] == projID)
      return commonArticles;
    List<ColumnDescriptor> substitutesColumns = SubstituteObjects.SubstitutesColumns;
    if (advColumns != null && advColumns.Count > 0)
    {
      for (int index = 0; index < advColumns.Count; ++index)
      {
        ColumnDescriptor advColumn = advColumns[index];
        if (!substitutesColumns.Contains(advColumn))
          substitutesColumns.Add(advColumn);
      }
    }
    List<int> attributes = new List<int>();
    attributes.Add(sessionById.IdentHelper.SubstitutesGroupNoID);
    attributes.Add(sessionById.IdentHelper.SubstituteInGroup);
    attributes.Add(sessionById.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545"));
    attributes.Add(sessionById.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545"));
    attributes.Add(sessionById.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545"));
    List<string> attributesGUIDs = new List<string>();
    attributesGUIDs.Add("cad001c0-306c-11d8-b4e9-00304f19f545");
    attributesGUIDs.Add("cad001c1-306c-11d8-b4e9-00304f19f545");
    attributesGUIDs.Add("cad00654-306c-11d8-b4e9-00304f19f545");
    attributesGUIDs.Add("cad00817-306c-11d8-b4e9-00304f19f545");
    attributesGUIDs.Add("cad00818-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects substitutes1;
    Dictionary<long, DataRow> relationsIndex1;
    DataTable composition1 = this.LoadComposition(sessionID, filtrationOwnerID, contexts, projID, relationTypeID, advColumns, out substitutes1, out relationsIndex1);
    List<long> relations1 = new List<long>(sc_17065.ssp_pdm_server_17066(1636068507));
    List<long> relations2 = new List<long>();
    List<long> relations3 = new List<long>();
    substitutes1.GatherRelations(ref relations1);
    substitutes1.GatherRelations(ref relations2);
    clientGroups.GatherRelations(ref relations1);
    clientGroups.GatherRelations(ref relations3);
    RelationAttributesPackage relAttrs1 = (RelationAttributesPackage) null;
    this.ExtractRelationPackage(composition1, attributes, attributesGUIDs, relations1, ref relAttrs1);
    int num1 = SubstituteObjects.AttrsIndex[-20];
    int columnIndex1 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteGroupNo];
    int columnIndex2 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteInGroup];
    int num2 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrDesignActualVariant];
    IRelationsComparerService customService = sessionById.GetCustomService(typeof (IRelationsComparerService)) as IRelationsComparerService;
    sessionById.GetCustomService(typeof (ISubstitutesSettings));
    sessionById.GetCustomService(typeof (ISubstitutesRemarksService));
    for (int index1 = 0; index1 < listInstances.Count; ++index1)
    {
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      RelationAttributesPackage attributesPackage = new RelationAttributesPackage(attributes);
      long num3 = listInstances[index1];
      newGroups.Add(num3, attributesPackage);
      List<long> relations4 = new List<long>();
      SubstituteObjects substitutes2;
      Dictionary<long, DataRow> relationsIndex2;
      DataTable composition2 = this.LoadComposition(sessionID, filtrationOwnerID, contexts, num3, relationTypeID, advColumns, out substitutes2, out relationsIndex2);
      substitutes2.GatherRelations(ref relations4);
      substitutes2.RebuildGroups();
      Dictionary<long, long> dictionary1 = new Dictionary<long, long>();
      List<long> groups1 = clientGroups.Groups;
      List<long> groups2 = substitutes2.Groups;
      long val1 = 0;
      for (int index2 = 0; index2 < groups2.Count; ++index2)
        val1 = Math.Max(val1, groups2[index2]);
      for (int index3 = 0; index3 < groups1.Count; ++index3)
        val1 = Math.Max(val1, groups1[index3]);
      long num4 = val1 + 1L;
      for (int index4 = 0; index4 < groups1.Count; ++index4)
      {
        dictionary1[groups1[index4]] = groups1[index4];
        if (groups2.IndexOf(groups1[index4]) >= 0)
        {
          dictionary1[groups1[index4]] = num4;
          ++num4;
        }
      }
      RelationAttributesPackage relAttrs2 = (RelationAttributesPackage) null;
      this.ExtractRelationPackage(composition2, attributes, attributesGUIDs, relations4, ref relAttrs2);
      Dictionary<long, List<long>> dictionary2 = new Dictionary<long, List<long>>(relations1.Count);
      for (int index5 = 0; index5 < relations1.Count; ++index5)
      {
        long num5 = relations1[index5];
        List<long> longList3 = new List<long>();
        dictionary2.Add(num5, longList3);
        foreach (KeyValuePair<long, DataRow> keyValuePair in relationsIndex2)
        {
          long key = keyValuePair.Key;
          if (relationsIndex1.ContainsKey(num5) && customService.EqualsTo(sessionById, attrIDs, num5, key, relationsIndex1[num5], relationsIndex2[key], true))
            longList3.Add(key);
        }
      }
      List<long> relations5 = new List<long>();
      List<long> longList4 = new List<long>();
      for (int index6 = 0; index6 < relations1.Count; ++index6)
      {
        long num6 = relations1[index6];
        if (!longList1.Contains(num6))
        {
          relations5.Clear();
          longList4.Clear();
          if (relationsIndex1.ContainsKey(num6))
          {
            DataRow dataRow = relationsIndex1[num6];
            long Group1;
            substitutes1.IndexOf(num6, out Group1, out long _);
            long Group2;
            clientGroups.IndexOf(num6, out Group2, out long _);
            List<long> longList5 = dictionary2.ContainsKey(num6) ? dictionary2[num6] : (List<long>) null;
            Dictionary<long, long> dictionary3 = new Dictionary<long, long>();
            if (Group2 <= 0L)
              substitutes1.GatherRelations(Group1, ref relations5);
            else
              clientGroups.GatherRelations(Group2, ref relations5);
            List<List<long>> longListList1 = Group2 <= 0L ? substitutes1.Items[Group1] : clientGroups.Items[Group2];
            long num7 = -1;
            for (int index7 = 0; index7 < longList5.Count; ++index7)
            {
              long Group3;
              substitutes2.IndexOf(longList5[index7], out Group3, out long _);
              if (Group3 > 0L)
              {
                List<List<long>> longListList2 = substitutes2.Items[Group3];
                if (longListList1.Count == longListList2.Count)
                {
                  List<long> relations6 = new List<long>();
                  substitutes2.GatherRelations(Group3, ref relations6);
                  if (relations6.Count == relations5.Count)
                  {
                    bool flag1 = true;
                    dictionary3.Clear();
                    for (int index8 = 0; index8 < relations5.Count; ++index8)
                    {
                      long key = relations5[index8];
                      List<long> longList6 = dictionary2.ContainsKey(key) ? dictionary2[key] : (List<long>) null;
                      flag1 = flag1 & longList6 != null & longList6.Count > 0;
                      if (flag1)
                      {
                        bool flag2 = false;
                        for (int index9 = 0; index9 < longList6.Count; ++index9)
                        {
                          if (relations6.Contains(longList6[index9]) && !dictionary3.ContainsValue(longList6[index9]))
                          {
                            dictionary3.Add(key, longList6[index9]);
                            flag2 = true;
                            break;
                          }
                        }
                        flag1 &= flag2;
                      }
                      else
                        break;
                    }
                    if (flag1)
                    {
                      num7 = Group3;
                      break;
                    }
                  }
                }
              }
            }
            if (num7 > 0L)
            {
              commonArticles = true;
              for (int index10 = 0; index10 < relations5.Count; ++index10)
              {
                longList1.Add(relations5[index10]);
                long prjLinkID = dictionary3[relations5[index10]];
                longList2.Add(prjLinkID);
                long Group4;
                long SubstInGroup;
                clientGroups.IndexOf(relations5[index10], out Group4, out SubstInGroup);
                Group4 = Group4 < 0L ? 0L : Group4;
                SubstInGroup = SubstInGroup < 0L ? 0L : SubstInGroup;
                if (Group4 > 0L)
                  Group4 = dictionary1[Group4];
                else
                  SubstInGroup = 0L;
                object relationAttribute1 = Group4 > 0L ? clientGroups.RelationAttributes[relations5[index10], sessionById.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545")] : (object) null;
                object relationAttribute2 = Group4 > 0L ? clientGroups.RelationAttributes[relations5[index10], sessionById.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545")] : (object) null;
                object relationAttribute3 = Group4 > 0L ? clientGroups.RelationAttributes[relations5[index10], sessionById.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545")] : (object) null;
                if (adminMode || Group2 != 0L)
                {
                  attributesPackage[prjLinkID, sessionById.IdentHelper.SubstitutesGroupNoID] = (object) Group4;
                  attributesPackage[prjLinkID, sessionById.IdentHelper.SubstituteInGroup] = (object) SubstInGroup;
                  attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545")] = relationAttribute1;
                  attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545")] = relationAttribute2;
                  attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545")] = relationAttribute3;
                }
              }
            }
          }
        }
      }
      for (int index11 = 0; index11 < relations1.Count; ++index11)
      {
        if (!longList1.Contains(relations1[index11]))
        {
          relations5.Clear();
          longList4.Clear();
          long num8 = relations1[index11];
          if (relationsIndex1.ContainsKey(num8))
          {
            DataRow dataRow1 = relationsIndex1[num8];
            long Group5;
            substitutes1.IndexOf(num8, out Group5, out long _);
            long Group6;
            clientGroups.IndexOf(num8, out Group6, out long _);
            if (Group6 < 0L)
              substitutes1.GatherRelations(Group5, ref relations5);
            else
              clientGroups.GatherRelations(Group6, ref relations5);
            Dictionary<long, long> dictionary4 = new Dictionary<long, long>();
            bool flag = true;
            for (int index12 = 0; index12 < relations5.Count; ++index12)
            {
              if (relations5.Contains(relations5[index12]))
              {
                List<long> longList7 = dictionary2.ContainsKey(relations5[index12]) ? dictionary2[num8] : (List<long>) null;
                flag = ((flag ? 1 : 0) & (longList7 == null ? 1 : (longList7.Count == 0 ? 1 : 0))) != 0;
              }
            }
            if (flag)
            {
              for (int index13 = 0; index13 < relations5.Count; ++index13)
                longList1.Add(relations5[index13]);
            }
            else
            {
              for (int index14 = 0; index14 < relations5.Count; ++index14)
              {
                long key1 = relations5[index14];
                List<long> longList8 = dictionary2[key1];
                for (int index15 = 0; index15 < longList8.Count; ++index15)
                {
                  long key2 = longList8[index15];
                  DataRow dataRow2 = relationsIndex2[key2];
                  if (!longList2.Contains(key2))
                  {
                    long result1 = 0;
                    long result2 = 0;
                    if ((long.TryParse(dataRow2[columnIndex1].ToString(), out result1) || dataRow2[columnIndex1] == DBNull.Value) && (long.TryParse(dataRow2[columnIndex2].ToString(), out result2) || dataRow2[columnIndex2] == DBNull.Value) && result1 == 0L && result2 == 0L)
                    {
                      dictionary4.Add(key1, key2);
                      break;
                    }
                  }
                }
              }
              if (dictionary4.Count == relations5.Count)
              {
                commonArticles = true;
                for (int index16 = 0; index16 < relations5.Count; ++index16)
                {
                  longList1.Add(relations5[index16]);
                  if (adminMode)
                  {
                    long prjLinkID = dictionary4[relations5[index16]];
                    longList2.Add(prjLinkID);
                    long Group7;
                    long SubstInGroup;
                    clientGroups.IndexOf(relations5[index16], out Group7, out SubstInGroup);
                    Group7 = Group7 < 0L ? 0L : Group7;
                    SubstInGroup = SubstInGroup < 0L ? 0L : SubstInGroup;
                    if (Group7 > 0L)
                      Group7 = dictionary1[Group7];
                    else
                      SubstInGroup = 0L;
                    object obj1 = Group7 > 0L ? clientGroups.RelationAttributes[relations5[index16], sessionById.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545")] : (object) DBNull.Value;
                    object obj2 = Group7 > 0L ? clientGroups.RelationAttributes[relations5[index16], sessionById.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545")] : (object) DBNull.Value;
                    object obj3 = Group7 > 0L ? clientGroups.RelationAttributes[relations5[index16], sessionById.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545")] : (object) DBNull.Value;
                    attributesPackage[prjLinkID, sessionById.IdentHelper.SubstitutesGroupNoID] = (object) Group7;
                    attributesPackage[prjLinkID, sessionById.IdentHelper.SubstituteInGroup] = (object) SubstInGroup;
                    attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545")] = obj1;
                    attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545")] = obj2;
                    attributesPackage[prjLinkID, sessionById.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545")] = obj3;
                  }
                }
              }
            }
          }
        }
      }
    }
    List<long> longList = new List<long>();
    foreach (KeyValuePair<long, RelationAttributesPackage> keyValuePair in newGroups)
    {
      if ((keyValuePair.Value.Values.Count == 0 || keyValuePair.Value.Attributes.Count == 0) && keyValuePair.Key == projID && spcForm != AVSSpecificationForm.B)
        longList.Add(keyValuePair.Key);
    }
    for (int index = 0; index < longList.Count; ++index)
      newGroups.Remove(longList[index]);
    return commonArticles;
  }

  public virtual long WriteSubstitutesInfo(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    SubstituteObjects substitutes,
    out List<long> chRels)
  {
    chRels = new List<long>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    substitutes.RebuildGroups();
    List<ColumnDescriptor> substitutesColumns = SubstituteObjects.SubstitutesColumns;
    DataTable dataTable = this.LoadComposition(sessionID, filtrationOwnerID, contexts, projID, relationType, substitutesColumns);
    int columnIndex1 = SubstituteObjects.AttrsIndex[-20];
    int columnIndex2 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteGroupNo];
    int columnIndex3 = SubstituteObjects.AttrsIndex[SubstituteObjects.attrSubstituteInGroup];
    RelationAttributesPackage relationAttributes = substitutes.RelationAttributes;
    bool flag = relationAttributes.WriteableAttributes == null || relationAttributes.WriteableAttributes.Count == 0;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long result1 = 0;
      long result2 = 0;
      long result3 = 0;
      if (long.TryParse(row[columnIndex1].ToString(), out result1))
      {
        if (!long.TryParse(row[columnIndex2].ToString(), out result2))
          result2 = 0L;
        if (!long.TryParse(row[columnIndex3].ToString(), out result3))
          result3 = 0L;
        long Group;
        long SubstInGroup;
        substitutes.IndexOf(result1, out Group, out SubstInGroup);
        if (result2 != Group || result2 != 0L || result3 != SubstInGroup || result3 != 0L)
        {
          IDBRelation relation = sessionById.GetRelation(result1, false);
          if (flag || relationAttributes.WriteableAttributes.Contains(SubstituteObjects.attrDesignActualVariant))
          {
            object obj = relationAttributes[result1, SubstituteObjects.attrDesignActualVariant];
            if (Group == 0L)
            {
              obj = (object) DBNull.Value;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
            IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, SubstituteObjects.attrDesignActualVariant, obj);
            if (addAttribute != null && !RelationsComparerHelper.EqualValues(addAttribute.Value, obj, addAttribute.DataType))
            {
              addAttribute.Value = obj;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
          }
          if (flag || relationAttributes.WriteableAttributes.Contains(SubstituteObjects.attrSubstituteGroupName))
          {
            object obj = (object) substitutes.GetSubstGroupName(Group) ?? (object) DBNull.Value;
            if (Group == 0L)
            {
              obj = (object) DBNull.Value;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
            IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, SubstituteObjects.attrSubstituteGroupName, obj);
            if (addAttribute != null && !RelationsComparerHelper.EqualValues(addAttribute.Value, obj, addAttribute.DataType))
            {
              addAttribute.Value = obj;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
          }
          if (flag || relationAttributes.WriteableAttributes.Contains(SubstituteObjects.attrSubstituteName))
          {
            object obj = relationAttributes[result1, SubstituteObjects.attrSubstituteName] ?? (object) DBNull.Value;
            if (Group == 0L)
            {
              obj = (object) DBNull.Value;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
            IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, SubstituteObjects.attrSubstituteName, obj);
            if (addAttribute != null && !RelationsComparerHelper.EqualValues(addAttribute.Value, obj, addAttribute.DataType))
            {
              addAttribute.Value = obj;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
          }
          if (flag || relationAttributes.WriteableAttributes.Contains(SubstituteObjects.attrSubstituteGroupNo))
          {
            object obj = (object) Group;
            if (Group == 0L)
            {
              obj = (object) DBNull.Value;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
            IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, SubstituteObjects.attrSubstituteGroupNo, obj);
            if (addAttribute != null && !RelationsComparerHelper.EqualValues(addAttribute.Value, obj, addAttribute.DataType))
            {
              addAttribute.Value = obj;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
          }
          if (flag || relationAttributes.WriteableAttributes.Contains(SubstituteObjects.attrSubstituteGroupNo))
          {
            object obj = (object) SubstInGroup;
            if (Group == 0L)
            {
              obj = (object) DBNull.Value;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
            IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, SubstituteObjects.attrSubstituteInGroup, obj);
            if (addAttribute != null && !RelationsComparerHelper.EqualValues(addAttribute.Value, obj, addAttribute.DataType))
            {
              addAttribute.Value = obj;
              if (!chRels.Contains(result1))
                chRels.Add(result1);
            }
          }
          object initValue = relationAttributes[result1, Constants.QuantityAttributeTypeID];
          relation.SetAttributesValues(new AttributeValues[1]
          {
            new AttributeValues(Constants.QuantityAttributeTypeID, initValue)
          });
          if (!chRels.Contains(result1))
            chRels.Add(result1);
        }
      }
    }
    return projID;
  }

  protected virtual IDBAttribute TryToAddAttribute(
    IUserSession session,
    IDBRelation relation,
    int attrID,
    object newValue)
  {
    if (session == null || relation == null || attrID < 0)
      return (IDBAttribute) null;
    IDBAttribute attributeById1 = relation.GetAttributeByID(attrID);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
    try
    {
      bool flag = false;
      if (attributeType != null && (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList))
        flag = !(newValue is object[] objArray) || objArray.Length == 0;
      if (((newValue == null ? 1 : (newValue == DBNull.Value ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        if (attributeById1 == null || !((session.GetRelationType(relation.TypeID).Attributes as IDBAttribute4RelationTypeCollection).GetAttributeByID(attrID) is IDBAttributeType4Relation attributeById2))
          return (IDBAttribute) null;
        if (attributeById2.Required == RequiredModes.Manual)
        {
          attributeById1.Delete(0L);
          return (IDBAttribute) null;
        }
      }
      return attributeById1 != null || newValue == null ? attributeById1 : relation.Attributes.AddAttribute(attrID, false);
    }
    catch
    {
      return (IDBAttribute) null;
    }
  }

  public virtual bool WriteRelationAttributesPackage(
    Guid sessionID,
    RelationAttributesPackage package,
    out List<long> chRels)
  {
    bool flag1 = false;
    chRels = new List<long>();
    if (package == null || package.Values.Count == 0 || package.Attributes.Count == 0)
      return flag1;
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    bool flag2 = package.WriteableAttributes != null && package.WriteableAttributes.Count > 0;
    foreach (KeyValuePair<long, object[]> keyValuePair in package.Values)
    {
      IDBRelation relation = sessionById.GetRelation(keyValuePair.Key, false);
      if (!chRels.Contains(keyValuePair.Key))
        chRels.Add(keyValuePair.Key);
      for (int index = 0; index < keyValuePair.Value.Length; ++index)
      {
        if (!flag2 || package.WriteableAttributes.Contains(package.Attributes[index]))
        {
          IDBAttribute addAttribute = this.TryToAddAttribute(sessionById, relation, package.Attributes[index], keyValuePair.Value[index]);
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(package.Attributes[index]);
          if (addAttribute != null && attributeType != null)
          {
            if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
            {
              if (!(keyValuePair.Value[index] is object[] objArray) || objArray.Length == 0)
                addAttribute.ClearValues();
              else
                addAttribute.Values = objArray;
            }
            else if (!RelationsComparerHelper.EqualValues(addAttribute.Value, keyValuePair.Value[index], addAttribute.DataType))
              addAttribute.Value = keyValuePair.Value[index];
          }
        }
      }
    }
    return flag1;
  }

  public bool WriteRelationAttributesPackages(
    Guid sessionID,
    Dictionary<long, RelationAttributesPackage> packages,
    out List<long> chRels)
  {
    chRels = new List<long>();
    if (packages == null || packages.Count == 0)
      return false;
    SubstituteObjects.InitStaticFields(UserSession.GetSessionByID(sessionID));
    foreach (KeyValuePair<long, RelationAttributesPackage> package in packages)
    {
      List<long> chRels1;
      this.WriteRelationAttributesPackage(sessionID, package.Value, out chRels1);
      if (chRels1 != null)
      {
        for (int index = 0; index < chRels1.Count; ++index)
        {
          if (!chRels.Contains(chRels1[index]))
            chRels.Add(chRels1[index]);
        }
      }
    }
    chRels.Sort();
    return chRels.Count > 0;
  }

  private bool SubstituteGroupApplicabilities(
    IUserSession session,
    SubstituteObjects substs,
    long groupNo,
    ref RelationAttributesPackage result)
  {
    RelationAttributesPackage relationAttributes = substs?.RelationAttributes;
    List<int> attributes = new List<int>();
    if (result == null)
      result = new RelationAttributesPackage(attributes);
    if (session == null || substs == null || substs.Count == 0 || groupNo <= 0L || relationAttributes == null || relationAttributes.Values.Count == 0)
      return false;
    List<List<long>> subst = substs[groupNo];
    List<long> relations = new List<long>();
    substs.GatherRelations(groupNo, ref relations);
    if (subst == null || subst.Count < 2 || relations.Count == 0 || relations.Count > relationAttributes.Values.Count)
      return false;
    ObjectsApplicabilitiesCriterionsCollection criterionsCollection = (ObjectsApplicabilitiesCriterionsCollection) null;
    object[] objArray = (object[]) null;
    for (int index1 = 0; index1 < subst.Count; ++index1)
    {
      int index2 = index1;
      List<long> longList = subst[index2];
      int count = longList.Count;
      for (int index3 = 0; index3 < count; ++index3)
      {
        long num = longList[index3];
        if (criterionsCollection == null)
        {
          IDBRelation relation = session.GetRelation(num, false);
          criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection();
          criterionsCollection.LoadFromObject((IDBAttributable) relation);
          objArray = criterionsCollection.ToAttributeValues(SubstituteObjects.attrApplicabilities);
        }
        result[num, SubstituteObjects.attrApplicabilities] = (object) objArray;
      }
    }
    return true;
  }

  private bool SubstituteApplicabilities(
    IUserSession session,
    SubstituteObjects substs,
    ref RelationAttributesPackage result)
  {
    if (session == null || substs == null || substs.Count == 0)
      return false;
    List<long> groups = substs.Groups;
    for (int index = 0; index < groups.Count; ++index)
      this.SubstituteGroupApplicabilities(session, substs, groups[index], ref result);
    return true;
  }

  public virtual bool CorrectConfiguratorApplicabilities(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    long groupNo,
    ref List<long> chRels)
  {
    if (chRels == null)
      chRels = new List<long>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    RelationAttributesPackage result = new RelationAttributesPackage(new List<int>()
    {
      MetaDataHelper.GetAttributeTypeID("cad015ac-306c-11d8-b4e9-00304f19f545")
    });
    SubstituteObjects substs = this.LoadSubstitutes(sessionID, filtrationOwnerID, contexts, projID, relationType);
    this.SubstituteGroupApplicabilities(sessionById, substs, groupNo, ref result);
    RelationAttributesPackage relationAttributes = substs.RelationAttributes;
    List<long> prjLinkIds = new List<long>();
    foreach (KeyValuePair<long, object[]> keyValuePair in relationAttributes.Values)
    {
      object obj = relationAttributes[keyValuePair.Key, SubstituteObjects.attrSubstituteGroupNo];
      if (obj == null || obj == DBNull.Value)
        prjLinkIds.Add(keyValuePair.Key);
      else if (Convert.ToInt64(obj) != groupNo)
        prjLinkIds.Add(keyValuePair.Key);
    }
    result.Remove(prjLinkIds);
    List<long> chRels1;
    this.WriteRelationAttributesPackage(sessionID, result, out chRels1);
    if (chRels1 != null)
    {
      for (int index = 0; index < chRels1.Count; ++index)
      {
        if (!chRels.Contains(chRels1[index]))
          chRels.Add(chRels1[index]);
      }
    }
    return true;
  }

  public virtual bool CorrectConfiguratorApplicabilities(
    Guid sessionID,
    string filtrationOwnerID,
    List<long> contexts,
    long projID,
    int relationType,
    ref List<long> chRels)
  {
    if (chRels == null)
      chRels = new List<long>();
    IUserSession sessionById = UserSession.GetSessionByID(sessionID);
    SubstituteObjects.InitStaticFields(sessionById);
    List<int> intList = new List<int>();
    intList.Add(MetaDataHelper.GetAttributeTypeID("cad015ac-306c-11d8-b4e9-00304f19f545"));
    RelationAttributesPackage result = new RelationAttributesPackage(intList, intList);
    SubstituteObjects substs = this.LoadSubstitutes(sessionID, filtrationOwnerID, contexts, projID, relationType);
    this.SubstituteApplicabilities(sessionById, substs, ref result);
    List<long> chRels1;
    this.WriteRelationAttributesPackage(sessionID, result, out chRels1);
    if (chRels1 != null)
    {
      for (int index = 0; index < chRels1.Count; ++index)
      {
        if (!chRels.Contains(chRels1[index]))
          chRels.Add(chRels1[index]);
      }
    }
    return true;
  }
}
