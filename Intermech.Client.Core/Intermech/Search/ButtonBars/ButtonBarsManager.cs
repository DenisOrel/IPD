
// Type: Intermech.Search.ButtonBars.ButtonBarsManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.ContextMenu;
using Intermech.Search.Configuration;
using Intermech.Search.UI;
using Intermech.Search.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ButtonBars;

public sealed class ButtonBarsManager
{
  private const string ButtonBarToolBarTag = "TechCardBar";
  private LazyService<BarManager> _barManager = new LazyService<BarManager>();
  private LazyService<IButtonBarClientService> _buttonBarClientService = new LazyService<IButtonBarClientService>();
  private LazyService<ICategoryTypeIconService> _categoryTypeIcon = new LazyService<ICategoryTypeIconService>();
  private LazyService<ICommandClientService> _commandClientService = new LazyService<ICommandClientService>();
  private LazyService<ICommandManager> _commandManager = new LazyService<ICommandManager>();
  private LazyService<ICommandStatisticsRepository> _commandStatisticsRepository = new LazyService<ICommandStatisticsRepository>();
  private LazyService<INamedImageList> _namedImageList = new LazyService<INamedImageList>();
  private Random _random = new Random();

  public ButtonBarsManager()
  {
    this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged += new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    ServiceLocator.Get<IPluginManager>().PluginAdded += new PluginEventHandler(this.PluginManager_PluginAdded);
    ServiceLocator.Get<IStartupService>().StartupComplete += new EventHandler(this.StartupService_StartupComplete);
  }

  public void ResetButtonBars()
  {
    foreach (Intermech.Bars.ToolBar toolBar in this._barManager.Value.GetToolBars())
    {
      if (object.Equals(toolBar.Tag, (object) "TechCardBar"))
      {
        toolBar.LocationChanged -= new EventHandler(this.ToolBar_LocationChanged);
        toolBar.VisibleChanged -= new EventHandler(this.ToolBar_VisibleChanged);
        toolBar.Parent = (Control) null;
        this._barManager.Value.RemoveToolbar(toolBar);
      }
    }
    ButtonBar[] barsForCurrentUser = this._buttonBarClientService.Value.FindButtonBarsForCurrentUser();
    foreach (ButtonBar buttonBar in barsForCurrentUser)
    {
      Intermech.Bars.ToolBar barFromButtonBar = this.CreateToolBarFromButtonBar(buttonBar);
      ToolBarContainer toolBarContainer = this._barManager.Value.FindContainer(buttonBar.ContainerGuid) ?? this._barManager.Value.FindSuitableContainer(DockStyle.Top);
      barFromButtonBar.Parent = (Control) toolBarContainer;
      this._barManager.Value.AddToolbar(barFromButtonBar);
    }
    this.RegsiterUsedCommands(barsForCurrentUser);
  }

  private void ButtonBarClientService_ButtonBarsForCurrentUserChanged(object sender, EventArgs e)
  {
    this.ResetButtonBars();
  }

  private void PluginManager_PluginAdded(object sender, PluginEventArgs e)
  {
    this.ResetButtonBars();
  }

  private void StartupService_StartupComplete(object sender, EventArgs e) => this.ResetButtonBars();

  private void ToolBar_LocationChanged(object sender, EventArgs e)
  {
    Intermech.Bars.ToolBar toolBar = (Intermech.Bars.ToolBar) sender;
    ButtonBar[] barsForCurrentUser = this._buttonBarClientService.Value.FindButtonBarsForCurrentUser();
    ButtonBar buttonBar = ((IEnumerable<ButtonBar>) barsForCurrentUser).FirstOrDefault<ButtonBar>((Func<ButtonBar, bool>) (o => o.Guid == toolBar.Guid));
    if (buttonBar == null)
      return;
    buttonBar.ContainerGuid = this.GetToolBarContainerGuid(toolBar);
    buttonBar.DockLine = toolBar.DockLine;
    buttonBar.DockOffset = toolBar.DockOffset;
    this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged -= new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    try
    {
      this._buttonBarClientService.Value.SaveButtonBarsForCurrentUser(barsForCurrentUser, true);
    }
    finally
    {
      this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged += new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    }
  }

  private void ToolBar_VisibleChanged(object sender, EventArgs e)
  {
    Intermech.Bars.ToolBar toolBar = (Intermech.Bars.ToolBar) sender;
    ButtonBar[] barsForCurrentUser = this._buttonBarClientService.Value.FindButtonBarsForCurrentUser();
    ButtonBar buttonBar = ((IEnumerable<ButtonBar>) barsForCurrentUser).FirstOrDefault<ButtonBar>((Func<ButtonBar, bool>) (o => o.Guid == toolBar.Guid));
    if (buttonBar == null)
      return;
    buttonBar.Visible = toolBar.Visible;
    this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged -= new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    try
    {
      this._buttonBarClientService.Value.SaveButtonBarsForCurrentUser(barsForCurrentUser, true);
    }
    finally
    {
      this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged += new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    }
  }

