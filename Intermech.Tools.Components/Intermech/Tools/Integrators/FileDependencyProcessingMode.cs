// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependencyProcessingMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Режим обработки ссылочной зависимости.</summary>
public enum FileDependencyProcessingMode
{
  /// <summary>
  /// Требуется создать или обновить документ IPS по ссылочной зависимости
  /// </summary>
  Analyze,
  /// <summary>Требуется игнорировать зависимоть</summary>
  Ignore,
  /// <summary>
  /// Требуется отложить анализ новой ссылочной зависимости на неопределенный срок.
  /// Данный режим может использоваться только для новых файлов, импортируемых в базу данных IPS.
  /// При выборе этого режима в базе данных будет создан черновик документа, связанный с импортируемым файлом.
  /// </summary>
  DeferImport,
}
