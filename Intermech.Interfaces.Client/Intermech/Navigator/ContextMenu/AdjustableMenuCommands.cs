// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.AdjustableMenuCommands
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Коллекция настраиваемых команд контекстных меню</summary>
[Serializable]
public class AdjustableMenuCommands : List<AdjustableMenuCommand>, ICloneable
{
  /// <summary>Guid настроек роли</summary>
  public static string RoleSettingsGuid = "{43957778-256F-4739-9C91-5B8E28E8607A}";
  /// <summary>Guid настроек пользователя</summary>
  public static string UserSettingsGuid = "{51403C2B-7A58-49FC-B847-88483DD07E3C}";
  /// <summary>Родительская коллекция</summary>
  protected AdjustableMenuCommands _Parent;

  /// <summary>Создать пустой экземпляр класса</summary>
  public AdjustableMenuCommands()
    : base(0)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="parent">Родительская коллекция</param>
  public AdjustableMenuCommands(AdjustableMenuCommands parent)
    : base(0)
  {
    this._Parent = parent;
  }

  /// <summary>Родительская коллекция</summary>
  public AdjustableMenuCommands Parent
  {
    [DebuggerStepThrough] get => this._Parent;
  }

  /// <summary>Отыскать описание команды по её имени в списке</summary>
  /// <param name="commands">Список команд</param>
  /// <param name="command">Имя разыскиваемой команды</param>
  /// <returns>Найденное описание команды или null</returns>
  public static AdjustableMenuCommand FindCommand(ArrayList commands, string command)
  {
    if (commands == null || command == string.Empty)
      return (AdjustableMenuCommand) null;
    for (int index = 0; index < commands.Count; ++index)
    {
      if (commands[index] is AdjustableMenuCommand command1 && command1.Command == command)
        return command1;
    }
    return (AdjustableMenuCommand) null;
  }

  /// <summary>
  /// Отыскать в коллекции (начиная с её корневой записи) команду с указанным именем.
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="command">Уникальное в пределах системы имя команды</param>
  /// <returns>null, если команда не найдена</returns>
  public virtual AdjustableMenuCommand FindCommandFromRoot(string command)
  {
    if (command == string.Empty)
      return (AdjustableMenuCommand) null;
    AdjustableMenuCommands adjustableMenuCommands = this;
    while (adjustableMenuCommands.Parent != null)
      adjustableMenuCommands = adjustableMenuCommands.Parent;
    return adjustableMenuCommands.FindCommand(command);
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
    for (int index = 0; index < this.Count; ++index)
    {
      AdjustableMenuCommand command1 = this[index].FindCommand(command);
      if (command1 != null)
        return command1;
    }
    return (AdjustableMenuCommand) null;
  }

  /// <summary>Найти предыдущую команду</summary>
  /// <param name="command">Команда в коллекции</param>
  /// <returns>Предыдущая команда в коллекции</returns>
  public virtual AdjustableMenuCommand FindPrevCommand(AdjustableMenuCommand command)
  {
    int num = this.IndexOf(command);
    return num > 0 ? this[num - 1] : (AdjustableMenuCommand) null;
  }

  /// <summary>Найти следующую команду</summary>
  /// <param name="command">Команда в коллекции</param>
  /// <returns>Следующая команда в коллекции</returns>
  public virtual AdjustableMenuCommand FindNextCommand(AdjustableMenuCommand command)
  {
    int num = this.IndexOf(command);
    return num >= 0 && num < this.Count - 1 ? this[num + 1] : (AdjustableMenuCommand) null;
  }

  /// <summary>Проверить, является ли команда первой в группе</summary>
  /// <param name="command">Команда в коллекции</param>
  /// <returns>true - команда является первой в группе</returns>
  public virtual bool IsCommandFirstInGroup(AdjustableMenuCommand command)
  {
    int num = this.IndexOf(command);
    return num <= 0 || command.Group > this[num - 1].Group;
  }

  /// <summary>Проверить, является ли команда последней в группе</summary>
  /// <param name="command">Команда в коллекции</param>
  /// <returns>true - команда является последней в группе</returns>
  public virtual bool IsCommandLastInGroup(AdjustableMenuCommand command)
  {
    int num1 = this.IndexOf(command);
    if (num1 < 0)
      return true;
    if (num1 < this.Count - 1)
      return command.Group < this[num1 + 1].Group;
    int num2 = this.Count - 1;
    return true;
  }

