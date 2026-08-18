// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IVaultSettings
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Vault.Interfaces;

public interface IVaultSettings
{
  /// <summary>
  /// максимальный размер тома. указывается в настройках
  /// изменения внесённые в файл настроек отобразятся
  /// только после заполения текущего тома, и открытия нового
  /// </summary>
  long CurrentVolumeSize { get; set; }

  /// <summary>время в днях жизни файлов истории</summary>
  uint HistoryLifeTime { get; set; }

  /// <summary>время в днях удалённых файлов</summary>
  uint DeletedLifeTime { get; set; }

  bool IsFullLogging { get; set; }

  string Password { set; }

  bool SyncModeOff { get; set; }

  bool ValidatePassword(string password);

  /// <summary>Добавление корневоого каталога для шкафа</summary>
  RootDirectory AddRootFolder(string path, short maxSize);

  /// <summary>Восстановление страховой копии шкафа</summary>
  /// <param name="path">путь</param>
  /// <param name="guid">guid восстанавливаемого файлового шкафа</param>
  /// <param name="maxSize">максимальный размер хранилища (в % от свободного места)</param>
  /// <returns></returns>
  RootDirectory RestoreRootFolder(string path, string guid, short maxSize);

  /// <summary>удаляет корневой каталог</summary>
  /// <param name="fileStorageGuid">guid шкафа</param>
  /// <param name="storageName">имя шкафа</param>
  void DeleteRootFolder(string fileStorageGuid, string storageName);

  /// <summary>перемещение корневого хранилища</summary>
  /// <param name="sourceRootDirectory">путь корневого хранилища</param>
  /// <param name="rootDestPath">новый путь для корневого хранилища</param>
  ICopierRootDirectory ReplaceRootDirectory(RootDirectory sourceRootDirectory, string rootDestPath);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="source"></param>
  /// <param name="path"></param>
  /// <param name="guid"></param>
  /// <param name="maxSize"></param>
  void CompleteReplaceDirectory(string source, string path, string guid, short maxSize);

  /// <summary>
  /// изменить максимальный допустимый размер корневого хранилища
  /// указывается в прцентах от свободного места на диске
  /// </summary>
  /// <param name="storageGuid">guid шкафа</param>
  /// <param name="storageName">имя шкафа</param>
  /// <param name="percent"></param>
  void ChangeRootDirectorySize(string storageGuid, string storageName, short percent);

  string EventLogFileName { get; }

  List<RootDirectory> RootDirectoriesList { get; }

  /// <summary>получить информацию о текущих подключениях</summary>
  /// <returns></returns>
  DataTable CurrentConnections { get; }

  /// <summary>
  /// найти для шкафа с указанным guid папки корневых хранилищ
  /// </summary>
  /// <param name="guid"></param>
  /// <returns></returns>
  List<string> GetNamesForStorage(string guid);
}
