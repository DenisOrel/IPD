// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.AdjustableMenuCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Описание настраиваемой команды меню</summary>
[DebuggerDisplay("Command = {Command}, Visible = {Visible}, Count = {Count}")]
[Serializable]
public class AdjustableMenuCommand : 
  IAdjustableMenuCommand,
  ICloneable,
  IComparable<AdjustableMenuCommand>
{
  /// <summary>Родительская коллекция</summary>
  private AdjustableMenuCommands _Parent;
  /// <summary>
  /// Уникальная в пределах всей системы команда контекстного меню
  /// </summary>
  private string _Command;
  /// <summary>Краткое текстовое описание команды</summary>
  private string _Caption;
  /// <summary>Более подробное текстовое описание команды</summary>
  private string _Hint;
  /// <summary>указывает, из какой коллекции брать иконку</summary>
  private ImageListSource _ImageListSource = ImageListSource.NamedImageList;
  /// <summary>
  /// Индекс значка команды (из коллекции указанной в ImageListSource)
  /// </summary>
  private int _ImageIndex = -1;
  /// <summary>
  /// Флажок позволяет прятать или показывать данную команду в контекстных меню "Навигатора"
  /// </summary>
  private bool _Visible = true;
  /// <summary>Номер группы</summary>
  private int _Group;
  /// <summary>Порядковый номер в группе</summary>
  private int _OrderID;
  /// <summary>Коллекция дочерних команд</summary>
  private AdjustableMenuCommands _Items;
  /// <summary>Горячие клавиши</summary>
  private Keys _Shortcut;
  /// <summary>для хранения различной информации</summary>
  private object _Tag;

  /// <summary>
  /// Создать экземпляр настраиваемой команды контекстного меню
  /// </summary>
  /// <param name="parent">Родительская коллекция</param>
  /// <param name="command">Уникальная в пределах системы команда</param>
  /// <param name="caption">Краткое текстовое описание команды</param>
  /// <param name="visible">Показывать ли указанную команду в контекстных меню</param>
  /// <param name="hint">Подробное текстовое описание команды</param>
  /// <param name="imageIndex">Индекс изображения команды (в списке именованных значков)</param>
  /// <param name="group">Номер группы команды</param>
  /// <param name="orderID">Порядковый номер в группе</param>
  /// <param name="shortcut">Горячие клавиши</param>
  /// <param name="imageListSource">С помощью какого сервиса получать иконку: INamedImageList или ICategoryTypeIconService</param>
  /// <param name="tag">Дополнительная информация</param>
  public AdjustableMenuCommand(
    AdjustableMenuCommands parent,
    string command,
    string caption,
    bool visible,
    string hint,
    int imageIndex,
    int group,
    int orderID,
    Keys shortcut,
    ImageListSource imageListSource,
    object tag)
  {
    this._Parent = parent;
    this._Command = command;
    this._Caption = caption;
    this._Visible = visible;
    this._Hint = hint;
    this._ImageIndex = imageIndex;
    this._Group = group;
    this._OrderID = orderID;
    this._Items = new AdjustableMenuCommands(parent);
    this._Shortcut = shortcut;
    this._ImageListSource = imageListSource;
    this._Tag = tag;
  }

  /// <summary>Родительская коллекция</summary>
  public AdjustableMenuCommands Parent
  {
    [DebuggerStepThrough] get => this._Parent;
  }

  /// <summary>
  /// Уникальная в пределах всей системы команда контекстного меню
  /// </summary>
  public string Command
  {
    [DebuggerStepThrough] get => this._Command;
  }

  /// <summary>Краткое текстовое описание команды</summary>
  public string Caption
  {
    [DebuggerStepThrough] get => this._Caption;
    set => this._Caption = value;
  }

  /// <summary>Более подробное текстовое описание команды</summary>
  public string Hint
  {
    [DebuggerStepThrough] get => this._Hint;
    set => this._Hint = value;
  }

  /// <summary>указывает, из какой коллекции брать иконку</summary>
  public ImageListSource ImageListSource
  {
    [DebuggerStepThrough] get => this._ImageListSource;
    set => this._ImageListSource = value;
  }

  /// <summary>
  /// Индекс значка команды (из коллекции указанной в ImageListSource)
  /// </summary>
  public int ImageIndex
  {
    [DebuggerStepThrough] get => this._ImageIndex;
  }

  /// <summary>
  /// Флажок позволяет прятать или показывать данную команду в контекстных меню "Навигатора"
  /// </summary>
  public bool Visible
  {
    [DebuggerStepThrough] get => this._Visible;
    set => this._Visible = value;
  }

  /// <summary>Номер группы команды</summary>
  public int Group
  {
    [DebuggerStepThrough] get => this._Group;
    set => this._Group = value;
  }

  /// <summary>Порядковый номер внутри группы</summary>
  public int OrderBy
  {
    [DebuggerStepThrough] get => this._OrderID;
    set => this._OrderID = value;
  }

  /// <summary>Коллекция дочерних команд</summary>
  public AdjustableMenuCommands Items
  {
    [DebuggerStepThrough] get => this._Items;
  }

  /// <summary>Дочерняя команда с указанным индексом</summary>
  /// <param name="index">Индекс</param>
  /// <returns>Дочерняя команда с указанным индексом</returns>
  public AdjustableMenuCommand this[int index] => this._Items[index];

  /// <summary>Количество дочерних команд</summary>
  public int Count => this._Items.Count;

  /// <summary>Горячие клавиши</summary>
  public Keys Shortcut
  {
    [DebuggerStepThrough] get => this._Shortcut;
    set => this._Shortcut = value;
  }

  /// <summary>для хранения различной информации</summary>
  public object Tag
  {
    [DebuggerStepThrough] set => this._Tag = value;
    get => this._Tag;
  }

  /// <summary>
  /// Отыскать в коллекции команду с указанным именем. Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="command">Уникальное в пределах системы имя команды</param>
  /// <returns>null, если команда не найдена</returns>
  public virtual AdjustableMenuCommand FindCommand(string command)
  {
    if (command == string.Empty)
      return (AdjustableMenuCommand) null;
    if (command == this._Command)
      return this;
    for (int index = 0; index < this._Items.Count; ++index)
    {
      AdjustableMenuCommand command1 = this._Items[index].FindCommand(command);
      if (command1 != null)
        return command1;
    }
    return (AdjustableMenuCommand) null;
  }

  /// <summary>Пакетная установка свойств</summary>
  /// <param name="options">Массив опций</param>
  public virtual void BatchPropertiesSet(params object[] options)
  {
    if (options == null || options.Length == 0)
      return;
    this._Visible = (bool) options[0];
    for (int index = 0; index < this._Items.Count; ++index)
      this._Items[index].BatchPropertiesSet(options);
  }

  /// <summary>Извлечение списка копий команд в словарь (Dictionary)</summary>
  /// <param name="command">Коллекция команд</param>
  /// <param name="list">Словарик с командами</param>
  /// <returns>Список команд меню</returns>
  public static void ExtractCommands(
    AdjustableMenuCommand command,
    ref Dictionary<string, AdjustableMenuCommand> list)
  {
    if (!list.ContainsKey(command.Command))
      list.Add(command.Command, command);
    for (int index = 0; index < command.Items.Count; ++index)
      AdjustableMenuCommand.ExtractCommands(command.Items[index], ref list);
  }

  /// <summary>Извлечение списка копий команд в список</summary>
  /// <param name="command">Коллекция команд</param>
  /// <param name="list">Список с командами</param>
  public static void ExtractCommands(
    AdjustableMenuCommand command,
    ref List<AdjustableMenuCommand> list)
  {
    list.Add(command);
    for (int index = 0; index < command.Items.Count; ++index)
      AdjustableMenuCommand.ExtractCommands(command.Items[index], ref list);
  }

  /// <summary>
  /// Выполнить синхронизацию команды меню с командами из указанного списка
  /// </summary>
  /// <param name="commands">Список команд, с которыми надо выполнить синхронизацию</param>
  public virtual void SyncWithCommands(Dictionary<string, AdjustableMenuCommand> commands)
  {
    if (commands == null)
      return;
    AdjustableMenuCommand command = commands.ContainsKey(this._Command) ? commands[this._Command] : (AdjustableMenuCommand) null;
    this._Visible = command != null ? command.Visible : this._Visible;
    if (command != null)
    {
      this._Group = command._Group;
      this._OrderID = command._OrderID;
      this._Hint = command._Hint;
      this._Tag = command.Tag;
    }
    for (int index = 0; index < this._Items.Count; ++index)
      this._Items[index].SyncWithCommands(commands);
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return !(obj is AdjustableMenuCommand adjustableMenuCommand) ? base.Equals(obj) : this._Command == adjustableMenuCommand._Command;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this._Command.GetHashCode();

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new AdjustableMenuCommand(this.Parent, this.Command, this.Caption, this.Visible, this.Hint, this.ImageIndex, this.Group, this.OrderBy, this.Shortcut, this.ImageListSource, this.Tag)
    {
      _Items = (this._Items.Clone() as AdjustableMenuCommands)
    };
  }

  /// <summary>Сравнить с другой настраиваемой командой меню</summary>
  /// <param name="other">Другая настраиваемая команда меню</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(AdjustableMenuCommand other)
  {
    if (other == null)
      return 1;
    int num = this._Group.CompareTo(other._Group);
    if (num == 0)
      num = this._OrderID.CompareTo(other._OrderID);
    return num;
  }
}
