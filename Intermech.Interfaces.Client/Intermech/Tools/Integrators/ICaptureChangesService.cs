// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ICaptureChangesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора, отвечающий за передачу изменений из файловой копии объекта в базу IPS.
/// </summary>
public interface ICaptureChangesService
{
  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта. Как правило, этот метод вызывается из обработчика команды
  /// "Сохранить изменения".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  void CaptureChanges(long objectId);

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта. Как правило, этот метод вызывается из обработчика команды
  /// "Сохранить изменения".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="options" /> не должен быть равен null</exception>
  void CaptureChanges(long objectId, CaptureChangesOptions options);
}
