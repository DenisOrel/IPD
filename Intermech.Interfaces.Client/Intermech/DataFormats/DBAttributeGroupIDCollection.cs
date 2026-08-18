// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBAttributeGroupIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

public class DBAttributeGroupIDCollection : 
  TypedIDCollection,
  IDBAttributeGroupIDCollection,
  ITypedIDCollection,
  IEnumerator
{
  public DBAttributeGroupIDCollection(ArrayList idList)
    : base(idList)
  {
    for (int index = 0; index < idList.Count; ++index)
    {
      if (!(idList[index] is IDBAttributeGroupID))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_61a"));
    }
  }

  public IDBAttributeGroupID GetAttributeGroupID(int index)
  {
    return this.idCoollection[index] as IDBAttributeGroupID;
  }

  public IDBAttributeGroupID[] GetAttributeGroups()
  {
    return (IDBAttributeGroupID[]) this.idCoollection.ToArray(typeof (IDBAttributeGroupID));
  }
}
