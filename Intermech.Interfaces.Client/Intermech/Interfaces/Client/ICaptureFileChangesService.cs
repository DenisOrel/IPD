// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICaptureFileChangesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса для захвата изменений в файлах объектов IPS на диске и
/// передачи этих изменений в базу IPS.
/// </summary>
/// <remarks>Сервис используется командой "Сохранить изменения".</remarks>
public interface ICaptureFileChangesService
{
  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файлах объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="mode">Режим сохранения изменений в объекте</param>
  /// <param name="contextServices">Контекст выполняемой операции. Параметр может быть не задан</param>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  /// <exception cref="T:System.Exception">В процессе работы сервиса произошла ошибка</exception>
  void CaptureChanges(long objectId, SaveChangesMode mode, IServiceProvider contextServices);
}
