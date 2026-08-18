// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileOrigin
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Содержит сведения о происхождении файла в рабочей области файлового хранилища.
/// </summary>
public sealed class FileOrigin
{
  private readonly string fileName;
  private readonly FileOriginType originType;
  private readonly long id;
  private readonly DBObjectState workObject;

  /// <summary>Создает объект.</summary>
  /// <param name="fileName">Путь и имя файла, путь может быть как абсолютным, так и относительным</param>
  /// <param name="originType">Код происхождения файла</param>
  /// <param name="id">Идентификатор объекта IPS или Consts.NoObject в случае нового файла</param>
  /// <param name="workObject">Описание опубликованной в рабочей области версии объекта или null</param>
  public FileOrigin(string fileName, FileOriginType originType, long id, DBObjectState workObject)
  {
    this.fileName = fileName;
    this.originType = originType;
    this.id = id;
    this.workObject = workObject;
  }

  /// <summary>
  /// Возвращает путь и имя файла. Путь может быть как абсолютным, так и относительным.
  /// </summary>
  public string FileName => this.fileName;

  /// <summary>Возвращает код происхождения файла.</summary>
  public FileOriginType OriginType => this.originType;

  /// <summary>
  /// Возвращает идентификатор объекта IPS или Consts.NoObject в случае нового файла.
  /// </summary>
  public long Id => this.id;

  /// <summary>
  /// Возвращает описание опубликованной в рабочей области версии объекта или null.
  /// </summary>
  public DBObjectState WorkObject => this.workObject;
}
