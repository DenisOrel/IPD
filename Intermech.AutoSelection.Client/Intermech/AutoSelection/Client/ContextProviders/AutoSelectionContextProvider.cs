// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.ContextProviders.AutoSelectionContextProvider
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Forms;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.ContextProviders;

internal class AutoSelectionContextProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || viewServices == null)
      return CommandsInfo.Empty;
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service != null ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) != ViewStateFlags.None || items.Count != 1 || (viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData) || itemData.Value == 0L)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("EditDocument", new CommandInfo(3, new ClickEventHandler(AutoSelectionContextProvider.EditSelectionRule)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if ((((IViewState) viewServices.GetService(typeof (IViewState))).ViewState & ViewStateFlags.ReadOnly) != ViewStateFlags.None || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Suppress("CreateInclude", 0);
    groupCommands.Suppress("CreateVersion", 0);
    return groupCommands;
  }

  private static void EditSelectionRule(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additional)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    long objectID = itemData.Value;
    if (objectID == 0L)
      return;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule autoSelectionRule;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      autoSelectionRule = Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule.Load(sessionKeeper.Session.GetObject(objectID));
    if (autoSelectionRule == null)
      return;
    AutoSelectionEditForm selectionEditForm = new AutoSelectionEditForm()
    {
      ReadOnly = false,
      Rule = autoSelectionRule
    };
    if (!selectionEditForm.ShowDialog().Equals((object) DialogResult.OK))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      selectionEditForm.Rule.Save(sessionKeeper.Session.GetObject(objectID), sessionKeeper.Session);
  }

  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    AutoSelectionContextProvider provider = new AutoSelectionContextProvider();
    factory.AddCommandsProvider(1, AutoSelectionConsts.objTypeRuleID, (ICommandsProvider) provider);
    factory.AddCommandsProvider(4, AutoSelectionConsts.objTypeRuleID, (ICommandsProvider) provider);
  }
}
