// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.MenuTemplateNode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Шаблон элемента контекстного меню</summary>
public class MenuTemplateNode : IAssignable, ICloneable
{
  /// <summary>Служба поддержки "горячих клавиш"</summary>
  private static IHotKeysManager _hotKeysManager;
  /// <summary>Название команды контекстного меню</summary>
  private string _name;
  /// <summary>Текстовое пояснение</summary>
  private string _text;
  /// <summary>
  /// Определяет imagelist, из которого будет браться иконка
  /// </summary>
  private ImageListSource _imageListSource;
  /// <summary>Индекс изображения</summary>
  private int _imageIndex;
  /// <summary>Изображение (более высокий приоритет, чем ImageIndex)</summary>
  private Image _image;
  /// <summary>Номер группы команды</summary>
  private int _groupID;
  /// <summary>Номер в группе</summary>
  private int _orderID;
  private bool _visible = true;
  /// <summary>Пользовательские данные</summary>
  private object _tag;
  /// <summary>Комбинация горячих клавиш</summary>
  private Keys _shortcut;
  /// <summary>Коллекция дочерних элементов</summary>
  private MenuTemplateNodeCollection _nodes;
  /// <summary>Родительский элемент</summary>
  internal MenuTemplateNode _parent;
  /// <summary>Шаблон меню</summary>
  internal MenuTemplate _template;

  /// <summary>Пустой элемент меню</summary>
  public MenuTemplateNode()
    : this(string.Empty, string.Empty, -1, 0, 0, Keys.None, true, ImageListSource.NamedImageList)
  {
  }

  /// <summary>Инициализированный элемент меню</summary>
  /// <param name="text">Текстовое пояснение команды</param>
  /// <param name="iconIndex">Индекс изображения</param>
  /// <param name="groupID">Номер группы</param>
  /// <param name="orderID">Номер в группе</param>
  public MenuTemplateNode(string text, int iconIndex, int groupID, int orderID)
    : this(string.Empty, text, iconIndex, groupID, orderID, Keys.None, true, ImageListSource.NamedImageList)
  {
  }

  /// <summary>Инициализированный элемент меню</summary>
  /// <param name="name">Команда контекстного меню</param>
  /// <param name="text">Текстовое пояснение команды</param>
  /// <param name="imageIndex">Индекс изображения</param>
  /// <param name="groupID">Номер группы</param>
  /// <param name="orderID">Номер в группе</param>
  public MenuTemplateNode(string name, string text, int imageIndex, int groupID, int orderID)
    : this(name, text, imageIndex, groupID, orderID, Keys.None, true, ImageListSource.NamedImageList)
  {
  }

  /// <summary>Инициализированный элемент меню</summary>
  /// <param name="name">Команда контекстного меню</param>
  /// <param name="text">Текстовое пояснение команды</param>
  /// <param name="imageIndex">Индекс изображения</param>
  /// <param name="groupID">Номер группы</param>
  /// <param name="orderID">Номер в группе</param>
  /// <param name="shortcut">Горячая клавиша для команды</param>
  public MenuTemplateNode(
    string name,
    string text,
    int imageIndex,
    int groupID,
    int orderID,
    Keys shortcut)
    : this(name, text, imageIndex, groupID, orderID, shortcut, true, ImageListSource.NamedImageList)
  {
  }

  /// <summary>Инициализированный элемент меню</summary>
  /// <param name="name">Команда контекстного меню</param>
  /// <param name="text">Текстовое пояснение команды</param>
  /// <param name="imageIndex">Индекс изображения</param>
  /// <param name="groupID">Номер группы</param>
  /// <param name="orderID">Номер в группе</param>
  /// <param name="shortcut">Горячая клавиша для команды</param>
  /// <param name="visible">Видима ли команда в шаблоне</param>
  /// <param name="imageListSource"> imagelist, из которого будет браться иконка </param>
  public MenuTemplateNode(
    string name,
    string text,
    int imageIndex,
    int groupID,
    int orderID,
    Keys shortcut,
    bool visible,
    ImageListSource imageListSource)
  {
    MenuTemplateNode._hotKeysManager = MenuTemplateNode._hotKeysManager == null ? ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager : MenuTemplateNode._hotKeysManager;
    this._name = name;
    this._text = text;
    this._tag = (object) null;
    this._imageIndex = imageIndex;
    this._groupID = groupID;
    this._orderID = orderID;
    this._shortcut = shortcut;
    if (this._shortcut != 0 & visible && MenuTemplateNode._hotKeysManager[name] == null)
      MenuTemplateNode._hotKeysManager.RegisterHotKeysCommand(this._shortcut, name, DefaultCommandHandler.ContectMenu);
    this._visible = visible;
    this._nodes = new MenuTemplateNodeCollection((MenuTemplate) null, this);
    this._parent = (MenuTemplateNode) null;
    this._imageListSource = imageListSource;
  }

  /// <summary>
  /// Создать узел шаблона меню, заполнить информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MenuTemplateNode(object source)
    : this()
  {
    this.Assign(source);
  }

