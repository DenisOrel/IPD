// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectTypedIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

public class DBObjectTypedIDCollection : 
  TypedIDCollection,
  IDBObjectTypedIDCollection,
  ITypedIDCollection,
  IEnumerator
{
  public DBObjectTypedIDCollection(ArrayList idList)
    : base(idList)
  {
    for (int index = 0; index < idList.Count; ++index)
    {
      if (!(idList[index] is IDBTypedObjectID))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_63"));
    }
  }

  public IDBTypedObjectID GetTypedObjectID(int index)
  {
    return this.idCoollection[index] as IDBTypedObjectID;
  }

  public IDBTypedObjectID[] GetTypedObjects()
  {
    return (IDBTypedObjectID[]) this.idCoollection.ToArray(typeof (IDBTypedObjectID));
  }

  public IDBRelationID GetRelationID(int index) => this.idCoollection[index] as IDBRelationID;

  public IDBRelationID[] GetRelations()
  {
    return (IDBRelationID[]) this.idCoollection.ToArray(typeof (IDBRelationID));
  }
}
