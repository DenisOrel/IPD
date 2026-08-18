// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.CommonVariables
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using Intermech.Vault.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Vault.Interfaces;

public static class CommonVariables
{
  public static readonly int BLOCK_SIZE = 131072 /*0x020000*/;
  /// <summary>имя папки дял удалённых файлов</summary>
  public static readonly string DELETED_FOLDER_NAME = "Deleted files";
  /// <summary>имя папки для файлов истории</summary>
  public static readonly string HISTORY_FOLDER_NAME = "Files history";
  /// <summary>имя папки для рабочих копий файлов</summary>
  public static readonly string WORKING_FOLDER_NAME = "Working files";
  /// <summary>имя папки для временных файлов</summary>
  public static readonly string TEMP_FOLDER_NAME = "Temp files";
  /// <summary>имя папки тома</summary>
  public static readonly string VOLUME_NAME = "volume_";
  /// <summary>ключ для зашифрованного пароля</summary>
  public static readonly string PASSWORD = "password";
  /// <summary>путь к папке с логами в документах</summary>
  public static readonly string LOG_PATH = "Intermech\\IPS\\DVS";
  /// <summary>файл логирования</summary>
  public static readonly string LOG_FILE_NAME = "IntermechVaultLog.log";
  /// <summary>папка для файлов логирования</summary>
  public static readonly string DEV_LOG_FOLDER_NAME = "DeveloperLog";
  /// <summary>файл настроек</summary>
  public static readonly string XML_FILE_NAME = "Settings.xml";
  /// <summary>имя файла бд для шкафа</summary>
  public static readonly TimeSpan WAIT_SPAN = new TimeSpan(24, 0, 0);
  public static readonly string SERVICE_NAME = "IPS.DVS";
  public static string ConfigFileName;
  public static string EventLogPath;
  public static string XmlFilePath;
  /// <summary>
  /// время жизни файлов истории
  /// пока будет в днях
  /// </summary>
  public static uint HistoryLife;
  /// <summary>
  /// время жизни удалённых файлов
  /// пока будет в днях
  /// </summary>
  public static uint DeletedLife;
  /// <summary>
  /// флаг: логировать все события или только события подключения
  /// </summary>
  public static bool FullLogging = false;
  /// <summary>максимальный размер тома</summary>
  public static long MaxVolumeSize;
  public static string Password;
  /// <summary>
  /// Режим синхронизации SQLite (synchronous=off/normal/full)
  /// true - synchronous=off
  /// false - synchronous=default (none)
  /// </summary>
  public static bool SyncModeOff = false;
  private static List<RootDirectory> rootDirectoriesList = new List<RootDirectory>();

  public static List<RootDirectory> RootDirectoriesList => CommonVariables.rootDirectoriesList;

  public static void AddRootDirectory(RootDirectory root)
  {
    lock (CommonVariables.rootDirectoriesList)
      CommonVariables.RootDirectoriesList.Add(root);
  }

  public static void RemoveRootDirectory(RootDirectory root)
  {
    lock (CommonVariables.rootDirectoriesList)
      CommonVariables.RootDirectoriesList.Remove(root);
  }

  /// <summary>
  /// получить файловое хранилище.
  /// 21.03.2011
  /// возникла проблема при копирование баз данных.
  /// файловый шкаф должен быть уникальным по комбинации
  /// имя  - гуид.
  /// тогда пользователи смогут в копии бд изменить имя шкафа,
  /// не изменяя гуида - и получить копию шкафа
  /// </summary>
  /// <param name="storageName">имя шкафа (не путь к файловому хранилищу, а именно имя файлового шкафа)</param>
  /// <param name="storageGuid">guid шкафа</param>
  /// <returns></returns>
  public static RootDirectory GetRootDirectory(string storageName, string storageGuid)
  {
    lock (CommonVariables.rootDirectoriesList)
    {
      foreach (RootDirectory rootDirectories in CommonVariables.RootDirectoriesList)
      {
        string fileName = Path.GetFileName(rootDirectories.StorageName);
        string guid = rootDirectories.Guid;
        if (fileName == storageName && guid == storageGuid || fileName == storageName && guid == string.Empty)
          return rootDirectories;
      }
    }
    throw new VaultException(string.Format(LocalizationHolder.rm.GetString("Vault.Interfaces_43"), (object) storageName, (object) storageGuid));
  }
}
