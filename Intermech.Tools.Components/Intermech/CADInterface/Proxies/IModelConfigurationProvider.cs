// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IModelConfigurationProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Интерфейс для связи с конфигурацией модели из CAD-системы.
/// </summary>
public interface IModelConfigurationProvider
{
  /// <summary>
  /// Находит и возвращает COM-объект конфигурации документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  IModelConfiguration RawConfiguration { get; }
}
