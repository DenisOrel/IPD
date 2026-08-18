// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.AdminUtilsCommandProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class AdminUtilsCommandProvider : InitializerModule
{
  private IMainMenuService mainMenuService;
  private List<MenuButtonItem> mainMenuItems;
  private ScriptCheckerIDCache idCache;
  private Func<CheckScriptStructureUIAction> createCheckScriptStructureUIAction;

  public AdminUtilsCommandProvider(
    IMainMenuService mainMenuService,
    ScriptCheckerIDCache idCache,
    Func<CheckScriptStructureUIAction> createCheckScriptStructureUIAction)
  {
    if (mainMenuService == null)
      throw new ArgumentNullException(nameof (mainMenuService));
    if (idCache == null)
      throw new ArgumentNullException(nameof (idCache));
    if (createCheckScriptStructureUIAction == null)
      throw new ArgumentNullException(nameof (createCheckScriptStructureUIAction));
    this.mainMenuService = mainMenuService;
    this.mainMenuItems = new List<MenuButtonItem>();
    this.idCache = idCache;
    this.createCheckScriptStructureUIAction = createCheckScriptStructureUIAction;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    MenuButtonItem menuButtonItem = new MenuButtonItem();
    this.mainMenuItems.Add(menuButtonItem);
    menuButtonItem.CommandName = ScriptCheckerMenuConsts.CheckScriptStructureCommandName;
    menuButtonItem.Text = ScriptCheckerMenuConsts.CheckScriptStructureDisplayName;
    menuButtonItem.ToolTipText = "Проверяет код всех сценариев C# и находит те сценарии, которые требуют преобразования под новую систему выполнения";
    menuButtonItem.BeginGroup = true;
    menuButtonItem.Click += new EventHandler(this.CheckAllScriptsStructureHandler);
    this.mainMenuService.RegisterMenuItems(MainMenuItemSite.AdministratorUtilities, MainMenuItemPosition.Last, menuButtonItem);
  }

  protected override void DoShutdown()
  {
    this.mainMenuService.UnregiterMenuItems(this.mainMenuItems.ToArray());
    foreach (IDisposable mainMenuItem in this.mainMenuItems)
      DisposeUtils.SafelyDispose(mainMenuItem);
    this.mainMenuItems.Clear();
    base.DoShutdown();
  }

  private void CheckAllScriptsStructureHandler(object sender, EventArgs e)
  {
    CheckScriptStructureUIAction structureUiAction = this.createCheckScriptStructureUIAction();
    structureUiAction.IsFullSystemCheck = true;
    structureUiAction.Execute(this.QueryAllScripts());
  }

  private List<ScriptInfo> QueryAllScripts()
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.CAPTION
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.GetObjectCollection(this.idCache.ScriptsBaseType.Id).SelectWithLocalObjects(paramSet);
    List<ScriptInfo> scriptInfoList = new List<ScriptInfo>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      scriptInfoList.Add(new ScriptInfo(Convert.ToInt64(row[0]), Convert.ToString(row[2])));
    return scriptInfoList;
  }
}
