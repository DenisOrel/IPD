
// Type: Intermech.Navigator.DBObjectTypes.AllObjectTypesNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>Идентификатор корневого узла "Все типы объектов"</summary>
/// <summary>Создать экземпляр класса</summary>
/// <param name="categoryID">Категория</param>
/// <param name="typeID">Тип</param>
internal sealed class AllObjectTypesNodeID(int categoryID, int typeID) : HiveNodeID(categoryID, typeID)
{
}
