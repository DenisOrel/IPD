// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.PluginEventArgs
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Аргументы событий менеджера плагинов</summary>
public class PluginEventArgs : EventArgs
{
  /// <summary>Модуль расширения</summary>
  public IPlugin Plugin { [DebuggerStepThrough] get; private set; }

  /// <summary>Создать аргументы</summary>
  /// <param name="plugin">Модуль расширения</param>
  public PluginEventArgs(IPlugin plugin) => this.Plugin = plugin;
}
