// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.RemoteMessage
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class RemoteMessage
{
  public string Message { get; private set; }

  public string AdditionalData { get; private set; }

  public RemoteMessage()
  {
    this.Message = string.Empty;
    this.AdditionalData = string.Empty;
  }

  public RemoteMessage(string message, string addData)
  {
    this.Message = message;
    this.AdditionalData = addData;
  }

  public RemoteMessage(Exception ex)
  {
    this.Message = ex.Message;
    this.AdditionalData = ex.StackTrace;
  }
}
