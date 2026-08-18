
// Type: Intermech.Navigator.Snapshots.SnapshotConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>
/// 
/// </summary>
public static class SnapshotConsts
{
  /// <summary>Название набора колонок - "Атрибуты снимка"</summary>
  public static string columnsSnapshot = LocalizationHolder.rm.GetString(nameof (columnsSnapshot));
  /// <summary>guid  для схемы колонок</summary>
  public static Guid SNAPSHOT_SCHEME_GUID = new Guid("{726FF395-F196-4280-E538-781E4BEDBFFB}");
  /// <summary>коллекция колонок, поддерживаемая гридом</summary>
  private static NodeColumnCollection columns = new NodeColumnCollection();
  /// <summary>коллекция колонок, поддерживаемая деревом</summary>
  private static NodeColumnCollection treeColumns = new NodeColumnCollection();
  /// <summary>
  /// здесь нужен id атрибута меньше NavigatorUndefinedAttributeID
  /// иначе атрибута не будет в группе.
  /// а вообще - это даже не атрибут
  /// </summary>
  public static int SNAPSHOT_ID = -10078;
  /// <summary>
  /// здесь нужен id атрибута меньше NavigatorUndefinedAttributeID
  /// иначе атрибута не будет в группе.
  /// а вообще - это даже не атрибут
  /// </summary>
  public static int SNAPSHOT_DATE = -10079;
  /// <summary>Наименование итерации</summary>
  public static int F_NAME = -10080;
  /// <summary>Результат сравнения (в случае сравнения сохранённого в итерации состава с актуальным)</summary>
  public static int F_COMPARE_RESULT = -10081;

  /// <summary>Возможные колонки при отображении списка итераций объекта в гриде
  /// Возвращает колонки для итерации + колонки для объектов</summary>
  /// <returns>Список возможных колонок</returns>
  public static NodeColumnCollection SnapshotGridColumns()
  {
    if (SnapshotConsts.columns.Count == 0)
    {
      IColumnSchemes service = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
      Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
      SnapshotConsts.columns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.SNAPSHOT_ID));
      SnapshotConsts.columns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.F_NAME));
      SnapshotConsts.columns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.SNAPSHOT_DATE));
      SnapshotConsts.columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
      SnapshotConsts.columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_ID));
      SnapshotConsts.columns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_USER_ID));
    }
    return SnapshotConsts.columns;
  }

  /// <summary>Возможные колонки при отображении структуры итерации в виде дерева вместе с содержимым
  /// Возвращает колонки для итерации + колонки для объектов</summary>
  /// <returns>Список возможных колонок</returns>
  public static NodeColumnCollection SnapshotTreeColumns(object sender = null)
  {
    if (SnapshotConsts.treeColumns.Count == 0)
    {
      IColumnSchemes service = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
      Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
      SnapshotConsts.treeColumns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.SNAPSHOT_ID));
      SnapshotConsts.treeColumns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.F_NAME));
      SnapshotConsts.treeColumns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.SNAPSHOT_DATE));
      SnapshotConsts.treeColumns.Add(service.CreateColumn(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (object) SnapshotConsts.F_COMPARE_RESULT));
      Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(SnapshotConsts.treeColumns, true, true);
      Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(SnapshotConsts.treeColumns);
      Intermech.Navigator.DBObjects.Helper.AddAllColumns(SnapshotConsts.treeColumns);
      Intermech.Navigator.DBObjects.Helper.AddAllColumnsRelation(SnapshotConsts.treeColumns);
    }
    return SnapshotConsts.treeColumns;
  }
}
