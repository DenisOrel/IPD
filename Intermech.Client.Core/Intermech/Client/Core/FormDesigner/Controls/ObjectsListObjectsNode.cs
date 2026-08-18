
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsListObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Узел для объектов.</summary>
public class ObjectsListObjectsNode : CompositeNode, IContextAware
{
  /// <summary>Контейнер сервисов</summary>
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  /// <summary>Формирование слотов-непапок.</summary>
  /// <returns>Коллекция слотов-непапок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    long num = 0;
    int objTypeID = -1;
    long objectID = 0;
    if (this._services != null && this._services.GetService(typeof (ObjectsListService)) is ObjectsListService service1)
    {
      num = service1.SelectionID;
      objTypeID = service1.ObjectsTypeID;
      objectID = service1.ObjectID;
    }
    ConditionStructure[] conditions = (ConditionStructure[]) null;
    if (num != 0L && ServicesManager.ServiceContainer.GetService(typeof (ISelectionsService)) is ISelectionsService service2)
    {
      if (!ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(num).Empty)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          conditions = service2.GetConditionStructures((object) sessionKeeper.Session, num, objectID);
      }
      else
        ExceptionHelper.ExceptionService.ShowException(new Exception(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ObjectListControls_NotFoundSelection"), (object) num)));
    }
    List<PartSlot> nonFolderSlots = new List<PartSlot>(1);
    ObjectsPart part = new ObjectsPart(objTypeID, conditions, this.Services);
    nonFolderSlots.Add(new PartSlot(ObjectsListConsts.ObjectsNodeGuid, (INodePart) part));
    return nonFolderSlots;
  }

  /// <summary>
  /// Вернуть список колонок по умолчанию для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <returns>Список по умолчанию для корневого узла</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    int objTypeID = -1;
    if (this._services != null && this._services.GetService(typeof (ObjectsListService)) is ObjectsListService service)
    {
      if (service.Columns != null)
        return service.Columns;
      objTypeID = service.ObjectsTypeID;
    }
    return (objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(objTypeID, AccessRights.Enabled)).GetDefaultColumns(ContentType.NonFolders);
  }

  /// <summary>
  /// Вернуть список поддерживаемых колонок для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <param name="ColumnSetName">Имя набора колонок</param>
  /// <returns>Список поддерживаемых колонок для корневого узла</returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    int objTypeID = -1;
    if (this._services != null && this._services.GetService(typeof (ObjectsListService)) is ObjectsListService service)
      objTypeID = service.ObjectsTypeID;
    return (objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(objTypeID, AccessRights.Enabled)).GetSupportedColumns(ContentType.NonFolders, string.Empty);
  }

  /// <summary>
  /// Обнуление данных, если необходимо обновить информацию в контроле.
  /// </summary>
  public override void Refresh()
  {
    this.folderSlots = (List<PartSlot>) null;
    this.nonFolderSlots = (List<PartSlot>) null;
  }
}
