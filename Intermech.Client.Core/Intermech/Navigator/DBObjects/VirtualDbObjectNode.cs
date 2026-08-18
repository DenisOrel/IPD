
// Type: Intermech.Navigator.DBObjects.VirtualDbObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Нода объекта, отсутствовавшего на данный момент в БД
/// (напр. ранее существовавший, который уже удалён из БД, однако где-то сохранённый и теперь его надо отобразить в дереве навигатора),
/// 
/// запись о котором однако сохранилась в итерации
/// Может быть создана либо в результате показа состава объекта, сохранённого в итерации,
/// либо при сравнении состава актуального объекта с составом, сохранённом в итерации</summary>
public class VirtualDbObjectNode : 
  CompositeNode,
  INode,
  INodeItems,
  IContextAware,
  INodeNotifications
{
  /// <summary>Тип объекта</summary>
  public readonly int ObjTypeID;
  /// <summary>ID версии объекта</summary>
  public readonly long ObjVersionID;
  /// <summary>Заголовок</summary>
  [NotNull]
  public readonly string Caption;
  /// <summary>Текущий пользователь и роль.</summary>
  [CanBeNull]
  private static ICurrentUserAndRole _userRole;
  /// <summary>Контейнер сервисов</summary>
  [CanBeNull]
  protected IServiceProvider _Services;

  /// <summary>Текущий пользователь и роль.</summary>
  [NotNull]
  protected static ICurrentUserAndRole UserRole
  {
    get
    {
      return VirtualDbObjectNode._userRole ?? (VirtualDbObjectNode._userRole = ApplicationServices.Container.GetService<ICurrentUserAndRole>());
    }
  }

  public VirtualDbObjectNode(int objTypeID, long objVersionID, [NotNull] string caption)
  {
    this.ObjTypeID = objTypeID;
    this.ObjVersionID = objVersionID;
    this.Caption = caption;
  }

  [CanBeNull]
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._Services;
    set => this._Services = value;
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public virtual ProcessResult Process([NotNull] NotificationEventArgs e, [CanBeNull] object additionalInfo)
  {
    if (e.EventName == "ObjectTypeAndRelationFiltrationChanged" || e.EventName == "SortedRelationsChanged" && MetaDataHelper.HasObjectTypeSortingRelTypes(this.ObjTypeID) && ((DBRelationsEventArgs) e).ProjIDs.Contains(this.ObjVersionID) || e.EventName == "ObjectTypesChanged" && e is DBObjectTypesEventArgs objectTypesEventArgs && objectTypesEventArgs.ObjectTypeIDs.Contains(this.ObjTypeID))
      return ProcessResult.RefreshNode;
    if ((e.EventName == "AttributeChanged" || e.EventName == "AttributeRemoved") && additionalInfo is NodeColumnCollection columnCollection1 && e is DBAttributesEventArgs attributesEventArgs && columnCollection1.ColumnIDsExists(attributesEventArgs.AttributeIDs))
      return ProcessResult.RefreshNodeAndColumns;
    if (e.EventName == "Attribute4RelTypeEvent" || e.EventName == "Attribute4ObjTypeEvent")
    {
      DBAttributes4TypeEventArgs attributes4TypeEventArgs = e as DBAttributes4TypeEventArgs;
      NodeColumnCollection columnCollection2 = additionalInfo as NodeColumnCollection;
      List<int> visibleRelations = VirtualDbObjectNode.UserRole.Rule.GetObjectTypeVisibleRelations(this.ObjTypeID, true);
      if (attributes4TypeEventArgs != null && columnCollection2 != null && columnCollection2.Count > 0 && (e.EventName == "Attribute4RelTypeEvent" && visibleRelations.Count > 0 && visibleRelations.Contains(attributes4TypeEventArgs.CategoryID) || e.EventName == "Attribute4ObjTypeEvent" && this.ObjTypeID == attributes4TypeEventArgs.CategoryID) && (columnCollection2.ColumnIDsExists(attributes4TypeEventArgs.ChangedIDs) || columnCollection2.ColumnIDsExists(attributes4TypeEventArgs.RemovedIDs)))
        return ProcessResult.RefreshNodeAndColumns;
    }
    return ProcessResult.None;
  }
}
