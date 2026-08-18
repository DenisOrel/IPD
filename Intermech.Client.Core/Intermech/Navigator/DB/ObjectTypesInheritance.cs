
// Type: Intermech.Navigator.DB.ObjectTypesInheritance
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.CacheServices;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DB;

public class ObjectTypesInheritance : ICategoryInheritance
{
  /// <summary>
  /// Кэш информации о наследовании типов объектов базы данных
  /// </summary>
  private static readonly IObjectTypeHierarchy service = (IObjectTypeHierarchy) ((ICacheServices) ServicesManager.GetService(typeof (ICacheServices))).GetService("ObjectTypeHierarchy");

  public int[] GetParentTypes(int typeID) => ObjectTypesInheritance.service.GetParentTypes(typeID);
}
