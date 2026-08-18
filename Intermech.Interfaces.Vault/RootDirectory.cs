// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.RootDirectory
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.IO;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// корневой каталог
/// (шкаф, путь к нему, максимальный размер)
/// </summary>
[Serializable]
public class RootDirectory
{
  /// <summary>путь к папке корневого каталога</summary>
  private string path;
  private short maxSize;
  /// <summary>
  /// гуид шкафа в IPS.
  /// имя глобального индекса корневого каталога
  /// </summary>
  private string guid;
  /// <summary>
  /// имя шкафа в IPS.
  /// имя папки корневого каталога.
  /// </summary>
  private string storageName;

  /// <summary>
  /// максимальный размер хранилища
  /// в процентах от свободного места на диске
  /// </summary>
  public short MaxSize
  {
    get => this.maxSize;
    set => this.maxSize = value;
  }

  /// <summary>guid хранилища</summary>
  public string Guid
  {
    get => this.guid;
    set => this.guid = value;
  }

  /// <summary>путь к хранилищу</summary>
  public string Path
  {
    get => this.path;
    set => this.path = value;
  }

  /// <summary>
  /// имя шкафа в IPS.
  /// имя папки корневого каталога.
  /// </summary>
  public string StorageName
  {
    get => this.storageName;
    set => this.storageName = value;
  }

  public RootDirectory(string path, short maxSize, string guid)
  {
    this.path = path;
    this.guid = guid;
    this.maxSize = maxSize;
    if (!Directory.Exists(path))
      return;
    this.storageName = new DirectoryInfo(path).Name;
  }

  public RootDirectory(string guid)
    : this(string.Empty, (short) 0, guid)
  {
  }

  public override bool Equals(object obj) => (obj as RootDirectory).Guid.Equals(this.Guid);

  public override int GetHashCode() => this.Guid.GetHashCode();
}
