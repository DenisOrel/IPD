// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ICADDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Интерфейс для связи с COM-объектом документа CAD-системы.
/// </summary>
public interface ICADDocumentProvider
{
  /// <summary>
  /// Возвращает абсолютный путь к файлу документа, если он известен провайдеру. Если путь не известен, то метод вернет null.
  /// </summary>
  /// <returns>Абсолютный путь к файлу документа или null</returns>
  string TryGetFullPath();

  /// <summary>
  /// Находит и возвращает COM-объект документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  ICADDocument Document { get; }
}
