
// Type: Intermech.Navigator.DBObjects.DesktopObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел, реализующий элемент навигации для объектов типа "Рабочий стол"
/// В качестве папок возвращает объекты, входящие в состав обрабатываемого
/// элементом объекта связью по умолчанию. Не-папок у этого элемента нет.
/// </summary>
public sealed class DesktopObjectNode : ObjectNode
{
  /// <summary>Коллекция дочерних элементов узла "Недавние объекты"</summary>
  private static DescriptorCollection _recentObjectsPart = (DescriptorCollection) null;
  /// <summary>
  /// Колонки для поиска рабочего стола текущего пользователя
  /// </summary>
  private static readonly object[] _desktopsColumns = new object[2]
  {
    (object) ObligatoryObjectAttributes.F_OBJECT_ID,
    (object) ObligatoryObjectAttributes.F_GUID
  };
  /// <summary>Параметры сортировки колонок</summary>
  private static readonly SortOrders[] _desktopSortOrders = new SortOrders[2]
  {
    SortOrders.ASC,
    SortOrders.ASC
  };
  private static Guid _desktopObjectVersionGuid = Guid.Empty;
  private static long _desktopObjectVersionID = 0;

  /// <summary>
  /// Получить идентификатор версии объекта "Рабочий стол" для текущего пользователя
  /// </summary>
  public static void GetDesktopID()
  {
    if (!ObjectHelper.IsUnknownObjectVersionID(DesktopObjectNode._desktopObjectVersionID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) sessionKeeper.Session.IdentHelper.WorkspaceTypeID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-8, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.NONE, 0, false)
      }, DesktopObjectNode._desktopsColumns, DesktopObjectNode._desktopsColumns, DesktopObjectNode._desktopSortOrders);
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(sessionKeeper.Session.IdentHelper.WorkspaceTypeID, dbRecordSetParams);
      if (dataTable.Rows.Count == 0)
        return;
      DesktopObjectNode._desktopObjectVersionID = Convert.ToInt64(dataTable.Rows[0][0]);
      DesktopObjectNode._desktopObjectVersionGuid = new Guid(dataTable.Rows[0][1].ToString());
      dataTable.Dispose();
    }
  }

  /// <summary>
  /// Идентификатор версии объекта рабочего стола текущего пользователя
  /// </summary>
  public static long DesktopObjectID
  {
    get
    {
      DesktopObjectNode.GetDesktopID();
      return DesktopObjectNode._desktopObjectVersionID;
    }
  }

  /// <summary>
  /// Guid версии объекта рабочего стола текущего пользователя
  /// </summary>
  public static Guid DesktopObjectGuid
  {
    get
    {
      DesktopObjectNode.GetDesktopID();
      return DesktopObjectNode._desktopObjectVersionGuid;
    }
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID"></param>
  public DesktopObjectNode(int objTypeID, long objID)
    : base(objTypeID, objID)
  {
    this.options = NodeOptions.CanContainsComposition;
  }

  /// <summary>Создать слоты у узла</summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = base.CreateFolderSlots();
    if (this._objID != DesktopObjectNode.DesktopObjectID || folderSlots != null)
      return folderSlots;
    folderSlots = new List<PartSlot>();
    return folderSlots;
  }
}
