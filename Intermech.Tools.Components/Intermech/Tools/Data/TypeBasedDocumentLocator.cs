// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.TypeBasedDocumentLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Реализует алгоритм поиска документа, входящего в документацию на изделие, используя тип документа.
/// Этот алгоритм может использоваться в тех случаях, когда есть гарантия, что в документации на
/// изделие может быть не более одного документа искомого типа. Иначе будет возвращен первый
/// попавшийся документ подходящего типа.
/// </summary>
public sealed class TypeBasedDocumentLocator : IObjectLocator
{
  private IDocumentTypesLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них список подходящих типов документов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public TypeBasedDocumentLocator(IDocumentTypesLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект документа в базе IPS.</summary>
  /// <returns>Описатель найденного документа в базе IPS или null, если документ не был найден</returns>
  public ObjectLocatorResult LocateObject()
  {
    ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) CollectionUtils.ToArray<int>(this.dataDecoder.GetDocumentTypes()), LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      dataTable = relationCollection.ConsistFrom(paramSet, this.dataDecoder.GetArticleId());
    }
    if (dataTable.Rows.Count <= 0)
      return (ObjectLocatorResult) null;
    DataRow row = dataTable.Rows[0];
    return new ObjectLocatorResult(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]));
  }
}
