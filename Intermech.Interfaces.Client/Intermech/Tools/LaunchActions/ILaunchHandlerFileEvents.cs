// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.LaunchActions.ILaunchHandlerFileEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Tools.LaunchActions;

/// <summary>
/// Дополнительный интерфейс события для обработчиков, открывающих файлы объектов в сторонних приложениях.
/// Этот интерфейс позволяет другим сервисам системы встроиться в процесс открытия файла.
/// </summary>
public interface ILaunchHandlerFileEvents
{
  /// <summary>
  /// Событие вызывается после публикации на диске файла объекта, который будет открыт в приложении.
  /// Используется другими сервисами системы для внедрения в файл информации о состоянии или статусе объекта (подписях и пр.).
  /// </summary>
  event EventHandler<LaunchHandlerEventArgs> AfterPublishFile;
}
