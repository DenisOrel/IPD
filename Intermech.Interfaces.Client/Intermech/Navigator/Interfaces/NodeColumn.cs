// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeColumn
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Search;
using Intermech.Search.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Данный класс представляет собой колонку «Навигатора».
/// Основное свойство – идентификатор атрибута, получаемого из источника данных,
/// например, атрибута объекта или связи.
/// ВНИМАНИЕ! КЛАСС СЕРИЛИЗУЕМЫЙ, ПОЛЯ НЕ ПЕРЕИМЕНОВЫВАТЬ
/// </summary>
[Serializable]
public class NodeColumn : 
  IAssignable,
  ICloneable,
  IComparable,
  IComparable<NodeColumn>,
  IColumnAttributeInfo
{
  /// <summary>
  /// Глобальный идентификатор схемы, к которой относится колонка
  /// </summary>
  private Guid _schemeGuid;
  /// <summary>
  /// Виртуальный идентификатор колонки внутри схемы. При выполнении
  /// запросов к источникам данных этот идентификатор отображается в
  /// идентификатор реального поля, присутствующего в источнике данных.
  /// </summary>
  private object _id;
  /// <summary>Тип данных, содержащихся в колонке.</summary>
  private Type _dataType;
  /// <summary>Тип данных атрибута, связанного с колонкой</summary>
  private FieldTypes _attrType;
  /// <summary>Направление сортировки данных, содержащихся в колонке</summary>
  protected NodeColumnSortOrder _sortOrder;
  /// <summary>Номер колонки в списке отсортированных колонок</summary>
  protected int _sortIndex;
  /// <summary>
  /// Название колонки для вывода в элементах визуального интерфейса.
  /// </summary>
  private string _caption;
  /// <summary>
  /// Краткое название колонки для вывода в элементах визуального интерфейса.
  /// </summary>
  private string _shortCaption;
  /// <summary>
  /// Хинт, который будет отображаться дя колонки, если включен режим отображения краткого названия колонки.
  /// </summary>
  private string _hint;
  /// <summary>
  /// Ширина колонки в пикселах в элементах визульного интерфейса.
  /// </summary>
  private int _width;
  /// <summary>Порядковый номер в коллекции сгруппированных колонок</summary>
  private int _groupIndex;
  /// <summary>Ключ колонки</summary>
  private string _key = string.Empty;
  /// <summary>Является ли атрибут системным</summary>
  private bool _systemAttr;
  /// <summary>Содержимое ячеек в колонке</summary>
  private ColumnContents _contents;
  /// <summary>Режим преобразования значений ячеек колонки</summary>
  private CellTransformationMode _transformationMode = CellTransformationMode.ConvertToString;
  /// <summary>Запрет сортировки по данной колонке</summary>
  protected bool _disableSorting;
  /// <summary>Запрет группировки по данной колонке</summary>
  private bool _disableGrouping;
  /// <summary>Приоритет колонки в коллекции колонок</summary>
  private SchemeColumnPriority _priority;
  /// <summary>Тег</summary>
  [NonSerialized]
  private object _tag;
  private object id;
  [NonSerialized]
  private IMSAttributeType _attribute;

  public static bool Equals(NodeColumn firstNodeColumn, NodeColumn secondNodeColumn)
  {
    if (firstNodeColumn == null)
      throw new ArgumentNullException(nameof (firstNodeColumn));
    if (secondNodeColumn == null)
      throw new ArgumentNullException(nameof (secondNodeColumn));
    return NodeColumn.EqualsWithNoWidth(firstNodeColumn, secondNodeColumn) && firstNodeColumn.Width == secondNodeColumn.Width;
  }

  public static bool EqualsWithNoWidth(NodeColumn firstNodeColumn, NodeColumn secondNodeColumn)
  {
    if (firstNodeColumn == null)
      throw new ArgumentNullException(nameof (firstNodeColumn));
    if (secondNodeColumn == null)
      throw new ArgumentNullException(nameof (secondNodeColumn));
    if (firstNodeColumn == secondNodeColumn)
      return true;
    return firstNodeColumn.Caption == secondNodeColumn.Caption && firstNodeColumn.Contents == secondNodeColumn.Contents && firstNodeColumn.DataType == secondNodeColumn.DataType && firstNodeColumn.DisableGrouping == secondNodeColumn.DisableGrouping && firstNodeColumn.DisableSorting == secondNodeColumn.DisableSorting && firstNodeColumn.GroupIndex == secondNodeColumn.GroupIndex && firstNodeColumn.ID == secondNodeColumn.ID && firstNodeColumn.Priority == secondNodeColumn.Priority && firstNodeColumn.SchemeGuid == secondNodeColumn.SchemeGuid && firstNodeColumn.SortIndex == secondNodeColumn.SortIndex && firstNodeColumn.SortOrder == secondNodeColumn.SortOrder && firstNodeColumn.Tag == secondNodeColumn.Tag;
  }

  /// <summary>
  /// Создать колонку, заполнить её информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public NodeColumn(object source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    this.Assign(source);
  }

  /// <summary>
  /// Конструктор, позволяющий создать колонку без сортировки.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption)
    : this(schemeGuid, id, dataType, attrType, caption, ColumnContents.Text)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать колонку без сортировки.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="contents">Содержимое ячеек колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    ColumnContents contents)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    this._schemeGuid = schemeGuid;
    this._id = id;
    this._dataType = dataType;
    this._attrType = this.AttrType;
    this._caption = caption;
    this._sortOrder = NodeColumnSortOrder.None;
    this._sortIndex = -1;
    this._groupIndex = -1;
    this._width = 150;
    this._shortCaption = this._caption;
    this._hint = this._caption;
    this._systemAttr = false;
    this._contents = contents;
  }

  /// <summary>
  /// Конструктор, позволяющий создать колонку без сортировки.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="hint">Подсказка</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    string shortCaption,
    string hint)
    : this(schemeGuid, id, dataType, attrType, caption, shortCaption, hint, ColumnContents.Text)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать колонку без сортировки.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="hint">Подсказка</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="contents">Содержимое ячеек колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    string shortCaption,
    string hint,
    ColumnContents contents)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    this._schemeGuid = schemeGuid;
    this._id = id;
    this._dataType = dataType;
    this._attrType = this.AttrType;
    this._caption = caption;
    this._sortOrder = NodeColumnSortOrder.None;
    this._sortIndex = -1;
    this._groupIndex = -1;
    this._width = 150;
    this._shortCaption = shortCaption != string.Empty ? shortCaption : caption;
    this._hint = hint;
    this._systemAttr = false;
    this._contents = contents;
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
    : this(schemeGuid, id, dataType, attrType, caption, sortOrder, sortIndex, ColumnContents.Text)
  {
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  /// <param name="contents">Содержимое ячеек колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex,
    ColumnContents contents)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    this._schemeGuid = schemeGuid;
    this._id = id;
    this._dataType = dataType;
    this._attrType = attrType;
    this._caption = caption;
    this._sortOrder = sortOrder;
    this._groupIndex = -1;
    this._sortIndex = sortIndex;
    this._width = 150;
    this._shortCaption = this._caption;
    this._hint = this._caption;
    this._systemAttr = false;
    this._contents = contents;
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="hint">Подсказка</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex,
    string shortCaption,
    string hint)
    : this(schemeGuid, id, dataType, attrType, caption, sortOrder, sortIndex, shortCaption, hint, ColumnContents.Text)
  {
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="hint">Подсказка</param>
  /// <param name="contents">Содержимое ячеек колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex,
    string shortCaption,
    string hint,
    ColumnContents contents)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    this._schemeGuid = schemeGuid;
    this._id = id;
    this._dataType = dataType;
    this._attrType = attrType;
    this._caption = caption;
    this._sortOrder = sortOrder;
    this._groupIndex = -1;
    this._sortIndex = sortIndex;
    this._width = 150;
    this._shortCaption = shortCaption != string.Empty ? shortCaption : caption;
    this._hint = hint;
    this._systemAttr = false;
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="hint">Подсказка</param>
  /// <param name="systemAttr">Является ли атрибут колонки системным</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex,
    string shortCaption,
    string hint,
    bool systemAttr)
    : this(schemeGuid, id, dataType, attrType, caption, sortOrder, sortIndex, shortCaption, hint, systemAttr, ColumnContents.Text)
  {
  }

  /// <summary>
  /// Конструктор колонки, позволяющий задать все ее свойства.
  /// </summary>
  /// <param name="schemeGuid">Идентификатор схемы колонки</param>
  /// <param name="id">Идентификатор колонки</param>
  /// <param name="dataType">Тип данных, содержащихся в колонке</param>
  /// <param name="attrType">Тип данных атрибута, связанного с колонкой</param>
  /// <param name="caption">Заголовок колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Номер колонки в списке отсортированных колонок</param>
  /// <param name="shortCaption">Краткое наименование колонки</param>
  /// <param name="hint">Подсказка</param>
  /// <param name="systemAttr">Является ли атрибут колонки системным</param>
  /// <param name="contents">Содержимое ячеек колонки</param>
  public NodeColumn(
    Guid schemeGuid,
    object id,
    Type dataType,
    FieldTypes attrType,
    string caption,
    NodeColumnSortOrder sortOrder,
    int sortIndex,
    string shortCaption,
    string hint,
    bool systemAttr,
    ColumnContents contents)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    this._schemeGuid = schemeGuid;
    this._id = id;
    this._dataType = dataType;
    this._attrType = attrType;
    this._caption = caption;
    this._sortOrder = sortOrder;
    this._groupIndex = -1;
    this._sortIndex = sortIndex;
    this._width = 150;
    this._shortCaption = shortCaption != string.Empty ? shortCaption : caption;
    this._hint = hint;
    this._systemAttr = systemAttr;
    this._contents = contents;
  }

  public bool IsValid => this.ID != null;

  /// <summary>
  /// Уникальный строковой ключ колонки для её быстрого поиска в коллекции колонок.
  /// Ключ состоит из идентификатора схемы, которой принадлежит колонка, и идентификатора самой колонки.
  /// </summary>
  public string Key
  {
    get
    {
      if (this._key != string.Empty)
        return this._key;
      this._key = $"{(object) this.SchemeGuid},{ServiceLocator.Get<IColumnSchemes>().ColumnIDToPersistName(this.SchemeGuid, this.ID)}";
      return this._key;
    }
  }

  /// <summary>Идентификатор схемы, которой принадлежит колонка.</summary>
  public Guid SchemeGuid
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._schemeGuid;
  }

  /// <summary>
  /// Идентификатор колонки. Например, может быть равен идентификатору или названию атрибута источника данных.
  /// </summary>
  public object ID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._id;
  }

  /// <summary>Тип данных, содержащихся в ячейках колонки.</summary>
  public Type DataType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dataType;
  }

  /// <summary>Тип данных атрибута, связанного с колонкой.</summary>
  public FieldTypes AttrType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._attrType;
  }

  /// <summary>Возвращает заголовок колонки</summary>
  public string Caption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Возвращает краткий заголовок колонки</summary>
  public string ShortCaption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._shortCaption;
    }
  }

  /// <summary>Возвращает подсказку для заголовка колонки</summary>
  public string Hint
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._hint;
  }

  /// <summary>
  /// Возвращает или устанавливает направление сортировки данных в колонке
  /// </summary>
  public virtual NodeColumnSortOrder SortOrder
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._sortOrder;
    set => this._sortOrder = value;
  }

  /// <summary>
  /// Порядковый номер в группе колонок или -1, если колонка не участвует в группировке колонок.
  /// </summary>
  public int GroupIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._groupIndex;
    set => this._groupIndex = value;
  }

  /// <summary>
  /// Порядковый номер колонки в списке сортируемых колонок или -1, если колонка не участвует в сортировке.
  /// </summary>
  public virtual int SortIndex
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._sortIndex;
    set => this._sortIndex = value;
  }

  /// <summary>
  /// Возвращает или устанавливает ширину колонки в пикселах
  /// </summary>
  public int Width
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._width;
    set => this._width = value;
  }

  /// <summary>
  /// Если true, то атрибут, который описан в колонке, является системным.
  /// </summary>
  public bool SystemAttr
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._systemAttr;
    set => this._systemAttr = value;
  }

  /// <summary>
  /// Содержимое ячеек колонки, которое будет запрашиваться у источника данных.
  /// </summary>
  public ColumnContents Contents
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._contents;
    set => this._contents = value;
  }

  /// <summary>
  /// Данное свойство определяет, каким образом значения ячеек данной колонки должны обрабатываться
  /// преобразователем (INodeColumnTransform), назначенным данной колонке.
  /// </summary>
  public CellTransformationMode TransformationMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._transformationMode;
    }
    set => this._transformationMode = value;
  }

  /// <summary>Дополнительные данные для колонки</summary>
  public object Tag
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Запрет сортировки по данной колонке</summary>
  public virtual bool DisableSorting
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._disableSorting;
    }
    set => this._disableSorting = value;
  }

  /// <summary>Запрет группировки по данной колонке</summary>
  public bool DisableGrouping
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._disableGrouping;
    }
    set => this._disableGrouping = value;
  }

  /// <summary>Приоритет колонки в коллекции колонок</summary>
  public SchemeColumnPriority Priority
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._priority;
    set => this._priority = value;
  }

  public INodeColumnSource Source { get; set; }

  /// <summary>
  /// Сравнить текущий экземпляр колонки атрибута с указанным объектом
  /// </summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если текущий экземпляр колонки атрибута равен указанному объекту</returns>
  public override bool Equals(object obj)
  {
    return obj is NodeColumn nodeColumn ? this.Key.Equals(nodeColumn.Key) : base.Equals(obj);
  }

  /// <summary>
  /// Сравнить текущий экземпляр колонки атрибута с указанным объектом
  /// </summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если текущий экземпляр колонки атрибута равен указанному объекту</returns>
  public virtual bool FullEquals(object obj)
  {
    if (!(obj is NodeColumn nodeColumn))
      return this.Equals(obj);
    return this._schemeGuid.Equals(nodeColumn._schemeGuid) && this._id.Equals(nodeColumn._id) && this._sortIndex == nodeColumn._sortIndex && this._sortOrder == nodeColumn._sortOrder && this._width == nodeColumn._width && this._groupIndex == nodeColumn._groupIndex;
  }

  /// <summary>
  /// Рассчитать 32-битный хэш-код для текущего экземпляра класса
  /// </summary>
  /// <returns>32-битный хэш-код для текущего экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this._schemeGuid.GetHashCode() << 16 /*0x10*/ ^ this._id.GetHashCode();
  }

  /// <summary>Вернуть строковое представление колонки</summary>
  /// <returns>Строковое представление колонки</returns>
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("\"");
    stringBuilder.Append(this.Caption);
    stringBuilder.Append("\" ");
    stringBuilder.Append(" (");
    if (this.ID != null)
      stringBuilder.Append($"{this.ID.ToString()}, ");
    stringBuilder.Append((object) this.AttrType);
    stringBuilder.Append(", ");
    stringBuilder.Append((object) this.SortOrder);
    stringBuilder.Append(", ");
    stringBuilder.Append(this.Width);
    stringBuilder.Append(", ");
    stringBuilder.Append((object) this.SchemeGuid);
    stringBuilder.Append(")");
    return stringBuilder.ToString();
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._schemeGuid = Guid.Empty;
    this._id = (object) null;
    this._dataType = (Type) null;
    this._attrType = FieldTypes.ftUnknown;
    this._sortOrder = NodeColumnSortOrder.None;
    this._sortIndex = -1;
    this._caption = string.Empty;
    this._shortCaption = string.Empty;
    this._hint = string.Empty;
    this._width = 0;
    this._groupIndex = -1;
    this._key = string.Empty;
    this._systemAttr = false;
    this._contents = ColumnContents.Text;
    this._transformationMode = CellTransformationMode.ConvertToString;
    this._tag = (object) null;
    this._disableSorting = false;
    this._disableGrouping = false;
    this._priority = SchemeColumnPriority.Standard;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    if (!(source is NodeColumn nodeColumn))
    {
      this.Clear();
    }
    else
    {
      this._schemeGuid = nodeColumn.SchemeGuid;
      this._id = nodeColumn.ID;
      this._dataType = nodeColumn.DataType;
      this._attrType = nodeColumn.AttrType;
      this._sortOrder = nodeColumn.SortOrder;
      this._sortIndex = nodeColumn.SortIndex;
      this._caption = nodeColumn.Caption;
      this._shortCaption = nodeColumn.ShortCaption;
      this._hint = nodeColumn.Hint;
      this._width = nodeColumn.Width;
      this._groupIndex = nodeColumn.GroupIndex;
      this._key = nodeColumn.Key;
      this._systemAttr = nodeColumn.SystemAttr;
      this._contents = nodeColumn.Contents;
      this._transformationMode = nodeColumn.TransformationMode;
      this._tag = nodeColumn.Tag;
      this._disableSorting = nodeColumn.DisableSorting;
      this._disableGrouping = nodeColumn.DisableGrouping;
      this._priority = nodeColumn.Priority;
    }
  }

  /// <summary>Создать копию объекта, идентичную натуральному</summary>
  /// <returns>Копия объекта, идентичная натуральному</returns>
  public virtual object Clone() => (object) new NodeColumn((object) this);

  /// <summary>Сравнить колонку атрибута с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as NodeColumn);

  /// <summary>Сравнить колонку атрибута с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(NodeColumn other)
  {
    return other == null ? 1 : this.Caption.CompareTo(other.Caption);
  }

  /// <summary>
  /// По некоему объектному идентификатору попробовать получить идентификатор типа атрибута
  /// </summary>
  /// <param name="id">Объектный идентификатор типа атрибута</param>
  /// <returns>Идентификатор типа атрибута или Intermech.Consts.UnknownAttributeId</returns>
  public static int GetAttributeID(object id)
  {
    switch (id)
    {
      case null:
        throw new ArgumentNullException(nameof (id));
      case int attributeId:
        return attributeId;
      case ObligatoryObjectAttributes _:
        return (int) id;
      case Guid attributeTypeGuid:
        return ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeGuidToAttributeTypeID(attributeTypeGuid);
      case string _:
        int attributeTypeId = ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeNameToAttributeTypeID((string) id);
        if (!AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeId))
          return attributeTypeId;
        if (GuidHelper.IsGuid((string) id))
          return ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeGuidToAttributeTypeID(Guid.Parse((string) id));
        if ((string) id == "F_STATUSES")
          return -77;
        if ((string) id == "CAPTION" || (string) id == "F_CAPTION")
          return -50;
        Enum enumValue = EnumTypeHelper.GetEnumValue(typeof (ObligatoryObjectAttributes), (string) id);
        return enumValue != null ? (int) enumValue : 0;
      case PortalAttributeType _:
        PortalAttributeType portalAttributeType = (PortalAttributeType) id;
        return string.IsNullOrEmpty(portalAttributeType.GUID) ? 0 : ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeGuidToAttributeTypeID(new Guid(portalAttributeType.GUID));
      default:
        try
        {
          return Convert.ToInt32(id);
        }
        catch
        {
          return 0;
        }
    }
  }

  /// <summary>Информация о типе атрибута</summary>
  public IMSAttributeType Attribute
  {
    get
    {
      if (this._attribute == null)
        this._attribute = this.GetAttributeType();
      return this._attribute;
    }
  }

  protected virtual IMSAttributeType GetAttributeType()
  {
    return MetaDataHelper.GetAttributeType(NodeColumn.GetAttributeID(this._id));
  }

  /// <summary>
  /// Информация об источнике атрибута. Допустимы только значения Object и Relation.
  /// </summary>
  public AttributeSourceTypes AttrSource
  {
    get
    {
      return this._schemeGuid == Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid || this._schemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid || this._schemeGuid == Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object;
    }
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    if (this._id != null)
      return;
    this._id = this.id;
  }
}
