// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.CheckScriptStructureUIAction
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class CheckScriptStructureUIAction
{
  private ScriptCheckerService scriptCheckerService;
  private HtmlReportGenerator reportGenerator;
  private HtmlReportWriter reportWriter;
  private INotificationService notificationService;
  private ILaunchActionService launchService;

  public CheckScriptStructureUIAction(
    ScriptCheckerService scriptCheckerService,
    HtmlReportGenerator reportGenerator,
    HtmlReportWriter reportWriter,
    INotificationService notificationService,
    ILaunchActionService launchService)
  {
    if (scriptCheckerService == null)
      throw new ArgumentNullException(nameof (scriptCheckerService));
    if (reportGenerator == null)
      throw new ArgumentNullException(nameof (reportGenerator));
    if (reportWriter == null)
      throw new ArgumentNullException(nameof (reportWriter));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (launchService == null)
      throw new ArgumentNullException(nameof (launchService));
    this.scriptCheckerService = scriptCheckerService;
    this.reportGenerator = reportGenerator;
    this.reportWriter = reportWriter;
    this.notificationService = notificationService;
    this.launchService = launchService;
  }

  public bool IsFullSystemCheck { get; set; }

  public void Execute(List<ScriptInfo> scripts)
  {
    if (scripts == null)
      throw new ArgumentNullException(nameof (scripts));
    try
    {
      this.ExecuteInternal(scripts);
    }
    catch
    {
      throw;
    }
  }

  private void ExecuteInternal(List<ScriptInfo> scripts)
  {
    List<ScriptCheckResult> resultList = this.scriptCheckerService.CanExecuteInSandbox((ICollection<ScriptInfo>) scripts);
    List<ScriptCheckResult> all = resultList.FindAll((Predicate<ScriptCheckResult>) (item => !item.IsValid));
    if (all.Count == 0)
    {
      int num1 = (int) MessageBox.Show("Все сценарии C# успешно прошли проверку.", ScriptCheckerMenuConsts.CheckScriptStructureResultBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      DateTime now = DateTime.Now;
      string reportName = this.IsFullSystemCheck ? "Отчет о полной проверке кода сценариев C#" : $"Отчет о выборочной проверке кода сценариев C# от {now:f}";
      string report = this.reportGenerator.CreateReport(now, reportName, resultList);
      UpdatedDBObjectInfo orUpdateReport = this.reportWriter.CreateOrUpdateReport(now, reportName, report);
      this.FireUIChanges(orUpdateReport);
      int num2 = (int) MessageBox.Show($"Найдено {all.Count} сценариев C#, которые требуют преобразования. Результаты проверки были сохранены в базе данных IPS в документе типа HTML-отчет.", ScriptCheckerMenuConsts.CheckScriptStructureResultBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.launchService.Launch(new LaunchParams(LaunchType.View, orUpdateReport.ObjectId, orUpdateReport.ObjectTypeId, VersionsRuleSources.GetEditorRule()));
    }
  }

  private void FireUIChanges(UpdatedDBObjectInfo info)
  {
    if (info.IsNew)
      this.notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", info.ObjectId, info.ObjectTypeId));
    else
      this.notificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", info.ObjectId, info.ObjectTypeId));
  }
}
