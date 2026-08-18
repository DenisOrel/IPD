// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IPlugin
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Сборка с модулем расширения</summary>
public interface IPlugin : IDisposable
{
  /// <summary>Является ли данный плагин корректным</summary>
  bool IsValid { get; }

  /// <summary>Полный путь сборки</summary>
  string Location { get; }

  /// <summary>Имя сборки</summary>
  string Name { get; }

  /// <summary>Коллекция модулей расширения</summary>
  IPackageCollection Packages { get; }

  /// <summary>
  /// true, если необходимо сохранять имя сборки в файле конфигурации
  /// для последующей автоматической загрузки.
  /// </summary>
  bool AutoReload { get; }

  /// <summary>
  /// Минимальная версия программы, в которую может быть загружен данный плагин
  /// </summary>
  string MinVersion { get; }

  /// <summary>
  /// Максимальная версия программы, в которую может быть загружен данный плагин
  /// </summary>
  string MaxVersion { get; }
}
