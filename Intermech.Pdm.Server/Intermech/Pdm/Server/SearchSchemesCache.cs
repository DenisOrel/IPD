// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.SearchSchemesCache
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Intermech.Pdm.Server;

internal class SearchSchemesCache
{
  private readonly int objTypePersonalShemeID = -1;
  private readonly int objTypeOwnShemeID = -1;
  private readonly IUserSession _session;

  public HybridDictionary SchemesForRelationTypes { get; } = new HybridDictionary();

  public SearchSchemesCache()
  {
    this._session = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionPermanentClone(nameof (SearchSchemesCache));
    this._session.ShowPersonalObjects = true;
    this.objTypePersonalShemeID = this._session.GetObjectType(new Guid("cad0012b-306c-11d8-b4e9-00304f19f545")).ObjectType;
    this.objTypeOwnShemeID = this._session.GetObjectType(new Guid("cad0012a-306c-11d8-b4e9-00304f19f545")).ObjectType;
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AfterCacheReload += new CacheReloadHandler(this.EHelper_AfterCacheReload);
    this.Reload();
  }

  private void EHelper_AfterCacheReload(IDbManager db) => this.Reload();

  private void Reload()
  {
    lock (this.SchemesForRelationTypes)
    {
      if (this.SchemesForRelationTypes.Count > 0)
        this.SchemesForRelationTypes.Clear();
      this.AddSchemesToCache(this._session.GetObjectCollection(new Guid("cad0012a-306c-11d8-b4e9-00304f19f545")));
      this.AddSchemesToCache(this._session.GetObjectCollection(new Guid("cad0012b-306c-11d8-b4e9-00304f19f545")));
    }
  }

  private void AddSchemesToCache(IDBObjectCollection objColl)
  {
    DataTable dataTable = objColl.Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    List<long> longList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    foreach (IDBObject scheme in this._session.GetObjects(longList.ToArray(), false))
      this.AddObject(scheme);
  }

  private void AddNewSchemeValue(Guid relationType, SearchSchemesCache.Scheme schemeID)
  {
    if (this.SchemesForRelationTypes[(object) relationType] == null)
    {
      List<SearchSchemesCache.Scheme> schemeList = new List<SearchSchemesCache.Scheme>()
      {
        schemeID
      };
      this.SchemesForRelationTypes.Add((object) relationType, (object) schemeList);
    }
    else if (!(this.SchemesForRelationTypes[(object) relationType] is List<SearchSchemesCache.Scheme> schemesForRelationType))
    {
      List<SearchSchemesCache.Scheme> schemeList = new List<SearchSchemesCache.Scheme>()
      {
        schemeID
      };
      this.SchemesForRelationTypes[(object) relationType] = (object) schemeList;
    }
    else
    {
      SearchSchemesCache.Scheme scheme = schemesForRelationType.Find((Predicate<SearchSchemesCache.Scheme>) (_ => _.SchemeID.Equals(schemeID.SchemeID)));
      if (scheme == null)
      {
        schemesForRelationType.Add(schemeID);
      }
      else
      {
        if (CompareValuesHelper.CompareCollections<long>((ICollection<long>) scheme.Roles, (ICollection<long>) schemeID.Roles))
          return;
        scheme.Roles = schemeID.Roles;
      }
    }
  }

  public List<long> GetSchemesForRelationTypes(IUserSession session, List<Guid> relationTypes)
  {
    return this.GetSchemesForRelationTypes(session, relationTypes, ContainsMode.None);
  }

  public List<long> GetSchemesForRelationTypes(
    IUserSession session,
    List<Guid> relationTypes,
    ContainsMode mode)
  {
    return this.GetSchemesForRelationTypesEx(session, relationTypes, mode, false)?.ConvertAll<long>((Converter<SearchSchemaInfo, long>) (_ => _.SchemeID));
  }

  private List<SearchSchemaInfo> GetSchemesForRelationType(
    Guid relationTypeGuid,
    ContainsMode mode,
    long userID)
  {
    if (!(this.SchemesForRelationTypes[(object) relationTypeGuid] is List<SearchSchemesCache.Scheme> schemesForRelationType))
      return (List<SearchSchemaInfo>) null;
    IQueryable<SearchSchemesCache.Scheme> source = schemesForRelationType.AsQueryable<SearchSchemesCache.Scheme>();
    if (mode != ContainsMode.None)
      source = source.Where<SearchSchemesCache.Scheme>((Expression<System.Func<SearchSchemesCache.Scheme, bool>>) (_ => _.Mode.Equals((object) mode)));
    return source.Where<SearchSchemesCache.Scheme>((Expression<System.Func<SearchSchemesCache.Scheme, bool>>) (_ => !_.Personal || _.Personal && _.OwnerID.Equals(userID))).ToList<SearchSchemesCache.Scheme>().ConvertAll<SearchSchemaInfo>((Converter<SearchSchemesCache.Scheme, SearchSchemaInfo>) (_ => new SearchSchemaInfo(_.Name, _.SchemeID, _.Roles)));
  }

  private void AddSchemesToResult(List<SearchSchemaInfo> result, List<SearchSchemaInfo> schemes)
  {
    if (schemes == null || schemes.Count <= 0)
      return;
    result.AddRange((IEnumerable<SearchSchemaInfo>) schemes.Where<SearchSchemaInfo>((System.Func<SearchSchemaInfo, bool>) (_ => !result.Contains(_))).ToList<SearchSchemaInfo>());
  }

