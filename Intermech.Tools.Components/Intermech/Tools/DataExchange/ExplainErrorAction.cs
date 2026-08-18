// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ExplainErrorAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Tools.DataExchange;

internal sealed class ExplainErrorAction : IAction
{
  private readonly IAction serverAction;
  private readonly SectionEntity workItem;
  private readonly string ownerName;

  public ExplainErrorAction(IAction serverAction, SectionEntity workItem)
  {
    if (serverAction == null)
      throw new ArgumentNullException(nameof (serverAction));
    if (workItem == null)
      throw new ArgumentNullException(nameof (workItem));
    this.serverAction = serverAction;
    this.workItem = workItem;
    this.ownerName = DisplaySection.GetQualifiedName(workItem);
  }

  public void Perform()
  {
    using (UIReport.CreateLogicalOperation((object) this.workItem))
    {
      try
      {
        this.serverAction.Perform();
      }
      catch (ObjectAlreadyExists ex)
      {
        SectionEntity byObjectId = ObjectSection.FindByObjectId((CaptureChangesDatabase) this.workItem.Database, ex.ObjectID, false);
        StringBuilder stringBuilder;
        if (byObjectId != null)
        {
          stringBuilder = new StringBuilder(256 /*0x0100*/);
          stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Tools.Components_500"), (object) DisplaySection.GetQualifiedName(byObjectId), (object) this.ownerName, (object) ex.AttributeName);
        }
        else
          stringBuilder = this.CreateErrorMessage((KernelException) ex);
        KernelException kernelException = new KernelException(stringBuilder.ToString(), (Exception) ex);
        if (UIReport.Enabled)
          UIReport.ReportEvent(kernelException.Message, TraceLevel.Error);
        throw kernelException;
      }
      catch (KernelException ex)
      {
        KernelException kernelException = new KernelException(this.CreateErrorMessage(ex).ToString(), (Exception) ex);
        if (UIReport.Enabled)
          UIReport.ReportEvent(kernelException.Message, TraceLevel.Error);
        throw kernelException;
      }
      catch (Exception ex)
      {
        if (UIReport.Enabled)
          UIReport.ReportEvent(ex.Message, TraceLevel.Error);
        throw;
      }
    }
  }

  private StringBuilder CreateErrorMessage(KernelException x)
  {
    StringBuilder errorMessage = new StringBuilder(256 /*0x0100*/);
    errorMessage.AppendFormat(LocalizationHolder.rm.GetString("Tools.Components_501"), (object) this.ownerName);
    errorMessage.Append(' ');
    errorMessage.Append(x.Message);
    return errorMessage;
  }

  public override string ToString() => this.serverAction.ToString();
}
