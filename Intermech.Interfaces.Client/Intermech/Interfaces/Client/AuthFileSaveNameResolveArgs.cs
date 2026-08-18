// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AuthFileSaveNameResolveArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
public class AuthFileSaveNameResolveArgs : EventArgs
{
  private string folderName;
  private string fileName;
  private long objectId;
  private string objectCaption;
  private long blobId;
  private bool autoAssign;

  /// <summary>Папка сохранения</summary>
  public string FolderName => this.folderName;

  /// <summary>Конфликтующее имя файла, требует изменения</summary>
  public string FileName
  {
    get => this.fileName;
    set => this.fileName = value;
  }

  /// <summary>идентификатор версии объекта</summary>
  public long ObjectId => this.objectId;

  /// <summary>наименование объекта</summary>
  public string ObjectCaption => this.objectCaption;

  /// <summary>идентификатор блоба в атрибуте Файл</summary>
  public long BlobId => this.blobId;

  /// <summary>
  /// При изменении в true - отказ от обработки конфликта, переименованием занимается вызывающая служба - самостоятельно
  /// </summary>
  public bool AutoAssign
  {
    get => this.autoAssign;
    set => this.autoAssign = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fileNameToResolve">конфликтное имя файла</param>
  /// <param name="folderName">папка сохранения</param>
  /// <param name="objectId">идентификатор версии объекта</param>
  /// <param name="objectCaption">наименование объекта</param>
  /// <param name="blobId">идентификатор блоба в атрибуте Файл</param>
  public AuthFileSaveNameResolveArgs(
    string fileNameToResolve,
    string folderName,
    long objectId,
    string objectCaption,
    long blobId)
  {
    this.fileName = fileNameToResolve;
    this.folderName = folderName;
    this.objectId = objectId;
    this.objectCaption = objectCaption;
    this.blobId = blobId;
  }
}
