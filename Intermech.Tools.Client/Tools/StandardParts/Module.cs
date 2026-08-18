// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Module
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Search;
using Intermech.Tools.StandardParts.Cadmech;
using Intermech.UI;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.StandardParts;

internal sealed class Module : InitializerModule
{
  private MenuButtonItem btImportCadmechLibrary;
  private MenuButtonItem btExportLibrary;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.CreateMainMenuCommands((IMainMenuService) ServicesManager.GetService(typeof (IMainMenuService)));
  }

  private void CreateMainMenuCommands(IMainMenuService mainMenuService)
  {
    this.CreateAllUsersCommands(mainMenuService);
    if (!Module.IsCurrentUserAdmin())
      return;
    this.CreateAdminCommands(mainMenuService);
  }

  private void CreateAllUsersCommands(IMainMenuService mainMenuService)
  {
    this.btExportLibrary = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_219"));
    this.btExportLibrary.ImageIndex = -1;
    this.btExportLibrary.CommandName = "ExportStandardLibrary";
    this.btExportLibrary.Click += new EventHandler(Module.ExportLibrary);
    mainMenuService.RegisterMenuItems(MainMenuItemSite.ExportImport, MainMenuItemPosition.Third, this.btExportLibrary);
  }

  private void CreateAdminCommands(IMainMenuService mainMenuService)
  {
    this.btImportCadmechLibrary = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_202"));
    this.btImportCadmechLibrary.ImageIndex = -1;
    this.btImportCadmechLibrary.CommandName = "ImportCadmechStandardLibrary";
    this.btImportCadmechLibrary.Click += new EventHandler(Module.ImportCadmechLibrary);
    this.btImportCadmechLibrary.BeginGroup = true;
    mainMenuService.RegisterMenuItemsGroup(MainMenuItemSite.ExportImport, MainMenuItemPosition.Default, false, this.btImportCadmechLibrary);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    this.DeleteMainMenuCommands((IMainMenuService) ServicesManager.GetService(typeof (IMainMenuService)));
  }

  private void DeleteMainMenuCommands(IMainMenuService mainMenuService)
  {
    if (this.btImportCadmechLibrary != null)
    {
      mainMenuService.UnregiterMenuItems(this.btImportCadmechLibrary);
      this.btImportCadmechLibrary.Dispose();
      this.btImportCadmechLibrary = (MenuButtonItem) null;
    }
    if (this.btExportLibrary == null)
      return;
    mainMenuService.UnregiterMenuItems(this.btExportLibrary);
    this.btExportLibrary.Dispose();
    this.btExportLibrary = (MenuButtonItem) null;
  }

  private static bool IsCurrentUserAdmin()
  {
    return (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
  }

  private static void ImportCadmechLibrary(object sender, EventArgs e)
  {
    using (ImportModelLibraryForm modelLibraryForm = new ImportModelLibraryForm())
    {
      int num = (int) modelLibraryForm.ShowDialog();
    }
  }

  private static void ExportLibrary(object sender, EventArgs e)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION
    };
    DataTable tbl;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      tbl = sessionKeeper.Session.GetObjectCollection(new Guid("cad015cb-306c-11d8-b4e9-00304f19f545")).Select(paramSet);
    if (tbl.Rows.Count <= 0)
      return;
    IFileVault fileVault = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    ProgressSinks.DialogService.Invoke(LocalizationHolder.rm.GetString("Tools.Client_219"), ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
    {
      IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, tbl.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        if (progressSink.IsCancelled)
          break;
        progressSink.SetState(Convert.ToString(row[1]));
        try
        {
          fileVault.PublishTree(Convert.ToInt64(row[0]), true, editorRule, (IFileArea) fileVault.WorkArea);
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
        progressUpdater.AddCompletedTasks(1);
      }
    }));
  }
}
