// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.Module
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal sealed class Module : InitializerModule
{
  internal static string Path = "";
  internal static int ObjTypeId;
  internal static List<int> AttrIDsList = new List<int>();
  private MenuButtonItem _btSubmenu;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.InstallMainMenuButtons();
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this._btSubmenu == null)
      return;
    this._btSubmenu.Dispose();
    this._btSubmenu = (MenuButtonItem) null;
  }

  private void InstallMainMenuButtons()
  {
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service))
      return;
    MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_223"));
    menuButtonItem.CommandName = Consts.CommandName;
    menuButtonItem.BeginGroup = false;
    this._btSubmenu = menuButtonItem;
    this._btSubmenu.Click += new EventHandler(this.LoadObjects);
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      this._btSubmenu
    };
    service.RegisterMenuItemsGroup(MainMenuItemSite.ExportImport, MainMenuItemPosition.Default, false, menuButtonItemArray);
  }

  private void LoadObjects(object sender, EventArgs e)
  {
    using (new SessionKeeper())
    {
      if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
        throw new Exception("IBackgroundTaskView service not found");
      AdvancedImportSettingsFrm importSettingsFrm = new AdvancedImportSettingsFrm(ApplicationServices.Container.GetService<ICurrentUserAndRole>().IsAdmin);
      if (importSettingsFrm.ShowDialog() != DialogResult.OK)
        return;
      DataTable resultDataTable = importSettingsFrm.GetResultDataTable();
      if (resultDataTable.Rows.Count <= 0)
        return;
      ImportDataBackGroundTask task = new ImportDataBackGroundTask(resultDataTable)
      {
        Name = string.Format(LocalizationHolder.rm.GetString("Tools.Client_272"), (object) importSettingsFrm.FileName)
      };
      service.AddTask((IBackgroundTask) task);
      task.Resume();
    }
  }
}
