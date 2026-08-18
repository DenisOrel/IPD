// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.FilesCopierEventArgs
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

[Serializable]
public abstract class FilesCopierEventArgs : EventArgs
{
  protected string eventName;

  public string EventName => this.eventName;
}
