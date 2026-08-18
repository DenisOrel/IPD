// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IPdmConfiguratorServerPlugin
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Интерфейс для проверки наличия загруженного плагина на сервере приложений
/// </summary>
public interface IPdmConfiguratorServerPlugin
{
  /// <summary>Guid плагина</summary>
  Guid PluginGuid { get; }
}
