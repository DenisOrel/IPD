// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IDiskFileStorage
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// логика работы со шкафами
/// корневое хранилище и шкафа понятия идентичные,
/// т.е. каждому шкафу соответсвует только одно корневое хранилище.
/// и наооборот.
/// логика создания корневых хранилищ.
/// админ создаёт корневое хранилище н-р, c:\storages\storage1
/// указывает максимально допустимый размер хранилища
/// после чего в ips создаёт шкаф с таким же именем. для шкафа указывает server=блаблабла
/// (вдруг существует несколько служб на нескольких серверах)
/// при создании шкафа, служба ищет в xml папку шкафа, смотрит не занята ли папка другими файлами,
/// ругается если там есть мусор, создаёт структуру шкафа.
/// аналогично при логине ищет в xml путь,
/// файл бд называем по guid...
/// 
/// уникальным идентификатором шкафа является составной ключ - гуид и имя шкафа.
/// связано с желаением пользователей копировать шкафы из базы в базу
/// 
/// администратор имеет возможность просматривать набор файловых шкафов-хранилищ,
/// изменять размер (в % от свободного места на диске), добавлять новые храннилища,
/// удалять старые
/// </summary>
public interface IDiskFileStorage : IDisposable
{
  /// <summary>Guid файлового хранилища</summary>
  string StorageGUID { get; }

  /// <summary>
  /// имя машины, с котрой произошло подключение (сервера ips)
  /// </summary>
  string СomputerName { get; }

  /// <summary>имя пользователя, котрой подключился к шкафу</summary>
  string UserName { get; set; }

  /// <summary>
  /// Дата и время начала последней транзакции
  /// Если через сутки не произошёл коммит транзакции, считаем,
  /// что клиент отвалился, транзакцию откатываем
  /// </summary>
  DateTime StartTransactionTime { get; set; }

  /// <summary>
  /// Уникальный идентификатор подключения сервера ips к шкафу
  /// </summary>
  int ConnectionID { get; set; }

  /// <summary>была ли начата транзакция для данного шкафа</summary>
  bool InTransaction { get; }

  short MaxPercent { get; set; }

  string StoragePath { get; }

  /// <summary>
  /// имя файлового шкафа  в ips.
  /// имя папки файлового хранилища
  /// </summary>
  string StorageName { get; set; }

  /// <summary>отключиться от хранилища</summary>
  void Logout();

  /// <summary>
  /// информация обо всех файлах хранимых в шкафу
  /// (суммарная для распределнного шкафа, т.е. суммируются все данные о всех файлах,
  /// хранящихся в этом шкафу в разных корневых хранилищах)
  /// </summary>
  /// <returns></returns>
  DataTable GetStorageInfo();

  /// <summary>
  /// удалить файл из хранилища
  /// (поместить в папку удалённых)
  /// </summary>
  /// <param name="blobID"></param>
  void DeleteFile(long blobID);

  /// <summary>
  /// удаляет текущий файловый шкаф (если в нем нет рабочих файлов).
  /// вообще, по идее надо бы стразу разлогинится,
  /// как только удалили шкаф
  /// </summary>
  void DeleteStorage();

  /// <summary>
  /// получить список с историей файла для объекта id
  /// Возвращает DataTable с идентификаторами файлов,
  /// размерами, датами модификации, именами пользователей, менявших файл.
  /// </summary>
  /// <param name="id"> id объекта </param>
  /// <returns></returns>
  DataTable GetObjectHistory(long id);

  /// <summary>
  /// получить список с историей файла для версии объекта objectID.
  /// Возвращает DataTable с идентификаторами файлов,
  /// размерами, датами модификации, именами пользователей, менявших файл.
  /// </summary>
  /// <param name="objectID"></param>
  /// <returns></returns>
  DataTable GetVersionHistory(long objectID);

  /// <summary>завершение транзакции</summary>
  void Commit();

  /// <summary>откат транзакции</summary>
  void Rollback();

  /// <summary>начало транзакции</summary>
  void StartTransaction();

  /// <summary>
  /// произвести чистку устаревших и удаленных файлов.
  /// В системе должны быть настройки сколько хранить файлы истории,
  /// сколько хранить удаленные файлы.
  /// Данная процедура удаляет все файлы, вышедшие за этот лимит.
  /// По умолчанию удалённые файлы хранить год, историю хранить без ограничений.
  /// </summary>
  void DeleteTrash();

  /// <summary>помещение в хранилище информации о файле</summary>
  /// <param name="fileInfo"></param>
  string WriteFileInfo(FileInformation fileInfo);

  /// <summary>
  /// возвращает информацию о файле по его идентификатору
  /// (информация о рабочей версии файла)
  /// </summary>
  /// <param name="blobID"></param>
  /// <returns></returns>
  FileInformation GetFileInformation(long blobID);

  FileInformation GetFileHistoryInformation(int historyID, long objectID);

  /// <summary>история изменения файла</summary>
  /// <returns></returns>
  DataTable GetHistoryForFile(long blobID, long id);

  DataTable GetHistoryForFile(string fileName, long id);

  /// <summary>
  /// Изменяет идентификатор объекта (связи),
  /// к которому принадлежит атрибут.
  /// (id истории файла при этом не изменяется)
  /// </summary>
  /// <param name="fileInfo">Информация о файле (где значение версии объекта/файла уже новые)</param>
  void ChangeObjectLinkID(FileInformation fileInfo);
}
