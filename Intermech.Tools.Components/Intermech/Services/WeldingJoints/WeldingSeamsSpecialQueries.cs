// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamsSpecialQueries
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class WeldingSeamsSpecialQueries : IWeldingSeamsSpecialQueries
{
  private IWeldingSeamsModelRoot modelRoot;
  private WeldingSeamsIDCache idCache;

  public WeldingSeamsSpecialQueries(IWeldingSeamsModelRoot modelRoot, WeldingSeamsIDCache idCache)
  {
    if (modelRoot == null)
      throw new ArgumentNullException(nameof (modelRoot));
    if (idCache == null)
      throw new ArgumentNullException(nameof (idCache));
    this.modelRoot = modelRoot;
    this.idCache = idCache;
  }

  public List<MechanicalArticleEntity> LoadLinkedArticles(
    MechanicalDocumentEntity document,
    VersionsRulePackage versionsRule)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    DataTable documentArticles = DBDocumentHelper.FindDocumentArticles(document.ObjectId, versionsRule, true);
    List<MechanicalArticleEntity> mechanicalArticleEntityList = new List<MechanicalArticleEntity>(documentArticles.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) documentArticles.Rows)
    {
      MechanicalArticleEntity entity = this.modelRoot.Articles.Load((object) Convert.ToInt64(row[1]));
      this.modelRoot.Articles.LoadReferences<MechanicalDocumentOccurence>(entity, (Expression<System.Func<MechanicalArticleEntity, MechanicalDocumentOccurence>>) (e => e.DocumentOccurence));
      mechanicalArticleEntityList.Add(entity);
    }
    return mechanicalArticleEntityList;
  }

  public WeldingSeamEntity LoadWeldingSeamByExternalKey(
    string externalKey,
    VersionsRulePackage versionsRule,
    bool throwIfNotFound)
  {
    if (externalKey == null)
      throw new ArgumentNullException(nameof (externalKey));
    long key = versionsRule != null ? this.FindWeldingSeamObjectId(externalKey, versionsRule) : throw new ArgumentNullException(nameof (versionsRule));
    if (key != 0L)
      return this.modelRoot.WeldingSeams.Load((object) key);
    if (!throwIfNotFound)
      return (WeldingSeamEntity) null;
    throw new KernelException("Не удалось найти сварной шов с указанным внешним ключем.");
  }

  private long FindWeldingSeamObjectId(string externalKey, VersionsRulePackage versionsRule)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.Conditions = new ConditionStructure[2]
    {
      new ConditionStructure(this.idCache.ExternalKey.Id, RelationalOperators.Equal, (object) externalKey, LogicalOperators.AND, 0, true),
      new ConditionStructure(this.idCache.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
    };
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_ID
    };
    paramSet.RecordCount = 2;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(this.idCache.WeldingSeams.Id).Select(paramSet);
      if (dataTable.Rows.Count == 0)
        return 0;
      if (dataTable.Rows.Count == 1)
        return Convert.ToInt64(dataTable.Rows[0][0]);
      long int64 = Convert.ToInt64(dataTable.Rows[0][1]);
      return sessionKeeper.Session.GetObjectByVersionsRule(int64, versionsRule.OwnerId, true).ObjectID;
    }
  }

  public MechanicalArticleEntity LoadWeldingSeamComponentByExternalKeys(
    long documentId,
    string externalKey,
    bool throwIfNotFound)
  {
    if (documentId == 0L)
      throw new ArgumentException("Не задан идентификатор документа.", nameof (documentId));
    if (string.IsNullOrEmpty(externalKey))
      throw new ArgumentException("Не задан внешний ключ компонента.", nameof (externalKey));
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.Conditions = new ConditionStructure[2]
    {
      new ConditionStructure(this.idCache.ExternalKey.Id, RelationalOperators.Equal, (object) externalKey, LogicalOperators.AND, 0, true),
      new ConditionStructure(this.idCache.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetRelationCollection(this.idCache.ArticleDocumentsLink.Id).EntersInVersion(paramSet, documentId);
      if (dataTable.Rows.Count != 0)
        return this.modelRoot.Articles.Load((object) Convert.ToInt64(dataTable.Rows[0][0]));
    }
    if (!throwIfNotFound)
      return (MechanicalArticleEntity) null;
    throw new KernelException("Не удалось найти компонент сварного шва с указанным внешним ключем.");
  }
}
