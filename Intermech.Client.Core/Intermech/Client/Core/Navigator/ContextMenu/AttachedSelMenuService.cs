
// Type: Intermech.Client.Core.Navigator.ContextMenu.AttachedSelMenuService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Client.Core.Navigator.ContextMenu;

/// <summary>
/// Сервис для построения меню для выборок, которые прикреплены
/// к различным объектам (табличным отчетам и т.д.)
/// </summary>
public class AttachedSelMenuService
{
  private IServiceProvider _currentViewServices;
  private readonly List<int> _objectTypes;
  public ButtonMenuPressedEventHandler ButtonMenuPressedEvent;

  public AttachedSelMenuService(int objectType)
    : this(new List<int>((IEnumerable<int>) new int[1]
    {
      objectType
    }))
  {
  }

  public AttachedSelMenuService(List<int> objectTypes) => this._objectTypes = objectTypes;

  public void AfterCreateMenu(Component contextMenu, IServiceProvider viewServices)
  {
    if (!(contextMenu is ContextMenuBarItem contextMenuBarItem))
      return;
    INavigatorTreeViewContextMenuHelper service = (INavigatorTreeViewContextMenuHelper) viewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper));
    if (service == null || service.Tree == null || service.Tree.FocusedItem == null || service.Tree.FocusedItem.ItemID == null)
      return;
    INodeID itemId = service.Tree.FocusedItem.ItemID;
    if (itemId.TypeID != MetaDataHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545") && itemId.TypeID != MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545"))
      return;
    foreach (MenuItemBase menuItemBase in (CollectionBase) contextMenuBarItem.Items)
    {
      if (menuItemBase.Text == LocalizationHolder.rm.GetString("Client.Core_1369"))
        menuItemBase.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.LaunchItem_BeforePopup);
    }
    this._currentViewServices = viewServices;
  }

  private void LaunchItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (!(sender is MenuButtonItem) || this._currentViewServices == null)
      return;
    MenuButtonItem menuButtonItem1 = (MenuButtonItem) sender;
    INavigatorTreeViewContextMenuHelper service1 = (INavigatorTreeViewContextMenuHelper) this._currentViewServices.GetService(typeof (INavigatorTreeViewContextMenuHelper));
    if (service1.MenuNode == null)
      return;
    NodeID nodeId = (NodeID) service1.MenuNode.NodeID;
    if (nodeId == null || service1.MenuNode.Handler == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICategoryTypeIconService service2 = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAttachedSelectionsService)) is IAttachedSelectionsService customService))
        return;
      for (int index = menuButtonItem1.Items.Count - 1; index >= 0; --index)
      {
        if (menuButtonItem1.Items[index].Tag is ButtonMenuPressedArgs && this._objectTypes.Contains(((ButtonMenuPressedArgs) menuButtonItem1.Items[index].Tag).ObjectInfo.ObjectType))
          menuButtonItem1.Items.Remove((ToolbarItemBase) menuButtonItem1.Items[index]);
      }
      AttachedSelObjectInfo[] objectsForSelection = customService.GetObjectsForSelection(nodeId.ObjectID, this._objectTypes.ToArray());
      if (objectsForSelection == null || objectsForSelection.Length == 0)
        return;
      INodeQuery query = service1.MenuNode.Handler.GetQuery(ContentType.Folders | ContentType.NonFolders);
      IServiceProvider services = (IServiceProvider) null;
      if (this._currentViewServices.GetService(typeof (IViewsManager)) is IViewsManager service3)
      {
        for (int index = 0; index < service3.ViewPages.Count; ++index)
        {
          if (service3.ViewPages[index].Name == "ChildrenView")
          {
            services = (IServiceProvider) ((ChildrenView) service3.ViewPages[index].View).Services;
            break;
          }
        }
      }
      for (int index = 0; index < objectsForSelection.Length; ++index)
      {
        MenuButtonItem menuButtonItem2 = new MenuButtonItem(sessionKeeper.Session.GetObjectInfo(objectsForSelection[index].ObjectID).Caption, new EventHandler(this.Launch));
        menuButtonItem2.Tag = (object) objectsForSelection[index].ObjectID;
        menuButtonItem2.Icon = service2.GetIcon(4, objectsForSelection[index].ObjectType);
        MenuButtonItem menuButtonItem3 = menuButtonItem2;
        if (index == 0)
          menuButtonItem3.BeginGroup = true;
        menuButtonItem3.Tag = (object) new ButtonMenuPressedArgs(objectsForSelection[index], services, query);
        menuButtonItem1.Items.Add((ToolbarItemBase) menuButtonItem3);
      }
    }
  }

  private void Launch(object sender, EventArgs e)
  {
    MenuButtonItem menuButtonItem = (MenuButtonItem) sender;
    if (menuButtonItem.Tag == null)
      return;
    ButtonMenuPressedEventHandler menuPressedEvent = this.ButtonMenuPressedEvent;
    if (menuPressedEvent == null)
      return;
    menuPressedEvent((object) this, (ButtonMenuPressedArgs) menuButtonItem.Tag);
  }
}
