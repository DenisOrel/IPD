// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Navigator.ProcRouteEntryColumnProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoute.Navigator;

/// <summary>
/// 
/// </summary>
internal class ProcRouteEntryColumnProvider : 
  INavigatorVirtualColumnProvider,
  ISpecialFieldsSupported
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="mapping"></param>
  /// <param name="sourceTable"></param>
  /// <returns></returns>
  private DataTable ConvertAssemblyCopyGuid(RecordMapping mapping, DataTable sourceTable)
  {
    int[] array = new int[2]
    {
      TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID,
      TechCardConsts.AttributeTypes.MemberOfAssemblyCopyAttrID
    };
    List<int> source = new List<int>();
    int count = mapping.Count;
    for (int index = 0; index < count; ++index)
    {
      IMSAttributeType attribute = mapping[index]?.Column?.Attribute;
      if (attribute != null && Array.IndexOf<int>(array, attribute.AttributeID) != -1)
      {
        source.Add(index);
        mapping[index].Transform = (INodeColumnTransform) null;
      }
    }
    if (!source.Any<int>())
      return sourceTable;
    HashSet<string> assemblyCopyGuids = new HashSet<string>();
    foreach (DataRow row in (InternalDataCollectionBase) sourceTable.Rows)
    {
      foreach (int columnIndex in source)
      {
        string stringValue = DataSetProcessor.GetStringValue(row, columnIndex, (string) null);
        if (!string.IsNullOrEmpty(stringValue))
          assemblyCopyGuids.Add(stringValue);
      }
    }
    IDictionary<string, string> assemblyCopyCaption;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      assemblyCopyCaption = ProcRouteEntryColumnProvider.GetAssemblyCopyCaption(sessionKeeper.Session, (IEnumerable<string>) assemblyCopyGuids);
    if (assemblyCopyCaption == null || !assemblyCopyCaption.Any<KeyValuePair<string, string>>())
      return sourceTable;
    bool flag = false;
    foreach (DataRow row in (InternalDataCollectionBase) sourceTable.Rows)
    {
      foreach (int columnIndex in source)
      {
        string stringValue = DataSetProcessor.GetStringValue(row, columnIndex, string.Empty);
        string str;
        if (assemblyCopyCaption.TryGetValue(stringValue, out str))
        {
          row[columnIndex] = (object) str;
          flag = true;
        }
      }
    }
    if (flag)
      sourceTable.AcceptChanges();
    return sourceTable;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public List<object> GetSpecialFields() => (List<object>) null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeItems"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  public object MapColumnToField(INodeItems nodeItems, NodeColumn column) => (object) null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeQuery"></param>
  /// <param name="mapping"></param>
  /// <param name="sourceTable"></param>
  /// <returns></returns>
  public DataTable GetDataTable(INodeQuery nodeQuery, NavigatorVirtualColumnProviderArgs args)
  {
    return args?.SourceTable == null || args?.Mapping == null ? (DataTable) null : this.ConvertAssemblyCopyGuid(args.Mapping, args.SourceTable);
  }

  /// <summary>
  /// Получение описательной части (заголовка) для по идентификатору ПК ДСЕ
  /// </summary>
  /// <param name="session"></param>
  /// <param name="assemblyCopyGuids"></param>
  /// <returns>Кэш вида идентификатору ПК ДСЕ =&gt; Заголовок ПК ДСЕ</returns>
  internal static IDictionary<string, string> GetAssemblyCopyCaption(
    [NotNull] IUserSession session,
    [NotNull] IEnumerable<string> assemblyCopyGuids)
  {
    Dictionary<string, string> assemblyCopyCaption = new Dictionary<string, string>();
    if (!assemblyCopyGuids.Any<string>())
      return (IDictionary<string, string>) assemblyCopyCaption;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProductionObjectUIDAttrID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    };
    DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(TechCardConsts.AttributeTypes.ProductionObjectUIDAttrID, RelationalOperators.In, (object) assemblyCopyGuids.ToArray<string>(), LogicalOperators.NONE, 0, false)
    }, columns);
    DataTable objectDataEx = DataHelper.GetObjectDataEx(TechCardConsts.ObjectTypes.ArticleCopyBaseID, session, dbRsp, (IEnumerable<ObjInfoItem>) null);
    if (objectDataEx == null)
      return (IDictionary<string, string>) assemblyCopyCaption;
    foreach (DataRow row in (InternalDataCollectionBase) objectDataEx.Rows)
      assemblyCopyCaption[DataSetProcessor.GetStringValue(row, 0, string.Empty)] = DataSetProcessor.GetStringValue(row, 1, string.Empty);
    return (IDictionary<string, string>) assemblyCopyCaption;
  }
}
