// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartGuidArticleLocator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class PartGuidArticleLocator : IObjectLocator
{
  private readonly IPartGuidArticleLocatorData data;
  private readonly VersionsRulePackage versionsRule;

  public PartGuidArticleLocator(IPartGuidArticleLocatorData data)
  {
    this.data = data != null ? data : throw new ArgumentNullException(nameof (data));
    this.versionsRule = VersionsRuleSources.GetEditorRule();
  }

  public IPartGuidArticleLocatorData Data
  {
    [DebuggerStepThrough] get => this.data;
  }

  public VersionsRulePackage VersionsRule
  {
    [DebuggerStepThrough] get => this.versionsRule;
  }

  public ObjectLocatorResult LocateObject()
  {
    Guid partGuid = this.data.GetPartGuid();
    if (partGuid == Guid.Empty)
      return (ObjectLocatorResult) null;
    ConditionStructure conditionStructure = new ConditionStructure(IDCache.Default.OccurenceKey.Id, RelationalOperators.Equal, (object) partGuid, LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleTree.Id);
      relationCollection.FiltrationOwnerID = this.versionsRule.OwnerId;
      dataTable = relationCollection.Select(paramSet);
    }
    if (dataTable.Rows.Count == 0)
      return (ObjectLocatorResult) null;
    DataRow row = dataTable.Rows[0];
    return new ObjectLocatorResult(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]));
  }
}
