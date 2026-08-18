// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ToolsControlPanelModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ToolsControlPanelModule : InitializerModule
{
  private const string ViewMenuCommandName = "View";
  private MenuButtonItem mainMenuButton;
  private ToolsControlPanelForm serviceWindow;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.CreateServiceWindow();
    this.CreateViewButton();
    this.CreateCommonControls();
    this.Create3DCadControls();
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    ServicesManager.RemoveService(typeof (IToolsControlPanel));
    if (this.mainMenuButton != null)
    {
      this.mainMenuButton.Dispose();
      this.mainMenuButton = (MenuButtonItem) null;
    }
    if (this.serviceWindow == null)
      return;
    this.serviceWindow.Dispose();
    this.serviceWindow = (ToolsControlPanelForm) null;
  }

  private void CreateServiceWindow()
  {
    this.serviceWindow = new ToolsControlPanelForm();
    this.serviceWindow.Manager = ServiceUtils.GetService<DockManager>((object) ServicesManager.ServiceContainer, false);
    ServicesManager.AddService(typeof (IToolsControlPanel), (object) this.serviceWindow);
  }

  private void CreateViewButton()
  {
    INamedImageList service1 = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, false);
    int imageIndex = service1 == null ? -1 : service1.ImageIndex("imgAdminPane");
    IMainMenuService service2 = ServicesManager.GetService(typeof (IMainMenuService)) as IMainMenuService;
    this.mainMenuButton = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_298"), new EventHandler(this.OpenWindow), imageIndex);
    service2?.RegisterMenuItems(MainMenuItemSite.ViewTop, MainMenuItemPosition.Default, this.mainMenuButton);
  }

  private MenuBarItem FindMenu(string commandName)
  {
    return ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, false)?.MenuBar.FindMenuBar(commandName);
  }

  private void OpenWindow(object sender, EventArgs e) => this.serviceWindow.Open();

  private void CreateCommonControls()
  {
    CheckBox checkBox1 = new CheckBox();
    checkBox1.Text = LocalizationHolder.rm.GetString("SR_299");
    checkBox1.DataBindings.Add(new Binding("Checked", (object) FileVars.SoftMode, "GlobalValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox1.AutoSize = true;
    CheckBox checkBox2 = new CheckBox();
    checkBox2.Text = LocalizationHolder.rm.GetString("SR_300");
    checkBox2.DataBindings.Add(new Binding("Checked", (object) FileVars.ExtendedMode, "GlobalValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox2.AutoSize = true;
    CheckBox checkBox3 = new CheckBox();
    checkBox3.Text = LocalizationHolder.rm.GetString("SR_302");
    checkBox3.DataBindings.Add(new Binding("Checked", (object) IntegratorVars.ConserveAppResources, "GlobalValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox3.AutoSize = true;
    this.serviceWindow.AddItem(string.Empty, (Control) checkBox1);
    this.serviceWindow.AddItem(string.Empty, (Control) checkBox2);
    this.serviceWindow.AddItem(string.Empty, (Control) checkBox3);
  }

  private void Create3DCadControls()
  {
    CheckBox checkBox1 = new CheckBox();
    checkBox1.Text = LocalizationHolder.rm.GetString("SR_303");
    checkBox1.DataBindings.Add(new Binding("Checked", (object) ExtendedSaveOnCheckinSettings.Instance.CreateArticles, "RawValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox1.AutoSize = true;
    CheckBox checkBox2 = new CheckBox();
    checkBox2.Text = LocalizationHolder.rm.GetString("SR_304");
    checkBox2.DataBindings.Add(new Binding("Checked", (object) ExtendedSaveOnCheckinSettings.Instance.UpdateArticles, "RawValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox2.AutoSize = true;
    CheckBox checkBox3 = new CheckBox();
    checkBox3.Text = LocalizationHolder.rm.GetString("SR_305");
    checkBox3.DataBindings.Add(new Binding("Checked", (object) ExtendedSaveOnCheckinSettings.Instance.RecalculateMass, "RawValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox3.AutoSize = true;
    CheckBox checkBox4 = new CheckBox();
    checkBox4.Text = "Не импортировать ассоциативные зависимости моделей";
    checkBox4.DataBindings.Add(new Binding("Checked", (object) CADIntegratorVars.DontImportAssociativeDependencies, "GlobalValue", false, DataSourceUpdateMode.OnPropertyChanged));
    checkBox4.AutoSize = true;
    this.serviceWindow.AddItem(LocalizationHolder.rm.GetString("SR_306"), (Control) checkBox1);
    this.serviceWindow.AddItem(LocalizationHolder.rm.GetString("SR_306"), (Control) checkBox2);
    this.serviceWindow.AddItem(LocalizationHolder.rm.GetString("SR_306"), (Control) checkBox3);
    this.serviceWindow.AddItem(LocalizationHolder.rm.GetString("SR_306"), (Control) checkBox4);
  }
}
