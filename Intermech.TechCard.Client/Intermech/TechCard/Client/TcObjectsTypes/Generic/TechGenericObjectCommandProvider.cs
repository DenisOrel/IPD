// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Generic.TechGenericObjectCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Generic;

/// <summary>
/// Класс реализующий команды меню для базового типа объекта (всех типов объектов)
/// </summary>
internal class TechGenericObjectCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  private TechGenericObjectCommandProvider()
  {
    IFactory service = ServiceUtils.GetService<IFactory>((object) TechCardClient.ServiceProvider, false);
    if (service == null)
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Реализация команды "Выделить все" в дереве навигатора</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void TechCheckAllCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    NavigatorTreeView service = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service == null)
      return;
    Action<NavigatorTreeNodes> checkNodeRecursive = (Action<NavigatorTreeNodes>) null;
    checkNodeRecursive = (Action<NavigatorTreeNodes>) (nodes =>
    {
      if (nodes == null)
        return;
      foreach (NavigatorTreeNode node in (List<NavigatorTreeNode>) nodes)
      {
        if (node != null)
        {
          if (node.CheckState == CheckState.Unchecked && (node is TechcardNavTreeNode techcardNavTreeNode2 ? (techcardNavTreeNode2.CheckBoxStyle != 0 ? 1 : 0) : 1) != 0)
            node.CheckState = CheckState.Checked;
          checkNodeRecursive(node.Children);
        }
      }
    });
    checkNodeRecursive(service.Nodes);
    (viewServices.GetService(typeof (ICommandManager)) as CommandManager)?.QueryStatus();
  }

  /// <summary>Реализация команды  Снять выделение у всех узлов</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void TechUncheckAllCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    NavigatorTreeView service = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service == null)
      return;
    foreach (NavigatorTreeNode checkedNode in service.CheckedNodes)
      checkedNode.CheckState = CheckState.Unchecked;
    (viewServices.GetService(typeof (ICommandManager)) as CommandManager)?.QueryStatus();
  }

  /// <summary>
  /// Проверка допустимости расширения команды "Вставить" для выделенных объектов
  /// </summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  private void IsPasteExCommandAllow(
    CommandsInfo commandsInfo,
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0)
      return;
    if (PasteCommand.AllowCommand(items, viewServices))
      commandsInfo.Add("Paste", new CommandInfo(3, new ClickEventHandler(TechGenericObjectCommandProvider.PasteExCommand)));
    else
      commandsInfo.Suppress("Paste", 0);
  }

  /// <summary>Расширение команды "Вставить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void PasteExCommand(
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
    items = PasteCommand.GetSelectedItems(items, viewServices);
    PasteCommand pasteCommand = new PasteCommand();
    pasteCommand.Init(items, viewServices, additionalInfo);
    pasteCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo commandsInfo = new CommandsInfo();
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service1) ? ViewStateFlags.None : service1.ViewState;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.ReadOnly)
    {
      IClipboard service2 = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
      if (service2 != null)
      {
        IDBTypedObjectID[] typedObjects = service2.GetDataObject() is IDBObjectTypedIDCollection dataObject ? dataObject.GetTypedObjects() : (IDBTypedObjectID[]) null;
        if (typedObjects != null && ((IEnumerable<IDBTypedObjectID>) typedObjects).All<IDBTypedObjectID>((Func<IDBTypedObjectID, bool>) (item => TechCardConsts.Utils.IsTechcardObjectType((object) item.ObjectType))))
          this.IsPasteExCommandAllow(commandsInfo, items, viewServices);
      }
    }
    if (viewStateFlags.HasFlag((Enum) ViewStateFlags.NodeInTree) && viewServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service3 && service3.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
    {
      commandsInfo.Add("techCheckAll", new CommandInfo(0, new ClickEventHandler(TechGenericObjectCommandProvider.TechCheckAllCommand)));
      if (service3.CheckedNodes.Length != 0)
        commandsInfo.Add("techUncheckAll", new CommandInfo(0, new ClickEventHandler(TechGenericObjectCommandProvider.TechUncheckAllCommand)));
    }
    return commandsInfo;
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    TechGenericObjectCommandProvider provider = new TechGenericObjectCommandProvider();
    factory.AddCommandsProvider(1, (ICommandsProvider) provider);
  }
}
