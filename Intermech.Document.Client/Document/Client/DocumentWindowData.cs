// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentWindowData
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using System;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Данные окна документа сохранённые в PersistString, для восстановления содержимого и состояния окна</summary>
public class DocumentWindowData
{
  public Guid DocumentObjectGuid;
  public long DocumentObjectID;
  public long DocumentObjectType;
  public bool ReadOnly;

  public bool IsEmpty => Consts.IsUndefinedObjectId(this.DocumentObjectID);

  public DocumentWindowData(
    Guid documentObjectGuid,
    long documentObjectID,
    int documentObjectType,
    bool readOnly)
  {
    this.DocumentObjectGuid = documentObjectGuid;
    this.DocumentObjectID = documentObjectID;
    this.DocumentObjectType = (long) documentObjectType;
    this.ReadOnly = readOnly;
  }
}
