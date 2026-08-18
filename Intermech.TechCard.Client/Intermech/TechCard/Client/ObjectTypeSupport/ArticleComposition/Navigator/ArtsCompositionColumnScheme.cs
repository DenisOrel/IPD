// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionColumnScheme
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Data;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

internal class ArtsCompositionColumnScheme : INodeColumnScheme
{
  /// <summary>
  /// 
  /// </summary>
  public string Name { get; } = LocalizationHolder.rm.GetString("TechCard.Client_530");

  /// <summary>
  /// 
  /// </summary>
  /// <param name="columnId"></param>
  /// <returns></returns>
  public string ColumnIDToPersistName(object columnId) => "virt." + columnId;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="persistName"></param>
  /// <returns></returns>
  public object PersistNameToColumnID(string persistName)
  {
    return (object) persistName.Replace("virt.", "");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="schemeGuid"></param>
  /// <param name="columnId"></param>
  /// <returns></returns>
  public NodeColumn CreateColumn(Guid schemeGuid, object columnId)
  {
    return this.CreateColumn(schemeGuid, columnId, NodeColumnSortOrder.None, -1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="schemeGuid"></param>
  /// <param name="columnId"></param>
  /// <param name="sortOrder"></param>
  /// <param name="sortIndex"></param>
  /// <returns></returns>
  public NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnId,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (!(columnId is int result) && !int.TryParse(Convert.ToString(columnId), out result))
      return (NodeColumn) null;
    string caption = (string) null;
    if (result == TechCardConsts.AttributeTypes.Count4TechProcAttrID || result == TechCardConsts.AttributeTypes.Count4ArticleAttrID || result == TechCardConsts.AttributeTypes.CountRemainAttrID)
      caption = MetaDataHelper.GetAttributeTypeName(result);
    else if (result == ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS)
      caption = LocalizationHolder.rm.GetString("TechCard.Client_531");
    if (caption == null)
      return (NodeColumn) null;
    FieldTypes attrType = FieldTypes.ftUnknown;
    Type dataType = typeof (NodeDelayedValue);
    VirtualNodeColumn column = new VirtualNodeColumn(schemeGuid, (object) result, dataType, attrType, caption);
    column.Priority = SchemeColumnPriority.Highest;
    return (NodeColumn) column;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="columnId"></param>
  /// <returns></returns>
  public INodeColumnTransform GetDefaultTransform(object columnId) => (INodeColumnTransform) null;

  /// <summary>Выполнить регистрацию схемы колонок</summary>
  public static void Register()
  {
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false)?.Register(ArtsCompositionColumnScheme.Consts.SchemeGuid, (INodeColumnScheme) new ArtsCompositionColumnScheme());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static NodeColumnCollection GetColumnCollection()
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    if (service == null)
      return columnCollection;
    Guid schemeGuid = ArtsCompositionColumnScheme.Consts.SchemeGuid;
    columnCollection.Add(service.CreateColumn(schemeGuid, (object) TechCardConsts.AttributeTypes.Count4TechProcAttrID));
    columnCollection.Add(service.CreateColumn(schemeGuid, (object) TechCardConsts.AttributeTypes.Count4ArticleAttrID));
    columnCollection.Add(service.CreateColumn(schemeGuid, (object) TechCardConsts.AttributeTypes.CountRemainAttrID));
    columnCollection.Add(service.CreateColumn(schemeGuid, (object) ArtsCompositionColumnScheme.Consts.F_ITEM_STATUS));
    return columnCollection;
  }

  /// <summary>
  /// 
  /// </summary>
  public static class Consts
  {
    /// <summary>Статус позиции</summary>
    /// <remarks>Здесь нужен id атрибута меньше NavigatorUndefinedAttributeID
    /// иначе атрибута не будет в группе.
    /// а вообще - это даже не атрибут
    /// </remarks>
    public static int F_ITEM_STATUS = -10050;

    /// <summary>
    /// 
    /// </summary>
    public static Guid SchemeGuid { get; } = new Guid("{75D1741B-98C9-4D8F-A2F5-24D47CD165F1}");
  }
}
