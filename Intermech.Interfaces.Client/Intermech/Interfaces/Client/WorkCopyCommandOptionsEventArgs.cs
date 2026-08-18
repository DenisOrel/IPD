// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.WorkCopyCommandOptionsEventArgs
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
/// Реализует класс для хранения аргументов событий по получению дополнительных опций выполнения.
/// </summary>
public sealed class WorkCopyCommandOptionsEventArgs : EventArgs
{
  private readonly ISelectedItems items;
  private readonly IServiceContainer contextServices;
  private readonly List<WorkCopyCommandOptionsEditor> contextServicesEditors;

  /// <summary>Создает объект.</summary>
  /// <param name="items">Выделенные элементы навигатора, к которым будет применена команда</param>
  /// <param name="contextServices">Контейнер сервисов окружения для команды, в которую должны быть помещены дополнительные опции выполнения</param>
  /// <param name="contextServicesEditors">Коллекция, в которую должны быть помещены редакторы дополнительных опций выполнения</param>
  /// <exception cref="T:ArgumentNullException">Параметры <paramref name="items" />, <paramref name="contextServices" />, <paramref name="contextServicesEditors" /> не должны быть равны null</exception>
  public WorkCopyCommandOptionsEventArgs(
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
    this.items = items;
    this.contextServices = contextServices;
    this.contextServicesEditors = contextServicesEditors;
  }

  /// <summary>
  /// Возвращает выделенные элементы навигатора, к которым будет применена команда.
  /// </summary>
  public ISelectedItems Items => this.items;

  /// <summary>
  /// Возвращает контейнер сервисов окружения для команды, в которую должны быть помещены дополнительные опции выполнения.
  /// </summary>
  public IServiceContainer ContextServices => this.contextServices;

  /// <summary>
  /// Возвращает коллекцию, в которую должны быть помещены редакторы дополнительных опций выполнения.
  /// </summary>
  public List<WorkCopyCommandOptionsEditor> ContextServicesEditors => this.contextServicesEditors;
}
