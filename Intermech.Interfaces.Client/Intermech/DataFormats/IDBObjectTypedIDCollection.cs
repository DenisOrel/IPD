// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBObjectTypedIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Коллекция типизированных идентификаторов объектов базы данных
/// </summary>
public interface IDBObjectTypedIDCollection : ITypedIDCollection, IEnumerator
{
  IDBTypedObjectID GetTypedObjectID(int index);

  IDBTypedObjectID[] GetTypedObjects();

  IDBRelationID GetRelationID(int index);

  IDBRelationID[] GetRelations();
}
