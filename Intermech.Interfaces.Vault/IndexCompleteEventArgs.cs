// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IndexCompleteEventArgs
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// Аргументы для события завершения индексации перемещаемых файлов
/// </summary>
[Serializable]
public class IndexCompleteEventArgs : FilesCopierEventArgs
{
  private int filesCount;
  private int foldersCount;

  public int FoldersCount
  {
    get => this.foldersCount;
    set => this.foldersCount = value;
  }

  public int FilesCount
  {
    get => this.filesCount;
    set => this.filesCount = value;
  }

  public IndexCompleteEventArgs(int files, int folders)
  {
    this.eventName = "IndexComplete";
    this.filesCount = files;
    this.foldersCount = folders;
  }
}
