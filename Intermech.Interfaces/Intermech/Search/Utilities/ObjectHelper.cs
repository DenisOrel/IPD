
// Type: Intermech.Search.Utilities.ObjectHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Utilities
{
    /// <summary>Хелпер объектов</summary>
    public static class ObjectHelper
    {
      /// <summary>Проверить идентификатор объекта</summary>
      /// <param name="id">Идентификатор объетка</param>
      /// <returns></returns>
      public static bool IsUnknownObjectID(long id) => Consts.IsUndefinedObjectId(id);

      /// <summary>Проверить идентификатор версии объекта</summary>
      /// <param name="versionID">Идентификатор версии объекта</param>
      /// <returns></returns>
      public static bool IsUnknownObjectVersionID(long versionID)
      {
        return Consts.IsUndefinedObjectId(versionID);
      }

      public static bool IsAnyUnknownObjectVersionID(IEnumerable<long> objectVersionIds)
      {
        if (objectVersionIds == null)
          throw new ArgumentNullException(nameof (objectVersionIds));
        return objectVersionIds.Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() > 0;
      }

      public static bool IsUnknownObjectModificationID(long objectModificationID)
      {
        return ObjectHelper.IsUnknownObjectVersionID(objectModificationID);
      }

      public static long ConvertBooleanToBaseVersionSing(bool value) => !value ? 0L : 1L;

      public static bool CheckObjectForModification(long objectVersionID, params int[] attributeTypeIds)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        if (attributeTypeIds != null && ((IEnumerable<int>) attributeTypeIds).Where<int>((Func<int, bool>) (o => AttributeTypeHelper.IsUnknownAttributeTypeID(o))).Count<int>() != 0)
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID, false);
          if (dbObject == null)
            return false;
          ObjectModifyModes objectModifyMode = dbObject.ObjectModifyMode;
          if (objectModifyMode == ObjectModifyModes.CantModify)
            return false;
          if (objectModifyMode == ObjectModifyModes.Checkout && (ObjectHelper.IsUnknownObjectVersionID(dbObject.CheckoutBy) || dbObject.CheckoutBy == sessionKeeper.Session.UserID) || objectModifyMode != ObjectModifyModes.Checkout || attributeTypeIds == null)
            return true;
          IAttributeTypeForObjectRepository objectRepository = ServiceLocator.Get<IAttributeTypeForObjectRepository>();
          foreach (int attributeTypeId in attributeTypeIds)
          {
            int attributeTypeID = attributeTypeId;
            IMSAttribute4ObjectType attribute4ObjectType = objectRepository.Find(dbObject.ObjectType).FirstOrDefault<IMSAttribute4ObjectType>((Func<IMSAttribute4ObjectType, bool>) (o => o.AttributeID == attributeTypeID));
            if (attribute4ObjectType == null || !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
              return false;
          }
        }
        return true;
      }

      public static bool IsObjectCheckedOut(long objectVersionID)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        return objectVersionID < 0L;
      }
    }
}
