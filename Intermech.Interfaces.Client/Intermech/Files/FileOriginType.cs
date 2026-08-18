// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileOriginType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Классифицирует происхождение файла в рабочей области файлового хранилища
/// </summary>
public enum FileOriginType
{
  /// <summary>
  /// Новый файл - добавлен в рабочую область пользователем извне
  /// </summary>
  NewFile,
  /// <summary>
  /// Рабочий файл объекта - добавлен в рабочую область при публикации в ней объекта IPS
  /// </summary>
  WorkFile,
  /// <summary>
  /// Отсоединенный файл - происхождение файла не установлено. В в базе IPS есть объект с таким файлом, но
  /// ни одна из версий этого объекта не опубликована в рабочей области
  /// </summary>
  DetachedFile,
}
