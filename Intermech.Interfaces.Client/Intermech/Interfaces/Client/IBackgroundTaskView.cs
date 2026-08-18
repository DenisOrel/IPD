// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IBackgroundTaskView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Сервис для отображения фоновых процессов в Навигаторе</summary>
public interface IBackgroundTaskView
{
  /// <summary>Добавить новое задание</summary>
  /// <param name="task">Новое фоновое задание</param>
  void AddTask(IBackgroundTask task);

  /// <summary>Удалить фоновое задание</summary>
  /// <param name="task">Удаляемое фоновое задание</param>
  void DeleteTask(IBackgroundTask task);

  /// <summary>
  /// Проверить, надо ли закрывать какое-либо фоновое задание
  /// </summary>
  /// <returns>false - нельзя остановить какое-то фоновое задание</returns>
  bool CheckClosing();
}
