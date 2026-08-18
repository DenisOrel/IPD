
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemesQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System.Collections.Generic;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Класс для получения списка схем ЖЦ</summary>
internal sealed class LifeCycleSchemesQuery : BaseNodeQuery
{
  /// <summary>Подготовка запроса к выполнению</summary>
  private INodeQuerySupport support;
  /// <summary>Схемы ЖЦ</summary>
  protected List<IMSLifeCycleScheme> items;
  /// <summary>Список найденных схем ЖЦ</summary>
  private List<IMSLifeCycleScheme> rows = new List<IMSLifeCycleScheme>();
  /// <summary>
  /// Составное значение: атрибут F_LC_STEP : источник - объект (имитируем специальное поле "Схема ЖЦ")
  /// </summary>
  protected internal static NodeColumnID ncImsScheme = new NodeColumnID((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object);
  /// <summary>
  /// Составное значение: атрибут CAPTION : источник - объект
  /// </summary>
  protected internal static string CAPTION = "F_CAPTION";
  /// <summary>Поля для сортировки</summary>
  protected internal static readonly object[] FieldsOrder = new object[2]
  {
    (object) LifeCycleSchemesQuery.CAPTION,
    (object) LifeCycleSchemesQuery.ncImsScheme
  };

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="support">Подготовка запроса к выполнению</param>
  public LifeCycleSchemesQuery(INodeQuerySupport support)
  {
    this.support = support;
    this.items = MetaDataHelper.GetLCSchemesList();
    this.items.Sort();
  }

  /// <summary>
  /// Выполняет запрос на чтение порции дочерних элементов. Позиция для
  /// чтения определяется закладкой (bookmark). Если закладка = null,
  /// то будет прочитана первая порция, иначе будет прочитана порция с
  /// позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    if (mapping != null && mapping.SortFields != null && mapping.SortFields.Length != 0)
    {
      bool flag = false;
      NodeColumnSortOrder nodeColumnSortOrder = NodeColumnSortOrder.None;
      for (int index = 0; index < mapping.SortFields.Length; ++index)
      {
        flag = mapping.SortFields[index].Equals((object) LifeCycleSchemesQuery.CAPTION);
        if (flag)
        {
          nodeColumnSortOrder = mapping.SortOrders == null || mapping.SortOrders.Length == 0 ? NodeColumnSortOrder.Ascending : mapping.SortOrders[index];
          break;
        }
      }
      if (flag && nodeColumnSortOrder == NodeColumnSortOrder.Descending)
        this.items.Sort((IComparer<IMSLifeCycleScheme>) new LifeCycleSchemesQuery.DescSchemesComparer());
    }
    int position1 = bookmark != null ? ((PositionBookmark) bookmark).Position : 0;
    if (position1 + count > this.items.Count)
      count = this.items.Count - position1;
    if (count <= 0)
      return NodeQueryResult.Empty;
    this.rows.Clear();
    for (int index = 0; index < count; ++index)
      this.rows.Add(this.items[position1 + index]);
    int position2 = position1 + count;
    return new NodeQueryResult(position2 < this.items.Count ? (object) new PositionBookmark(position2) : (object) (PositionBookmark) null, count, this.TotalRecordCount, LifeCycleSchemesQuery.FieldsOrder);
  }

  /// <summary>
  /// Читает сведения об указанных элементах источника данных.
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    this.rows.Clear();
    for (int index1 = 0; index1 < recordIds.Length; ++index1)
    {
      int index2 = this.items.IndexOf(recordIds[index1] as IMSLifeCycleScheme);
      if (index2 >= 0)
        this.rows.Add(this.items[index2]);
    }
    return new NodeQueryResult(this.rows.Count, this.TotalRecordCount, LifeCycleSchemesQuery.FieldsOrder);
  }

  /// <summary>
  /// Возвращает запись, полученную из источника данных в результате выполнения запроса.
  /// </summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  protected override object[] GetFieldValues(int index)
  {
    return new object[2]
    {
      (object) this.rows[index].Name,
      (object) this.rows[index]
    };
  }

  /// <summary>
  /// Возвращает объект, помогающий подготовить запрос к выполнению и обработать
  /// его результаты.
  /// </summary>
  protected override INodeQuerySupport Support => this.support;

  /// <summary>Сравнение описаний схем ЖЦ по убыванию</summary>
  private class DescSchemesComparer : IComparer<IMSLifeCycleScheme>
  {
    /// <summary>Сравнить два описания схем ЖЦ по убыванию</summary>
    /// <param name="x">Первая схема</param>
    /// <param name="y">Вторая схема</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(IMSLifeCycleScheme x, IMSLifeCycleScheme y) => -x.Name.CompareTo(y.Name);
  }
}
