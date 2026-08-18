
// Type: Intermech.Navigator.IObjectTypesInheritanceCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator;

/// <summary>
/// Интерфейс для извлечения информации о наследовании типов объектов
/// базы данных из кэша.
/// </summary>
[Obsolete("Рекомендуется использовать статический класс Intermech.Interfaces.MetaDataHelper")]
public interface IObjectTypesInheritanceCache
{
  int GetParentType(int objType);

  int[] GetChildrenTypes(int objType);

  int[] GetObjectTypes(int objTypeID);
}
