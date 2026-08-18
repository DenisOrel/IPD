
// Type: Intermech.Navigator.Queries.RecordMapping
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Extensions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Содержит схему соответствия виртуальных колонок и полей данных. Позволяет
/// получить список полей данных, значения которых необходимо запросить у
/// источника данных.
/// </summary>
public sealed class RecordMapping
{
  private List<RecordMappingItem> items;
  private List<object> specialFields;
  private bool modified;
  private object[] fields;
  private object[] sortFields;
  private NodeColumnSortOrder[] sortOrders;
  /// <summary>Внешняя ф-ия полечения списка полей
  /// требуется для переопределения порядка следования полей в таблице - результате запроса
  /// Некоторые API ф-ии возвращают их в фиксированном порядке, например IDBObjectSnapshot.ConsistFromSnapshotObjects</summary>
  public Func<object[]> GetFields;

  /// <summary>Создает пустую схему.</summary>
  public RecordMapping()
  {
    this.items = new List<RecordMappingItem>();
    this.specialFields = new List<object>();
    this.modified = true;
    this.fields = (object[]) null;
    this.sortFields = (object[]) null;
    this.sortOrders = (NodeColumnSortOrder[]) null;
  }

  /// <summary>
  /// Добавляет в схему новую виртуальную колонку навигатора.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <param name="field">Соответствующее ей поле данных или null, если соответствия нет</param>
  /// <param name="transform">Преобразование значений поля данных или null, если преобразование не требуется</param>
  public void RegisterColumn(NodeColumn column, object field, INodeColumnTransform transform)
  {
    if (column == null)
      throw new ArgumentNullException(sc_4310.ssp_imclient_4311(), LocalizationHolder.rm.GetString("Client.Core_636"));
    this.items.Add(new RecordMappingItem(column, field, transform));
    this.modified = true;
  }

  /// <summary>Возвращает описатель оторбажения виртуальной колонки.</summary>
  /// <param name="index">Индекс колонки в схеме</param>
  /// <returns>Описатеть отображения колони</returns>
  public RecordMappingItem this[int index] => this.items[index];

  /// <summary>Возвращает количество виртуальных колонок в схеме.</summary>
  public int Count => this.items.Count;

  /// <summary>
  /// Добавляет в схему идентификатор поля данных, значения которого
  /// обязательно должны быть получены в результате выполнения запроса.
  /// </summary>
  /// <param name="field">Идентификатор поля данныъ</param>
  public void RegisterSpecialField(object field)
  {
    if (field == null)
      throw new ArgumentNullException(sc_4310.ssp_imclient_4312(), LocalizationHolder.rm.GetString("Client.Core_637"));
    if (this.specialFields.Contains(field))
      return;
    this.specialFields.Add(field);
    this.modified = true;
  }

  /// <summary>
  /// Возвращает массив идентификаторов полей данных, значения которых
  /// должны быть получены в результате выполнения запроса.
  /// </summary>
  public object[] Fields
  {
    get
    {
      this.ApplyModifications();
      return this.GetFields != null ? this.GetFields() : this.fields;
    }
  }

  /// <summary>
  /// Возвращает массив идентификаторов полей данных, по которым
  /// должны быть отсортированы результаты выполнения запроса.
  /// </summary>
  public object[] SortFields
  {
    get
    {
      this.ApplyModifications();
      return this.sortFields;
    }
    set => this.sortFields = value;
  }

  /// <summary>Возвращает массив направлений сортировки.</summary>
  public NodeColumnSortOrder[] SortOrders
  {
    get
    {
      this.ApplyModifications();
      return this.sortOrders;
    }
    set => this.sortOrders = value;
  }

  /// <summary>Получение списка спец. полей</summary>
  /// <returns></returns>
  public object[] SpecialFields => this.specialFields.ToArray();

  private void ApplyModifications()
  {
    if (!this.modified)
      return;
    this.CollectFieldInfo();
    this.CollectSortInfo();
    this.modified = false;
  }

  private void CollectFieldInfo()
  {
    if (this.GetFields != null)
      return;
    this.fields = this.UniqueFieldsEnumeratorInternal().ToArray<object>();
    if (this.fields.Length != 0)
      return;
    this.fields = (object[]) null;
  }

  /// <summary>Метод-енумератор, необходимый для работы метода CollectFieldInfo()</summary>
  private IEnumerable<object> UniqueFieldsEnumeratorInternal()
  {
    List<object> virtualFields = new List<object>();
    foreach (object obj in this.items.Select<RecordMappingItem, object>((Func<RecordMappingItem, object>) (item => item.Field)).Concat<object>((IEnumerable<object>) this.specialFields).Where<object>((Func<object, bool>) (field => field != null)).Distinct<object>())
    {
      if (obj is NodeColumnID)
        yield return obj;
      else
        virtualFields.Add(obj);
    }
    foreach (object obj in virtualFields)
      yield return obj;
  }

  /// <summary>Собрать информацию о сортируемых колонках</summary>
  private void CollectSortInfo()
  {
    IEnumerable<RecordMappingItem> source = (IEnumerable<RecordMappingItem>) this.items.Where<RecordMappingItem>((Func<RecordMappingItem, bool>) (item => item.Field != null && item.Column.SortOrder != NodeColumnSortOrder.None && item.Column.SortIndex >= 0)).Distinct<RecordMappingItem, object>((Func<RecordMappingItem, object>) (item => item.Field)).OrderBy<RecordMappingItem, int>((Func<RecordMappingItem, int>) (item => item.Column.SortIndex));
    RecordMappingItem recordMappingItem = source.FirstOrDefault<RecordMappingItem>((Func<RecordMappingItem, bool>) (item => item.Column.SortIndex < 0));
    if (recordMappingItem != null)
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_638"), (object) recordMappingItem.Column.Caption));
    this.sortFields = source.Select<RecordMappingItem, object>((Func<RecordMappingItem, object>) (item => item.Field)).ToArray<object>(this.items.Count);
    if (this.sortFields.Length == 0)
      this.sortFields = (object[]) null;
    this.sortOrders = source.Select<RecordMappingItem, NodeColumnSortOrder>((Func<RecordMappingItem, NodeColumnSortOrder>) (item => item.Column.SortOrder)).ToArray<NodeColumnSortOrder>(this.items.Count);
    if (this.sortOrders.Length != 0)
      return;
    this.sortOrders = (NodeColumnSortOrder[]) null;
  }
}
