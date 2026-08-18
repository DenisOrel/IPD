
// Type: Intermech.Navigator.ObjectTypesInheritanceCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Кэш информации о наследовании типов объектов базы данных.
/// Кэш автоматически очищает себя по окончании заданного интервала
/// времени.
/// </summary>
[Obsolete("Рекомендуется использовать статический класс Intermech.Interfaces.MetaDataHelper")]
public class ObjectTypesInheritanceCache : ICache, IObjectTypesInheritanceCache
{
  public ObjectTypesInheritanceCache(TimeSpan threshold)
  {
  }

  public void Reset()
  {
  }

  public int GetParentType(int objType) => MetaDataHelper.GetObjectTypeParentID(objType);

  public int[] GetChildrenTypes(int objType)
  {
    return MetaDataHelper.GetObjectTypeChildrenID(objType).ToArray();
  }

  public int[] GetObjectTypes(int objTypeID)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeID);
    if (childrenIdRecursive == null)
      return new int[0];
    childrenIdRecursive.Remove(objTypeID);
    return childrenIdRecursive.ToArray();
  }
}
