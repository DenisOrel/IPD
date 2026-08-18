// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.Module
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Search;
using Intermech.Settings;
using Intermech.UI.PropertyPages;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class Module : InitializerModule
{
  private MenuButtonItem btSubmenu;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.InstallMvpViews();
    this.InstallArchiveParametersEditor();
    this.InstallMainMenuButtons();
  }

  private void InstallMvpViews()
  {
    MvpContext.ViewService.RegisterView(typeof (IBackgroundCommandView), typeof (BackgroundCommandWindow));
  }

  private void InstallArchiveParametersEditor()
  {
    ArchiveParametersEditorModel model = new ArchiveParametersEditorModel();
    ArchiveParametersControl parametersControl = new ArchiveParametersControl();
    ArchiveParametersPresenter presenter = new ArchiveParametersPresenter();
    presenter.Model = model;
    presenter.View = (IArchiveParametersView) parametersControl;
    PropertyPageMvpAdapter page = new PropertyPageMvpAdapter("Исходный архив предприятия", (IPropertyPageMvpModel) model, (IView) parametersControl, (IPropertyPageMvpPresenter) presenter);
    ServiceUtils.GetService<IPropertyPagesService>((object) ServicesManager.ServiceContainer, true).AddPage(Path.Combine("Файловое хранилище", page.PageName), (IPropertyPage) page);
  }

  private void InstallMainMenuButtons()
  {
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service))
      return;
    this.btSubmenu = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_250"));
    this.btSubmenu.CommandName = "EnterpriseArchive";
    this.btSubmenu.BeginGroup = true;
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_91"));
    menuButtonItem1.CommandName = "EnterpriseArchive.ImportFiles";
    menuButtonItem1.Click += new EventHandler(Module.ImportFilesClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem1);
    MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_284"));
    menuButtonItem2.CommandName = "EnterpriseArchive.AddFileToQueue";
    menuButtonItem2.Click += new EventHandler(Module.AddFileToQueueClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem2);
    MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_285"));
    menuButtonItem3.CommandName = "EnterpriseArchive.ShowQueue";
    menuButtonItem3.Click += new EventHandler(Module.ShowQueueClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem3);
    MenuButtonItem menuButtonItem4 = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_286"));
    menuButtonItem4.CommandName = "EnterpriseArchive.ClearQueue";
    menuButtonItem4.Click += new EventHandler(Module.ClearQueueClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem4);
    MenuButtonItem menuButtonItem5 = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_287"));
    menuButtonItem5.BeginGroup = true;
    menuButtonItem5.CommandName = "EnterpriseArchive.ShowStatistics";
    menuButtonItem5.Click += new EventHandler(Module.ShowStatisticsClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem5);
    MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("SR_288"));
    menuButtonItem6.CommandName = "EnterpriseArchive.ShowArchiveState";
    menuButtonItem6.Click += new EventHandler(Module.ShowArchiveStateClick);
    this.btSubmenu.Items.Add((ToolbarItemBase) menuButtonItem6);
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      this.btSubmenu
    };
    service.RegisterMenuItemsGroup(MainMenuItemSite.ExportImport, MainMenuItemPosition.Default, false, menuButtonItemArray);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.btSubmenu != null)
    {
      this.btSubmenu.Dispose();
      this.btSubmenu = (MenuButtonItem) null;
    }
    this.UninstallMvpViews();
  }

  private void UninstallMvpViews()
  {
    MvpContext.ViewService.UnregisterView(typeof (IBackgroundCommandView));
  }

  private static void ImportFilesClick(object sender, EventArgs e)
  {
    MvpContext.ViewService.ShowModal((IPresenter) new ImportFilesCommand());
  }

  private static void AddFileToQueueClick(object sender, EventArgs e)
  {
    MvpContext.ViewService.ShowModal((IPresenter) new AddFileToQueueCommand());
  }

  private static void ShowQueueClick(object sender, EventArgs e)
  {
    MvpContext.ViewService.ShowModal((IPresenter) new ViewQueueFilePresenter());
  }

  private static void ClearQueueClick(object sender, EventArgs e)
  {
    new ClearQueueCommand().Perform();
  }

  private static void ShowStatisticsClick(object sender, EventArgs e)
  {
    MvpContext.ViewService.ShowModal((IPresenter) new FileStatisticsPresenter()
    {
      RootDirectory = (string) (ValueCell<string>) ArchiveParameters.Common.Location
    });
  }

  private static void ShowArchiveStateClick(object sender, EventArgs e)
  {
    MvpContext.ViewService.ShowModal((IPresenter) new FileStatesPresenter()
    {
      RootDirectory = (string) (ValueCell<string>) ArchiveParameters.Common.Location
    });
  }
}