  private void MenuItem_AfterPopup(object sender, EventArgs e)
  {
    MenuItemBase menuItemBase = (MenuItemBase) sender;
    MenuItemBase[] data = (MenuItemBase[]) ((ButtonBarsManager.ButtonBarButtonMenuItemTag) menuItemBase.Tag).Data;
    foreach (ToolbarItemBase toolbarItemBase in data)
      toolbarItemBase.Importance = ToolBarItemImportance.Medium;
    menuItemBase.Items.Clear();
    menuItemBase.Items.AddRange((ToolbarItemBase[]) data);
  }

  private void MenuItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    MenuItemBase menuItemBase1 = (MenuItemBase) sender;
    MenuItemBase[] array1 = menuItemBase1.Items.Cast<MenuItemBase>().ToArray<MenuItemBase>();
    ((ButtonBarsManager.ButtonBarButtonMenuItemTag) menuItemBase1.Tag).Data = (object) array1;
    MenuItemBase[] array2 = ((IEnumerable<MenuItemBase>) array1).Where<MenuItemBase>((Func<MenuItemBase, bool>) (o => o.Items.Count == 0 && o.Enabled || this.IsAnyChildrenEnabled(o))).ToArray<MenuItemBase>();
    if (CoreConfigurationOptions.UI_MinimizeContextMenu && (long) menuItemBase1.Items.Count > CoreConfigurationOptions.UI_MinimizedContextMenuCommandsCount)
    {
      string[] array3 = ((IEnumerable<MenuItemBase>) array2).Select<MenuItemBase, Tuple<string, CommandStatistics>>((Func<MenuItemBase, Tuple<string, CommandStatistics>>) (o => new Tuple<string, CommandStatistics>(o.CommandName, this.FindStatisticsForCommand(o)))).OrderByDescending<Tuple<string, CommandStatistics>, int>((Func<Tuple<string, CommandStatistics>, int>) (o => o.Item2.CurrentSessionUsesCount)).ThenByDescending<Tuple<string, CommandStatistics>, long>((Func<Tuple<string, CommandStatistics>, long>) (o => o.Item2.TotalUsesCount)).Take<Tuple<string, CommandStatistics>>((int) CoreConfigurationOptions.UI_MinimizedContextMenuCommandsCount).Select<Tuple<string, CommandStatistics>, string>((Func<Tuple<string, CommandStatistics>, string>) (o => o.Item1)).ToArray<string>();
      foreach (MenuItemBase menuItemBase2 in array2)
      {
        if (((IEnumerable<string>) array3).Contains<string>(menuItemBase2.CommandName))
          menuItemBase2.Importance = ToolBarItemImportance.Medium;
        else
          menuItemBase2.Importance = ToolBarItemImportance.Low;
      }
    }
    menuItemBase1.Items.Clear();
    menuItemBase1.Items.AddRange((ToolbarItemBase[]) array2);
  }

  private void MenuItem_VisibleChanged(object sender, EventArgs e)
  {
    ButtonItemBase buttonItem = (ButtonItemBase) sender;
    ButtonBar[] barsForCurrentUser = this._buttonBarClientService.Value.FindButtonBarsForCurrentUser();
    ButtonBar buttonBar = ((IEnumerable<ButtonBar>) barsForCurrentUser).FirstOrDefault<ButtonBar>((Func<ButtonBar, bool>) (o => o.Guid == buttonItem.ToolBar.Guid));
    if (buttonBar == null || buttonItem.Index >= buttonBar.Buttons.Count)
      return;
    buttonBar.Buttons[buttonItem.Index].Visible = buttonItem.Visible;
    this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged -= new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    try
    {
      this._buttonBarClientService.Value.SaveButtonBarsForCurrentUser(barsForCurrentUser, true);
    }
    finally
    {
      this._buttonBarClientService.Value.ButtonBarsForCurrentUserChanged += new EventHandler(this.ButtonBarClientService_ButtonBarsForCurrentUserChanged);
    }
  }

  private Intermech.Bars.ToolBar CreateToolBarFromButtonBar(ButtonBar buttonBar)
  {
    Intermech.Bars.ToolBar barFromButtonBar = new Intermech.Bars.ToolBar();
    barFromButtonBar.FullMenus = false;
    barFromButtonBar.Name = this._random.Next().ToString();
    barFromButtonBar.Guid = buttonBar.Guid;
    barFromButtonBar.Dock = DockStyle.Top;
    barFromButtonBar.DockLine = buttonBar.DockLine;
    barFromButtonBar.DockOffset = buttonBar.DockOffset;
    barFromButtonBar.Text = buttonBar.Name;
    barFromButtonBar.Visible = buttonBar.Visible;
    barFromButtonBar.Tag = (object) "TechCardBar";
    foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBar.Buttons)
    {
      ButtonItemBase buttonItemBase = button.Buttons.Count == 0 ? (ButtonItemBase) this.CreateButtonItemFromButtonBarButton(button) : (ButtonItemBase) this.CreateDropDownMenuItemFromButtonBarButton(button);
      barFromButtonBar.Items.Add((ToolbarItemBase) buttonItemBase);
    }
    barFromButtonBar.LocationChanged += new EventHandler(this.ToolBar_LocationChanged);
    barFromButtonBar.VisibleChanged += new EventHandler(this.ToolBar_VisibleChanged);
    return barFromButtonBar;
  }

  private DropDownMenuItem CreateDropDownMenuItemFromButtonBarButton(ButtonBarButton buttonBarButton)
  {
    ButtonBarsManager.ButtonBarButtonDropDownMenuItem fromButtonBarButton1 = new ButtonBarsManager.ButtonBarButtonDropDownMenuItem();
    fromButtonBarButton1.AfterPopup += new EventHandler(this.MenuItem_AfterPopup);
    fromButtonBarButton1.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.MenuItem_BeforePopup);
    fromButtonBarButton1.BeginGroup = buttonBarButton.BeginGroup;
    fromButtonBarButton1.CommandName = buttonBarButton.CommandName;
    if (buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Image || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText)
      fromButtonBarButton1.Image = this.GetImageForCommand(buttonBarButton.CommandName);
    fromButtonBarButton1.ShowText = buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Text;
    fromButtonBarButton1.Tag = (object) new ButtonBarsManager.ButtonBarButtonMenuItemTag(buttonBarButton);
    fromButtonBarButton1.Text = buttonBarButton.Text;
    fromButtonBarButton1.ToolTipText = buttonBarButton.ToolTipText;
    fromButtonBarButton1.Visible = buttonBarButton.Visible;
    fromButtonBarButton1.VisibleChanged += new EventHandler(this.MenuItem_VisibleChanged);
    foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBarButton.Buttons)
    {
      MenuButtonItem fromButtonBarButton2 = this.CreateMenuButtonItemFromButtonBarButton(button);
      fromButtonBarButton1.Items.Add((ToolbarItemBase) fromButtonBarButton2);
    }
    return (DropDownMenuItem) fromButtonBarButton1;
  }

  private MenuButtonItem CreateMenuButtonItemFromButtonBarButton(ButtonBarButton buttonBarButton)
  {
    MenuButtonItem fromButtonBarButton1 = new MenuButtonItem();
    fromButtonBarButton1.AfterPopup += new EventHandler(this.MenuItem_AfterPopup);
    fromButtonBarButton1.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.MenuItem_BeforePopup);
    fromButtonBarButton1.BeginGroup = buttonBarButton.BeginGroup;
    fromButtonBarButton1.CommandName = buttonBarButton.CommandName;
    if (buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Image || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText)
      fromButtonBarButton1.Image = this.GetImageForCommand(buttonBarButton.CommandName);
    fromButtonBarButton1.ShowText = buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Text;
    if (buttonBarButton.Buttons.Count != 0)
      fromButtonBarButton1.Tag = (object) new ButtonBarsManager.ButtonBarButtonMenuItemTag(buttonBarButton);
    fromButtonBarButton1.Text = buttonBarButton.Text;
    fromButtonBarButton1.ToolTipText = buttonBarButton.ToolTipText;
    if (buttonBarButton.Buttons.Count == 0)
      this._commandManager.Value.Add((ButtonItemBase) fromButtonBarButton1);
    foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBarButton.Buttons)
    {
      MenuButtonItem fromButtonBarButton2 = this.CreateMenuButtonItemFromButtonBarButton(button);
      fromButtonBarButton1.Items.Add((ToolbarItemBase) fromButtonBarButton2);
    }
    return fromButtonBarButton1;
  }

  private ButtonItem CreateButtonItemFromButtonBarButton(ButtonBarButton buttonBarButton)
  {
    ButtonBarsManager.ButtonBarButtonButtonItem fromButtonBarButton = new ButtonBarsManager.ButtonBarButtonButtonItem();
    fromButtonBarButton.BeginGroup = buttonBarButton.BeginGroup;
    fromButtonBarButton.CommandName = buttonBarButton.CommandName;
    if (buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Image || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText)
      fromButtonBarButton.Image = this.GetImageForCommand(buttonBarButton.CommandName);
    fromButtonBarButton.ShowText = buttonBarButton.DisplayType == ButtonBarButtonDisplayType.ImageAndText || buttonBarButton.DisplayType == ButtonBarButtonDisplayType.Text;
    fromButtonBarButton.Text = buttonBarButton.Text;
    fromButtonBarButton.ToolTipText = buttonBarButton.ToolTipText;
    fromButtonBarButton.Visible = buttonBarButton.Visible;
    fromButtonBarButton.VisibleChanged += new EventHandler(this.MenuItem_VisibleChanged);
    this._commandManager.Value.Add((ButtonItemBase) fromButtonBarButton);
    return (ButtonItem) fromButtonBarButton;
  }

  private Image GetImageForCommand(string commandName)
  {
    MenuTemplateNode templateNodeForCommand = ContextMenuHelper.GetContextMenuTemplateNodeForCommand(commandName);
    if (templateNodeForCommand != null)
    {
      if (templateNodeForCommand.Image != null)
        return templateNodeForCommand.Image;
      if (templateNodeForCommand.ImageIndex >= 0)
      {
        if (templateNodeForCommand.ImageListSource == ImageListSource.CategoryImageList)
          return this._categoryTypeIcon.Value.ImageList.Images[templateNodeForCommand.ImageIndex];
        if (templateNodeForCommand.ImageListSource == ImageListSource.NamedImageList)
          return this._namedImageList.Value.ImageList.Images[templateNodeForCommand.ImageIndex];
      }
    }
    return (Image) null;
  }

  private bool IsAnyChildrenEnabled(MenuItemBase menuItem)
  {
    return menuItem.Items.Cast<MenuButtonItem>().Any<MenuButtonItem>((Func<MenuButtonItem, bool>) (o => o.Enabled || this.IsAnyChildrenEnabled((MenuItemBase) o)));
  }

  private CommandStatistics FindStatisticsForCommand(MenuItemBase menuItem)
  {
    return menuItem.Items.Count == 0 ? this._commandStatisticsRepository.Value.Find(menuItem.CommandName) ?? new CommandStatistics() : menuItem.Items.Cast<MenuItemBase>().Select<MenuItemBase, CommandStatistics>((Func<MenuItemBase, CommandStatistics>) (o => this.FindStatisticsForCommand(o))).Where<CommandStatistics>((Func<CommandStatistics, bool>) (o => o != null)).OrderByDescending<CommandStatistics, int>((Func<CommandStatistics, int>) (o => o.CurrentSessionUsesCount)).ThenByDescending<CommandStatistics, long>((Func<CommandStatistics, long>) (o => o.TotalUsesCount)).FirstOrDefault<CommandStatistics>() ?? new CommandStatistics();
  }

  private void RegsiterUsedCommands(ButtonBar[] buttonBars)
  {
    this._commandClientService.Value.ClearUsedCommands();
    foreach (ButtonBar buttonBar in buttonBars)
    {
      foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBar.Buttons)
        this.RegisterUsedCommands(button);
    }
  }

  private void RegisterUsedCommands(ButtonBarButton buttonBarButton)
  {
    this._commandClientService.Value.RegisterUsedCommand(buttonBarButton.CommandName);
    foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBarButton.Buttons)
      this.RegisterUsedCommands(button);
  }

  private Guid GetToolBarContainerGuid(Intermech.Bars.ToolBar toolBar)
  {
    return !(toolBar.Parent is ToolBarContainer parent) ? Guid.Empty : parent.Guid;
  }

  private sealed class ButtonBarButtonMenuItemTag
  {
    public ButtonBarButtonMenuItemTag(ButtonBarButton buttonBarButton)
    {
      this.ButtonBarButton = buttonBarButton != null ? buttonBarButton : throw new ArgumentNullException(nameof (buttonBarButton));
    }

    public ButtonBarButton ButtonBarButton { get; private set; }

    public object Data { get; set; }
  }

  private sealed class ButtonBarButtonDropDownMenuItem : DropDownMenuItem
  {
    public event EventHandler VisibleChanged;

    public override bool Visible
    {
      get => base.Visible;
      set
      {
        base.Visible = value;
        this.OnVisibleChanged();
      }
    }

    private void OnVisibleChanged()
    {
      EventHandler visibleChanged = this.VisibleChanged;
      if (visibleChanged == null)
        return;
      visibleChanged((object) this, EventArgs.Empty);
    }
  }

  private sealed class ButtonBarButtonButtonItem : ButtonItem
  {
    public event EventHandler VisibleChanged;

    public override bool Visible
    {
      get => base.Visible;
      set
      {
        base.Visible = value;
        this.OnVisibleChanged();
      }
    }

    private void OnVisibleChanged()
    {
      EventHandler visibleChanged = this.VisibleChanged;
      if (visibleChanged == null)
        return;
      visibleChanged((object) this, EventArgs.Empty);
    }
  }
}
