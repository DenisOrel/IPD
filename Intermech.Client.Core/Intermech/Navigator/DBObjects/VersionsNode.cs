
// Type: Intermech.Navigator.DBObjects.VersionsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

internal sealed class VersionsNode : ObjectNode, IContextAware, INodeNotifications
{
  private readonly VersionsPart _part;

  public VersionsNode(
    long objectID,
    long id,
    int objectType,
    VersionsWindowVisualModes mode,
    DateTime onDate)
    : base(objectType, objectID)
  {
    this._services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
    this.options = NodeOptions.CanContainsComposition;
    this._part = new VersionsPart(objectID, id, mode, onDate, this.Services);
  }

  protected override List<PartSlot> CreateNonFolderSlots() => this.CreateFolderSlots();

  /// <summary>
  /// Создает и возвращает часть, которая отвечает за дочерние элементы-папки.
  /// </summary>
  /// <returns>Ссылка на интерфейс части</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    IViewState service = this.Services != null ? this.Services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    if (service == null)
      return (List<PartSlot>) null;
    if ((service.ViewState & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree || this._objID == -1L)
      return this.SlotsFromSinglePart((INodePart) this._part);
    List<Guid> visibleRelationsGuids = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).Rule.GetObjectTypeVisibleRelationsGuids(this._objTypeID, true);
    if (visibleRelationsGuids == null || visibleRelationsGuids.Count == 0)
      return (List<PartSlot>) null;
    List<PartSlot> folderSlots = new List<PartSlot>();
    for (int index = 0; index < visibleRelationsGuids.Count; ++index)
    {
      INodePart folderPart = this.CreateFolderPart(MetaDataHelper.GetRelationTypeID(visibleRelationsGuids[index]));
      if (folderPart != null)
        folderSlots.Add(new PartSlot(visibleRelationsGuids[index], folderPart));
    }
    return folderSlots;
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return this._part.GetDefaultColumns();
  }

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return this._part.GetSupportedColumns(ColumnSetName);
  }

  /// <summary>
  /// Вернуть коллекцию колонок для дерева окна отображения версий
  /// </summary>
  /// <param name="objectTypes"></param>
  /// <param name="mode"></param>
  /// <returns></returns>
  public static NodeColumnCollection VersionsTreeSupportedColumns(
    List<int> objectTypes,
    VersionsWindowVisualModes mode)
  {
    NodeColumnCollection columns = Utils.VersionColumns(NodeColumnSortOrder.None, mode == VersionsWindowVisualModes.LIST);
    for (int index = 0; index < objectTypes.Count; ++index)
      Helper.AddObjectTypeColumns(columns, objectTypes[index]);
    Helper.AddObligatoryColumns(columns, false, true);
    Helper.AddObligatoryColumnsAdv(columns);
    Helper.AddAllColumns(columns);
    return columns;
  }
}
