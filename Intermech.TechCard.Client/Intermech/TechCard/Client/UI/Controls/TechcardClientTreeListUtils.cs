// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechcardClientTreeListUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Tree List utils class</summary>
internal class TechcardClientTreeListUtils
{
  /// <summary>Обработчик события ShowTreeListMenu</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public static void TreeList_ShowTreeListMenu(object sender, TreeListMenuEventArgs e)
  {
    if (!(sender is TreeList treeList))
      return;
    TreeListHitInfo hitInfo = treeList.GetHitInfo(e.Point);
    if (hitInfo.HitInfoType == HitInfoType.Column)
    {
      e.Menu.Items[0].Caption = LocalizationHolder.rm.GetString("TechCard.Client_319");
      e.Menu.Items[1].Caption = LocalizationHolder.rm.GetString("TechCard.Client_320");
      e.Menu.Items[2].Caption = LocalizationHolder.rm.GetString("TechCard.Client_321");
      e.Menu.Items[3].Caption = LocalizationHolder.rm.GetString("TechCard.Client_322");
      e.Menu.Items[4].Caption = LocalizationHolder.rm.GetString("TechCard.Client_323");
    }
    if (hitInfo.HitInfoType != HitInfoType.BehindColumn)
      return;
    e.Menu.Items[0].Caption = LocalizationHolder.rm.GetString("TechCard.Client_324");
    e.Menu.Items[1].Caption = LocalizationHolder.rm.GetString("TechCard.Client_325");
  }

  /// <summary>Загрузка параметров XtraTreeList</summary>
  /// <param name="config"></param>
  /// <param name="treeList"></param>
  public static void LoadSettings(IConfiguration config, TreeList treeList)
  {
    treeList.Columns.Clear();
    if (config != null && config.HasProperty(treeList.Name + "_CollumnsLayout"))
    {
      string property = config.GetProperty(treeList.Name + "_CollumnsLayout");
      TreeListHelper.SetCollumnsStateByCaption(treeList, property, true);
    }
    for (int index = treeList.Columns.Count - 1; index >= 0; --index)
    {
      if (treeList.Columns[index].Tag == null)
        treeList.Columns.Remove(treeList.Columns[index]);
    }
    if (treeList.Columns.Count == 0)
    {
      IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(-50);
      if (attributeType1 != null)
      {
        TreeListColumn treeListColumn = treeList.Columns.Add();
        treeListColumn.Caption = attributeType1.Name;
        treeListColumn.Tag = (object) attributeType1.AttributeGuid;
        treeListColumn.Options &= ~ColumnOptions.CanSorted;
        treeListColumn.VisibleIndex = treeListColumn.AbsoluteIndex;
        treeListColumn.Width = 150;
      }
      IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
      if (attributeType2 != null)
      {
        TreeListColumn treeListColumn = treeList.Columns.Add();
        treeListColumn.Caption = attributeType2.Name;
        treeListColumn.Tag = (object) attributeType2.AttributeGuid;
        treeListColumn.Options &= ~ColumnOptions.CanSorted;
        treeListColumn.VisibleIndex = treeListColumn.AbsoluteIndex;
        treeListColumn.Width = 150;
      }
      IMSAttributeType attributeType3 = MetaDataHelper.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
      if (attributeType3 == null)
        return;
      TreeListColumn treeListColumn1 = treeList.Columns.Add();
      treeListColumn1.Caption = attributeType3.Name;
      treeListColumn1.Options &= ~ColumnOptions.CanSorted;
      treeListColumn1.Tag = (object) attributeType3.AttributeGuid;
      treeListColumn1.VisibleIndex = treeListColumn1.AbsoluteIndex;
      treeListColumn1.Width = 150;
    }
    else
    {
      foreach (TreeListColumn column in (CollectionBase) treeList.Columns)
        column.Options &= ~ColumnOptions.CanSorted;
    }
  }

  /// <summary>Сохранение параметров XtraTreeList</summary>
  /// <param name="config"></param>
  /// <param name="treeList"></param>
  public static void SaveSettings(IConfiguration config, TreeList treeList)
  {
    if (config == null)
      return;
    string collumnsState = TreeListHelper.GetCollumnsState(treeList);
    config.SetProperty(treeList.Name + "_CollumnsLayout", collumnsState);
  }

  /// <summary>Add object to tree list</summary>
  /// <param name="treeList"></param>
  /// <param name="dbAttributable"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public static TreeListNode AddObjectToTreeList(
    TreeList treeList,
    IDBAttributable dbAttributable,
    object data)
  {
    return TechcardClientTreeListUtils.AddObjectToTreeList(treeList, dbAttributable, data, (TreeListNode) null);
  }

  /// <summary>Add object to tree list</summary>
  /// <param name="treeList"></param>
  /// <param name="dbAttributable"></param>
  /// <param name="data"></param>
  /// <param name="ownerNode"></param>
  /// <returns></returns>
  public static TreeListNode AddObjectToTreeList(
    TreeList treeList,
    IDBAttributable dbAttributable,
    object data,
    TreeListNode ownerNode)
  {
    if (treeList == null || dbAttributable == null)
      return (TreeListNode) null;
    TreeListNode treeList1 = treeList.AppendNode((object) null, ownerNode);
    treeList1.Tag = (object) new TechCntrDataHolder(dbAttributable, data);
    if (treeList.Columns.Count == 0)
      return treeList1;
    AttributeValues[] attributesValues = dbAttributable.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption);
    Dictionary<Guid, AttributeValues> dictionary = new Dictionary<Guid, AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
      dictionary.Add(attributeValues.AttributeGuid, attributeValues);
    foreach (TreeListColumn column in (CollectionBase) treeList.Columns)
    {
      if (column != null && column.Tag is Guid)
      {
        int absoluteIndex = column.AbsoluteIndex;
        if (dictionary.ContainsKey((Guid) column.Tag))
        {
          AttributeValues attributeValues = dictionary[(Guid) column.Tag];
          if (attributeValues.Descriptions != null && attributeValues.Descriptions.Length != 0)
            treeList1.SetValue((object) absoluteIndex, attributeValues.Descriptions[0]);
          else if (attributeValues.Values != null && attributeValues.Values.Length != 0)
            treeList1.SetValue((object) absoluteIndex, attributeValues.Values[0]);
          else
            treeList1.SetValue((object) absoluteIndex, (object) null);
        }
        else
          treeList1.SetValue((object) absoluteIndex, (object) null);
      }
    }
    return treeList1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="treeNode"></param>
  /// <returns></returns>
  public static object GetNodeData(TreeListNode treeNode)
  {
    if (treeNode == null)
      return (object) null;
    object nodeData = treeNode.Tag;
    if (nodeData is TechCntrDataHolder)
      nodeData = ((TechCntrDataHolder) nodeData).Data;
    return nodeData;
  }
}
