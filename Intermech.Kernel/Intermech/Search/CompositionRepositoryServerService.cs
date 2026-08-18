// Decompiled with JetBrains decompiler
// Type: Intermech.Search.CompositionRepositoryServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Data;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search;

public sealed class CompositionRepositoryServerService : 
  LongLifeObject,
  ICompositionRepositoryServerService
{
  private LazyService<IObjectTypeApplicabilityRepository> _objectTypeApplicablityRepository = new LazyService<IObjectTypeApplicabilityRepository>();
  private LazyService<IAttributeValueConverter> _attributeValueConverter = new LazyService<IAttributeValueConverter>();

  public CompositionPart[] FindComposition(
    Guid userSessionGuid,
    FindCompositionParams findCompositionParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (findCompositionParams == null)
        throw new ArgumentNullException(nameof (findCompositionParams));
      return FindCompositionParams.Check(findCompositionParams) ? this.FindCompositionInternal(findCompositionParams) : throw new ArgumentException();
    }
  }

  public CompositionPart[] FindRecursiveComposition(
    Guid userSessionGuid,
    FindCompositionParams findCompositionParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (findCompositionParams == null)
        throw new ArgumentNullException(nameof (findCompositionParams));
      return FindCompositionParams.Check(findCompositionParams) ? this.FindRecursiveCompositionInternal(findCompositionParams) : throw new ArgumentException();
    }
  }

  private CompositionPart[] FindCompositionInternal(FindCompositionParams findCompositionParams)
  {
    return this.FindCompositionInternal(this.GetProjectVersionIds(findCompositionParams).ToArray(), findCompositionParams);
  }

  private CompositionPart[] FindCompositionInternal(
    long[] projectVersionIds,
    FindCompositionParams findCompositionParams)
  {
    List<CompositionPart> compositionPartList = new List<CompositionPart>();
    foreach (long projectVersionId in projectVersionIds)
    {
      foreach (KeyValuePair<int, List<int>> byRelationTypeId in this.GetPartTypeIdsByRelationTypeIDDictionary(projectVersionId, findCompositionParams))
      {
        int key = byRelationTypeId.Key;
        List<int> source = byRelationTypeId.Value;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = !RelationTypeHelper.IsUnknownRelationTypeID(key) ? sessionKeeper.Session.GetRelationCollection(key) : sessionKeeper.Session.GetRelationCollection(-1);
          if (source != null && source.Count != 0 && (source.Count != 1 || !ObjectTypeHelper.IsUnknownObjectTypeID(source[0])) && !findCompositionParams.AllPartTypes)
            relationCollection.ChildObjectTypes = (IList<int>) source.Distinct<int>().ToList<int>();
          if (findCompositionParams.LocalTypesMode)
            relationCollection.LocalTypesMode = true;
          if (!string.IsNullOrEmpty(findCompositionParams.FiltrationOwnerID))
            relationCollection.FiltrationOwnerID = findCompositionParams.FiltrationOwnerID;
          DBRecordSetParams recordSetParams = this.GetRecordSetParams(projectVersionId, key, findCompositionParams);
          DataTable dataTable = relationCollection.Select(recordSetParams, projectVersionId, -1L, DateTime.UtcNow + sessionKeeper.Session.TimeZoneOffset);
          compositionPartList.AddRange(this.CreateCompositionPartsFromDataTable(dataTable, recordSetParams));
        }
      }
    }
    return compositionPartList.ToArray();
  }

  private CompositionPart[] FindRecursiveCompositionInternal(
    FindCompositionParams findCompositionParams)
  {
    List<CompositionPart> compositionPartList = new List<CompositionPart>();
    List<long> preparedProjectVersionIds = new List<long>();
    if (findCompositionParams.ProjectVersionIds != null)
      preparedProjectVersionIds.AddRange((IEnumerable<long>) findCompositionParams.ProjectVersionIds);
    else
      preparedProjectVersionIds.Add(findCompositionParams.ProjectVersionID);
    CompositionPart[] compositionInternal1 = this.FindCompositionInternal(findCompositionParams);
    compositionPartList.AddRange((IEnumerable<CompositionPart>) compositionInternal1);
    long[] array = ((IEnumerable<CompositionPart>) compositionInternal1).Select<CompositionPart, long>((System.Func<CompositionPart, long>) (o => o.Object.VersionID)).Distinct<long>().ToArray<long>();
    preparedProjectVersionIds.AddRange((IEnumerable<long>) array);
    while (array.Length != 0)
    {
      CompositionPart[] compositionInternal2 = this.FindCompositionInternal(array, findCompositionParams);
      compositionPartList.AddRange((IEnumerable<CompositionPart>) compositionInternal2);
      array = ((IEnumerable<CompositionPart>) compositionInternal2).Select<CompositionPart, long>((System.Func<CompositionPart, long>) (o => o.Object.VersionID)).Where<long>((System.Func<long, bool>) (o => !preparedProjectVersionIds.Contains(o))).Distinct<long>().ToArray<long>();
      preparedProjectVersionIds.AddRange((IEnumerable<long>) array);
    }
    return compositionPartList.ToArray();
  }

  private List<long> GetProjectVersionIds(FindCompositionParams findCompositionParams)
  {
    if (findCompositionParams.ProjectVersionIds != null && findCompositionParams.ProjectVersionIds.Length != 0)
      return ((IEnumerable<long>) findCompositionParams.ProjectVersionIds).Distinct<long>().ToList<long>();
    return new List<long>()
    {
      findCompositionParams.ProjectVersionID
    };
  }

  private Dictionary<int, List<int>> GetPartTypeIdsByRelationTypeIDDictionary(
    long projectVersionID,
    FindCompositionParams findCompositionParams)
  {
    Dictionary<int, List<int>> typeIdDictionary = new Dictionary<int, List<int>>();
    int[] collection = (int[]) null;
    if (findCompositionParams.PartTypeIds != null && findCompositionParams.PartTypeIds.Length != 0)
      collection = ((IEnumerable<int>) findCompositionParams.PartTypeIds).Distinct<int>().ToArray<int>();
    else if (!ObjectTypeHelper.IsUnknownObjectTypeID(findCompositionParams.PartTypeID))
      collection = new int[1]
      {
        findCompositionParams.PartTypeID
      };
    if (findCompositionParams.AllRelations)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (IMSApplicability imsApplicability in this._objectTypeApplicablityRepository.Value.Find(sessionKeeper.Session.GetObject(projectVersionID).ObjectType))
        {
          List<int> intList = (List<int>) null;
          if (!typeIdDictionary.TryGetValue(imsApplicability.RelationTypeID, out intList))
          {
            intList = new List<int>();
            typeIdDictionary.Add(imsApplicability.RelationTypeID, intList);
          }
          if (collection != null)
            intList.AddRange((IEnumerable<int>) collection);
          else if (!intList.Contains(imsApplicability.ChildObjectTypeID))
            intList.Add(imsApplicability.ChildObjectTypeID);
        }
      }
    }
    else if (findCompositionParams.PartTypeIdsByRelationTypeIDDictionary != null && findCompositionParams.PartTypeIdsByRelationTypeIDDictionary.Count != 0)
    {
      foreach (KeyValuePair<int, int[]> byRelationTypeId in findCompositionParams.PartTypeIdsByRelationTypeIDDictionary)
        typeIdDictionary.Add(byRelationTypeId.Key, byRelationTypeId.Value != null ? ((IEnumerable<int>) byRelationTypeId.Value).ToList<int>() : (List<int>) null);
    }
    else if (findCompositionParams.PartTypeIds != null && findCompositionParams.PartTypeIds.Length != 0)
      typeIdDictionary.Add(findCompositionParams.RelationTypeID, ((IEnumerable<int>) findCompositionParams.PartTypeIds).ToList<int>());
    else
      typeIdDictionary.Add(findCompositionParams.RelationTypeID, new List<int>()
      {
        findCompositionParams.PartTypeID
      });
    return typeIdDictionary;
  }

  private DBRecordSetParams GetRecordSetParams(
    long projectVersionID,
    int relationTypeID,
    FindCompositionParams findCompositionParams)
  {
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    List<int> intList1 = new List<int>()
    {
      -20,
      -21,
      -23,
      -22
    };
    if (findCompositionParams.RelationAttributeTypeIds != null)
    {
      foreach (int relationAttributeTypeId in findCompositionParams.RelationAttributeTypeIds)
      {
        if (!intList1.Contains(relationAttributeTypeId))
          intList1.Add(relationAttributeTypeId);
      }
    }
    foreach (int num in intList1)
    {
      ColumnDescriptor columnDescriptor = new ColumnDescriptor()
      {
        AttributeID = (object) num,
        AttributeSource = AttributeSourceTypes.Relation
      };
      columnDescriptorList.Add(columnDescriptor);
    }
    List<int> intList2 = new List<int>() { -2, -3, -7 };
    if (findCompositionParams.ObjectAttributeTypeIds != null)
    {
      foreach (int objectAttributeTypeId in findCompositionParams.ObjectAttributeTypeIds)
      {
        if (!intList2.Contains(objectAttributeTypeId))
          intList2.Add(objectAttributeTypeId);
      }
    }
    foreach (int num in intList2)
    {
      ColumnDescriptor columnDescriptor = new ColumnDescriptor()
      {
        AttributeID = (object) num,
        AttributeSource = AttributeSourceTypes.Object
      };
      columnDescriptorList.Add(columnDescriptor);
    }
    return new DBRecordSetParams(findCompositionParams.Conditions, columnDescriptorList.ToArray());
  }

  private IEnumerable<CompositionPart> CreateCompositionPartsFromDataTable(
    DataTable dataTable,
    DBRecordSetParams recordSetParams)
  {
    List<CompositionPart> partsFromDataTable = new List<CompositionPart>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      CompositionPart compositionPart = new CompositionPart();
      for (int columnIndex = 0; columnIndex < recordSetParams.ColumnsInfo.Length; ++columnIndex)
      {
        Intermech.Kernel.Search.ColumnInfo columnInfo = recordSetParams.ColumnsInfo[columnIndex];
        object obj = this._attributeValueConverter.Value.Convert(row[columnIndex], (int) columnInfo.AttributeID);
        _Attribute attribute = new _Attribute((int) columnInfo.AttributeID, obj);
        if (columnInfo.AttributeSource == AttributeSourceTypes.Object)
          compositionPart.Object.Attributes.Add(attribute);
        else if (columnInfo.AttributeSource == AttributeSourceTypes.Relation)
          compositionPart.Relation.Attributes.Add(attribute);
      }
      partsFromDataTable.Add(compositionPart);
    }
    return (IEnumerable<CompositionPart>) partsFromDataTable;
  }
}