  public List<SearchSchemaInfo> GetSchemesForRelationTypesEx(
    IUserSession session,
    List<Guid> relationTypes,
    ContainsMode mode,
    bool roleFiltration)
  {
    List<SearchSchemaInfo> searchSchemaInfoList = new List<SearchSchemaInfo>();
    foreach (Guid relationType in relationTypes)
      this.AddSchemesToResult(searchSchemaInfoList, this.GetSchemesForRelationType(relationType, mode, session.UserID));
    this.AddSchemesToResult(searchSchemaInfoList, this.GetSchemesForRelationType(Guid.Empty, mode, session.UserID));
    if (searchSchemaInfoList.Count > 0 & roleFiltration)
      searchSchemaInfoList = searchSchemaInfoList.Where<SearchSchemaInfo>((System.Func<SearchSchemaInfo, bool>) (_ => _.Roles.Count == 0 || _.Roles.Contains(session.RoleID))).ToList<SearchSchemaInfo>();
    return searchSchemaInfoList.Count <= 0 ? (List<SearchSchemaInfo>) null : searchSchemaInfoList;
  }

  public void AddScheme(IUserSession session, long schemeID)
  {
    IDBObject scheme = session.GetObject(schemeID, false);
    if (scheme == null)
      return;
    lock (this.SchemesForRelationTypes)
      this.AddObject(scheme);
  }

  public void DeleteScheme(IUserSession session, long schemeID)
  {
    lock (this.SchemesForRelationTypes)
    {
      IDictionaryEnumerator enumerator1 = this.SchemesForRelationTypes.GetEnumerator();
      List<Guid> guidList = new List<Guid>();
      HybridDictionary hybridDictionary = new HybridDictionary();
      while (enumerator1.MoveNext())
      {
        if (enumerator1.Value is List<SearchSchemesCache.Scheme> schemeList1)
        {
          bool flag = false;
          foreach (SearchSchemesCache.Scheme scheme in schemeList1)
          {
            if (scheme.SchemeID == schemeID)
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            List<SearchSchemesCache.Scheme> schemeList = new List<SearchSchemesCache.Scheme>();
            foreach (SearchSchemesCache.Scheme scheme in schemeList1)
            {
              if (scheme.SchemeID != schemeID)
                schemeList.Add(scheme);
            }
            hybridDictionary.Add(enumerator1.Key, (object) schemeList);
            if (schemeList.Count == 0)
              guidList.Add((Guid) enumerator1.Key);
          }
        }
      }
      IDictionaryEnumerator enumerator2 = hybridDictionary.GetEnumerator();
      while (enumerator2.MoveNext())
        this.SchemesForRelationTypes[enumerator2.Key] = enumerator2.Value;
      foreach (Guid key in guidList)
        this.SchemesForRelationTypes.Remove((object) key);
    }
  }

  private void AddObject(IDBObject scheme)
  {
    ContainsMode mode = ContainsMode.None;
    IDBAttribute attributeByGuid1 = scheme.GetAttributeByGuid(new Guid("cad00131-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 != null)
    {
      int num;
      switch ((SearchDirection) Convert.ToInt32(attributeByGuid1.Value))
      {
        case SearchDirection.Contains:
        case SearchDirection.RecursiveContains:
          num = 1;
          break;
        default:
          num = 2;
          break;
      }
      mode = (ContainsMode) num;
    }
    IDBAttribute attributeByGuid2 = scheme.GetAttributeByGuid(new Guid("cad00d18-306c-11d8-b4e9-00304f19f545"));
    List<long> roles = new List<long>();
    if (attributeByGuid2 != null && !attributeByGuid2.IsNull && attributeByGuid2.ValuesCount > 0)
    {
      for (int index = 0; index < attributeByGuid2.ValuesCount; ++index)
      {
        attributeByGuid2.Index = index;
        if (GuidHelper.IsGuid(attributeByGuid2.AsString))
        {
          QuickObjectInfo objectInfo = this._session.GetObjectInfo(new Guid(attributeByGuid2.AsString));
          if (!objectInfo.Empty && !roles.Contains(objectInfo.ObjectID))
            roles.Add(objectInfo.ObjectID);
        }
      }
    }
    IDBAttribute attributeByGuid3 = scheme.GetAttributeByGuid(new Guid("cad0014a-306c-11d8-b4e9-00304f19f545"));
    bool flag = false;
    if (attributeByGuid3.Values != null)
    {
      foreach (object obj in attributeByGuid3.Values)
      {
        if (obj != null && GuidHelper.IsGuid(obj.ToString()))
        {
          this.AddNewSchemeValue(new Guid(obj.ToString()), new SearchSchemesCache.Scheme(Math.Abs(scheme.ObjectID), scheme.Caption, mode, scheme.OwnerID, scheme.ObjectType == this.objTypePersonalShemeID, roles));
          flag = true;
        }
      }
    }
    if (flag)
      return;
    this.AddNewSchemeValue(Guid.Empty, new SearchSchemesCache.Scheme(Math.Abs(scheme.ObjectID), scheme.Caption, mode, scheme.OwnerID, scheme.ObjectType == this.objTypePersonalShemeID, roles));
  }

  private class Scheme
  {
    public long SchemeID;
    public ContainsMode Mode;
    public long OwnerID;
    public bool Personal;

    public string Name { get; private set; }

    public List<long> Roles { get; set; }

    public Scheme(
      long schemeID,
      string name,
      ContainsMode mode,
      long ownerID,
      bool personal,
      List<long> roles)
    {
      this.SchemeID = schemeID;
      this.Name = name;
      this.Mode = mode;
      this.OwnerID = ownerID;
      this.Personal = personal;
      this.Roles = roles;
    }
  }
}
