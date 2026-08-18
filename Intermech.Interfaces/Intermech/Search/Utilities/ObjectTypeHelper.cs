
// Type: Intermech.Search.Utilities.ObjectTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Utilities
{
    public static class ObjectTypeHelper
    {
      public static bool IsUnknownObjectTypeID(int objectTypeID)
      {
        return objectTypeID == -1 || objectTypeID == -1;
      }

      public static bool IsAnyUnknownObjectTypeID(IEnumerable<int> objectTypeIds)
      {
        return objectTypeIds != null ? objectTypeIds.Any<int>((Func<int, bool>) (o => ObjectTypeHelper.IsUnknownObjectTypeID(o))) : throw new ArgumentNullException(nameof (objectTypeIds));
      }

      public static bool IsVersionedObjectTypeID(int objectTypeID)
      {
        IMSObjectType imsObjectType = objectTypeID != -1 ? MetaDataHelper.GetObjectType(objectTypeID) : throw new ArgumentException();
        return imsObjectType != null && imsObjectType.VersionsMode == ObjectVersionModes.MultiVersion;
      }

      public static bool IsAllVersionedObjectTypeIds(IEnumerable<int> objectTypeIds)
      {
        if (objectTypeIds == null)
          throw new ArgumentNullException(nameof (objectTypeIds));
        if (ObjectTypeHelper.IsAnyUnknownObjectTypeID(objectTypeIds))
          throw new ArgumentException();
        return objectTypeIds.All<int>((Func<int, bool>) (o =>
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(o);
          return objectType != null && objectType.VersionsMode == ObjectVersionModes.MultiVersion;
        }));
      }

      public static bool IsAbstract(int objectTypeID)
      {
        IMSObjectType imsObjectType = !ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID) ? MetaDataHelper.GetObjectType(objectTypeID) : throw new ArgumentException();
        if (imsObjectType == null)
          throw new Exception();
        return imsObjectType.VersionsMode == ObjectVersionModes.Abstract;
      }

      public static bool IsManualOrAnyAttribute(int objectTypeID, int attributeTypeID)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objectTypeID, attributeTypeID);
        return attribute4ObjectType != null && attribute4ObjectType.Required == RequiredModes.Manual || MetaDataHelper.GetObjectType(objectTypeID).AnyAttributes;
      }

      public static int[] GetDescendantsAndSelf(int[] parentObjectTypes)
      {
        if (parentObjectTypes == null || parentObjectTypes.Length == 0 || ObjectTypeHelper.IsAnyUnknownObjectTypeID((IEnumerable<int>) parentObjectTypes))
          throw new ArgumentException();
        List<int> source = new List<int>();
        foreach (int parentTypeID in ((IEnumerable<int>) parentObjectTypes).Distinct<int>())
        {
          source.Add(parentTypeID);
          source.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID));
        }
        return source.Distinct<int>().ToArray<int>();
      }
    }
}
