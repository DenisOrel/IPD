// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBAttributeIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

public class DBAttributeIDCollection : 
  TypedIDCollection,
  IDBAttributeIDCollection,
  ITypedIDCollection,
  IEnumerator
{
  public DBAttributeIDCollection(ArrayList idList)
    : base(idList)
  {
    for (int index = 0; index < idList.Count; ++index)
    {
      if (!(idList[index] is IDBAttributeID))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_61"));
    }
  }

  public IDBAttributeID GetAttributeID(int index) => this.idCoollection[index] as IDBAttributeID;

  public IDBAttributeID[] GetAttributes()
  {
    return (IDBAttributeID[]) this.idCoollection.ToArray(typeof (IDBAttributeID));
  }
}
