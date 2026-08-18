// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.ExternalKeyArticleLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Реализует поиск изделий в применяемости документа по внешним ключам изделий, хранимым в файле документа.
/// </summary>
public sealed class ExternalKeyArticleLocator : IObjectLocator
{
  private IExternalKeyLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них идентификатор версии документа и внешний ключ изделия</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public ExternalKeyArticleLocator(IExternalKeyLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект изделия в базе IPS.</summary>
  /// <returns>Описатель найденного изделия в базе IPS или null, если изделие не было найдено</returns>
  public ObjectLocatorResult LocateObject()
  {
    string externalKey = this.dataDecoder.GetExternalKey();
    if (string.IsNullOrEmpty(externalKey))
      return (ObjectLocatorResult) null;
    long documentId = this.dataDecoder.GetDocumentId();
    if (documentId == 0L)
      return (ObjectLocatorResult) null;
    Tuple<long, int> article = this.FindArticle(documentId, externalKey, VersionsRuleSources.GetEditorRule());
    return article == null ? (ObjectLocatorResult) null : new ObjectLocatorResult(article.Item1, article.Item2);
  }

  private Tuple<long, int> FindArticle(
    long modelObjectId,
    string externalKey,
    VersionsRulePackage versionsRule)
  {
    ConditionStructure conditionStructure = new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.Equal, (object) externalKey, LogicalOperators.NOT, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    paramSet.ColumnsInfo = new ColumnInfo[2]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, (object) null)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = versionsRule.OwnerId;
      relationCollection.ObjectTypeID = IDCache.Default.AllArticles.Id;
      dataTable = relationCollection.EntersInVersion(paramSet, modelObjectId);
    }
    return dataTable.Rows.Count <= 0 ? (Tuple<long, int>) null : Tuple.Create<long, int>(Convert.ToInt64(dataTable.Rows[0][0]), Convert.ToInt32(dataTable.Rows[0][1]));
  }
}
