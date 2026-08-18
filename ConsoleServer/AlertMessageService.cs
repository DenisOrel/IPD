// Decompiled with JetBrains decompiler
// Type: ConsoleServer.AlertMessageService
// Assembly: ConsoleServer, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A2572001-4A8A-44C7-AECE-87B2080D6C9F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\ConsoleServer.exe

using Intermech.ApplicationModel;
using System;

#nullable disable
namespace ConsoleServer;

internal sealed class AlertMessageService : AlertMessageServiceBase
{
  private IApplicationEventLogService eventLogService;
  private IConsoleService consoleService;

  public AlertMessageService(
    IApplicationEventLogService eventLogService,
    IConsoleService consoleService)
  {
    if (eventLogService == null)
      throw new ArgumentNullException(nameof (eventLogService));
    if (consoleService == null)
      throw new ArgumentNullException(nameof (consoleService));
    this.eventLogService = eventLogService;
    this.consoleService = consoleService;
  }

  protected override void DoShowMessage(
    string caption,
    string message,
    AlertMessageType messageType)
  {
    base.DoShowMessage(caption, message, messageType);
    string message1 = this.CombineCaptionWithMessage(caption, message);
    this.eventLogService.DefaultLog.Write(message1, this.MessageTypeToEventLogItemType(messageType));
    this.consoleService.WriteLine($"[Особые события] {message1}", this.MessageTypeToConsoleColor(messageType));
  }

  private ConsoleColor MessageTypeToConsoleColor(AlertMessageType messageType)
  {
    if (messageType == AlertMessageType.Warning)
      return ConsoleColor.Yellow;
    return messageType == AlertMessageType.Error ? ConsoleColor.Red : ConsoleColor.Green;
  }
}
