// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.DocumsPart
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Data;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Часть элемента "Архив" из пространства навигации, отвечающая за документы, содержащиеся в указанном архиве
/// </summary>
internal class DocumsPart : ObjectsPart
{
  /// <summary>Идентификатор версии архива</summary>
  private long arcID;

  /// <summary>
  /// Конструктор, позволяющий указать идентификаторы объекта-архива,
  /// с документами которого будет работать эта часть.
  /// </summary>
  /// <param name="arcID">Идентификатор архива</param>
  /// <param name="services">Контейнер сервисов</param>
  public DocumsPart(long arcID, IServiceProvider services)
    : base(ConstsHolder.DocTypeID, DocumsPart.GetArcConditions(arcID), services)
  {
    this.arcID = arcID;
  }

  /// <summary>
  /// Конструктор, позволяющий указать идентификаторы типа и объекта-архива,
  /// с документами которого будет работать эта часть.
  /// </summary>
  /// <param name="arcID">Идентификатор архива</param>
  /// <param name="conditionsProvider">Провайдер условий выбора документов</param>
  /// <param name="services">Контейнер сервисов</param>
  public DocumsPart(long arcID, IConditionsProvider conditionsProvider, IServiceProvider services)
    : base(ConstsHolder.DocTypeID, new ConditionStructure[1]
    {
      DocumsPart.GetArcConditions(arcID)
    }, conditionsProvider, services)
  {
    this.arcID = arcID;
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// со списком документов из архива. Используется только в том случае, если
  /// для данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns() => base.GetDefaultColumns();

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    StructureColumns.AddStructureColumns(columns, this.arcID);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns, true, true);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(columns);
    Intermech.Navigator.DBObjects.Helper.AddObjectTypeColumns(columns, this.objTypeID);
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(columns);
    return columns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="column"></param>
  /// <returns></returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == ArchivesStructureScheme.ArchivesStructureSchemeGuid ? (object) new NodeColumnID(column.ID, AttributeSourceTypes.Object) : base.MapColumnToField(column);
  }

  /// <summary>
  /// Формирует и возвращает условия запроса, позволяющее получить документы из
  /// указанного архива, а при необходимости - и из всех подархивов.
  /// </summary>
  /// <returns>Массив условий запроса к базе данных</returns>
  private static ConditionStructure GetArcConditions(long arcID)
  {
    long[] arcIds = DocumsPart.GetArcIDs(arcID);
    return arcIds.Length != 1 ? new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.In, (object) arcIds, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.ID) : new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.Equal, (object) arcIds[0], (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.ID);
  }

  /// <summary>
  /// Возвращает массив идентификаторов архивов, из которых будут читаться
  /// документы.
  /// </summary>
  /// <returns>Массив идентификаторов архивов</returns>
  private static long[] GetArcIDs(long arcID)
  {
    ArrayList arcIDs = new ArrayList();
    arcIDs.Add((object) arcID);
    if (Consts.ShowInternalDocums)
      DocumsPart.CollectSubarchives(arcID, arcIDs);
    return (long[]) arcIDs.ToArray(typeof (long));
  }

  /// <summary>
  /// Рекурсивно собирает и помещает в список идентификаторы подархивов,
  /// входящих в указанный родительский архив.
  /// </summary>
  /// <param name="parentArcID">Идентификатор родительского архива</param>
  /// <param name="arcIDs">Список идентификаторов архивов</param>
  private static void CollectSubarchives(long parentArcID, ArrayList arcIDs)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) parentArcID, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(ConstsHolder.ArcTypeID, dbRecordSetParams);
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      long[] c = new long[dataTable.Rows.Count];
      for (int index = 0; index < c.Length; ++index)
        c[index] = Convert.ToInt64(dataTable.Rows[index][0]);
      arcIDs.AddRange((ICollection) c);
      for (int index = 0; index < c.Length; ++index)
        DocumsPart.CollectSubarchives(c[index], arcIDs);
    }
  }
}
