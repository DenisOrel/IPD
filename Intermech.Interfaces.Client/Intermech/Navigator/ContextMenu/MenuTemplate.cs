// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.MenuTemplate
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Содержит коллекцию корневых узлов и отвечает за управление
/// общим состоянием шаблона (включение/выключение автоматической
/// сортировки и др).
/// </summary>
public class MenuTemplate : IAssignable, ICloneable
{
  /// <summary>Коллекция узлов в шаблоне меню</summary>
  private MenuTemplateNodeCollection _nodes;
  /// <summary>Таблица названий узлов меню</summary>
  internal Dictionary<string, MenuTemplateNode> _namedNodes = new Dictionary<string, MenuTemplateNode>();
  /// <summary>Количество обновлений</summary>
  internal int _updateCount;

  /// <summary>Создать шаблон меню</summary>
  public MenuTemplate()
  {
    this._nodes = new MenuTemplateNodeCollection(this, (MenuTemplateNode) null);
    this._updateCount = 0;
  }

  /// <summary>
  /// Создать шаблон меню, заполнить его информацией из объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MenuTemplate(object source)
    : this()
  {
    this.Assign(source);
  }

  /// <summary>Коллекция узлов для шаблона</summary>
  public MenuTemplateNodeCollection Nodes => this._nodes;

  /// <summary>Шаблон элемента меню с указанным именем</summary>
  /// <param name="name">Имя элемента меню</param>
  /// <returns>Шаблон элемента меню с указанным именем или null</returns>
  public MenuTemplateNode this[string name]
  {
    get => !this._namedNodes.ContainsKey(name) ? (MenuTemplateNode) null : this._namedNodes[name];
  }

  public IEnumerable<MenuTemplateNode> GetDescendents()
  {
    foreach (MenuTemplateNode node in this.Nodes)
    {
      foreach (MenuTemplateNode selfAndDescendent in node.GetSelfAndDescendents())
        yield return selfAndDescendent;
    }
  }

  /// <summary>
  /// Событие генерируется каждый раз, когда завершается изменение шаблона меню вызовом метода EndUpdates
  /// </summary>
  public event EventHandler OnChanged;

  /// <summary>Сгенерировать событие ObChanged, если есть подписчики</summary>
  private void FireOnChanged()
  {
    if (this.OnChanged == null)
      return;
    this.OnChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Начать обновление шаблона</summary>
  public void BeginUpdate() => ++this._updateCount;

  /// <summary>Завершить обновление шаблона</summary>
  public void EndUpdate()
  {
    if (this._updateCount <= 0)
      return;
    --this._updateCount;
    if (this._updateCount != 0)
      return;
    this._nodes.Sort();
    this.FireOnChanged();
  }

  /// <summary>Обновить шаблон элемента меню в словарике</summary>
  /// <param name="node">Шаблон элемента меню</param>
  /// <param name="oldName">Старое имя</param>
  /// <param name="newName">Новое имя</param>
  internal void UpdateNameHash(MenuTemplateNode node, string oldName, string newName)
  {
    if (oldName.Length != 0 && this._namedNodes.ContainsKey(oldName))
      this._namedNodes.Remove(oldName);
    if (newName.Length == 0)
      return;
    this._namedNodes[newName] = node;
  }

  /// <summary>Перестроить хэш имён</summary>
  /// <param name="node">Узел</param>
  internal void RecursiveRebuildNameHash(MenuTemplateNode node)
  {
    if (node == null)
      return;
    this._namedNodes[node.Name] = node;
    node.Template = this;
    node.Nodes.MenuTemplate = this;
    if (node.Nodes == null)
      return;
    for (int index = 0; index < node.Nodes.Count; ++index)
      this.RecursiveRebuildNameHash(node.Nodes[index]);
  }

  /// <summary>Перестроить хэш имён</summary>
  public void RebuildNameHash()
  {
    this._namedNodes.Clear();
    if (this.Nodes.Count <= 0)
      return;
    for (int index = 0; index < this.Nodes.Count; ++index)
      this.RecursiveRebuildNameHash(this.Nodes[index]);
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._nodes.Clear();
    this._namedNodes.Clear();
    this._updateCount = 0;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MenuTemplate menuTemplate))
      return;
    for (int index = 0; index < menuTemplate._nodes.Count; ++index)
    {
      MenuTemplateNode node = menuTemplate._nodes[index].Clone() as MenuTemplateNode;
      node._template = this;
      node.Nodes.MenuTemplate = this;
      this._nodes.Add(node);
    }
    this.RebuildNameHash();
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new MenuTemplate((object) this);
}
