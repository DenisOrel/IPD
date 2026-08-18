
// Type: Intermech.Files.AreaBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.IO;


namespace Intermech.Files;

/// <summary>
/// Базовый класс для файловых областей в файловом хранилище пользователя.
/// </summary>
internal abstract class AreaBase : IFileArea
{
  protected readonly FileVaultService vault;
  protected readonly string areaDirectory;
  protected readonly string areaPath;
  protected readonly string areaDisplayName;

  /// <summary>Создает объект.</summary>
  /// <param name="vault">Ссылка на сервис файлового хранилища</param>
  /// <param name="areaDirectory">Имя каталога файловой области</param>
  /// <param name="displayName">Понятное пользовалею имя файловой области</param>
  public AreaBase(FileVaultService vault, string areaDirectory, string displayName)
  {
    if (vault == null)
      throw new ArgumentNullException("null");
    if (string.IsNullOrEmpty(areaDirectory))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(displayName))
      throw new ArgumentException();
    this.vault = vault;
    this.areaDirectory = areaDirectory;
    this.areaPath = Path.Combine(vault.VaultPath, areaDirectory);
    this.areaDisplayName = displayName;
  }

  /// <summary>Выполняет инициализацию файловой области.</summary>
  internal virtual void Initialize()
  {
    if (Directory.Exists(this.areaPath))
      return;
    Directory.CreateDirectory(this.areaPath);
  }

  protected void HideAreaDirectory()
  {
    FileAttributes attributes = File.GetAttributes(this.areaPath);
    if ((attributes & FileAttributes.Hidden) != (FileAttributes) 0 && (attributes & FileAttributes.System) != (FileAttributes) 0)
      return;
    File.SetAttributes(this.areaPath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory);
  }

  /// <summary>
  /// Возвращает понятное пользователю название файловой области.
  /// </summary>
  public string DisplayName => this.areaDisplayName;

  /// <summary>
  /// Возврашает абсолютный путь к каталогу файловой области.
  /// </summary>
  public string AreaPath => this.areaPath;

  internal static void CheckAccessRights(long objectId, ActionType accessRight)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IDBSecurity) sessionKeeper.Session.GetObject(objectId, true)).CheckAccess(accessRight, true);
  }

  internal static bool HasAccessRights(long objectId, ActionType accessRight)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
        return ((IDBSecurity) dbObject).CheckAccess(accessRight, true, false);
    }
    return false;
  }
}
