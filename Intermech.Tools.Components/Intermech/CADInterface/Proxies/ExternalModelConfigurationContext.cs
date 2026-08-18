// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ExternalModelConfigurationContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает контекст получения конфигурации документа CAD-системы, когда конфигурация передается из внешней системы (например, из PDM-браузера).
/// </summary>
public sealed class ExternalModelConfigurationContext : IModelConfigurationCreationContext
{
  private static readonly ExternalModelConfigurationContext defaultInstance = new ExternalModelConfigurationContext();

  /// <summary>
  /// Возвращает экземпляр контекста, используемый по умолчанию.
  /// </summary>
  public static ExternalModelConfigurationContext Default
  {
    get => ExternalModelConfigurationContext.defaultInstance;
  }
}
