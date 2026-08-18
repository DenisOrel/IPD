// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.BeforeCommitCreationAVSDocumentEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Аргументы для события, возникающего перед коммитом создания объекта
/// </summary>
public sealed class BeforeCommitCreationAVSDocumentEventArgs
{
  /// <summary>Объект созданного документа</summary>
  public IDBObject Document { get; private set; }

  /// <summary>Идентификатор прототипа объекта</summary>
  public long DocumentPrototypeID { get; private set; }

  /// <summary>
  /// Список идентификаторов изделий (исполнений) связанных с документом. Изделия могут быть как только что созданные так и уже существующие.
  /// </summary>
  public List<long> ArticleIDs { get; private set; }

  /// <summary>
  /// Список идентификаторов объектов автоматически созданных при создании документа
  /// </summary>
  public List<long> NewObjectIDs { get; private set; }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectTypeID">Тип объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  public BeforeCommitCreationAVSDocumentEventArgs(
    IDBObject obj,
    long prototypeID,
    List<long> newArticleIDs,
    List<long> newObjectIDs)
  {
    this.Document = obj;
    this.DocumentPrototypeID = prototypeID;
    this.ArticleIDs = newArticleIDs;
    this.NewObjectIDs = newObjectIDs;
  }
}
