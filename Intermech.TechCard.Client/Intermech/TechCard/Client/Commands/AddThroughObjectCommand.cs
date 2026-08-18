// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.AddThroughObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды контекстного меню "Добавить объекты сквозного ТП"
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="addTpNodes"></param>
internal class AddThroughObjectCommand(bool addTpNodes = false) : AddObjectCommand(addTpNodes, "AddTechThroughObject")
{
  /// <summary>Список допустимых "сквозных" типов объектов</summary>
  /// <remarks>Содержит допустимые типы объектов по типу связи "Технологический сквозной ТП"</remarks>
  private readonly List<int> _throughObjTypeList = new List<int>();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool SelectThroughtObjectComposition()
  {
    List<IDBTypedObjectID> source = new List<IDBTypedObjectID>(this._selectedObjInfoItems.Count);
    List<IDBTypedObjectID> collection = new List<IDBTypedObjectID>(this._selectedObjInfoItems.Count);
    foreach (IDBTypedObjectID selectedObjInfoItem in this._selectedObjInfoItems)
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(selectedObjInfoItem.ObjectType, TechCardConsts.ObjectTypes.TechProcBaseID))
        collection.Add(selectedObjInfoItem);
      else
        source.Add(selectedObjInfoItem);
    }
    if (source.Count == 0)
      return true;
    GenericListHelper.MakeUnique<int>(this._throughObjTypeList);
    DescriptorCollection descriptors = new DescriptorCollection((IEnumerable<IDescriptor>) source.Select<IDBTypedObjectID, Intermech.Navigator.DBObjects.Descriptor>((Func<IDBTypedObjectID, Intermech.Navigator.DBObjects.Descriptor>) (item => new Intermech.Navigator.DBObjects.Descriptor(item.ObjectID))));
    using (TechcardObjectForm techcardObjectForm = new TechcardObjectForm())
    {
      techcardObjectForm.Name = "AddTechThroughObjectDialog";
      techcardObjectForm.tolcTechObjList.CheckRootNode = true;
      techcardObjectForm.tolcTechObjList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
      techcardObjectForm.tolcTechObjList.AfterCreateNode += (EventHandler<NodeEventArgs>) ((sender, e) =>
      {
        TechcardNavTreeNode node = e != null ? e.Node as TechcardNavTreeNode : (TechcardNavTreeNode) null;
        NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
        if (node == null || navigatorTreeView == null)
          return;
        INode nodeHandler = navigatorTreeView.GetNodeHandler((NavigatorTreeNode) node);
        if (nodeHandler == null)
          return;
        IDBTypedObjectID data = node.NodeID is NodeID nodeId2 ? nodeHandler.GetData((INodeID) nodeId2, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
        if (data == null || this._throughObjTypeList.BinarySearch(data.ObjectType) < 0)
          node.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
        else
          node.SetCheckStateInternal(CheckState.Unchecked);
      });
      techcardObjectForm.tolcTechObjList.CheckStateChanging += (EventHandler<CheckStateEventArgs>) ((sender, e) =>
      {
        if (!(e.Node is TechcardNavTreeNode node2) || e.OldValue != CheckState.Indeterminate || e.OldValue == e.NewValue)
          return;
        e.NewValue = e.OldValue;
        node2.SetCheckStateInternal(e.OldValue);
      });
      techcardObjectForm.Load += (EventHandler) ((sender, e) => { });
      techcardObjectForm.LoadData(LocalizationHolder.rm.GetString("TechCard.Client_522"), (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, LocalizationHolder.rm.GetString("TechCard.Client_505"), descriptors));
      if (techcardObjectForm.ShowDialog() != DialogResult.OK || techcardObjectForm.tolcTechObjList.CheckedItems.Count == 0)
      {
        this._selectedObjInfoItems.Clear();
        return false;
      }
      this._selectedObjInfoItems.Clear();
      this._selectedObjInfoItems.AddRange((IEnumerable<IDBTypedObjectID>) collection);
      for (int index = 0; index < techcardObjectForm.tolcTechObjList.CheckedItems.Count; ++index)
        this._selectedObjInfoItems.Add(techcardObjectForm.tolcTechObjList.CheckedItems.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID);
      return true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  protected override void LoadMetaDataInfo(ObjInfoItem targetObjInfo)
  {
    this._linkTypes = new Hashtable();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> intList = new List<int>((IEnumerable<int>) sessionKeeper.Session.GetObjectTypeCollection(-2, true).GetVisibleList());
      intList.Sort();
      foreach (int parentTypeID in MetaDataHelper.GetApplicabilityChildObjectTypesID(targetObjInfo.ObjTypeID, TechCardConsts.RelTypes.TechThroughtTPRelationID))
      {
        if (intList.BinarySearch(parentTypeID) >= 0)
          this._throughObjTypeList.Add(parentTypeID);
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID))
        {
          if (intList.BinarySearch(num) >= 0)
            this._throughObjTypeList.Add(parentTypeID);
        }
      }
    }
    if (this._throughObjTypeList.Count == 0)
      return;
    foreach (int throughObjType in this._throughObjTypeList)
      this._linkTypes[(object) throughObjType] = (object) TechCardConsts.RelTypes.TechThroughtTPRelationID;
    if (this._linkTypes.Contains((object) TechCardConsts.ObjectTypes.TechProcBaseID))
      return;
    this._linkTypes.Add((object) TechCardConsts.ObjectTypes.TechProcBaseID, (object) TechCardConsts.RelTypes.TechThroughtTPRelationID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <returns></returns>
  protected override bool SelectObjects4Command(ObjInfoItem targetObjInfo)
  {
    return base.SelectObjects4Command(targetObjInfo) && this.SelectThroughtObjectComposition();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <param name="rootDescriptor"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  protected override bool DoSelectObjects4Command(
    ObjInfoItem targetObjInfo,
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor,
    IServiceContainer services)
  {
    IDBTypedObjectID[] source = (IDBTypedObjectID[]) Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("TechCard.Client_521"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), (System.IServiceProvider) services, SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule);
    this._selectedObjInfoItems = source != null ? ((IEnumerable<IDBTypedObjectID>) source).ToList<IDBTypedObjectID>() : (List<IDBTypedObjectID>) null;
    return this._selectedObjInfoItems != null;
  }
}
