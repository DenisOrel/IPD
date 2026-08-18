// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemAction
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ControlFlow;
using Intermech.UI;
using System;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class PDMSystemAction : IAction
{
  protected readonly PDMSystem pdmSystem;
  protected readonly string actionName;
  protected readonly UIReportBuilder uiReportBuilder;

  public PDMSystemAction(PDMSystem pdmSystem, string actionName)
  {
    if (pdmSystem == null)
      throw new ArgumentNullException(nameof (pdmSystem));
    if (string.IsNullOrEmpty(actionName))
      throw new ArgumentException();
    this.pdmSystem = pdmSystem;
    this.actionName = actionName;
    this.uiReportBuilder = new UIReportBuilder();
  }

  public void Perform()
  {
    using (UIReport.CreateScope())
    {
      if (UIReport.Enabled)
        this.uiReportBuilder.ReportStart(this.actionName);
      try
      {
        this.DoPerform();
        if (!UIReport.Enabled)
          return;
        this.uiReportBuilder.ReportSuccess();
      }
      catch (Exception ex)
      {
        if (UIReport.Enabled)
          this.uiReportBuilder.ReportFail(ex);
        throw;
      }
      finally
      {
        this.DoCleanup();
      }
    }
  }

  protected virtual void DoPerform()
  {
  }

  protected virtual void DoCleanup()
  {
  }
}
