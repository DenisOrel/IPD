
// Type: Intermech.Navigator.Selections.PasteCommand.PasteHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.PasteCommand;

/// <summary>
/// Статические методы для вставки объектов в выборки/классификаторы
/// </summary>
internal static class PasteHelper
{
  /// <summary>Произвести вставку объектов в классификатор/выборку</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="targetObject">Классификатор или выборка</param>
  /// <param name="pasteObjects">Список объектов для вставки</param>
  /// <param name="isCut">Признак того, что производится операция Переместить</param>
  public static void Paste(
    IUserSession session,
    IDBObject targetObject,
    List<IDBTypedObjectID> pasteObjects,
    bool isCut)
  {
  }

  /// <summary>
  /// Определяет список идентификаторов типов объектов, которые можно вставлять в targetObject
  /// </summary>
  public static List<int> EnableTypes4Paste(IDBObject targetObject)
  {
    List<int> intList = new List<int>();
    IDBAttribute attributeByGuid = targetObject.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.ValuesCount == 0)
      return (List<int>) null;
    foreach (object obj in attributeByGuid.Values)
    {
      string str = Convert.ToString(obj);
      if (GuidHelper.IsGuid(str))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(str);
        if (objectTypeId != -1 && !intList.Contains(objectTypeId))
        {
          intList.Add(objectTypeId);
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
          for (int index = 0; index < childrenIdRecursive.Count; ++index)
          {
            if (!intList.Contains(childrenIdRecursive[index]))
              intList.Add(childrenIdRecursive[index]);
          }
        }
      }
    }
    if (intList.Count == 0)
      intList = (List<int>) null;
    return intList;
  }
}
