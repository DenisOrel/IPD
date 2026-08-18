
// Type: Intermech.Files.IWorkAreaIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal interface IWorkAreaIndex
{
  void Append(DBObjectState objectState);

  void Remove(DBObjectState objectState);

  void Update(DBObjectState objectState);

  void BatchAppend(ICollection<DBObjectState> list);

  void BatchRemove(ICollection<DBObjectState> list);

  void BatchUpdate(ICollection<DBObjectState> updateList, ICollection<DBObjectState> appendList);

  bool Contains(long objectId);

  DBObjectState Find(long id);

  DBObjectState FindByVersionId(long objectId);

  DateTime? GetPublishTime(long objectId);

  List<DBObjectState> Query();

  List<DBObjectState> QueryNotUsed(DateTime noUseSinceDate);

  void Flush();
}
