// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.TableChangedEventArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

internal class TableChangedEventArgs : EventArgs
{
  public TableChangedEventNames EventName;
  public IUserSession Session;

  public TableChangedEventArgs(TableChangedEventNames eventName, IUserSession session)
  {
    this.EventName = eventName;
    this.Session = session;
  }
}
