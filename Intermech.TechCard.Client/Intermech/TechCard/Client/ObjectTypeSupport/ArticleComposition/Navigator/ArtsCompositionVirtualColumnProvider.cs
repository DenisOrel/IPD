// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionVirtualColumnProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Data;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualColumns;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionVirtualColumnProvider : 
  INavigatorVirtualColumnProvider,
  ISpecialFieldsSupported
{
  /// <summary>Ид. схемы</summary>
  private readonly Guid _schemeGuid;
  /// <summary>"Виртуальная" (то есть добавляемая в результат запроса уже на клиенте) колонка "Количество по ТП"</summary>
  internal static readonly VirtualQueryResultColumn VirtualColumnCountTech = new VirtualQueryResultColumn("F_COUNT_TECH", typeof (NodeDelayedValue), (object) NodeDelayedValue.EmptyValue);
  /// <summary>"Виртуальная" (то есть добавляемая в результат запроса уже на клиенте) колонка "Количество по конструкторскому составу"</summary>
  internal static readonly VirtualQueryResultColumn VirtualColumnCountArt = new VirtualQueryResultColumn("F_COUNT_ART", typeof (NodeDelayedValue), (object) NodeDelayedValue.EmptyValue);
  /// <summary>"Виртуальная" (то есть добавляемая в результат запроса уже на клиенте) колонка "Оставшееся количество"</summary>
  internal static readonly VirtualQueryResultColumn VirtualColumnCountRemain = new VirtualQueryResultColumn("F_COUNT_REMAIN", typeof (NodeDelayedValue), (object) NodeDelayedValue.EmptyValue);
  /// <summary>"Виртуальная" (то есть добавляемая в результат запроса уже на клиенте) колонка "результат сравнения"</summary>
  internal static readonly VirtualQueryResultColumn VirtualColumnItemStatus = new VirtualQueryResultColumn("F_ITEM_STATUS ", typeof (NodeDelayedValue), (object) NodeDelayedValue.EmptyValue);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="schemeGuid"></param>
  public ArtsCompositionVirtualColumnProvider(Guid schemeGuid) => this._schemeGuid = schemeGuid;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public List<object> GetSpecialFields()
  {
    return new List<object>()
    {
      (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountTech,
      (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountArt,
      (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountRemain,
      (object) ArtsCompositionVirtualColumnProvider.VirtualColumnItemStatus
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeItems"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  public object MapColumnToField(INodeItems nodeItems, NodeColumn column)
  {
    if (this._schemeGuid != Guid.Empty && !column.SchemeGuid.Equals(this._schemeGuid))
      return (object) null;
    if (column.ID.Equals((object) TechCardConsts.AttributeTypes.Count4TechProcAttrID))
      return (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountTech;
    if (column.ID.Equals((object) TechCardConsts.AttributeTypes.Count4ArticleAttrID))
      return (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountArt;
    if (column.ID.Equals((object) TechCardConsts.AttributeTypes.CountRemainAttrID))
      return (object) ArtsCompositionVirtualColumnProvider.VirtualColumnCountRemain;
    return column.ID.Equals((object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS) ? (object) ArtsCompositionVirtualColumnProvider.VirtualColumnItemStatus : (object) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeQuery"></param>
  /// <param name="mapping"></param>
  /// <param name="sourceTable"></param>
  /// <returns></returns>
  public DataTable GetDataTable(INodeQuery nodeQuery, NavigatorVirtualColumnProviderArgs args)
  {
    if (args.SourceTable == null)
      return (DataTable) null;
    VirtualQueryResultColumn.AddVirtualColumns(args.SourceTable, args.Mapping, (System.Func<VirtualQueryResultColumn, object>) (virtualColumn => virtualColumn.DefaultValue));
    FillDataTableEventArgs e = new FillDataTableEventArgs(nodeQuery, args.Mapping, args.SourceTable);
    FillDataTableEventHandler fillDataTableEvent = this.FillDataTableEvent;
    if (fillDataTableEvent != null)
      fillDataTableEvent((object) this, e);
    return e.DataTable;
  }

  /// <summary>
  /// 
  /// </summary>
  public event FillDataTableEventHandler FillDataTableEvent;
}
