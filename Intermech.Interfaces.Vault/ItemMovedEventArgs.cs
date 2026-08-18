// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.ItemMovedEventArgs
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// Аргументы для события перемещения файла / создания папки
/// </summary>
[Serializable]
public class ItemMovedEventArgs : FilesCopierEventArgs
{
  private string source;
  private string destination;
  private bool isFolder;

  public ItemMovedEventArgs(string source, string destination, bool isFolder)
  {
    this.eventName = "ItemMoved";
    this.source = source;
    this.destination = destination;
    this.isFolder = isFolder;
  }

  public string Source => this.source;

  public string Destination => this.destination;

  public bool IsFolder => this.isFolder;
}
