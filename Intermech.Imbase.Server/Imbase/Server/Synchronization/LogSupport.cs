// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.LogSupport
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class LogSupport : ILogSupport
{
  private bool _extendedLog;
  private List<Tuple<MessageType, string>> _logData = new List<Tuple<MessageType, string>>();

  public LogSupport(bool extendedLog) => this._extendedLog = extendedLog;

  public void AddMessage(MessageType messageType, string message)
  {
    this._logData.Add(new Tuple<MessageType, string>(messageType, message));
  }

  public string GetLog()
  {
    return string.Join(Environment.NewLine, this._extendedLog ? this._logData.Select<Tuple<MessageType, string>, string>((Func<Tuple<MessageType, string>, string>) (x => x.Item2)).ToArray<string>() : this._logData.Where<Tuple<MessageType, string>>((Func<Tuple<MessageType, string>, bool>) (x => x.Item1 == MessageType.Normal)).Select<Tuple<MessageType, string>, string>((Func<Tuple<MessageType, string>, string>) (x => x.Item2)).ToArray<string>());
  }
}
