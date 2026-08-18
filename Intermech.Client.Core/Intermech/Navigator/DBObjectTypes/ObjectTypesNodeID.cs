
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>Идентификатор корневого узла "Все объекты"</summary>
public sealed class ObjectTypesNodeID : HiveNodeID
{
  /// <summary>Создать экземпляр класса</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="accessRights">Права доступа к списку объектов</param>
  public ObjectTypesNodeID(int categoryID, int typeID, AccessRights accessRights)
    : base(categoryID, typeID)
  {
    this.AccessRights = accessRights;
    if (accessRights != AccessRights.NotDefined)
      return;
    accessRights = this.GetAccessRights();
  }

  /// <summary>Права доступа к списку объектов</summary>
  public AccessRights AccessRights { get; set; }

  /// <summary>
  /// Метод проверяет права доступа "Просмотр" для текущего типа объектов
  /// </summary>
  /// <returns>Права доступа "Просмотр" для текущего типа объектов</returns>
  protected AccessRights GetAccessRights()
  {
    if (this.AccessRights != AccessRights.NotDefined)
      return this.AccessRights;
    this.AccessRights = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(0).CanViewObjects() ? AccessRights.Enabled : AccessRights.Disabled;
    return this.AccessRights;
  }
}
