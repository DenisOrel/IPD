// Decompiled with JetBrains decompiler
// Type: Intermech.Files.WorkAreaUpdateStats
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Содержит статистику по выполненным файловым операциям в процессе обновления рабочей области
/// файлового хранилища.
/// </summary>
[Serializable]
public sealed class WorkAreaUpdateStats
{
  private int downloadedFiles;
  private int refreshedFiles;
  private int reloadedFiles;

  /// <summary>
  /// Возвращает true, если в рабочей области есть новые/обновленные файлы.
  /// </summary>
  public bool HasDBUpdates => this.DownloadedFiles != 0 || this.RefreshedFiles != 0;

  /// <summary>
  /// Возвращает количество файлов, извлеченных из базы IPS в рабочую область файлового хранилища.
  /// </summary>
  public int DownloadedFiles
  {
    get => this.downloadedFiles;
    set => this.downloadedFiles = value;
  }

  /// <summary>
  /// Возвращает количество файлов в рабочей области файлового хранилища, чье содержимое было обновлено из базы IPS.
  /// </summary>
  public int RefreshedFiles
  {
    get => this.refreshedFiles;
    set => this.refreshedFiles = value;
  }

  /// <summary>
  /// Возвращает количество переоткрытых файлов, чье содержимое было обновлено из базы IPS.
  /// </summary>
  public int ReloadedFiles
  {
    get => this.reloadedFiles;
    set => this.reloadedFiles = value;
  }
}
