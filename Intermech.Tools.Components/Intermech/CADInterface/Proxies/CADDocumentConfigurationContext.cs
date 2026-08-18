// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADDocumentConfigurationContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает контекст получения конфигурации документа CAD-системы, когда конфигурация получена из документа CAD-системы.
/// </summary>
public sealed class CADDocumentConfigurationContext : IModelConfigurationCreationContext
{
  private static readonly CADDocumentConfigurationContext defaultInstance = new CADDocumentConfigurationContext();

  /// <summary>
  /// Возвращает экземпляр контекста, используемый по умолчанию.
  /// </summary>
  public static CADDocumentConfigurationContext Default
  {
    get => CADDocumentConfigurationContext.defaultInstance;
  }
}
