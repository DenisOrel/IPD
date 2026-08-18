
// Type: Intermech.Navigator.DBObjects.VirtualObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный узел, который должен появляться в корне дерева, если на указанную дату
/// объект ещё не существовал
/// </summary>
public class VirtualObjectNode : CompositeNode, IContextAware
{
  /// <summary>Заголовок узла</summary>
  protected string _caption;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  /// <summary>Создать узел</summary>
  public VirtualObjectNode() => this._caption = LocalizationHolder.rm.GetString("Client.Core_331");

  /// <summary>Создать узел</summary>
  /// <param name="caption">Заголовок узла</param>
  public VirtualObjectNode(string caption) => this._caption = caption;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    get => this.services;
    set => this.services = value;
  }
}
