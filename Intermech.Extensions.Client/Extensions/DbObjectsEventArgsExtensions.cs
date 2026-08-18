// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DbObjectsEventArgsExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

public static class DbObjectsEventArgsExtensions
{
  [NotNull]
  public static IReadOnlyCollection<(long ObjectID, int ObjectTypeID)> GetObjectsInfo(
    [CanBeNull] this DBObjectsEventArgs ea,
    [NotNull] LazySession sk,
    [CanBeEmpty] int onlyObjectsWithType = -1)
  {
    List<(long, int)> valueTupleList = (List<(long, int)>) null;
    if (ea != null)
    {
      IList<int> objectTypeIds = ea.ObjectTypeIDs;
      if (objectTypeIds != null && objectTypeIds.Count > 0)
      {
        int count = objectTypeIds.Count;
        IList<long> objectIds = ea.ObjectIDs;
        if (objectIds != null)
        {
          for (int index = 0; index < count; ++index)
          {
            long objectID = objectIds[index];
            if (!Intermech.Check.ObjectIdIsEmpty(objectID))
            {
              int objectTypeId = objectTypeIds[index];
              if (!Intermech.Check.ObjectTypeIdIsEmpty(objectTypeId))
              {
                QuickObjectInfo objectInfo = sk.Session.GetObjectInfo(objectID);
                if (!objectInfo.Empty)
                  objectTypeId = objectInfo.ObjectTypeID;
                else
                  continue;
              }
              if (Intermech.Check.ObjectTypeIdIsEmpty(onlyObjectsWithType) || objectTypeId == onlyObjectsWithType || !MetaDataHelperService.Instance.IsObjectTypeChildOf(objectTypeId, onlyObjectsWithType))
              {
                if (valueTupleList == null)
                  valueTupleList = new List<(long, int)>(objectTypeIds.Count);
                valueTupleList.Add((objectID, objectTypeId));
              }
            }
          }
        }
      }
    }
    return (IReadOnlyCollection<(long, int)>) valueTupleList ?? (IReadOnlyCollection<(long, int)>) Array.Empty<(long, int)>();
  }
}
