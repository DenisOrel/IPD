
// Type: Intermech.Client.Core.DefaultCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>Класс команды по умолчанию для типа объекта</summary>
[DebuggerDisplay("ObjectType [{ObjectTypeID}]; command: \"{DefaultCommandName}\"; handler: {CommandHandler}")]
[Serializable]
public sealed class DefaultCommand : IDefaultCommand, ICloneable
{
  /// <summary>
  /// Тип объекта, для которого назначается команда по умолчанию
  /// </summary>
  private int _objectTypeID = -1;
  /// <summary>Имя команды по умолчанию</summary>
  private string _defaultCommandName = string.Empty;
  /// <summary>
  /// Обработчик команды по умолчанию - контекстное меню или ICommandManager
  /// </summary>
  private DefaultCommandHandler _commandHandler;

  /// <summary>Создать экземпляр класса DefaultCommand</summary>
  /// <param name="AnObjectTypeID">Тип объекта, для которого назначается команда по умолчанию</param>
  /// <param name="ADefaultCommandName">Имя команды по умолчанию</param>
  /// <param name="ACommandHandler">Чья команда - контекстного меню или ICommandManager</param>
  public DefaultCommand(
    int AnObjectTypeID,
    string ADefaultCommandName,
    DefaultCommandHandler ACommandHandler)
  {
    this._objectTypeID = AnObjectTypeID;
    this._defaultCommandName = ADefaultCommandName;
    this._commandHandler = ACommandHandler;
  }

  /// <summary>
  /// Тип объекта, для которого назначается команда по умолчанию
  /// </summary>
  public int ObjectTypeID
  {
    get => this._objectTypeID;
    set => this._objectTypeID = value;
  }

  /// <summary>Имя команды по умолчанию</summary>
  public string DefaultCommandName
  {
    get => this._defaultCommandName;
    set => this._defaultCommandName = value;
  }

  /// <summary>
  /// Обработчик команды по умолчанию - контекстное меню или ICommandManager
  /// </summary>
  public DefaultCommandHandler CommandHandler
  {
    get => this._commandHandler;
    set => this._commandHandler = value;
  }

  /// <summary>Создать копию экземпляра класса DefaultCommand</summary>
  /// <returns>Копия экземпляра класса DefaultCommand</returns>
  public object Clone()
  {
    return (object) new DefaultCommand(this._objectTypeID, this._defaultCommandName, this._commandHandler);
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если текущий экземпляр класса равен указанному объекту</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is DefaultCommand defaultCommand))
      return base.Equals(obj);
    return this._commandHandler == defaultCommand._commandHandler && this._defaultCommandName == defaultCommand._defaultCommandName && this._objectTypeID == defaultCommand._objectTypeID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return (int) this._commandHandler << 31 /*0x1F*/ ^ this._objectTypeID.GetHashCode() ^ this._defaultCommandName.GetHashCode();
  }
}
