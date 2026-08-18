// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileDifferenceType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Описывает тип различия между локальным и удаленным (remote) состояниями файла.
/// </summary>
public enum FileDifferenceType
{
  /// <summary>Локальный файл отсутствует на диске.</summary>
  MissingFile,
  /// <summary>
  /// Локальный файл устарел по сравнению с удаленным (remote) файлом.
  /// </summary>
  OutdatedFile,
  /// <summary>Локальный файл не изменен</summary>
  UnchangedFile,
  /// <summary>
  /// Локальный файл изменен по сравнению с удаленным (remote) файлом.
  /// </summary>
  UpdatedFile,
  /// <summary>
  /// Новый локальный файл, не имеющий соответствия в удаленном хранилище данных
  /// </summary>
  NewFile,
}
