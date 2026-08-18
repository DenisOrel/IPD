// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.ContextProviders.AutoSelectionExecuteContextProvider
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.ContextProviders;

internal class AutoSelectionExecuteContextProvider : ICommandsProvider
{
  private AutoSelectionExecuteContextProvider(IFactory factory)
  {
    MenuTemplate menuTemplate = factory != null ? factory.ContextMenuTemplate : throw new ArgumentNullException(nameof (factory));
    menuTemplate.BeginUpdate();
    try
    {
      menuTemplate.Nodes.Add(new MenuTemplateNode("ExecuteAutoSelection", Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_98"), -1, 13, 20));
    }
    finally
    {
      menuTemplate.EndUpdate();
    }
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || viewServices == null)
      return CommandsInfo.Empty;
    long viewState = viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L;
    CommandsInfo mergedCommands = new CommandsInfo();
    if ((viewState & 2L) == 0L && items.Count == 1 && ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false) != null)
    {
      List<int> objectTypes = AutoSelectionUtils.Cache.GetObjectTypes();
      List<int> objectTypesWithRules = AutoSelectionUtils.Cache.GetObjectTypesWithRules();
      bool flag = false;
      for (int index = 0; index < items.Count; ++index)
      {
        if (items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData && (objectTypes.BinarySearch(itemData.Value) >= 0 || objectTypesWithRules.BinarySearch(itemData.Value) >= 0))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        mergedCommands.Add("ExecuteAutoSelection", new CommandInfo(0, new ClickEventHandler(AutoSelectionExecuteContextProvider.ExecuteAutoSelectionCommand)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private static void ExecuteAutoSelectionCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additional)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service1.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (items == null || items.Count == 0)
      return;
    IAutoSelectionService service2 = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
    if (service2 == null)
      return;
    AutoSelectionMode mode = AutoSelectionMode.All;
    if (MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_99"), (object) string.Empty), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_664.ssp_automatch_665()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    for (int index2 = 0; index2 < items.Count; ++index2)
    {
      if (items.GetItemData(index2, typeof (IDBObjectID)) is IDBObjectID itemData1)
      {
        IDBRelationID itemData = items.GetItemData(index2, typeof (IDBRelationID)) as IDBRelationID;
        List<RelObjInfoItem> collection = service2.ExecuteSelection(new AutoSelectionParams(itemData1.Value, itemData != null ? itemData.Value : 0L, mode));
        if (collection != null)
          source.AddRange((IEnumerable<RelObjInfoItem>) collection);
      }
    }
    if (source.Count != 0)
      ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
    int num2 = (int) MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_664.ssp_automatch_666()), (object) source.Count), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_98"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  internal static void RegisterCommandProvider(IFactory factory)
  {
    AutoSelectionExecuteContextProvider provider = factory != null ? new AutoSelectionExecuteContextProvider(factory) : throw new ArgumentNullException(nameof (factory));
    factory.AddCommandsProvider(1, (ICommandsProvider) provider);
  }
}
