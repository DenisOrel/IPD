// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.FileInformation
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// структру для передачи данных при
/// изменении файла
/// </summary>
[Serializable]
public class FileInformation
{
  /// <summary>
  /// Идентификатор записи о файле (BLOB-поля).
  /// Однозначно идентифицирует файл.
  /// При взятии на изменение файл получет новый id
  /// Но по сути это уже другой файл
  /// </summary>
  public long BlobID;
  /// <summary>Идентификатор объекта</summary>
  public long ID;
  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID;
  /// <summary>
  /// Имя файла. Может и не быть его.
  /// Не может однозначно идентифицировать файл
  /// </summary>
  public string Name;
  /// <summary>
  /// Дата изменения файла
  /// (или дата создания?)
  /// </summary>
  public DateTime FileDate;
  /// <summary>
  /// имя пользователя в системе ips,
  /// который совершил изменения с файлом
  /// </summary>
  public string UserName;
  /// <summary>
  /// имя компьютера,
  /// с которого соверашали изменения
  /// </summary>
  public string MachineName;
  /// <summary>метод упаковки файла</summary>
  public ArcMethods ArcMethod;
  /// <summary>размер файла</summary>
  public long RealSize;
  /// <summary>Упакованный размер файла</summary>
  public long PacketFileSize;
  /// <summary>Некие комментарии</summary>
  public string Note;
  /// <summary>сам  файл</summary>
  public bool IsStreamEmty;
  /// <summary>имя тома в которо хранится файл</summary>
  public string Volume;
  /// <summary>
  /// пакпка в которой хранится файл
  /// (рабочая, истории, удалённых файлов)
  /// </summary>
  public string Folder;
  /// <summary>
  /// id истории, кот. уникально идентифицирует файл
  /// в пределах объека (не версии)
  /// </summary>
  public int HistoryID;
}
