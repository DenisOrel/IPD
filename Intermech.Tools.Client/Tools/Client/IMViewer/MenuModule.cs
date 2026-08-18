// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.MenuModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class MenuModule : InitializerModule
{
  private IFactory navigatorFactory;
  private Func<MenuCommandsProvider> commandsProviderFactory;
  private IToolsControlPanel toolsControlPanel;
  private MenuCommandsFlags imviewerControlFlags;
  private MenuTemplateNode updateIMVFilesCommandNode;
  private MenuTemplateNode updateIMVFilesRecursiveCommandNode;

  public MenuModule(
    IFactory navigatorFactory,
    Func<MenuCommandsProvider> commandsProviderFactory,
    IToolsControlPanel toolsControlPanel,
    MenuCommandsFlags imvControlFlags)
  {
    this.navigatorFactory = navigatorFactory;
    this.commandsProviderFactory = commandsProviderFactory;
    this.toolsControlPanel = toolsControlPanel;
    this.imviewerControlFlags = imvControlFlags;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddCommandItemsToContextMenuTemplate();
    this.AddCommandsProviderToNavigator();
    this.AddExperimentalControls();
  }

  private void AddCommandItemsToContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName];
      if (menuTemplateNode == null)
        return;
      this.updateIMVFilesCommandNode = new MenuTemplateNode(MenuConsts.UpdateIMVFilesCommandName, MenuConsts.UpdateIMVFilesDisplayName, -1, 28, 30);
      this.updateIMVFilesRecursiveCommandNode = new MenuTemplateNode(MenuConsts.UpdateIMVFilesRecursiveCommandName, MenuConsts.UpdateIMVFilesRecursiveDisplayName, -1, 28, 30);
      menuTemplateNode.Nodes.Add(this.updateIMVFilesCommandNode);
      menuTemplateNode.Nodes.Add(this.updateIMVFilesRecursiveCommandNode);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  private void AddCommandsProviderToNavigator()
  {
    this.navigatorFactory.AddCommandsProvider(1, (ICommandsProvider) this.commandsProviderFactory());
  }

  private void AddExperimentalControls()
  {
    CheckBox checkBox = new CheckBox();
    checkBox.Text = "Режим предварительного открытия документов";
    checkBox.DataBindings.Add(new Binding("Checked", (object) this.imviewerControlFlags, "PreOpenDocumentsMode", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox.AutoSize = true;
    this.toolsControlPanel.AddItem("Обновление файлов IMViewer", (Control) checkBox);
  }

  protected override void DoShutdown()
  {
    this.RemoveCommandItemsFromContextMenuTemplate();
    base.DoShutdown();
  }

  private void RemoveCommandItemsFromContextMenuTemplate()
  {
    MenuTemplate contextMenuTemplate = this.navigatorFactory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode menuTemplateNode = contextMenuTemplate[Intermech.Tools.Client.IntegratorsContextMenu.MenuConsts.IntegratorsMenuName];
      if (menuTemplateNode == null)
        return;
      if (this.updateIMVFilesCommandNode != null)
      {
        menuTemplateNode.Nodes.Remove(this.updateIMVFilesCommandNode);
        this.updateIMVFilesCommandNode = (MenuTemplateNode) null;
      }
      if (this.updateIMVFilesRecursiveCommandNode == null)
        return;
      menuTemplateNode.Nodes.Remove(this.updateIMVFilesRecursiveCommandNode);
      this.updateIMVFilesRecursiveCommandNode = (MenuTemplateNode) null;
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }
}
