// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.SelectedItemsCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Commands;

public abstract class SelectedItemsCommand(string name) : Command(name)
{
  private ISelectedItems items;
  private object additionalInfo;
  private ObjectCommandsOptions? commonOptions;

  /// <summary>Инициализация комманды</summary>
  /// <param name="items">Коллекция элементов пространства навигации</param>
  /// <param name="viewServices">Провайдер сервисов контекста, в котором создается команда. Параметр может быть не задан</param>
  /// <param name="additionalInfo">Дополнительная информация для команды. Параметр может быть не задан</param>
  public virtual void Init(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.items = items != null ? items : throw new ArgumentNullException(nameof (items));
    if (viewServices != null)
      this.ContextServices = viewServices;
    this.additionalInfo = additionalInfo;
  }

  /// <summary>
  /// Возвращает коллекцию элементов пространства навигации.
  /// </summary>
  public ISelectedItems Items => this.items;

  /// <summary>
  /// Возвращает или задает дополнительную информацию для команды.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  public object AdditionalInfo
  {
    get => this.additionalInfo;
    set => this.additionalInfo = value;
  }

  /// <summary>
  /// Возвращает или задает общие опции выполнения команды.
  /// Значение свойства может быть не задано и равно null.
  /// </summary>
  /// <remarks>
  /// Общие опции выполнения команды могут быть переданы команде двумя способами: либо через этого свойство,
  /// либо через свойство <see cref="P:Command.ContextServices" /> в виде объекта типа <see cref="T:ObjectCommandsOptionsHolder" />.
  /// Значение этого свойства более приоритетно, чем значение свойства <see cref="P:Command.ContextServices" />.
  /// </remarks>
  public ObjectCommandsOptions? CommonOptions
  {
    get => this.commonOptions;
    set => this.commonOptions = value;
  }

  /// <summary>Получение общих опций выполнения команд.</summary>
  /// <returns>Общие опции выполнения команд</returns>
  protected ObjectCommandsOptions GetCommonOptions()
  {
    if (this.CommonOptions.HasValue)
      return this.CommonOptions.Value;
    ObjectCommandsOptionsHolder service = (ObjectCommandsOptionsHolder) this.ContextServices.GetService(typeof (ObjectCommandsOptionsHolder));
    return service != null ? service.Value : ObjectCommandsOptions.None;
  }
}
