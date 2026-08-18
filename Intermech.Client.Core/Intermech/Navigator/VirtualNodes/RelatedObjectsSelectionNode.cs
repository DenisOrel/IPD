
// Type: Intermech.Navigator.VirtualNodes.RelatedObjectsSelectionNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>Нода содержимого объекта, с фильтрацией по переданным в конструктор условиям</summary>
public class RelatedObjectsSelectionNode : ObjectsSelectionNode, INode, INodeItems, IContextAware
{
  /// <summary>Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</summary>
  protected readonly long _ObjectVersionID;
  /// <summary>Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</summary>
  protected readonly RelatedObjectsRole _Role;
  /// <summary>Тип связи, по которой получается состав/входимость</summary>
  protected readonly int _RelationTypeID;

  /// <summary>Constructor</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="relationTypeID">Тип связи, по которой получается состав/входимость</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objTypeID">Тип объектов</param>
  /// <param name="conditionsProvider">Провайдер списка условий выбора объектов</param>
  public RelatedObjectsSelectionNode(
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int objTypeID,
    [NotNull] IConditionsProvider conditionsProvider)
    : base(objTypeID, conditionsProvider)
  {
    Intermech.Check.ArgumentObjectIdNotEmpty(objectVersionID, nameof (objectVersionID));
    Intermech.Check.ArgumentRelationIdNotEmpty((long) relationTypeID, nameof (relationTypeID));
    this._ObjectVersionID = objectVersionID;
    this._RelationTypeID = relationTypeID;
    this._Role = role;
  }

  /// <summary>Создать список слотов-не-папок</summary>
  /// <returns>Список слотов-не-папок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    RelatedObjectsPart part = new RelatedObjectsPart(this._ObjTypeID, this._ObjectVersionID, this._Role, this._RelationTypeID, this._ConditionsProvider, (IServiceProvider) this._Services);
    part.AcceptManagedEvents = false;
    return this.SlotsFromSinglePart((INodePart) part);
  }

  public new Image GetMainIcon()
  {
    return Images32x16_Cache.GetImage32x16(4, this._ObjTypeID, (object) this);
  }

  public new Image GetPrefixIcon() => (Image) null;

  public new CellWidget GetCustomCellWidget(RowWidget rowWidget, NavigatorTreeColumn column)
  {
    return (CellWidget) null;
  }
}
