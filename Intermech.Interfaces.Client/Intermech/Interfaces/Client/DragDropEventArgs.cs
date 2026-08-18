// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DragDropEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Аргументы события, возникающего во время Drag'N'Drop</summary>
[DebuggerDisplay("EventName: {EventName}; Handled: {Handled}")]
public class DragDropEventArgs : NotificationEventArgs
{
  /// <summary>Если равно true, событие успешно обработано</summary>
  public bool Handled;
  /// <summary>
  /// Если равно true, выполняется цепочка команд "Cut", "Paste", иначе - "Copy", "Paste"
  /// </summary>
  public bool IsCut;
  /// <summary>
  /// Коллекция выделенных элементов, которые перетаскиваются
  /// </summary>
  public ISelectedItems SourceItems;
  /// <summary>
  /// Контекст, в рамках которого создана коллекция перетаскиваемых выделенных элементов
  /// </summary>
  public IServiceProvider SourceProvider;
  /// <summary>
  /// Элемент управления, из которого перетаскиваются выделенные элементы
  /// </summary>
  public object SourceControl;
  /// <summary>
  /// Коллекция выделенных элементов, на которые был выполнен "сброс"
  /// </summary>
  public ISelectedItems DestItems;
  /// <summary>
  /// Контекст, в рамках которого создана коллекция выделенных элементов, на которые был выполнен "сброс"
  /// </summary>
  public IServiceProvider DestProvider;
  /// <summary>
  /// Элемент управления, на который были "сброшены" перетаскиваемые выделенные элементы
  /// </summary>
  public object DestControl;

  /// <summary>Создать событие с указанным именем</summary>
  /// <param name="eventName">Имя события</param>
  /// <param name="handled">Если равно true, событие успешно обработано</param>
  /// <param name="isCut">Если равно true, выполняется цепочка команд "Cut", "Paste", иначе - "Copy", "Paste"</param>
  /// <param name="sourceItems">Коллекция выделенных элементов, которые перетаскиваются</param>
  /// <param name="sourceProvider">Контекст, в рамках которого создана коллекция перетаскиваемых выделенных элементов</param>
  /// <param name="sourceControl">Элемент управления, из которого перетаскиваются выделенные элементы</param>
  /// <param name="destItems">Коллекция выделенных элементов, на которые был выполнен "сброс"</param>
  /// <param name="destProvider">Контекст, в рамках которого создана коллекция выделенных элементов, на которые был выполнен "сброс"</param>
  /// <param name="destControl">Элемент управления, на который были "сброшены" перетаскиваемые выделенные элементы</param>
  public DragDropEventArgs(
    string eventName,
    bool handled,
    bool isCut,
    ISelectedItems sourceItems,
    IServiceProvider sourceProvider,
    object sourceControl,
    ISelectedItems destItems,
    IServiceProvider destProvider,
    object destControl)
    : base(eventName)
  {
    this.Handled = handled;
    this.IsCut = isCut;
    this.SourceItems = sourceItems;
    this.SourceProvider = sourceProvider;
    this.SourceControl = sourceControl;
    this.DestItems = destItems;
    this.DestProvider = destProvider;
    this.DestControl = destControl;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this.SourceItems != null)
        num += this.SourceItems.Count;
      return num <= 0 ? base.ItemsCount : num;
    }
  }

  /// <summary>
  /// Проверить, поддерживается ли указанный режим оптимизации аргументами события и,
  /// в случае необходимости, вернуть максимальный уровень поддерживаемой оптимизации
  /// </summary>
  /// <param name="mode">Запрашиваемый режим оптимизации</param>
  /// <returns>Допустимый режим оптимизации</returns>
  public override NotificationServiceMode GetSupportedOptimization(NotificationServiceMode mode)
  {
    return mode;
  }
}
