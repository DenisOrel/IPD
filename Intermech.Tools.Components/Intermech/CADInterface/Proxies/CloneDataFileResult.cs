// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneDataFileResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает возможные результаты клонирования файла CAD-системы
/// </summary>
public enum CloneDataFileResult
{
  /// <summary>Файл еще не был обработан</summary>
  NotProcessed,
  /// <summary>Клонирование не требуется, файл используется как есть</summary>
  RemainUnchanged,
  /// <summary>
  /// Клонирование файла не удалось из-за ошибки. Подробную информацию об ошибке
  /// можно получить из свойства <see cref="P:Intermech.CADInterface.Proxies.CloneDataFileProxy.ErrorMessage" />
  /// </summary>
  Failed,
  /// <summary>Клонирование выполнено частично</summary>
  FileOnly,
  /// <summary>Клонирование выполнено полностью</summary>
  FileAndAttributes,
}
