// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectFileEntry
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.IO;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectFileEntry
{
  private readonly int valueIndex;
  private readonly string originalName;
  private readonly FileTypes valueFileType;
  private string newName;
  private DBObjectFileContent content;

  public DBObjectFileEntry(int valueIndex, FileTypes valueFileType, string originalName)
  {
    if (valueIndex < 0)
      throw new ArgumentOutOfRangeException(nameof (valueIndex));
    if (string.IsNullOrEmpty(originalName))
      throw new ArgumentException("Не задано имя файла объекта IPS.", nameof (originalName));
    this.valueIndex = valueIndex;
    this.valueFileType = valueFileType;
    this.originalName = originalName;
    this.newName = originalName;
  }

  public int ValueIndex
  {
    [DebuggerStepThrough] get => this.valueIndex;
  }

  public FileTypes ValueFileType
  {
    [DebuggerStepThrough] get => this.valueFileType;
  }

  public string OriginalName
  {
    [DebuggerStepThrough] get => this.originalName;
  }

  public string NewName
  {
    [DebuggerStepThrough] get => this.newName;
    set
    {
      if (string.IsNullOrEmpty(value))
        throw new ArgumentException("Не задано имя файла объекта IPS.", nameof (value));
      if (!(this.newName != value))
        return;
      this.newName = value;
    }
  }

  public bool IsRenamed
  {
    [DebuggerStepThrough] get => !PathUtils.IsSamePath(this.newName, this.originalName);
  }

  public DBObjectFileContent Content
  {
    [DebuggerStepThrough] get => this.content;
    set
    {
      if (this.content == value)
        return;
      this.content = value;
    }
  }
}
