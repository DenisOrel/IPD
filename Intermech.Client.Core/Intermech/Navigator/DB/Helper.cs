
// Type: Intermech.Navigator.DB.Helper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DB;

public sealed class Helper
{
  private static ICategoryInheritance typeInheritance;

  public static ICategoryInheritance TypeInheritance
  {
    get
    {
      if (Helper.typeInheritance == null)
        Helper.typeInheritance = (ICategoryInheritance) new ObjectTypesInheritance();
      return Helper.typeInheritance;
    }
  }

  /// <summary>
  /// Возвращает идентификатор атрибута по его глобальному идентификатору.
  /// Если указанный атрибут не удалось найти, то результатом будет
  /// (see cref="Intermech.Navigator.DB.Consts.UndefinedAttributeID"/).
  /// </summary>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута.</param>
  /// <returns>Идентификатор атрибута.</returns>
  public static int GetAttributeID(Guid attributeGuid)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (attributeTypeId != -10000)
      return attributeTypeId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeGuid, false);
      return attributeType != null ? attributeType.AttributeID : -10000;
    }
  }

  /// <summary>
  /// Возвращает идентификатор типа объекта по его глобальному идентификатору.
  /// Если указанный тип объекта не удалось найти, то результатом будет
  /// (see cref="Intermech.Navigator.DB.Consts.UndefinedObjectTypeID"/).
  /// </summary>
  /// <param name="objectTypeGuid">Глобальный идентификатор типа объекта.</param>
  /// <returns>Идентификатор типа объекта.</returns>
  public static int GetObjectTypeID(Guid objectTypeGuid)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(objectTypeGuid);
    if (objectTypeId != -1)
      return objectTypeId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objectTypeGuid, false);
      return objectType != null ? objectType.ObjectType : -1;
    }
  }
}
