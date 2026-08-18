// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.MoveCompleteEventArgs
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// Аргументы для события завершения перемещения корневого хранилища
/// </summary>
[Serializable]
public class MoveCompleteEventArgs : FilesCopierEventArgs
{
  private RootDirectory sourсeDirectory;
  /// <summary>Путь к новому корневому хранилищу</summary>
  private string destPath;

  /// <summary>Копируемое корневое хранилище</summary>
  public string DesignationPath
  {
    get => this.destPath;
    set => this.destPath = value;
  }

  /// <summary>Копируемое корневое хранилище</summary>
  public RootDirectory SourсeDirectory
  {
    get => this.sourсeDirectory;
    set => this.sourсeDirectory = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="source"></param>
  /// <param name="destPath"></param>
  public MoveCompleteEventArgs(RootDirectory source, string destPath)
  {
    this.eventName = "MoveComplete";
    this.destPath = destPath;
    this.SourсeDirectory = source;
  }
}