  /// <summary>Команда контекстного меню</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this._name;
    set
    {
      if (!(this._name != value))
        return;
      if (this._nodes.MenuTemplate != null)
        this._nodes.MenuTemplate.UpdateNameHash(this, this._name, value);
      this._name = value;
    }
  }

  /// <summary>Текстовое пояснение к команде меню</summary>
  public string Text
  {
    [DebuggerStepThrough] get => this._text;
    set => this._text = value;
  }

  /// <summary>
  /// Определяет imagelist, из которого будет браться значок
  /// </summary>
  public ImageListSource ImageListSource
  {
    [DebuggerStepThrough] get => this._imageListSource;
    set => this._imageListSource = value;
  }

  /// <summary>Индекс изображения</summary>
  public int ImageIndex
  {
    [DebuggerStepThrough] get => this._imageIndex;
    set => this._imageIndex = value;
  }

  /// <summary>Изображение (более высокий приоритет, чем ImageIndex)</summary>
  public Image Image
  {
    [DebuggerStepThrough] get => this._image;
    set => this._image = value;
  }

  /// <summary>Пользовательские данные</summary>
  public object Tag
  {
    [DebuggerStepThrough] get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Номер группы</summary>
  public int GroupID
  {
    [DebuggerStepThrough] get => this._groupID;
    set
    {
      if (this._groupID == value)
        return;
      this._groupID = value;
      if (this.Nodes.MenuTemplate == null || this.Nodes.MenuTemplate._updateCount != 0)
        return;
      if (this._parent != null)
        this._parent.Nodes.RelocateNode(this);
      else
        this.Nodes.MenuTemplate.Nodes.RelocateNode(this);
    }
  }

  /// <summary>Номер в группе</summary>
  public int OrderID
  {
    [DebuggerStepThrough] get => this._orderID;
    set
    {
      if (this._orderID == value)
        return;
      this._orderID = value;
      if (this.Nodes.MenuTemplate == null || this.Nodes.MenuTemplate._updateCount != 0)
        return;
      if (this._parent != null)
        this._parent.Nodes.RelocateNode(this);
      else
        this.Nodes.MenuTemplate.Nodes.RelocateNode(this);
    }
  }

  /// <summary>Комбинация "горячих клавиш"</summary>
  public Keys Shortcut
  {
    [DebuggerStepThrough] get => this._shortcut;
  }

  /// <summary>Список дочерних элементов меню</summary>
  public MenuTemplateNodeCollection Nodes
  {
    [DebuggerStepThrough] get => this._nodes;
  }

  /// <summary>Родительский элемент меню</summary>
  public MenuTemplateNode Parent
  {
    [DebuggerStepThrough] get => this._parent;
  }

  /// <summary>Шаблон меню</summary>
  public MenuTemplate Template
  {
    [DebuggerStepThrough] get => this._template;
    set => this._template = value;
  }

  public bool Visible => this._visible;

  public IEnumerable<MenuTemplateNode> GetSelfAndDescendents()
  {
    yield return this;
    foreach (MenuTemplateNode descendent in this.GetDescendents())
      yield return descendent;
  }

  public IEnumerable<MenuTemplateNode> GetDescendents()
  {
    foreach (MenuTemplateNode node in this.Nodes)
    {
      foreach (MenuTemplateNode selfAndDescendent in node.GetSelfAndDescendents())
        yield return selfAndDescendent;
    }
  }

  public bool IsSelfOrDescendentsContainsText(string text)
  {
    string findWhat = !string.IsNullOrEmpty(text) ? text.ToLowerInvariant() : throw new ArgumentException();
    return this.GetSelfAndDescendents().Any<MenuTemplateNode>((Func<MenuTemplateNode, bool>) (o => o.Text != null && o.Text.ToLowerInvariant().Contains(findWhat)));
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._name = string.Empty;
    this._text = string.Empty;
    this._imageListSource = ImageListSource.NamedImageList;
    this._imageIndex = -1;
    this._groupID = 0;
    this._orderID = 0;
    this._tag = (object) null;
    this._shortcut = Keys.None;
    if (this._nodes != null)
      this._nodes.Clear();
    this._parent = (MenuTemplateNode) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MenuTemplateNode menuTemplateNode))
      return;
    this._name = menuTemplateNode._name;
    this._text = menuTemplateNode._text;
    this._imageListSource = menuTemplateNode._imageListSource;
    this._imageIndex = menuTemplateNode._imageIndex;
    this._image = menuTemplateNode._image;
    this._groupID = menuTemplateNode._groupID;
    this._orderID = menuTemplateNode._orderID;
    this._tag = menuTemplateNode._tag;
    this._shortcut = menuTemplateNode._shortcut;
    this._nodes = menuTemplateNode._nodes != null ? menuTemplateNode._nodes.Clone() as MenuTemplateNodeCollection : this._nodes;
    this._parent = menuTemplateNode._parent;
    if (this._nodes == null)
      return;
    this._nodes._owner = this;
    this._nodes._template = this._template;
    if (this._nodes._template == null)
      return;
    this._nodes._template._namedNodes[this._name] = this;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new MenuTemplateNode((object) this);
}
