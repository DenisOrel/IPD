// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageSelectedItems
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Forums;

/// <summary>описывает выделенное сообщение на закладке</summary>
public class UserMessageSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  private List<string> userMessages = new List<string>();
  private NodeIDPath handlerPath;
  private IServiceProvider services;

  public UserMessageSelectedItems(NodeIDPath handlerPath, IServiceProvider services)
  {
    this.handlerPath = handlerPath;
    this.services = services;
  }

  public void AddMessage(string message)
  {
    if (this.userMessages.Contains(message))
      return;
    this.userMessages.Add(message);
  }

  public void RemoveMessage(string message)
  {
    if (!this.userMessages.Contains(message))
      return;
    this.userMessages.Remove(message);
  }

  public void Ivalidate() => this.userMessages.Clear();

  /// <summary>
  /// Возвращает true, если коллекция содержит разнородные идентификаторы
  /// элементов (т.е. созданные разными элементами навигации). Такие
  /// разнородные коллекции образуются при множественном выделении в дереве
  /// навигатора и других подобных этой ситуациях.
  /// </summary>
  public bool IsCollage => false;

  /// <summary>
  /// Возвращает количество идентификаторов элементов навигации в коллеции.
  /// </summary>
  public int Count => this.userMessages.Count;

  /// <summary>
  /// Возвращает данные указанного формата для элемента коллекции. Если элемент
  /// не поддерживает указанный формат, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  public object GetItemData(int index, Type dataFormat)
  {
    if (dataFormat != typeof (string))
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Design_191"));
    return (object) this.userMessages[index];
  }

  /// <summary>Возвращает идентификатор элемента в коллекции.</summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Идентификатор элемента.</returns>
  public INodeID GetItemID(int index) => (INodeID) new MessageNodeId();

  /// <summary>
  /// Возвращает данные требуемого формата для родительского элемента,
  /// создавшего указанный идентификатор элемента. Если родительский элемент
  /// не поддерживает запрошенный формат данных, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  public object GetParentData(int index, Type dataFormat)
  {
    return Utils.GetDataFromPath(this.handlerPath, dataFormat, this.services);
  }

  /// <summary>
  /// Возвращает полный путь родительского элемента для указанного
  /// идентификатора в коллекции.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Путь родительского элемента.</returns>
  public NodeIDPath GetParentPath(int index) => this.handlerPath;
}
