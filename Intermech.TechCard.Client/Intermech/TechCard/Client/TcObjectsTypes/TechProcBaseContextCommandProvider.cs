// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBaseContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands.Edit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// Класс реализующий команды контекстного меню для объектов типа "Техпроцесс базовый"
/// </summary>
internal class TechProcBaseContextCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void ViewObjectCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1))
      return;
    bool flag = false;
    if (MetaDataHelper.GetObjectType(itemData1.ObjectType).AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(TechCardConsts.AttributeTypes.FileAttrTypeID, itemData1.ObjectType) != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(itemData1.ObjectID, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeById = objectActualCopy.GetAttributeByID(TechCardConsts.AttributeTypes.FileAttrTypeID);
          flag = attributeById != null && attributeById.ValuesCount != 0;
        }
      }
    }
    if (flag)
    {
      ObjectCommands.ViewCommand(items, viewServices, additionalInfo);
    }
    else
    {
      List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ComplectDocBaseID).ToArray(), LogicalOperators.NONE, 0, false)
        };
        ColumnDescriptor[] columns = new ColumnDescriptor[0];
        List<ObjInfoItem> projObjList = new List<ObjInfoItem>(items.Count);
        for (int index2 = 0; index2 < items.Count; ++index2)
        {
          if (items.GetItemData(index2, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData2)
            projObjList.Add(new ObjInfoItem(itemData2.ObjectID, itemData2.ObjectType));
        }
        DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.SortedRelationID
        }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns, (HybridDictionary) null);
        if (childSostavData != null)
        {
          int columnIndex1 = childSostavData.Columns.IndexOf("F_OBJECT_ID");
          int columnIndex2 = childSostavData.Columns.IndexOf("F_OBJECT_TYPE");
          foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
          {
            ObjInfoItem objInfoItem = new ObjInfoItem(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex2]));
            objInfoItemList.Add(objInfoItem);
          }
        }
      }
      if (objInfoItemList.Count == 0)
      {
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_517"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        if (objInfoItemList.Count > 1)
        {
          List<long> ids = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ComplectDocBaseID, (IList<ObjInfoItem>) objInfoItemList, "", LocalizationHolder.rm.GetString("TechCard.Client_516"));
          if (ids == null || ids.Count <= 0)
            return;
          objInfoItemList = objInfoItemList.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => ids.Contains(item.ObjectID))).ToList<ObjInfoItem>();
        }
        items = Services.GetItems(objInfoItemList.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)).ToArray<long>());
        CommandsTable commandsTable = Services.GetCommandsTable(items, viewServices);
        if (!commandsTable.Contains("ViewDocument"))
          return;
        Services.InvokeCommand("ViewDocument", commandsTable, viewServices);
      }
    }
  }

  private void EditObjectCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    SimpleEditCommand simpleEditCommand = new SimpleEditCommand();
    simpleEditCommand.Init(items, viewServices, additionalInfo);
    simpleEditCommand.Execute();
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo empty = CommandsInfo.Empty;
    if (items == null || items.Count == 0)
      return empty;
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count == 1)
      mergedCommands.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(this.ViewObjectCommand)));
    if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.EditObjectCommand)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
