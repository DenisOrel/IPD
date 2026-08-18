
// Type: Intermech.Client.Core.WorkCopyCommandOptionsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;


namespace Intermech.Client.Core;

/// <summary>
/// Класс сервиса для получения дополнительных опции выполнения для команд checkout, save changes, checkin, cancel changes.
/// Реализация является thread safe.
/// </summary>
internal sealed class WorkCopyCommandOptionsService : IWorkCopyCommandOptions
{
  /// <summary>
  /// Собирает и возвращает дополнительные опции выполнения для команды checkin.
  /// </summary>
  /// <param name="items">Выделенные элементы навигатора, к которым будет применена команда</param>
  /// <param name="contextServices">Контейнер сервисов окружения для команды, в которую должны быть помещены дополнительные опции выполнения</param>
  /// <param name="contextServicesEditors">Коллекция, в которую должны быть помещены редакторы дополнительных опций выполнения</param>
  /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="items" />, <paramref name="contextServices" />, <paramref name="contextServicesEditors" /> не должны быть равны null</exception>
  public void GetCheckinOptions(
    ISelectedItems items,
    IServiceContainer contextServices,
    List<WorkCopyCommandOptionsEditor> contextServicesEditors)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (contextServices == null)
      throw new ArgumentNullException(nameof (contextServices));
    if (contextServicesEditors == null)
      throw new ArgumentNullException(nameof (contextServicesEditors));
    EventHandler<WorkCopyCommandOptionsEventArgs> collectCheckinOptions = this.CollectCheckinOptions;
    if (collectCheckinOptions == null)
      return;
    WorkCopyCommandOptionsEventArgs e = new WorkCopyCommandOptionsEventArgs(items, contextServices, contextServicesEditors);
    collectCheckinOptions((object) null, e);
  }

  /// <summary>
  /// Событие для сбора дополнительных опций выполнения для команды checkin.
  /// </summary>
  public event EventHandler<WorkCopyCommandOptionsEventArgs> CollectCheckinOptions;
}
