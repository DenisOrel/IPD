
// Type: Intermech.Files.WorkAreaPublishItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class WorkAreaPublishItem
{
  private readonly DBObjectState dbObject;
  private readonly DBObjectState publishedObject;
  private readonly List<FileDifferencePair> filePairs;

  public WorkAreaPublishItem(
    DBObjectState dbObject,
    DBObjectState publishedObject,
    List<FileDifferencePair> filePairs)
  {
    if (dbObject == null)
      throw new ArgumentNullException();
    if (filePairs == null)
      throw new ArgumentNullException();
    this.dbObject = dbObject;
    this.publishedObject = publishedObject;
    this.filePairs = filePairs;
  }

  public DBObjectState DBObject => this.dbObject;

  /// <summary>
  /// Возвращает предыдущее состояние объекта в файловом индексе. Может быть null, если объект впервые
  /// публикуется в рабочей области.
  /// </summary>
  public DBObjectState PublishedObject => this.publishedObject;

  public List<FileDifferencePair> FilePairs => this.filePairs;
}
