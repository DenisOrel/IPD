// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IWorkCopyCommandOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса для получения дополнительных опции выполнения для команд checkout, save changes, checkin, cancel changes.
/// Реализация должна быть thread safe.
/// </summary>
public interface IWorkCopyCommandOptions
{
  /// <summary>
  /// Собирает и возвращает дополнительные опции выполнения для команды checkin.
  /// </summary>
  /// <param name="items">Выделенные элементы навигатора, к которым будет применена команда</param>
  /// <param name="contextServices">Контейнер сервисов окружения для команды, в которую должны быть помещены дополнительные опции выполнения</param>
  /// <param name="contextServicesEditors">Коллекция, в которую должны быть помещены редакторы дополнительных опций выполнения</param>
  /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="items" />, <paramref name="contextServices" />, <paramref name="contextServicesEditors" /> не должны быть равны null</exception>
  void GetCheckinOptions(
    ISelectedItems items,
    IServiceContainer contextServices,
    List<WorkCopyCommandOptionsEditor> contextServicesEditors);

  /// <summary>
  /// Событие для сбора дополнительных опций выполнения для команды checkin.
  /// </summary>
  event EventHandler<WorkCopyCommandOptionsEventArgs> CollectCheckinOptions;
}
