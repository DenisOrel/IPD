// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IArticleLaunchActionSupport
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.LaunchActions;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора, отвечающий за открытие документов CAD-системы в контексте изделия.
/// Когда специализированный вариант команды запуска CAD-системы применяется к изделию, то сервис позволяет
/// открыть в CAD-системе файл модели, связанной с изделием, и активизировать в модели конфигурацию, соответствующую изделию.
/// </summary>
public interface IArticleLaunchActionSupport
{
  /// <summary>Проверяет возможность использования сервиса.</summary>
  /// <param name="articleId">Идентификатор версии изделия, к которому изначально была применена команда</param>
  /// <param name="documentLaunchParams">Параметры запуска приложения для документа, связанного с изделием и выбранного пользователем</param>
  /// <param name="documentType">Идентификатор типа документа, связанного с изделием и выбранного пользователем</param>
  /// <returns>true, если это модель CAD-системы, с которой может работать сервис</returns>
  bool IsSupported(long articleId, LaunchParams documentLaunchParams, int documentType);

  /// <summary>
  /// Заполняет контекст открытия файла, сохраняя в нем информацию об изделии, для которого была вызвана команда запуска приложения.
  /// Позже эта информация будет использована при открытии файла модели в CAD-системе.
  /// </summary>
  /// <param name="articleId">Идентификатор версии изделия, к которому изначально была применена команда</param>
  /// <param name="documentLaunchParams">Параметры запуска приложения для документа, связанного с изделием и выбранного пользователем</param>
  void MakeLaunchContext(long articleId, LaunchParams documentLaunchParams);
}
