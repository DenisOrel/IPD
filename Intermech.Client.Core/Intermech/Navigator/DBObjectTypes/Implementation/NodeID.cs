
// Type: Intermech.Navigator.DBObjectTypes.Implementation.NodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjectTypes.Implementation;

/// <summary>
/// Реализует унифицированный идентификатор, предназначенный для обозначения
/// элементов "Тип объекта базы данных" из пространства навигации.
/// </summary>
public class NodeID : INodeID
{
  /// <summary>Права доступа к списку объектов</summary>
  private AccessRights _accessRights;

  /// <summary>
  /// Конструктор, позволяющий создать идентификатор, описывающий тип объекта.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="accessRights"></param>
  public NodeID(int objTypeID, AccessRights accessRights)
  {
    this.TypeID = objTypeID;
    this._accessRights = accessRights;
  }

  /// <summary>
  /// Метод проверяет права доступа "Просмотр" для текущего типа объектов
  /// </summary>
  /// <returns>Права доступа "Просмотр" для текущего типа объектов</returns>
  protected AccessRights GetAccessRights()
  {
    if (this._accessRights != AccessRights.NotDefined)
      return this._accessRights;
    this._accessRights = this.TypeID != -1 ? ((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(this.TypeID).CanViewObjects() ? AccessRights.Enabled : AccessRights.Disabled) : AccessRights.Disabled;
    return this._accessRights;
  }

  /// <summary>
  /// Возвращает идентификатор категории описываемого элемента
  /// </summary>
  public int CategoryID => 4;

  /// <summary>Возвращает идентификатор типа описываемого элемента</summary>
  public int TypeID { get; }

  public object Cookie { get; set; }

  /// <summary>Права доступа к списку объектов</summary>
  public AccessRights AccessRights
  {
    get
    {
      if (this._accessRights == AccessRights.NotDefined)
        this._accessRights = this.GetAccessRights();
      return this._accessRights;
    }
    set => this._accessRights = value;
  }

  public override bool Equals(object obj) => obj is NodeID nodeId && nodeId.TypeID == this.TypeID;

  public override int GetHashCode() => this.TypeID;
}
