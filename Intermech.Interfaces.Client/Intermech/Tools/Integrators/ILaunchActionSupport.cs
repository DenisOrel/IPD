// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ILaunchActionSupport
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора, отвечающий за открытие документов приложения для редактирования или просмтора.
/// </summary>
public interface ILaunchActionSupport
{
  /// <summary>
  /// Возвращает true, если интегратор поддерживает тип команды открытия документа в интегрируемом приложении.
  /// </summary>
  /// <param name="launchType">Тип команды</param>
  /// <returns>Признак, что интегратор поддерживает указанный тип команды</returns>
  bool IsSupported(LaunchType launchType);

  /// <summary>Открывает документ в приложении.</summary>
  /// <param name="launchParams">Параметры команды открытия документа</param>
  /// <param name="afterPublishFile">Событие публикации открываемого файла на диске. Может быть null</param>
  void OpenDocument(
    LaunchParams launchParams,
    EventHandler<LaunchHandlerEventArgs> afterPublishFile);
}
