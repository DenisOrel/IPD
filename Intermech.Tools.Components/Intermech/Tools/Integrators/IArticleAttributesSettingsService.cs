// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IArticleAttributesSettingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора для получения проекции настроек интегратора, которая содержит параметры синхронизации атрибутов изделий.
/// </summary>
public interface IArticleAttributesSettingsService : IIntegratorSettingsService, IIntegratorService
{
  /// <summary>
  /// Возвращает объект, позволяющий получить коллекцию синхронизируемых атрибутов изделий.
  /// </summary>
  ISynchronizedObjectAttributes SynchronizedArticleAttributes { get; }
}
