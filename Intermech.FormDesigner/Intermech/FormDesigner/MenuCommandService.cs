// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.MenuCommandService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class MenuCommandService : IMenuCommandService
{
  private Hashtable _menuCommands = new Hashtable();
  private BidirectHashtable _menuItemVerb = new BidirectHashtable();
  private IDesignerHost _host;
  private IComponent _lastSelectedComponent;
  /// <summary>"FormDesigner_16" = Контекстное меню</summary>
  private MenuBarItem _contextMenu = ProviderHolder.BarManager.MenuBar.AddMenuBar(LocalizationHolder.rm.GetString("FormDesigner_16"));
  internal MenuButtonItem _linkTo;
  private Dictionary<MenuButtonItem, CommandID> _menuItems = new Dictionary<MenuButtonItem, CommandID>(7);
  private bool _readOnly;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  /// <param name="readOnly">Возможность редактировать данные</param>
  public MenuCommandService(IDesignerHost host, bool readOnly)
  {
    this.Verbs = new DesignerVerbCollection();
    this._host = host;
    this._readOnly = readOnly;
    this._contextMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.OncontextMenu_BeforePopup);
    this._contextMenu.Visible = false;
    INamedImageList service = ProviderHolder.ServiceProvider.GetService(typeof (INamedImageList)) as INamedImageList;
    this._linkTo = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_17"), new EventHandler(this.OnCommand_Click));
    MenuButtonItem key1 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_22"), new EventHandler(this.OnCommand_Click));
    key1.BeginGroup = true;
    key1.ImageIndex = service != null ? service.ImageIndex("imgCopy") : -1;
    this._menuItems.Add(key1, StandardCommands.Copy);
    MenuButtonItem key2 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_23"), new EventHandler(this.OnCommand_Click));
    key2.ImageIndex = service != null ? service.ImageIndex("imgCut") : -1;
    this._menuItems.Add(key2, StandardCommands.Cut);
    MenuButtonItem key3 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_24"), new EventHandler(this.OnCommand_Click));
    key3.ImageIndex = service != null ? service.ImageIndex("imgPaste") : -1;
    this._menuItems.Add(key3, StandardCommands.Paste);
    MenuButtonItem key4 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_19"), new EventHandler(this.OnCommand_Click));
    key4.BeginGroup = true;
    key4.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 17];
    this._menuItems.Add(key4, StandardCommands.BringToFront);
    MenuButtonItem key5 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_18"), new EventHandler(this.OnCommand_Click));
    key5.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 18];
    this._menuItems.Add(key5, StandardCommands.SendToBack);
    MenuButtonItem key6 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_20"), new EventHandler(this.OnCommand_Click));
    key6.BeginGroup = true;
    key6.ImageIndex = (int) ProviderHolder.MenuIndex[(object) 5];
    this._menuItems.Add(key6, StandardCommands.AlignToGrid);
    MenuButtonItem key7 = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_21"), new EventHandler(this.OnCommand_Click));
    key7.BeginGroup = true;
    key7.ImageIndex = service != null ? service.ImageIndex("imgDelete") : -1;
    this._menuItems.Add(key7, StandardCommands.Delete);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnItem_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem key))
      return;
    if (!(this._menuItemVerb[(object) key] is DesignerVerb designerVerb))
      return;
    try
    {
      designerVerb.Invoke();
    }
    catch
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnCommand_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem key))
      return;
    if (key == this._linkTo)
    {
      DesForm rootComponent = this._host.RootComponent as DesForm;
      FormLinks links = rootComponent.Links;
      FormLinks formLinks = (TypeDescriptor.GetEditor((object) links, typeof (UITypeEditor)) as UITypeEditor).EditValue((System.IServiceProvider) this._host, (object) links) as FormLinks;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) rootComponent);
      if (formLinks == null || properties == null)
        return;
      properties["Links"].SetValue((object) rootComponent, (object) formLinks);
    }
    else
    {
      if (!this._menuItems.ContainsKey(key))
        return;
      this.GlobalInvoke(this._menuItems[key]);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OncontextMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this._linkTo.Visible = TypeDescriptor.GetEditor((object) (this._host.RootComponent as DesForm).Links, typeof (UITypeEditor)) is UITypeEditor && !this._readOnly;
    foreach (KeyValuePair<MenuButtonItem, CommandID> menuItem in this._menuItems)
    {
      MenuCommand command = this.FindCommand(menuItem.Value);
      if (command != null && command.Enabled)
        menuItem.Key.Enabled = menuItem.Value == StandardCommands.Copy || !this._readOnly;
      else
        menuItem.Key.Enabled = false;
    }
    foreach (object key in (IEnumerable) this._menuItemVerb.forward.Keys)
    {
      if (key is MenuButtonItem menuButtonItem)
        menuButtonItem.Visible = !this._readOnly;
    }
  }

  /// <summary>Добавляет в меню указанную стандартную команду меню.</summary>
  /// <param name="command">Команда меню</param>
  public void AddCommand(MenuCommand command)
  {
    this._menuCommands.Add((object) command.CommandID, (object) command);
  }

  /// <summary>
  /// Добавляет указанную команду конструктора в набор общих команд конструктора.
  /// </summary>
  /// <param name="verb">Команда конструктора</param>
  public void AddVerb(DesignerVerb verb)
  {
    if (verb == null)
      return;
    this.Verbs.Add(verb);
    MenuButtonItem key = new MenuButtonItem(verb.Text, new EventHandler(this.OnItem_Click));
    this._menuItemVerb.Add((object) key, (object) verb);
    this._contextMenu.Items.Add((ToolbarItemBase) key);
  }

  /// <summary>Поиск команды меню по идентификатору команды.</summary>
  /// <param name="commandID">Идентификатор команды, которую необходимо найти</param>
  /// <returns>Связанная с идентификатором команда меню</returns>
  public MenuCommand FindCommand(CommandID commandID)
  {
    return this._menuCommands == null ? (MenuCommand) null : this._menuCommands[(object) commandID] as MenuCommand;
  }

  /// <summary>
  /// Вызывает команду меню или команду конструктора, соответствующую указанному идентификатору команды.
  /// </summary>
  /// <param name="commandID">Идентификатор команды, которую необходимо найти и выполнить</param>
  /// <returns>Значение true, если команда найдена и вызвана успешно</returns>
  public bool GlobalInvoke(CommandID commandID)
  {
    bool flag = false;
    MenuCommand command = this.FindCommand(commandID);
    if (command != null)
    {
      command.Invoke();
      flag = true;
    }
    return flag;
  }

  /// <summary>Удаляет из меню указанную стандартную команду меню.</summary>
  /// <param name="command">Команда меню</param>
  public void RemoveCommand(MenuCommand command)
  {
    this._menuCommands.Remove((object) command.CommandID);
  }

  /// <summary>
  /// Удаляет указанную команду конструктора из коллекции глобальных команд конструктора.
  /// </summary>
  /// <param name="verb">Команда конструктора</param>
  public void RemoveVerb(DesignerVerb verb)
  {
    if (verb == null)
      return;
    this.Verbs.Remove(verb);
    MenuButtonItem key = this._menuItemVerb[(object) verb] as MenuButtonItem;
    this._contextMenu.Items.Remove((ToolbarItemBase) key);
    this._menuItemVerb.Remove((object) key);
  }

  /// <summary>
  /// Отображает указанное контекстное меню в заданном месте.
  /// </summary>
  /// <param name="menuID">Идентификатор отображаемого контекстного меню</param>
  /// <param name="x">Координата по оси X, в которой отображается меню (в экранных координатах)</param>
  /// <param name="y">Координата по оси Y, в которой отображается меню (в экранных координатах)</param>
  public void ShowContextMenu(CommandID menuID, int x, int y)
  {
    if (!(this._host.GetService(typeof (ISelectionService)) is ISelectionService service))
      return;
    IComponent primarySelection = service.PrimarySelection as IComponent;
    if (this._lastSelectedComponent != primarySelection)
    {
      this.Reset();
      IDesigner designer = this._host.GetDesigner(primarySelection);
      if (designer != null)
      {
        foreach (DesignerVerb verb in (CollectionBase) designer.Verbs)
          this.AddVerb(verb);
      }
      this.AddGlobalMenu();
    }
    if (primarySelection is Control control)
    {
      Point screen = control.PointToScreen(new Point(0, 0));
      this._contextMenu.Show(control, new Point(x - screen.X, y - screen.Y));
    }
    this._lastSelectedComponent = primarySelection;
  }

  /// <summary>
  /// Возвращает коллекцию команд конструктора, доступных в настоящий момент.
  /// </summary>
  public DesignerVerbCollection Verbs { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  private void AddGlobalMenu()
  {
    this._contextMenu.Items.Add((ToolbarItemBase) this._linkTo);
    foreach (ToolbarItemBase key in this._menuItems.Keys)
      this._contextMenu.Items.Add(key);
  }

  /// <summary>
  /// 
  /// </summary>
  private void Reset()
  {
    if (this._contextMenu != null && this._contextMenu.Items != null && this._contextMenu.Items.Count > 0)
      this._contextMenu.Items.Clear();
    this.Verbs.Clear();
    this._menuItemVerb.Clear();
  }
}
