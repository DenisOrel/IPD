// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ApplyEventArgs
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class ApplyEventArgs : EventArgs
{
  private object _data;

  public ApplyEventArgs(object data) => this._data = data;

  public object Data => this._data;
}
