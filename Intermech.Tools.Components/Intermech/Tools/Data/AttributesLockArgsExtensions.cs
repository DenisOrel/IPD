// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.AttributesLockArgsExtensions
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

public static class AttributesLockArgsExtensions
{
  private static readonly object GetIntegratorDocumentTypesByArticleIdCacheKey = new object();
  private static readonly object DoesArticleHaveInstancesCacheKey = new object();

  public static IReadOnlyList<int> GetIntegratorDocumentTypesByArticleId(
    this AttributesLockArgs args)
  {
    if (args == null)
      throw new ArgumentNullException(nameof (args));
    object typesByArticleId1;
    if (args.Tags.TryGetValue(AttributesLockArgsExtensions.GetIntegratorDocumentTypesByArticleIdCacheKey, out typesByArticleId1))
      return (IReadOnlyList<int>) typesByArticleId1;
    IReadOnlyList<int> typesByArticleId2 = args.ElementKind == AttributableElements.Object ? AttributesLockArgsExtensions.GetIntegratorDocumentTypesByArticleIdSlow(args.ElementId) : (IReadOnlyList<int>) new int[0];
    args.Tags[AttributesLockArgsExtensions.GetIntegratorDocumentTypesByArticleIdCacheKey] = (object) typesByArticleId2;
    return typesByArticleId2;
  }

  private static IReadOnlyList<int> GetIntegratorDocumentTypesByArticleIdSlow(long articleId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
      relationCollection.ObjectTypeID = IDCache.Default.AllDocuments.Id;
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams()
      {
        RecordCount = -1,
        Columns = new object[1]{ (object) -7 },
        Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true)
        }
      }, articleId);
      List<int> list = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32 = Convert.ToInt32(row[0]);
        CollectionUtils.AddSorted<int>(list, int32);
      }
      return (IReadOnlyList<int>) list;
    }
  }

  public static bool DoesArticleHaveInstances(this AttributesLockArgs args)
  {
    if (args == null)
      throw new ArgumentNullException(nameof (args));
    object obj;
    if (args.Tags.TryGetValue(AttributesLockArgsExtensions.DoesArticleHaveInstancesCacheKey, out obj))
      return (bool) obj;
    bool flag = args.ElementKind == AttributableElements.Object && AttributesLockArgsExtensions.DoesArticleHaveInstancesSlow(args.ElementId);
    args.Tags[AttributesLockArgsExtensions.DoesArticleHaveInstancesCacheKey] = (object) flag;
    return flag;
  }

  private static bool DoesArticleHaveInstancesSlow(long articleID)
  {
    IArticleService service = ServiceUtils.GetService<IArticleService>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (service.GetListInstances(articleID, (object) sessionKeeper.Session).Count > 1)
          return true;
      }
    }
    return false;
  }
}
