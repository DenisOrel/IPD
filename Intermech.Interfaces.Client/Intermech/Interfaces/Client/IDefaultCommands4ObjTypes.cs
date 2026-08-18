// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IDefaultCommands4ObjTypes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс коллекции команд по умолчанию для указанных типов объектов
/// </summary>
public interface IDefaultCommands4ObjTypes
{
  /// <summary>
  /// Найти команду по умолчанию для указанного типа объекта.
  /// Если команда не найдена у данного типа объектов, то изучается весь список родительских
  /// типов объектов.
  /// Если команда не была найдена, то вернёт null или
  /// системную команду по умолчанию, если GetSystemDefaultOnError = true.
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого отыскивается команда по умолчанию</param>
  /// <param name="GetSystemDefaultOnError">Если указать true, то в случае, если не будет найдена команда по умолчанию,
  /// будет возвращена системная команда по умолчанию для указанного типа объектов</param>
  /// <returns>Команда по умолчанию для указанного типа объектов или null</returns>
  IDefaultCommand this[int ObjectTypeID, bool GetSystemDefaultOnError] { get; }

  /// <summary>
  /// Найти команду по умолчанию для указанного типа объекта.
  /// Если команда не найдена у данного типа объектов, то изучается весь список родительских
  /// типов объектов.
  /// Если команда не была найдена, то вернёт null или
  /// системную команду по умолчанию, если GetSystemDefaultOnError = true.
  /// </summary>
  /// <param name="ObjectTypeGuid">Тип объекта, для которого отыскивается команда по умолчанию</param>
  /// <param name="GetSystemDefaultOnError">Если указать true, то в случае, если не будет найдена команда по умолчанию,
  /// будет возвращена системная команда по умолчанию для указанного типа объектов</param>
  /// <returns>Команда по умолчанию для указанного типа объектов или null</returns>
  IDefaultCommand this[Guid ObjectTypeGuid, bool GetSystemDefaultOnError] { get; }

  /// <summary>
  /// Добавить команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого назначается команда по умолчанию</param>
  /// <param name="DefaultCommandName">Имя команды по умолчанию</param>
  /// <param name="CommandHandler">Чья команда - контекстного меню или ICommandManager</param>
  void AddDefaultCommand(
    int ObjectTypeID,
    string DefaultCommandName,
    DefaultCommandHandler CommandHandler);

  /// <summary>
  /// Удалить команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого удаляется команда по умолчанию</param>
  void RemoveDefaultCommand(int ObjectTypeID);

  IDefaultCommand GetDefaultCommandWithoutDefaultCommandSettings(int objectTypeID);

  void ReloadDefaultCommandsSettings();
}
