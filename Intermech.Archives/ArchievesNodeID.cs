// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchievesNodeID
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces;
using Intermech.Navigator.VirtualNodes;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>Идентификатор корневого узла "Архивы документов"</summary>
public class ArchievesNodeID : HiveNodeID
{
  /// <summary>Права доступа к списку документов</summary>
  private AccessRights _accessRights;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="accessRights">Права доступа к списку документов</param>
  public ArchievesNodeID(int categoryID, int typeID, AccessRights accessRights)
    : base(categoryID, typeID)
  {
    this._accessRights = accessRights;
    if (accessRights != AccessRights.NotDefined)
      return;
    accessRights = this.GetAccessRights();
  }

  /// <summary>Права доступа к списку документов</summary>
  public AccessRights AccessRights
  {
    [DebuggerStepThrough] get => this._accessRights;
    set => this._accessRights = value;
  }

  /// <summary>
  /// Метод проверяет права доступа "Просмотр" для всех архивов
  /// </summary>
  /// <returns>Права доступа "Просмотр" для всех архивов</returns>
  protected AccessRights GetAccessRights()
  {
    if (this._accessRights != AccessRights.NotDefined)
      return this._accessRights;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._accessRights = !(sessionKeeper.Session.GetObjectType(MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")) is IDBSecurity objectType) || !objectType.CheckAccess(ActionType.View, true, false) ? AccessRights.Disabled : AccessRights.Enabled;
    return this._accessRights;
  }
}
