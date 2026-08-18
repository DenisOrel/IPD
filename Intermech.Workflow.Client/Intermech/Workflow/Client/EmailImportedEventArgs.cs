// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailImportedEventArgs
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.Workflow.Client;

public class EmailImportedEventArgs : NotificationEventArgs
{
  private string _email;

  public EmailImportedEventArgs(string eventName, string email)
    : base(eventName)
  {
    this._email = email;
  }

  public string Email => this._email;
}