  /// <summary>Добавить новую настраиваемую команду</summary>
  /// <param name="command">Уникальная в пределах системы команда</param>
  /// <param name="caption">Краткое текстовое описание команды</param>
  /// <param name="hint">Подробное текстовое описание команды</param>
  /// <param name="imageIndex">Индекс изображения команды (из списка именованных значков)</param>
  /// <param name="visible">Показывать ли указанную команду в контекстных меню</param>
  /// <param name="group">Номер группы команды</param>
  /// <param name="orderBy">Порядковый номер команды в группе</param>
  /// <param name="shortcut">Горячие клавиши</param>
  /// <param name="imageListSource">С помощью какого сервиса получать иконку: INamedImageList или ICategoryTypeIconService</param>
  /// <returns>Ссылка на новую настраиваемую команду</returns>
  public virtual AdjustableMenuCommand Add(
    string command,
    string caption,
    string hint,
    int imageIndex,
    bool visible,
    int group,
    int orderBy,
    Keys shortcut,
    ImageListSource imageListSource)
  {
    return this.Add(command, caption, hint, imageIndex, visible, group, orderBy, shortcut, imageListSource, (object) null);
  }

  public virtual AdjustableMenuCommand Add(
    string command,
    string caption,
    string hint,
    int imageIndex,
    bool visible,
    int group,
    int orderBy,
    Keys shortcut,
    ImageListSource imageListSource,
    object tag)
  {
    AdjustableMenuCommand commandFromRoot = this.FindCommandFromRoot(command);
    if (commandFromRoot != null)
      return commandFromRoot;
    AdjustableMenuCommand adjustableMenuCommand = new AdjustableMenuCommand(this, command, caption, visible, hint, imageIndex, group, orderBy, shortcut, imageListSource, tag);
    this.Add(adjustableMenuCommand);
    return adjustableMenuCommand;
  }

  /// <summary>Пакетная установка свойств всем командам</summary>
  /// <param name="options">Массив опций</param>
  public virtual void BatchPropertiesSet(params object[] options)
  {
    if (options == null || options.Length == 0)
      return;
    for (int index = 0; index < this.Count; ++index)
      this[index].BatchPropertiesSet(options);
  }

  /// <summary>Извлечение списка копий команд в словарь (Dictionary)</summary>
  /// <param name="commands">Коллекция команд</param>
  /// <param name="list">Словарик с командами</param>
  /// <returns>Список команд меню</returns>
  public static void ExtractCommands(
    AdjustableMenuCommands commands,
    ref Dictionary<string, AdjustableMenuCommand> list)
  {
    if (list == null)
      list = new Dictionary<string, AdjustableMenuCommand>();
    AdjustableMenuCommands adjustableMenuCommands = commands;
    while (adjustableMenuCommands.Parent != null)
      adjustableMenuCommands = adjustableMenuCommands.Parent;
    for (int index = 0; index < adjustableMenuCommands.Count; ++index)
      AdjustableMenuCommand.ExtractCommands(adjustableMenuCommands[index], ref list);
  }

  /// <summary>Извлечение списка копий команд в список</summary>
  /// <param name="commands">Коллекция команд</param>
  /// <param name="list">Список с командами</param>
  public static void ExtractCommands(
    AdjustableMenuCommands commands,
    ref List<AdjustableMenuCommand> list)
  {
    if (list == null)
      list = new List<AdjustableMenuCommand>();
    AdjustableMenuCommands adjustableMenuCommands = commands;
    while (adjustableMenuCommands.Parent != null)
      adjustableMenuCommands = adjustableMenuCommands.Parent;
    for (int index = 0; index < adjustableMenuCommands.Count; ++index)
      AdjustableMenuCommand.ExtractCommands(adjustableMenuCommands[index], ref list);
  }

  /// <summary>Полное присваивание другого списка команд</summary>
  /// <param name="source">Источник</param>
  public virtual void Assign(AdjustableMenuCommands source)
  {
    this.Clear();
    if (source == null)
      return;
    this._Parent = source._Parent;
    for (int index = 0; index < source.Count; ++index)
      this.Add(source[index]);
  }

  /// <summary>
  /// Выполнить синхронизацию настроек коллекции команд меню с командами из указанного списка
  /// </summary>
  /// <param name="commands">Список команд, с которыми надо выполнить синхронизацию</param>
  public virtual void SyncWithCommands(AdjustableMenuCommands commands)
  {
    if (commands == null || commands.Count == 0)
      return;
    AdjustableMenuCommands commands1 = commands;
    while (commands1.Parent != null)
      commands1 = commands1.Parent;
    Dictionary<string, AdjustableMenuCommand> list = (Dictionary<string, AdjustableMenuCommand>) null;
    AdjustableMenuCommands.ExtractCommands(commands1, ref list);
    this.SyncWithCommands(list);
  }

