
// Type: Intermech.Client.Core.DefaultCommands4ObjTypes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;


namespace Intermech.Client.Core;

/// <summary>
/// Класс коллекции команд по умолчанию для указанных типов объектов
/// </summary>
public class DefaultCommands4ObjTypes : IDefaultCommands4ObjTypes
{
  /// <summary>
  /// Системная команда по умолчанию, если вообще ничего не найдено для указанного типа объекта
  /// </summary>
  private IDefaultCommand defaultSystemCommand = (IDefaultCommand) new DefaultCommand(-1, "ParametersCard", DefaultCommandHandler.ContectMenu);
  /// <summary>
  /// Коллекция системных команд по умолчанию.
  /// Пары значений [(Int32)ID типа объекта] = [(DefaultCommand)Команда по умолчанию]
  /// </summary>
  private HybridDictionary _systemCommands = new HybridDictionary(0, true);
  /// <summary>
  /// Коллекция пользовательских команд по умолчанию.
  /// Пары значений [(Int32)ID типа объекта] = [(DefaultCommand)Команда по умолчанию]
  /// </summary>
  private HybridDictionary _Commands = new HybridDictionary(0, true);
  private DefaultCommandSettings[] _defaultCommandsSettings;
  private bool _isDefaultCommandsSettingsLoaded;
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();

  /// <summary>Создать экземпляр класса DefaultCommands4ObjTypes</summary>
  public DefaultCommands4ObjTypes() => this.RegisterSystemCommands();

  /// <summary>Отыскать команду по умолчанию для указанного типа</summary>
  /// <param name="objectTypeID">Тип объекта, для которого отыскивается команда по умолчанию</param>
  /// <param name="GetSystemDefaultOnError">Если указать true, то в случае, если не будет найдена команда по умолчанию,
  /// будет возвращена системная команда по умолчанию для указанного типа объектов</param>
  /// <returns>Команда по умолчанию для указанного типа объектов или null</returns>
  internal virtual IDefaultCommand FindTypeCommand(
    int objectTypeID,
    bool GetSystemDefaultOnError,
    bool useDefaultCommandSettings = true)
  {
    if (useDefaultCommandSettings)
    {
      DefaultCommandSettings settingsForObjectType = this.GetDefaultCommandSettingsForObjectType(objectTypeID);
      if (settingsForObjectType != null)
        return this.CreateDefaultCommandForDefaultCommandSettings(settingsForObjectType);
    }
    List<int> intList = new List<int>(0);
    if (this._Commands[(object) objectTypeID] is IDefaultCommand command1)
      return command1;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBObjectTypeInfo objectType = service.GetObjectType(objectTypeID, false);
    if (objectType == null)
      return (IDefaultCommand) null;
    while (objectType.ParentTypeID != -1)
    {
      intList.Add(objectType.ParentTypeID);
      objectType = service.GetObjectType(objectType.ParentTypeID, false);
      if (objectType != null)
      {
        if (this._Commands[(object) objectType.ObjectType] is IDefaultCommand command2)
          return command2;
      }
      else
        break;
    }
    if (intList.Count == 0)
    {
      if (this._systemCommands[(object) objectTypeID] is IDefaultCommand systemCommand)
        return systemCommand;
      return GetSystemDefaultOnError ? this.defaultSystemCommand : (IDefaultCommand) null;
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      if (this._systemCommands[(object) intList[index]] is IDefaultCommand systemCommand)
        return systemCommand;
    }
    if (!GetSystemDefaultOnError)
      return (IDefaultCommand) null;
    return this._systemCommands[(object) objectTypeID] is IDefaultCommand systemCommand1 ? systemCommand1 : this.defaultSystemCommand;
  }

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
  public IDefaultCommand this[int ObjectTypeID, bool GetSystemDefaultOnError]
  {
    get
    {
      return this._Commands[(object) ObjectTypeID] is IDefaultCommand command ? command : this.FindTypeCommand(ObjectTypeID, GetSystemDefaultOnError);
    }
  }

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
  public IDefaultCommand this[Guid ObjectTypeGuid, bool GetSystemDefaultOnError]
  {
    get
    {
      IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(ObjectTypeGuid, false);
      return objectType == null ? (IDefaultCommand) null : this.FindTypeCommand(objectType.ObjectType, GetSystemDefaultOnError);
    }
  }

  /// <summary>
  /// Добавить команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого назначается команда по умолчанию</param>
  /// <param name="DefaultCommandName">Имя команды по умолчанию</param>
  /// <param name="CommandHandler">Чья команда - контекстного меню или ICommandManager</param>
  public virtual void AddDefaultCommand(
    int ObjectTypeID,
    string DefaultCommandName,
    DefaultCommandHandler CommandHandler)
  {
    if (ObjectTypeID == -1 || DefaultCommandName == string.Empty)
      return;
    this.RemoveDefaultCommand(ObjectTypeID);
    this._Commands[(object) ObjectTypeID] = (object) new DefaultCommand(ObjectTypeID, DefaultCommandName, CommandHandler);
  }

