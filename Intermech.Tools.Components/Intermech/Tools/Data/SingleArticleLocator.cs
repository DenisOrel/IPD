// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SingleArticleLocator
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
/// Реализует алгоритм поиска изделия, связанного с документом, путем подъема от документа по связи
/// типа "Документация на изделие". Поиск оказывается успешным, если документ связан только с
/// одним единственным изделием.
/// </summary>
public sealed class SingleArticleLocator : IObjectLocator
{
  private ISingleArticleLocatorData dataDecoder;

  /// <summary>Создает объекта.</summary>
  /// <param name="dataDecoder">Декодер исходных данных</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public SingleArticleLocator(ISingleArticleLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект изделия в базе IPS.</summary>
  /// <returns>Описатель найденного изделия в базе IPS или null, если изделие не было найдено</returns>
  public ObjectLocatorResult LocateObject()
  {
    long documentId = this.dataDecoder.GetDocumentId();
    if (documentId == 0L)
      return (ObjectLocatorResult) null;
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 2;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      relationCollection.ObjectTypeID = IDCache.Default.AllArticles.Id;
      dataTable = relationCollection.EntersInVersion(paramSet, documentId);
    }
    return dataTable.Rows.Count != 1 ? (ObjectLocatorResult) null : new ObjectLocatorResult(Convert.ToInt64(dataTable.Rows[0][0]), Convert.ToInt32(dataTable.Rows[0][1]));
  }
}