  /// <summary>
  /// Выполнить синхронизацию настроек коллекции команд меню с командами из указанного списка
  /// </summary>
  /// <param name="commands">Список команд, с которыми надо выполнить синхронизацию</param>
  public virtual void SyncWithCommands(Dictionary<string, AdjustableMenuCommand> commands)
  {
    if (commands == null || commands.Count == 0)
      return;
    for (int index = 0; index < this.Count; ++index)
      this[index].SyncWithCommands(commands);
    this.Sort();
  }

  /// <summary>
  /// Выполнить синхронизацию с настройками команд меню у указанной роли
  /// </summary>
  /// <param name="RoleID">Идентификатор роли</param>
  public virtual void SyncWithRoleSettings(long RoleID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
        return;
      if (!(customService.GetRoleSettingsObject(RoleID, (object) AdjustableMenuCommands.RoleSettingsGuid) is byte[] roleSettingsObject))
      {
        customService.LoadRolesSettings((object) sessionKeeper.Session.SessionGUID);
        roleSettingsObject = customService.GetRoleSettingsObject(RoleID, (object) AdjustableMenuCommands.RoleSettingsGuid) as byte[];
      }
      AdjustableMenuCommands commands = (AdjustableMenuCommands) null;
      if (roleSettingsObject != null)
      {
        try
        {
          MemoryStream memoryStream = new MemoryStream(roleSettingsObject);
          MemoryStream outStream = new MemoryStream();
          long num = ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
          if (num > 0L)
          {
            memoryStream.Close();
            memoryStream = outStream;
          }
          else
            memoryStream.Seek(0L, SeekOrigin.Begin);
          try
          {
            commands = new BinaryFormatter().Deserialize((Stream) memoryStream) as AdjustableMenuCommands;
          }
          catch
          {
            commands = (AdjustableMenuCommands) null;
          }
          finally
          {
            if (num > 0L)
            {
              outStream.Close();
            }
            else
            {
              memoryStream.Close();
              outStream.Close();
            }
          }
        }
        catch
        {
          commands = (AdjustableMenuCommands) null;
        }
      }
      this.BatchPropertiesSet((object) true);
      this.SyncWithCommands(commands);
    }
  }

  /// <summary>
  /// Сохранить настройки команд контекстных меню в настройки указанной роли
  /// </summary>
  /// <param name="RoleID">Идентификатор роли</param>
  public virtual void SaveToRoleSettings(long RoleID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        try
        {
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService.SetRoleSettingsObject(RoleID, (object) AdjustableMenuCommands.RoleSettingsGuid, (object) outStream.ToArray());
        }
        catch
        {
        }
      }
    }
  }

  /// <summary>
  /// Выполнить синхронизацию с настройками команд меню у указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public virtual void SyncWithUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    byte[] buffer = customService[UserID, (object) AdjustableMenuCommands.UserSettingsGuid] as byte[];
    AdjustableMenuCommands commands = (AdjustableMenuCommands) null;
    if (buffer != null)
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream(buffer);
        MemoryStream outStream = new MemoryStream();
        long num = ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
        if (num > 0L)
        {
          memoryStream.Close();
          memoryStream = outStream;
        }
        else
          memoryStream.Seek(0L, SeekOrigin.Begin);
        try
        {
          commands = new BinaryFormatter().Deserialize((Stream) memoryStream) as AdjustableMenuCommands;
        }
        catch
        {
          commands = (AdjustableMenuCommands) null;
        }
        finally
        {
          if (num > 0L)
          {
            outStream.Close();
          }
          else
          {
            memoryStream.Close();
            outStream.Close();
          }
        }
      }
      catch
      {
        commands = (AdjustableMenuCommands) null;
      }
    }
    this.SyncWithCommands(commands);
  }

  /// <summary>
  /// Сохранить настройки команд контекстных меню в настройки указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public virtual void SaveToUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        try
        {
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService[UserID, (object) AdjustableMenuCommands.UserSettingsGuid] = (object) outStream.ToArray();
        }
        catch
        {
        }
      }
    }
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    AdjustableMenuCommands adjustableMenuCommands = new AdjustableMenuCommands(this.Parent);
    for (int index = 0; index < this.Count; ++index)
      adjustableMenuCommands.Add(this[index].Clone() as AdjustableMenuCommand);
    return (object) adjustableMenuCommands;
  }

  /// <summary>Сортировка коллекции</summary>
  public new virtual void Sort()
  {
    base.Sort();
    for (int index = 0; index < this.Count; ++index)
      this[index]?.Items.Sort();
    this.RebuildNumbers();
  }

  /// <summary>Перестроить номера групп и внутри групп</summary>
  public virtual void RebuildNumbers()
  {
    if (this.Count == 0)
      return;
    int group = this[0].Group;
    int key = 0;
    int num1 = 10;
    int num2 = 0;
    int num3 = 10;
    Dictionary<int, List<AdjustableMenuCommand>> dictionary = new Dictionary<int, List<AdjustableMenuCommand>>();
    for (int index = 0; index < this.Count; ++index)
    {
      AdjustableMenuCommand adjustableMenuCommand = this[index];
      if (adjustableMenuCommand.Group != group)
      {
        group = adjustableMenuCommand.Group;
        key += num1;
      }
      adjustableMenuCommand.Group = key;
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, new List<AdjustableMenuCommand>());
      dictionary[key].Add(adjustableMenuCommand);
      adjustableMenuCommand.Items.RebuildNumbers();
    }
    foreach (KeyValuePair<int, List<AdjustableMenuCommand>> keyValuePair in dictionary)
    {
      for (int index = 0; index < keyValuePair.Value.Count; ++index)
      {
        keyValuePair.Value[index].OrderBy = num2;
        num2 += num3;
      }
    }
  }

  public bool CanMoveTop(AdjustableMenuCommand adjustableMenuCommand)
  {
    if (adjustableMenuCommand == null)
      throw new ArgumentNullException(nameof (adjustableMenuCommand));
    return this.IndexOf(adjustableMenuCommand) > 0;
  }

  /// <summary>Можно ли переместить команду меню вверх</summary>
  /// <param name="command">Команда меню</param>
  /// <param name="onlyInGroup">true - перемещение допустимо только в рамках группы команды,
  /// иначе - в пределах всей коллекции</param>
  /// <returns>true - переместить вверх можно</returns>
  public bool CanMoveUp(AdjustableMenuCommand command, bool onlyInGroup)
  {
    if (command == null)
      return false;
    if (!onlyInGroup)
      return this.IndexOf(command) > 0;
    return !this.IsCommandFirstInGroup(command);
  }

  /// <summary>Можно ли переместить команду меню вниз</summary>
  /// <param name="command">Команда меню</param>
  /// <param name="onlyInGroup">true - перемещение допустимо только в рамках группы команды,
  /// иначе - в пределах всей коллекции</param>
  /// <returns>true - переместить вниз можно</returns>
  public bool CanMoveDown(AdjustableMenuCommand command, bool onlyInGroup)
  {
    if (command == null)
      return false;
    if (onlyInGroup)
      return !this.IsCommandLastInGroup(command);
    int num = this.IndexOf(command);
    return num >= 0 && num < this.Count - 1;
  }

  public bool CanMoveBottom(AdjustableMenuCommand adjustableMenuCommand)
  {
    int num = adjustableMenuCommand != null ? this.IndexOf(adjustableMenuCommand) : throw new ArgumentNullException(nameof (adjustableMenuCommand));
    return num >= 0 && num < this.Count - 1;
  }

  public void MoveTop(AdjustableMenuCommand adjustableMenuCommand)
  {
    if (adjustableMenuCommand == null)
      throw new ArgumentNullException(nameof (adjustableMenuCommand));
    if (!this.CanMoveTop(adjustableMenuCommand))
      throw new ArgumentException();
    AdjustableMenuCommand adjustableMenuCommand1 = this[0];
    adjustableMenuCommand.Group = adjustableMenuCommand1.Group;
    adjustableMenuCommand.OrderBy = adjustableMenuCommand1.OrderBy;
    this.Remove(adjustableMenuCommand);
    this.Insert(0, adjustableMenuCommand);
    for (int index = 1; index < this.Count; ++index)
    {
      if (adjustableMenuCommand.Group == this[index].Group)
        ++this[index].OrderBy;
    }
  }

  /// <summary>Переместить команду меню вверх</summary>
  /// <param name="command">Команда меню</param>
  /// <param name="onlyInGroup">true - перемещение допустимо только в рамках группы команды,
  /// иначе - в пределах всей коллекции</param>
  /// <returns>true - команда была успешно перемещена вверх</returns>
  public bool MoveUp(AdjustableMenuCommand command, bool onlyInGroup)
  {
    if (!this.CanMoveUp(command, onlyInGroup))
      return false;
    int num = this.IndexOf(command);
    AdjustableMenuCommand adjustableMenuCommand = this[num - 1];
    this.Remove(command);
    this.Insert(num - 1, command);
    command.Group = adjustableMenuCommand.Group;
    int orderBy = adjustableMenuCommand.OrderBy;
    adjustableMenuCommand.OrderBy = command.OrderBy;
    command.OrderBy = orderBy;
    return true;
  }

  /// <summary>Переместить команду меню вниз внутри своей группы</summary>
  /// <param name="command">Команда меню</param>
  /// <param name="onlyInGroup">true - перемещение допустимо только в рамках группы команды,
  /// иначе - в пределах всей коллекции</param>
  /// <returns>true - команда была успешно перемещена вниз</returns>
  public bool MoveDown(AdjustableMenuCommand command, bool onlyInGroup)
  {
    if (!this.CanMoveDown(command, onlyInGroup))
      return false;
    int num = this.IndexOf(command);
    AdjustableMenuCommand adjustableMenuCommand = this[num + 1];
    this.Remove(command);
    this.Insert(num + 1, command);
    command.Group = adjustableMenuCommand.Group;
    int orderBy = adjustableMenuCommand.OrderBy;
    adjustableMenuCommand.OrderBy = command.OrderBy;
    command.OrderBy = orderBy;
    return true;
  }

  public void MoveBottom(AdjustableMenuCommand adjustableMenuCommand)
  {
    if (adjustableMenuCommand == null)
      throw new ArgumentNullException(nameof (adjustableMenuCommand));
    if (!this.CanMoveBottom(adjustableMenuCommand))
      throw new ArgumentException();
    AdjustableMenuCommand adjustableMenuCommand1 = this[this.Count - 1];
    adjustableMenuCommand.Group = adjustableMenuCommand1.Group;
    adjustableMenuCommand.OrderBy = adjustableMenuCommand1.OrderBy + 1;
    this.Remove(adjustableMenuCommand);
    this.Add(adjustableMenuCommand);
  }

  /// <summary>
  /// Добавить группу в указанной команде
  /// Внимание! При выполнении удаления группы все команды её группы перемещаются
  /// в новую группу
  /// </summary>
  /// <param name="command">Настраиваемая команда контекстного меню</param>
  /// <returns>true - создание группы (перемещение команды в новую группу) выполнено успешно</returns>
  public bool AddGroup(AdjustableMenuCommand command)
  {
    if (command == null)
      return false;
    for (int index = this.IndexOf(command); index < this.Count; ++index)
    {
      command = this[index];
      command.Group += 100;
    }
    return true;
  }

  /// <summary>
  /// Можно ли удалить группу у указанной команды  (переместить её в предыдущую группу).
  /// Внимание! При выполнении удаления группы все команды удаляемой группы перемещаются
  /// в предыдущую группу
  /// </summary>
  /// <param name="command">Настраиваемая команда контекстного меню</param>
  /// <returns>true - можно удалить группу (переместить команду в предыдущую группу)</returns>
  public bool CanRemoveGroup(AdjustableMenuCommand command)
  {
    AdjustableMenuCommand prevCommand = this.FindPrevCommand(command);
    return prevCommand != null && prevCommand.Group != command.Group;
  }

  /// <summary>
  /// Удалить группу у указанной команды  (переместить её в предыдущую группу).
  /// Внимание! При выполнении удаления группы все команды удаляемой группы перемещаются
  /// в предыдущую группу
  /// </summary>
  /// <param name="command">Настраиваемая команда контекстного меню</param>
  /// <returns>true - удаление группы (перемещение команды в предыдущую группу) выполнено успешно</returns>
  public bool RemoveGroup(AdjustableMenuCommand command)
  {
    AdjustableMenuCommand prevCommand = this.FindPrevCommand(command);
    if (prevCommand == null || prevCommand.Group == command.Group)
      return false;
    int index = this.IndexOf(command);
    int group = command.Group;
    int num = prevCommand.OrderBy + 10;
    for (; index < this.Count; ++index)
    {
      command = this[index];
      if (command.Group == group)
      {
        command.Group = prevCommand.Group;
        command.OrderBy = num;
        num += 10;
      }
      else
        break;
    }
    return true;
  }
}
