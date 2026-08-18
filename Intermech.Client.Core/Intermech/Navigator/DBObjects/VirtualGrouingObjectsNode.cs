
// Type: Intermech.Navigator.DBObjects.VirtualGrouingObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Виртуальный узел "Найденные группирующие объекты"</summary>
public class VirtualGrouingObjectsNode : CompositeNode, IContextAware
{
  /// <summary>Заголовок узла</summary>
  protected string _caption;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  /// <summary>Создать узел</summary>
  public VirtualGrouingObjectsNode()
  {
    this._caption = LocalizationHolder.rm.GetString("Client.Core_332");
  }

  /// <summary>Создать узел</summary>
  /// <param name="caption">Заголовок узла</param>
  public VirtualGrouingObjectsNode(string caption) => this._caption = caption;

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    get => this.services;
    set => this.services = value;
  }
}
