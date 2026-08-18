// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.MoveErrorEventArgs
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// Аргументы для события возникновения ошибки при перемещении корневого хранилища
/// </summary>
[Serializable]
public class MoveErrorEventArgs : FilesCopierEventArgs
{
  private RootDirectory source;
  private Exception error;

  public Exception Error => this.error;

  public RootDirectory Source => this.source;

  public MoveErrorEventArgs(RootDirectory source, Exception error)
  {
    this.eventName = "MoveError";
    this.source = source;
    this.error = error;
  }
}