  /// <summary>
  /// Удалить команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого удаляется команда по умолчанию</param>
  public virtual void RemoveDefaultCommand(int ObjectTypeID)
  {
    this._Commands.Remove((object) ObjectTypeID);
  }

  public void ReloadDefaultCommandsSettings()
  {
    this._defaultCommandsSettings = (DefaultCommandSettings[]) null;
    this._isDefaultCommandsSettingsLoaded = false;
  }

  public IDefaultCommand GetDefaultCommandWithoutDefaultCommandSettings(int objectTypeID)
  {
    return !ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID) ? this.FindTypeCommand(objectTypeID, true, false) : throw new ArgumentException();
  }

  /// <summary>
  /// Добавить системную команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeGuid">Тип объекта, для которого назначается команда по умолчанию</param>
  /// <param name="DefaultCommandName">Имя команды по умолчанию</param>
  /// <param name="CommandHandler">Чья команда - контекстного меню или ICommandManager</param>
  internal virtual void AddSystemCommand(
    Guid ObjectTypeGuid,
    string DefaultCommandName,
    DefaultCommandHandler CommandHandler)
  {
    if (ObjectTypeGuid == Guid.Empty || DefaultCommandName == string.Empty)
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(ObjectTypeGuid);
    if (objectType == null)
      return;
    this.AddSystemCommand(objectType.ObjectTypeID, DefaultCommandName, CommandHandler);
  }

  /// <summary>
  /// Добавить системную команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого назначается команда по умолчанию</param>
  /// <param name="DefaultCommandName">Имя команды по умолчанию</param>
  /// <param name="CommandHandler">Чья команда - контекстного меню или ICommandManager</param>
  internal virtual void AddSystemCommand(
    int ObjectTypeID,
    string DefaultCommandName,
    DefaultCommandHandler CommandHandler)
  {
    if (ObjectTypeID == -1 || DefaultCommandName == string.Empty)
      return;
    this.RemoveSystemCommand(ObjectTypeID);
    this._systemCommands[(object) ObjectTypeID] = (object) new DefaultCommand(ObjectTypeID, DefaultCommandName, CommandHandler);
  }

  /// <summary>
  /// Удалить системную команду по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="ObjectTypeID">Тип объекта, для которого удаляется системная команда по умолчанию</param>
  internal virtual void RemoveSystemCommand(int ObjectTypeID)
  {
    this._systemCommands.Remove((object) ObjectTypeID);
  }

  /// <summary>
  /// Зарегистрировать команды по умолчанию для некоторых системных типов
  /// </summary>
  internal virtual void RegisterSystemCommands()
  {
    this.AddSystemCommand(new Guid("cad001b3-306c-11d8-b4e9-00304f19f545"), "ParametersCard", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad00129-306c-11d8-b4e9-00304f19f545"), "EditDocument", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"), "EditDocument", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad00134-306c-11d8-b4e9-00304f19f545"), "EditDocument", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"), "EditDocument", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad0011e-306c-11d8-b4e9-00304f19f545"), "OpenInNewWindow", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad0011b-306c-11d8-b4e9-00304f19f545"), "EditDocument", DefaultCommandHandler.ContectMenu);
    this.AddSystemCommand(new Guid("cad00140-306c-11d8-b4e9-00304f19f545"), "ViewDocument", DefaultCommandHandler.ContectMenu);
  }

  private DefaultCommandSettings GetDefaultCommandSettingsForObjectType(int objectTypeID)
  {
    DefaultCommandSettings[] commandsSettings = this.GetDefaultCommandsSettings();
    DefaultCommandSettings settingsForObjectType;
    for (settingsForObjectType = (DefaultCommandSettings) null; settingsForObjectType == null && !ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID); objectTypeID = MetaDataHelper.GetObjectTypeParentID(objectTypeID))
      settingsForObjectType = ((IEnumerable<DefaultCommandSettings>) commandsSettings).FirstOrDefault<DefaultCommandSettings>((Func<DefaultCommandSettings, bool>) (o => o.ObjectTypeID == objectTypeID));
    return settingsForObjectType;
  }

  private DefaultCommandSettings[] GetDefaultCommandsSettings()
  {
    this.LoadDefaultCommandsSettings();
    return this._defaultCommandsSettings;
  }

  private void LoadDefaultCommandsSettings()
  {
    if (this._isDefaultCommandsSettingsLoaded)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._defaultCommandsSettings = (sessionKeeper.Session.GetCustomService(typeof (IDefaultCommandsSettingsServerService)) as IDefaultCommandsSettingsServerService).FindDefaultCommandsSettingsForRole(sessionKeeper.Session.SessionGUID, this._currentUserAndRole.Value.RoleID);
    this._isDefaultCommandsSettingsLoaded = true;
  }

  private IDefaultCommand CreateDefaultCommandForDefaultCommandSettings(
    DefaultCommandSettings defaultCommandSettings)
  {
    return (IDefaultCommand) new DefaultCommand(defaultCommandSettings.ObjectTypeID, defaultCommandSettings.CommandName, DefaultCommandHandler.ContectMenu);
  }
}
