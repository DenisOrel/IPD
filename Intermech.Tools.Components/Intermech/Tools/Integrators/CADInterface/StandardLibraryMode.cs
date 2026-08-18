// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.StandardLibraryMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Режим взаимодействия с CADMECH</summary>
public enum StandardLibraryMode
{
  /// <summary>
  /// Нет поддержки библиотеки стандартных со стороны CADMECH
  /// </summary>
  NotSupported,
  /// <summary>Модели типоразмеров хранятся в отдельных файлах</summary>
  SeparateStandardSizes,
  /// <summary>
  /// Модели типоразмеров задаются конфигурациями мастер-модели
  /// </summary>
  EmbeddedStandardSizes,
}
