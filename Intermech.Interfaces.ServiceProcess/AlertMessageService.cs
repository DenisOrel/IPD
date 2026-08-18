// Decompiled with JetBrains decompiler
// Type: Intermech.ApplicationModel.AlertMessageService
// Assembly: Intermech.Interfaces.ServiceProcess, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B7815DB0-27BA-4236-9871-0983141542BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.ServiceProcess.dll

using System;

#nullable disable
namespace Intermech.ApplicationModel;

internal sealed class AlertMessageService : AlertMessageServiceBase
{
  private IApplicationEventLogService eventLogService;

  public AlertMessageService(IApplicationEventLogService eventLogService)
  {
    this.eventLogService = eventLogService != null ? eventLogService : throw new ArgumentNullException(nameof (eventLogService));
  }

  protected override void DoShowMessage(
    string caption,
    string message,
    AlertMessageType messageType)
  {
    base.DoShowMessage(caption, message, messageType);
    this.eventLogService.DefaultLog.Write(this.CombineCaptionWithMessage(caption, message), this.MessageTypeToEventLogItemType(messageType));
  }
}
